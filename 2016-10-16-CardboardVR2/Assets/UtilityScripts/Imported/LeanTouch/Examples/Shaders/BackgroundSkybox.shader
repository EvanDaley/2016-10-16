Shader "Lean/BackgroundSkybox"
{
	Properties
	{
		_Color1("Color 1", Color) = (1.0,0.5,0.5,1.0)
		_Color2("Color 2", Color) = (0.5,0.5,1.0,1.0)
	}

	SubShader
	{
		Tags
		{
			"Queue"       = "Background"
			"RenderType"  = "Background"
			"PreviewType" = "Skybox"
		}

		Cull Off
		ZWrite Off
		Fog
		{
			Mode Off
		}

		Pass
		{
		CGPROGRAM
		#pragma vertex Vert
		#pragma fragment Frag
		#pragma multi_compile DUMMY PIXELSNAP_ON
		
		#include "UnityCG.cginc"
		
		float4 _Color1;
		float4 _Color2;
		
		struct a2v
		{
			float4 vertex : POSITION;
		};

		struct v2f
		{
			float4 vertex : SV_POSITION;
			float2 coord  : TEXCOORD0;
		};

		void Vert(a2v i, out v2f o)
		{
			o.vertex = mul(UNITY_MATRIX_MVP, i.vertex);
			o.coord  = o.vertex.xy / o.vertex.w * 0.5f;
		}

		void Frag(v2f i, out float4 o:COLOR0)
		{
			o.rgba = lerp(_Color1, _Color2, length(i.coord));
		}
		ENDCG
		}
	}
}

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Lean/Smooth"
{
	Properties
	{
		_Color("Color", Color) = (1.0,1.0,1.0,1.0)
		_MainColor("Main Color", Color) = (1.0,0.5,0.5,1.0)
		_RimColor("Rim Color", Color) = (0.5,1.0,0.5,1.0)
		_RimPower("Rim Power", Float) = 2.0
		_FadePower("Fade Power", Float) = 2.0
	}

	SubShader
	{
		Blend SrcAlpha OneMinusSrcAlpha

		Fog
		{
			Mode Off
		}

		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex Vert
			#pragma fragment Frag

			float3 _Color;
			float3 _MainColor;
			float3 _RimColor;
			float  _RimPower;
			float  _FadePower;

			struct a2v
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : TEXCOORD0;
				float3 facing : TEXCOORD1;
			};

			void Vert(a2v i, out v2f o)
			{
				float4 wVertex = mul(unity_ObjectToWorld, i.vertex);

				o.vertex = mul(UNITY_MATRIX_MVP, i.vertex);
				o.normal = mul((float3x3)unity_ObjectToWorld, i.normal);
				o.facing = _WorldSpaceCameraPos - wVertex.xyz;
			}

			void Frag(v2f i, out float4 o:COLOR0)
			{
				// Find dot between normal and facing directions
				i.normal = normalize(i.normal);
				i.facing = normalize(i.facing);

				float nfDot = dot(i.normal, i.facing);

				// Make the color a rim lit gradient
				float gradient = 1.0f - pow(1.0f - nfDot, _RimPower);

				o.rgb = lerp(_RimColor, _MainColor, gradient) * _Color;

				// Make the alpha rim lit
				float fade = 1.0f - pow(1.0f - nfDot, _FadePower);

				o.a = fade;
			}
			ENDCG
		}
	}
}

Shader "Unlit/cellShader"
{
	Properties
	{
		_MainTexture("Texture", 2D) = "white" {}
		_FirstColor("First Color", Color) = (1,1,1,1)
		_SecondColor("Second Color", Color) = (1,1,1,1)
		_ThirdColor("Third Color", Color) = (1,1,1,1)
		_FourthColor("Fourth Color", Color) = (1,1,1,1)
		_FirstBorder("First Border", Float) = 0.25
		_SecondBorder("Second Border", Float) = 0.50
		_ThirdBorder("Third Border", Float) = 0.75
	}

		SubShader
	{
		// Use For Lighting
		Tags { "LightMode" = "ForwardBase"}
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTexture;
			float4 _FirstColor;
			float4 _SecondColor;
			float4 _ThirdColor;
			float4 _FourthColor;
			float _FirstBorder;
			float _SecondBorder;
			float _ThirdBorder;

			v2f vert(appdata v)
			{
				v2f returnV;
				returnV.normal = v.normal;
				returnV.vertex = UnityObjectToClipPos(v.vertex);
				returnV.uv = v.uv;
				return returnV;
			}

			fixed4 frag(v2f i) : Color
			{
				float4 white = (1, 1, 1, 1);
				float4 black = (0, 0, 0, 0);




				// Get the Light Vector, normal
				float3 normal = normalize(i.normal);
				float3 lightDir = _WorldSpaceLightPos0.xyz;

				float2 uv = i.uv;

				float4 colour = tex2D(_MainTexture, uv);

				// Calculate the dot product between object normal and light direction
				float dotProduct = dot(normal, lightDir);


				// Find angle between vectors, then get cosine of that angle
				float theta = acos(dotProduct);
				float segment = cos(theta);


				//return colour;

				float3 blend;
				float4 finalBlend;

				// Split into different sections by values
				if (segment >= _ThirdBorder)
				{
					/*
					float t = step(segment, 0.9);
					return t;
					blend = lerp(white, _FirstColor, t);

					finalBlend = (blend, 1.0);
					return finalBlend;

					colour.rgb *= blend;

					return _FirstColor;
					finalBlend = (blend, 1.0);
					colour *= finalBlend;
					return colour;
					*/
					float4 offset = _FirstColor *= float4(_FirstColor.rgb, segment);
					return offset;
				}
				else if (segment < _ThirdBorder && segment > _SecondBorder)
				{
					/*
					float t = step(uv.y, 0.5);
					blend = lerp(_FirstColor, _SecondColor, t);
					finalBlend = (blend, 0.0);
					colour *= finalBlend;
					return colour;
					*/
					float4 offset = _SecondColor *= float4(_SecondColor.rgb, segment);
					return offset;
					return _SecondColor;
				}
				else if (segment <= _SecondBorder && segment > _FirstBorder)
				{
					/*
					float t = step(uv.y, 0.5);
					blend = lerp(_SecondColor, _ThirdColor, t);
					finalBlend = (blend, 0.0);
					colour *= finalBlend;
					return colour;
					*/
					float4 offset = _ThirdColor *= float4(_ThirdColor.rgb, segment);
					return offset;
					return _ThirdColor;
				}
				else
				{
					/*
					float t = step(uv.y, 0.5);
					blend = lerp(_ThirdColor, _FourthColor, t);
					finalBlend = (blend, 0.0);
					colour *= finalBlend;
					return colour;
					*/
					return _FourthColor;
				}

			}
			ENDCG
		}
	}
}

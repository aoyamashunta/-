Shader "Unlit/Manto"
{
	Properties
	{
		_Color("Color",Color) = (0,0,1,1)
	}

		SubShader
	{
		Pass
		{

			Cull off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float3 worldPosition : TEXCOORD1;
			};

			fixed4 _Color;
			fixed4 _specularColor;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPosition = mul(unity_ObjectToWorld, v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// Ž‹“_ƒxƒNƒgƒ‹
				float3 eyeDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPosition);
				float3 halfVector = normalize(eyeDir + _WorldSpaceLightPos0);
				float intensity = saturate(dot(normalize(i.normal), halfVector));

				float diffuseIntensity = saturate(dot(normalize(i.normal), _WorldSpaceLightPos0));
				_specularColor = (1, 1, 1, 1);


				// ŠÂ‹«Œõ
				fixed4 ambient = (1 - smoothstep(0.3,0.35, diffuseIntensity)) * _Color * 0.3;
				// ŠgŽU”½ŽËŒõ
				fixed4 diffuse = smoothstep(0.3,0.35, diffuseIntensity) * _Color;
				// ‹¾–Ê”½ŽËŒõ
				fixed4 specular = intensity * _specularColor;
				specular = pow(specular, 50);
				specular = smoothstep(0.8,0.85, specular);

				fixed4 ads = (ambient + diffuse);
				return ads;
			}
			ENDCG
		}
	}
}

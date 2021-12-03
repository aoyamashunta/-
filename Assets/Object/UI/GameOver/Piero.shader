Shader "Unlit/04_Texture"
{
	Properties
	{
		_MainTex("Texture",2D) = "White"{}
		_Dissolve("Dissolve",Range(0.01,1.1)) = 0
	}
		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
			}

			Blend SrcAlpha OneMinusSrcAlpha

			Pass
			{
				Cull front
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv :TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex :SV_POSITION;
					float2 uv : TEXCOORD0;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				float _Dissolve;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{

					fixed4 col = tex2D(_MainTex, i.uv);
					col.a = step(_Dissolve, col.r);
					return col;

				}
				ENDCG
			}
		}


}

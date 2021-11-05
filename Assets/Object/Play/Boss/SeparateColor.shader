Shader "Unlit/SeparateColor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FirstColor ("First Color", Color) = (1, 1, 1, 1)
        _SecondColor ("Second Color", Color) = (1, 1, 1, 1)
        _Ratio("Ratio", Range(-0.5, 0.5)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 localPos : TEXCOORD1;
                float3 normal : NORMAL;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _FirstColor;
            fixed4 _SecondColor;
            fixed _Ratio;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.localPos = v.vertex.xyz;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {

               float pos = i.localPos.x;
               float2 uv = i.uv;
               float intensity = 
               saturate(dot(normalize(i.normal), _WorldSpaceLightPos0));

               fixed4 firstColor = step(pos, _Ratio) * _FirstColor;
               fixed4 secondColor = (step(1 - pos, _Ratio) * 0.5) *  _SecondColor;

               fixed4 color = firstColor + secondColor;

               return color;
            }
            ENDCG
        }
    }
}

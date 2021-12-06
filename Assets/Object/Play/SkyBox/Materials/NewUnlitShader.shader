Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _AmbColor   ("AmbColor", Color) = (0.2 ,0 ,0 ,1)
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
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed4 _AmbColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //環境光
                //fixed4 ambient = _AmbColor;
                fixed4 ambient = step(_AmbColor, 0.5f);

                fixed4 block = fixed4(0, 0, 0, 1);


                float2 tiling = _MainTex_ST.xy;
                float2 offset = _MainTex_ST.zw;
                fixed4 col = tex2D(_MainTex, i.uv * tiling + offset);

                
                return (-ambient / 2) + col + block * (1 - col.r);
            }
            ENDCG
        }
    }
}

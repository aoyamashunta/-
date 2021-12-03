Shader "Unlit/Body"
{
    Properties
    {
        _MainColor ("MainColor", Color) = (1, 0, 0, 1)
        _SubColor  ("SubColor", Color) = (1, 0.5, 0, 1)
        _MaskTex ("MaskTexTure", 2D) = "white" {}

        //回転の速度
        _RotateSpeed ("Rotate Speed", float) = 1.0
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

            fixed4 _MainColor; //赤
            fixed4 _SubColor;  //オレンジ
            sampler2D _MaskTex; //両画像の比率

            float _RotateSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Timeを入力として現在の回転角度を作る
                half timer = _Time.x;
                // 回転行列を作る
                half angleCos = cos(timer * _RotateSpeed);
                half angleSin = sin(timer * _RotateSpeed);
                /*       |cosΘ -sinΘ|
                  R(Θ) = |sinΘ  cosΘ|  2次元回転行列の公式*/
                half2x2 rotateMatrix = half2x2(angleCos, -angleSin, angleSin, angleCos);
                //中心
                half2 uv = i.uv - 0.5;
                // 中心を起点にUVを回転させる
                i.uv = mul(uv, rotateMatrix) + 0.5;

                fixed4 mask = tex2D(_MaskTex, i.uv);
                fixed4 col = mask.r * _SubColor + (1 - mask.r) * _MainColor;

                return col;
            }
            ENDCG
        }
    }
}

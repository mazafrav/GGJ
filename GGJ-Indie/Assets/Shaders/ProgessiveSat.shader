Shader "Unlit/ProgessiveSat"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Progress ("Progress", range(0.0, 1.0)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Progress;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                float a = col.a;
                // apply fog
                // UNITY_APPLY_FOG(i.fogCoord, col);
                float grayscale = col.r * 0.2126 + col.g * 0.7152 + col.b * 0.0722;
                // col = float4(grayscale, grayscale, grayscale, 1);
                col = col * _Progress + float4(grayscale, grayscale, grayscale, 0) * (1 - _Progress);
                // col = float4(grayscale, grayscale, grayscale, 0) * (1 - _Progress);
                // col = col * _Progress;

                //calcular grayscale
                //lerp entre grayscale y color
                col.a = a;
                return col;
            }
            ENDCG
        }
    }
}

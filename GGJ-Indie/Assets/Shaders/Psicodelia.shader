Shader "Unlit/Psicodelia"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;


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
                // float2 uv = i.uv * _MainTex_TexelSize.xy;
                float2 uv = i.uv;
                float2 center = float2(0.5, 0.5);
                float2 delta = uv - center;
                float strength = lerp(0., 30., (_SinTime + 1.) / 2.);
                float angle = strength * length(delta);

                float x = cos(angle) * delta.x - sin(angle) * delta.y;
                float y = sin(angle) * delta.x - cos(angle) * delta.y;
                uv = float2(x + center.x, y + center.y);
                // sample the texture
                // fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 col = tex2D(_MainTex, uv);
                // apply fog
                // UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}

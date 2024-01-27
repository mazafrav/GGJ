Shader "Unlit/Psicodelia"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PerlinTex ("Perlin", 2D) = "" {}
        _VoronoiTex ("Voronoi", 2D) = "" {}
        _GradientTex ("Gradient Map", 2D) = "" {}
        _Frequency ("Frequency", float) = 1
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
            sampler2D _PerlinTex;
            sampler2D _VoronoiTex;
            sampler2D _GradientTex;
            float4 _GradientTex_TexelSize;
            float _Frequency;
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
                // float2 uv = i.uv * _MainTex_TexelSize.xy;
                float2 uv = i.uv;
                float2 center = float2(0.5, 0.5);
                float2 delta = uv - center;

                float strength = lerp(0.0, 30.0, (_SinTime * _Frequency * _CosTime + 1.) / 2.);
                float angle = strength * pow(length(delta), 0.5);

                float x = cos(angle) * delta.x - sin(angle) * delta.y;
                float y = sin(angle) * delta.x - cos(angle) * delta.y;
                uv = float2(lerp(i.uv.x, x + center.x, _Progress), lerp(i.uv.y, y + center.y, _Progress));
                // sample the texture
                // fixed4 col = tex2D(_MainTex, i.uv);
                // fixed4 col = tex2D(_MainTex, uv);

                float noise = tex2D(_PerlinTex, uv) + _GradientTex_TexelSize.x;
                noise = noise + step(0.5, noise) * 0.1 - step(0.5, 1 - noise) * 0.1;
                // noise = clamp(noise, 0.15)
                noise = lerp(0, 1, noise);
                
                
                float4 col = tex2D(_GradientTex, float2(noise , 0.0));
                float grayscale = col.r * 0.2126 + col.g * 0.7152 + col.b * 0.0722;
                col = col * _Progress + float4(grayscale, grayscale, grayscale, 0) * (1 - _Progress);
                col.a = _Progress;
                // apply fog
                // UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}

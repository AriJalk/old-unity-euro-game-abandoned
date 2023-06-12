Shader "Unlit/HighlightShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _EdgeThreshold ("EdgeThreshold", Range(0,1)) = 1
        _HighlightColor("Highlight Color", Color) = (0, 0, 0, 0)
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
            float _EdgeThreshold;
            float4 _HighlightColor;

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
                fixed4 currentColor = tex2D(_MainTex, i.uv);
                fixed4 neighborColor;

                float2 texelSize = 1.0 / _ScreenParams.xy;

                // Sample left neighbor
                neighborColor = tex2D(_MainTex, i.uv - float2(texelSize.x, 0));
                // Perform the edge test
                if (length(currentColor.rgb - neighborColor.rgb) > _EdgeThreshold)
                {
                    return _HighlightColor;
                }

                // Sample right neighbor
                neighborColor = tex2D(_MainTex, i.uv + float2(texelSize.x, 0));
                // Perform the edge test
                if (length(currentColor.rgb - neighborColor.rgb) > _EdgeThreshold)
                {
                    return _HighlightColor;
                }

                // Sample top neighbor
                neighborColor = tex2D(_MainTex, i.uv + float2(0, texelSize.y));
                // Perform the edge test
                if (length(currentColor.rgb - neighborColor.rgb) > _EdgeThreshold)
                {
                    return _HighlightColor;
                }

                // Sample bottom neighbor
                neighborColor = tex2D(_MainTex, i.uv - float2(0, texelSize.y));
                // Perform the edge test
                if (length(currentColor.rgb - neighborColor.rgb) > _EdgeThreshold)
                {
                    return _HighlightColor;
                }

                // No edge detected, return the original color
                return currentColor;
            }

            ENDCG
        }
    }
}

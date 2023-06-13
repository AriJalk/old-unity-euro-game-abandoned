Shader "Unlit/HighlightShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _UVTex ("UV Texture", 2D) = "white" {}
        _EdgeThreshold ("Edge Threshold", Range(0, 1)) = 0.5
        _HightlightColor("Highlight Color", Color) = (1,1,1,1)

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
            sampler2D _UVTex;
            float _EdgeThreshold;
            float4 _MainTex_ST;
            float4 _HightlightColor;

            bool IsColorSame(fixed4 colorA, fixed4 colorB);

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

                fixed4 uvCol=tex2D(_UVTex, i.uv);

                fixed4 leftNeighbor=tex2D(_UVTex,i.uv+float2(-1,0));
                fixed4 rightNeighbor=tex2D(_UVTex,i.uv+float2(1,0));

                fixed4 topNeighbor=tex2D(_UVTex,i.uv+float2(0,1));
                fixed4 bottomNeighbor=tex2D(_UVTex,i.uv+float2(0,-1));

                fixed4 topLeftNeighbor=tex2D(_UVTex,i.uv+float2(-1,1));
                fixed4 topRightNeighbor=tex2D(_UVTex,i.uv+float2(1,1));

                fixed4 bottomLeftNeighbor=tex2D(_UVTex,i.uv+float2(-1,-1));
                fixed4 bottomRightNeighbor=tex2D(_UVTex,i.uv+float2(1,-1));

                if(leftNeighbor.a<_EdgeThreshold||rightNeighbor.a<_EdgeThreshold||topLeftNeighbor.a<_EdgeThreshold||topRightNeighbor.a<_EdgeThreshold||
                bottomLeftNeighbor.a<_EdgeThreshold||bottomRightNeighbor.a<_EdgeThreshold)
                {
                    return _HightlightColor;
                }

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }

            bool IsColorSame(fixed4 colorA, fixed4 colorB){
                if(colorA.r==colorB.r&&colorA.g==colorB.g&&colorA.b==colorB.b)
                return true;
                return false;
            }

            ENDCG
        }
    }
}

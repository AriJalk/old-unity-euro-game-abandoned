Shader"Custom/TestShader"
{
    Properties
    {
        _Albedo ("Albedo", Color) = (1,1,1,1)
        _SpecColor("SpecColor",Color)=(1,1,1,1)
        _MainTex("MainTex",2D)="white"{}
        _Spec("Specular", Range(0,1)) = 0.5
        _Gloss("Gloss", Range(0,1)) = 0.5
        _Emission("Emission",Color)=(0,0,0,0)
    }
    SubShader
    {
        CGPROGRAM
        
        #pragma surface surf BlinnPhong
        
        sampler2D _MainTex;
        float4 _Albedo;
        half _Spec;
        fixed _Gloss;
        fixed4 _Emission;

        // Use shader model 3.0 target, to get nicer looking lighting

        struct Input
        {
            float2 uv_MainTex;
        };


        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo=tex2D(_MainTex,IN.uv_MainTex).rgb*_Albedo.rgb;
            o.Specular=_Spec;
            o.Gloss=_Gloss;
            o.Emission=_Emission;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

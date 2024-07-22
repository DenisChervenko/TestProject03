Shader "Custom/Toon"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Threshold ("Threshold", Range(0, 1)) = 0.5
        _Smoothness ("Smoothness", Range(0, 1)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf ToonRamp

        sampler2D _MainTex;
        fixed4 _Color;
        float _Threshold;
        float _Smoothness;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            half diff = dot(o.Normal, _WorldSpaceLightPos0.xyz);
            diff = smoothstep(_Threshold - _Smoothness, _Threshold + _Smoothness, diff);

            o.Albedo *= diff;
            o.Alpha = c.a;
        }

        half4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
        {
            half4 c;
            c.rgb = s.Albedo * _LightColor0.rgb * (atten * 2.0);
            c.a = s.Alpha;
            return c;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

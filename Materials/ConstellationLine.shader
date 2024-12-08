Shader "Custom/ConstellationLine"
{
    Properties
    {
        _MainColor ("Line Color", Color) = (1, 1, 0, 1)
        _EmissionMultiplier ("Emission Multiplier", Range(1.0, 100.0)) = 10.0
        _Opacity("Opacity", Range(0, 1)) = 1

    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 300
        //Blend SrcAlpha OneMinusSrcAlpha
        //ZWrite Off

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows nofog alpha:fade



        #include "UnityCG.cginc"

        fixed4 _MainColor;
        float _EmissionMultiplier;

        float _Opacity;

        struct Input
        {
            float2 uv;
            float3 worldPos; // World position for per-object variation
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Base color
            fixed4 baseColor = _MainColor;

            o.Albedo = baseColor.rgb;
            o.Emission = baseColor.rgb * (_EmissionMultiplier);
            o.Alpha = _Opacity;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

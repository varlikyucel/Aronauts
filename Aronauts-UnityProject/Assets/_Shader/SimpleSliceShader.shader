Shader "Custom/SlicedSurfaceShader" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" { }
        _SliceNormal ("Slice Normal", Vector) = (0,1,0,0)
        _SlicePosition ("Slice Position", Vector) = (0,0,0,0)
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _Color;
        float3 _SliceNormal;
        float3 _SlicePosition;

        struct Input {
            float2 uv_MainTex;
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutputStandard o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Calculate distance from slice position
            float dist = dot(IN.worldPos - _SlicePosition, _SliceNormal);
            // Discard fragment if on one side of the plane
            clip(dist);
        }
        ENDCG
    }
    FallBack "Diffuse"
}

Shader "Custom/RingShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _InnerRadius("Inner Radius", Range(0,1)) = 0.3
        _OuterRadius("Outer Radius", Range(0,1)) = 0.5
    }
        SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _Color;
            float _InnerRadius;
            float _OuterRadius;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv - 0.5; // center the UV
                float dist = length(uv);

                if (dist < _InnerRadius || dist > _OuterRadius)
                    discard;

                return _Color;
            }
            ENDCG
        }
    }
}

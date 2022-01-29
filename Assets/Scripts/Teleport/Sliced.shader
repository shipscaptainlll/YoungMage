Shader "Custom/Portal"
{
    Properties
    {
        _InactiveColour ("Inactive Colour", Color) = (1, 1, 1, 1)

        sliceNormal("normal", Vector) = (0,0,0,0)
        sliceCentre("centre", Vector) = (0,0,0,0)
        sliceOffsetDst("offset", Float) = 0
    }
    SubShader
    {
        Tags { "Queue"="Geometry" "IgnoreProjector" = "True" "RenderType" = "Geometry" }
        LOD 200
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD0;
            };
            struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float3 sliceNormal;
        float3 sliceCentre;
        float sliceOffsetDst;

            sampler2D _MainTex;
            float4 _InactiveColour;
            int displayMask; // set to 1 to display texture, otherwise will draw test colour
            
            void surf (Input IN, inout SurfaceOutputStandard o)
        {
            
        }
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.screenPos.xy / i.screenPos.w;
                fixed4 portalCol = tex2D(_MainTex, uv);
                return portalCol * displayMask + _InactiveColour * (1-displayMask);
            }
            
            ENDCG
        }
    }
    Fallback "Standard" // for shadows
}

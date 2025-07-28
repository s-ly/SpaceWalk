Shader "Unlit/MixUnlitShader"
{
    Properties
    {
        _MainTex1 ("Texture 1", 2D) = "white" {}
        _MainTex2 ("Texture 2", 2D) = "black" {}
        _MaskTex ("Mask (B/W)", 2D) = "gray" {}

        _MainTex1_ST ("Texture 1 ST", Vector) = (1, 1, 0, 0)
        _MainTex2_ST ("Texture 2 ST", Vector) = (1, 1, 0, 0)
        _MaskTex_ST ("Mask ST", Vector) = (1, 1, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv1 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float2 uvMask : TEXCOORD2;
            };

            sampler2D _MainTex1;
            sampler2D _MainTex2;
            sampler2D _MaskTex;

            float4 _MainTex1_ST;
            float4 _MainTex2_ST;
            float4 _MaskTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv1 = TRANSFORM_TEX(v.uv, _MainTex1);
                o.uv2 = TRANSFORM_TEX(v.uv, _MainTex2);
                o.uvMask = TRANSFORM_TEX(v.uv, _MaskTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 c1 = tex2D(_MainTex1, i.uv1);
                fixed4 c2 = tex2D(_MainTex2, i.uv2);
                float mask = tex2D(_MaskTex, i.uvMask).r;
                return lerp(c1, c2, mask);
            }
            ENDCG
        }
    }
}

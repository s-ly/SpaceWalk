// This is similar to the standard Projector/Multiply shader, but in addition to
// a falloff texture, we let the user modify the alpha using a per-renderer value.
 
// Alpha is set per-renderer, so you have to set it with a MaterialPropertyBlock.
// (EDIT: no it's not, because I can't find any way to use those with a Projector.)
 
Shader "Projector/MultiplyAlpha" {
    Properties {
        _ShadowTex ("Cookie", 2D) = "gray" {}
        _FalloffTex ("FallOff", 2D) = "white" {}
        _Alpha ("Alpha", Range (0, 1)) = 1
    }
    Subshader {
        Tags {"Queue"="Transparent"}
        Pass {
            ZWrite Off
            ColorMask RGB
            Blend DstColor Zero
            Offset -1, -1
 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #include "UnityCG.cginc"
           
            struct v2f {
                float4 uvShadow : TEXCOORD0;
                float4 uvFalloff : TEXCOORD1;
                UNITY_FOG_COORDS(2)
                float4 pos : SV_POSITION;
            };
           
            float4x4 unity_Projector;
            float4x4 unity_ProjectorClip;
           
            v2f vert (float4 vertex : POSITION)
            {
                v2f o;
                o.pos = UnityObjectToClipPos (vertex);
                o.uvShadow = mul (unity_Projector, vertex);
                o.uvFalloff = mul (unity_ProjectorClip, vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
           
            sampler2D _ShadowTex;
            sampler2D _FalloffTex;
            float _Alpha;
           
            fixed4 frag (v2f i) : SV_Target
            {
                // Compute a shadow color to apply, where white is invisible
                // and black is dark.  Actual alpha channel is ignored.
                fixed4 result = tex2Dproj (_ShadowTex, UNITY_PROJ_COORD(i.uvShadow));
               
                // Lerp between the color from the shadow cookie, and full white
                // (invisible), based on alpha parameter and the uvFalloff (which
                // relates to the clipping planes).
                fixed4 texF = tex2Dproj (_FalloffTex, UNITY_PROJ_COORD(i.uvFalloff));
                result = lerp(fixed4(1,1,1,0), result, _Alpha * texF.a);
 
                UNITY_APPLY_FOG_COLOR(i.fogCoord, result, fixed4(1,1,1,1));
               
                return result;
            }
            ENDCG
        }
    }
}
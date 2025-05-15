Shader "Custom/TransparentExceptBlack"
{
    Properties { _MainTex ("Texture", 2D) = "white" {} }

    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);

                // 검은색인지 검사 (약간의 여유를 둠)
                if (col.r < 0.05 && col.g < 0.05 && col.b < 0.05) {
                    col.a = 0; // 완전 투명
                } else {
                    col.a = 1; // 불투명하게 출력
                }

                return col;
            }
            ENDCG
        }
    }
}

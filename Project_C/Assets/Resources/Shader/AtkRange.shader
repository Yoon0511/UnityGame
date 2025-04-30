Shader "Custom/AtkRange"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FillColor ("Fill Color", Color) = (1,1,1,1)
        _GlowColor ("Glow Color", Color) = (1, 0.8, 0.2, 1) // 빛나는 경계선 색
        _Progress ("Progress (0 to 1)", Range(0,1)) = 0.0
        _GlowSize ("Glow Size", Float) = 0.02
        _CenterX ("CenterX",Float) = 0.5
        _CenterY ("CenterY",Float) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

        Pass
        {
            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _FillColor;
            float4 _GlowColor;
            float _Progress;
            float _GlowSize;
            float _CenterX;
            float _CenterY;

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float2 origin = float2(_CenterX, _CenterY); // 위 꼭짓점

                float dist = distance(uv, origin);
                float threshold = _Progress;

                // 채워지는 부분 알파
                float alpha = smoothstep(threshold + 0.02, threshold - 0.02, dist);

                // Glow: 경계선 근처에서만 작동
                float glow = smoothstep(threshold + _GlowSize, threshold, dist);

                float4 texCol = tex2D(_MainTex, uv) * _FillColor;

                // Glow 색상 섞기
                texCol.rgb = lerp(texCol.rgb, _GlowColor.rgb, glow);
                //texCol.a *= alpha;

                return texCol;
            }
            ENDCG
        }
    }
}
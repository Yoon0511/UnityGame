Shader "Custom/ToonPostProcessing"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Threshold ("Edge Threshold", Range(0.1, 1)) = 0.5
        _PosterizationLevels ("Color Steps", Range(2, 8)) = 4
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" }
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _MainTex;
            float _Threshold;
            int _PosterizationLevels;
            
            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                float2 texelSize = 1.0 / _ScreenParams.xy;
                
                float4 color = tex2D(_MainTex, i.uv);
                
                // 포스터라이제이션 (색 단순화)
                color.rgb = round(color.rgb * _PosterizationLevels) / _PosterizationLevels;
                
                // 엣지 검출 (차분 계산)
                float3 sobelX = tex2D(_MainTex, i.uv + float2(texelSize.x, 0)).rgb - tex2D(_MainTex, i.uv - float2(texelSize.x, 0)).rgb;
                float3 sobelY = tex2D(_MainTex, i.uv + float2(0, texelSize.y)).rgb - tex2D(_MainTex, i.uv - float2(0, texelSize.y)).rgb;
                float edge = length(sobelX + sobelY);
                
                if (edge > _Threshold)
                    color.rgb = 0; // 검은색 엣지 적용
                
                return color;
            }
            
            ENDCG
        }
    }
}

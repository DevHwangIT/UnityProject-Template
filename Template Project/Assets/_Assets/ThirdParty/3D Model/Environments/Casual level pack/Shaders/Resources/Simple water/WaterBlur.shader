Shader "Hidden/WaterBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float2 _Direction;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 colCenter = tex2D(_MainTex, i.uv);
                float2 shift = _MainTex_TexelSize.xy * _Direction;
                fixed4 colA = tex2D(_MainTex, i.uv + shift);
                fixed4 colB = tex2D(_MainTex, i.uv - shift);
                return (colCenter + colA + colB) * 0.33333333f;
            }
            ENDCG
        }
    }
}

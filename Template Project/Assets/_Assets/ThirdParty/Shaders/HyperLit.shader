Shader "Custom/URP/HyperLit"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _ShadowColor ("Shadow", Color) = (1,1,1,1)
        _HighlightColor ("Highlight", Color) = (1,1,1,1)
        _ShadowOffset ("Shadow Offset", Range(-1, 1)) = 0
        _HighlightOffset ("Highlight Offset", Range(0, 1)) = 0.5
        _LightStep ("LightStep", Range(0, 1)) = 1
        _Luminosity ("Luminosity", Range(0, 2)) = 0
        [Toggle]_ReceiveShadow("Receive Shadow", float) = 0
        [Toggle]_CastShadow("Cast Shadow", float) = 0
    }  
    SubShader
    {
        Tags {"RenderType" = "Opaque"}
     
        Pass
        {          
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile_fog
            //shadows
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            half4 _Color;
            float4 _HighlightColor;
            float4 _ShadowColor;
            float _HighlightOffset;
            float _ShadowOffset;
            float _LightStep;
            float _Luminosity;
            float _ReceiveShadow;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
                float3 normal : NORMAL;                        
            };
            struct v2f
            {
                float4 vertex      : SV_POSITION;
                float2 uv          : TEXCOORD0;
                float3 normal      : NORMAL;
                            
                float4 shadowCoord : TEXCOORD2;
                half fogFactor: TEXCOORD5;
            };
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                        
                o.uv = v.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw; ;
                o.normal = normalize(mul(v.normal, (float3x3)UNITY_MATRIX_I_M));
                VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
                o.shadowCoord = GetShadowCoord(vertexInput);
                o.fogFactor = ComputeFogFactor(vertexInput.positionCS.z);
                return o;
            }
            half4 frag(v2f i) : SV_Target
            {
                float3 normal = normalize(_MainLightPosition.xyz);
                float NdotL = dot(normal, i.normal);
                float shadowIntensity = smoothstep(-_LightStep, _LightStep, NdotL + _ShadowOffset);
                float lightIntensity = smoothstep(-_LightStep, _LightStep, NdotL + _ShadowOffset + _HighlightOffset);
                Light mainLight = GetMainLight(i.shadowCoord);
                float atten = mainLight.shadowAttenuation;
                atten = clamp (atten, 1 - _ReceiveShadow, 1);
                int dotSign = sign(NdotL);
                dotSign = clamp(dotSign, 0, 1);
                atten = atten * dotSign + (1 - dotSign);
                half4 color = tex2D(_MainTex, i.uv) * (((1 - shadowIntensity) * _ShadowColor) + (shadowIntensity * _Color)) * atten + (tex2D(_MainTex, i.uv) * _Luminosity);
                color.rgb = MixFog(color.rgb, i.fogFactor);
               
                return color;
            }
            ENDHLSL  
        }
        Tags{"LightMode" = "ShadowCaster"}
        Pass
        {
            HLSLPROGRAM
            #pragma vertex ShadowPassVertex
            #pragma fragment ShadowPassFragment
          
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/LitInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/ShadowCasterPass.hlsl"
            float _CastShadow;
            struct VertexInput
            {         
                float4 vertex : POSITION;
                float4 normal : NORMAL;
            };
         
            struct VertexOutput
            {         
                float4 vertex : SV_POSITION; 
            };
            VertexOutput ShadowPassVertex(VertexInput v)
            {
                VertexOutput o;
                float3 positionWS = TransformObjectToWorld(v.vertex.xyz);
                float3 normalWS   = TransformObjectToWorldNormal(v.normal.xyz);
                //ApplyShadowBias(positionWS, normalWS, _MainLightPosition.xyz)//
                float4 positionCS = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, _MainLightPosition.xyz)) * _CastShadow;
                o.vertex = positionCS;
                return o;
            }
            half4 ShadowPassFragment(VertexOutput i) : SV_TARGET
            {
                return 0;
            }
            ENDHLSL
        }
        Tags{"LightMode" = "DepthOnly"}
        
        Pass
        {
            ZWrite On
            ColorMask 0
            Cull Back
            HLSLPROGRAM
           
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0
   
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma vertex vert
            #pragma fragment frag
               
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
               
            CBUFFER_START(UnityPerMaterial)
            CBUFFER_END
               
            struct VertexInput
            {
                float4 vertex : POSITION;                   
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
                struct VertexOutput
                {           
                float4 vertex : SV_POSITION;
                 
                UNITY_VERTEX_INPUT_INSTANCE_ID           
                UNITY_VERTEX_OUTPUT_STEREO                 
                };
            VertexOutput vert(VertexInput v)
            {
                VertexOutput o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                return o;
            }
            half4 frag(VertexOutput IN) : SV_TARGET
            {       
                return 0;
            }
            ENDHLSL
        }
    }
}
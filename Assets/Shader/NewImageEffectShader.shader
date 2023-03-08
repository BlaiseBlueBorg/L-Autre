Shader "Hidden/NewImageEffectShader"
{
    Properties
    {
        void MainLight_float(float3 WorldPos)
        {
        #ifdef SHADERGRAPH_PREVIEW
            
        #else
            float4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
            Light mainLight = GetMainLight(shadowCoord);
        #endif
        }
    }
}

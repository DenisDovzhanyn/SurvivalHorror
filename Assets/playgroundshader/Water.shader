Shader "Unlit/Water"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Speed", float) = 0.1
        _Frequency ("Frequency", float) = 1
        _Amplitude ("Amplitude", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        
        LOD 100
        ZWrite Off
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;

            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 worldSpace : TEXCOORD1;
            };

            sampler2D _MainTex;
            float _Speed;
            float _Frequency;
            float _Amplitude;

            float getWaveHeight(float y) {
                return sin((y + _Time.y * _Speed) * _Frequency) * _Amplitude;
            } 

            v2f vert (appdata v)
            {
                v2f o;
                float4 worldSpace = mul(unity_ObjectToWorld, v.vertex);
                o.worldSpace = worldSpace;
                o.uv = v.uv;

                worldSpace.y += getWaveHeight(worldSpace.z);
                o.vertex = mul(UNITY_MATRIX_VP, worldSpace);
                return o;
            }
            
            float4 frag (v2f i) : SV_Target
            {
                return float4(0,0,max(0.2,getWaveHeight(i.worldSpace.z)),0.8);
            }
            ENDCG
        }
    }
}

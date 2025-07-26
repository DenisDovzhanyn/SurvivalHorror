Shader "Unlit/Leaf"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Cull Off
        Pass
        {
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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
            };

            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;
                v.vertex.x = sin((v.uv.y + _Time.y * 0.5) * 10) * 0.1 + v.vertex.x;
                // v.vertex.z = sin((v.uv.x + _Time.y * 0.5) * 10) * 0.1 + v.vertex.z;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                return float4(0,0.5,0,1) * max(0.1, dot(i.normal, _WorldSpaceLightPos0.xyz)) * max(0.5, frac(sin(i.uv.y * i.uv.x) * 2000));
            }
            ENDCG
        }
    }
}

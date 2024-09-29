Shader "Unlit/RainWIndow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Size ("Size", Float) = 2
        _T("Time", Float) = 0
        [Toggle(_True)] _IsTest("Is Test", Float) = 1
        _Distortion("Distortion", Range(-5,5)) = 1
        _Blur("Blur", Range(0,1)) = 1
        [Vector2]_Aspect("Aspect", Vector) = (1,1,1,1)
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Size;
            float _T;
            bool _IsTest;
            float _Distortion;
            float _Blur;
            float2 _Aspect;

            float Time()
            {
                return fmod(_IsTest ? _T : _Time.y, 7200);
            }

            bool IsBorder(float2 pos, float borderWidth, float2 aspect)
            {
                return pos.x > 0.5 - borderWidth * aspect.x || pos.x < -0.5 + borderWidth * aspect.x
                    || pos.y > 0.5 - borderWidth * aspect.y || pos.y < -0.5 + borderWidth * aspect.y;
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            float Random(float2 p)
            {
                p = frac(p * float2(123.34, 345.45));
                p += dot(p, p + 34.345);
                return frac(p.x * p.y);
            }

            float3 Layer(float2 UV, float time)
            {
                float moveOffset = time * .25;

                float2 aspect = _Aspect;
                float2 uv = UV * _Size * aspect;
                uv.y += moveOffset;
                float2 id = floor(uv);
                float n = Random(id);
                time += n * 6.28;
                float2 gv = frac(uv) - 0.5;

                float w = UV.y * 10;
                float x = (n - .5) * .8;
                x += (0.4 - abs(x)) * sin(3 * w) * pow(sin(w), 6) * .45;
                float y = -sin(time + sin(time + sin(time) * .5)) * .45;
                y -= (gv.x - x) * (gv.x - x);

                float2 dropPos = (gv - float2(x, y)) / aspect;
                float dropColor = smoothstep(0.05, 0.03, length(dropPos));

                float2 trailPos = (gv - float2(x, moveOffset)) / aspect;
                int trailCount = 8;
                trailPos.y = (frac(trailPos.y * trailCount) - .5) / trailCount;
                float trailColor = smoothstep(.03, .01, length(trailPos));
                float trailFog = smoothstep(-.05, .05, dropPos.y);
                trailFog *= smoothstep(.5, y, gv.y);
                trailColor *= trailFog;
                trailFog *= smoothstep(.05, .04, abs(dropPos.x));

                float2 offset = dropColor * dropPos + trailColor * trailPos;
                return float3(offset * _Distortion, trailFog);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float time = Time();
                float3 fog = Layer(i.uv * 1.23 + 7.54, time);
                fog += Layer(i.uv * 1.23 + 1.54, time);
                fog += Layer(i.uv * 1.23 - 7.54, time);
                float blur = _Blur * 7 * (1 - fog.z);
                float4 color = tex2Dlod(_MainTex, float4(i.uv + fog.xy, 0, blur));
                return color;
            }
            ENDCG
        }
    }
}
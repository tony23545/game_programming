Shader "Toon" {
  Properties {
    _ToonColor ("Toon Color", Color) = (1,1,1,1)
  }

  SubShader {

    Tags {
      "LightMode"="ForwardBase"
      "Queue"="Geometry"
      "RenderType"="Opaque"

    }

    Pass {
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #include "UnityCG.cginc"
      #include "UnityLightingCommon.cginc"
      struct vertInput {
        float4 pos : POSITION;
        float3 normal : NORMAL;
      };

      struct vertOutput {
        float4 pos : SV_POSITION;
        float3 normal : TEXCOORD0;
        //half4 color : COLOR0;
      };

      vertOutput vert(vertInput input) {
        vertOutput o;
        o.pos = UnityObjectToClipPos(input.pos);
        o.normal = UnityObjectToWorldNormal(input.normal);
        //o.color = _ToonColor; 
        return o;
      }

      half4 _ToonColor;

      half4 frag(vertOutput output) : COLOR {
          half toon = 1.0;

          // TODO: update toon to implement toon shading
        float dot_p = max(0, dot(UnityObjectToWorldNormal(output.normal), _WorldSpaceLightPos0.xyz));
        if(dot_p > 0.7){
        	toon = 1.0;
        }
        else if(dot_p <=0.7 && dot_p > 0.3){
        	toon = 0.7;
        }
        else{
        	toon = 0.3;
        }
          return toon * _ToonColor;
      }
      ENDCG
    }
  }
}
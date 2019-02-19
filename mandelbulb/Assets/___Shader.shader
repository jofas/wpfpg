Shader "Unlit/___Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert fragment frag

void vert(inout appdata_full v, out Input o) {
  //v.vertex.xyz += 2 * v.normal.xyz;
  UNITY_INITIALIZE_OUTPUT(Input, o);
}

float mandelboxDF(float4 z, inout float orbit) {

	//distance field formula was completely script-kiddied off of fractalforums
	//(knighty and Rrrola are some radical dudes)

	float4 offset=z;

	//in this case, this z0 value could be replaced with offset...
	//...but i might play with non-default offsets later
	float3 z0=z.xyz;

	//orbit trap:
	//square distance from current position to negative starting position,
	//added to current position's square length
	orbit=dot(z.xyz+z0,z.xyz+z0)+dot(z.xyz,z.xyz);

	for (int i=0;i<boxIters;i++) {
		//boxfold
		z.xyz=clamp(z.xyz,-1.0,1.0)*2.0-z.xyz;
		//spherefold
		z*=clamp(max(mr2/dot(z.xyz,z.xyz),mr2),0.0,1.0);

		//orbit trap
		//performed before scale/offset because...
		//...i dunno, it's prettier
		orbit=min(orbit,dot(z.xyz+z0,z.xyz+z0)+dot(z.xyz,z.xyz));

		//scale+offset
		z=z*scalevec+offset;
	}
	return (length(z.xyz)-C1)/z.w - C2;
}

fixed4 frag(vertO input):COLOR {
	int i;

	float3 rayDir=normalize(input.viewDir);
	fixed4 output=fixed4(0.0,0.0,0.0,0.0);
	float3 normal;
	float3 lightVector;

	float4 pos=float4(_WorldSpaceCameraPos,1.0);

	//compute a few things outside of the big loops
	C1=abs(_Scale-1.0);
	C2=pow(abs(_Scale),float(1.0-boxIters));
	mr2=dot(_SFMinRadius,_SFMinRadius);
	scalevec=float4(_Scale,_Scale,_Scale,abs(_Scale));

	float dist=1.0;
	float orbit=10.0;

	for (i=0;i<rayIters&&dist>_HitDist;i++) {
		dist=mandelboxDF(pos,orbit);
		pos.xyz+=rayDir*dist*_StepMultiplier;
	}
	if (dist<_HitDist) {
		//starter shading from raymarch iteration count
		output=(1.0-i/float(rayIters));

		//apply some colors based on orbit trap
		float ct=(abs(frac(orbit*1.0)-0.5)*2.0)*0.35+0.65;
		float ct2=abs(frac(orbit*.071)-0.5)*2.0;
		output.xyz*=lerp(fixed3(0.8,0.7,0.4)*ct,fixed3(0.7,0.15,0.2)*ct,ct2);

		//z-buffer for mixing with polygonal geometry
		fixed rawDepth=tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD(input.screenPos)).r;
		if (rawDepth!=1.0) {
			float depth=LinearEyeDepth(rawDepth)*.01;
			float3 finalRay=_WorldSpaceCameraPos-pos.xyz;
			if (depth*depth<dot(finalRay,finalRay)) {
				discard;
			}
		}

	}
	if (i>=rayIters) {
		discard;
	} else {
		//apply some simple lighting
		normal=0.0;
		normal.x=mandelboxDF(pos+float4(0.001,0.0,0.0,0.0),orbit)-mandelboxDF(pos-float4(0.001,0.0,0.0,0.0),orbit);
		normal.y=mandelboxDF(pos+float4(0.0,0.001,0.0,0.0),orbit)-mandelboxDF(pos-float4(0.0,0.001,0.0,0.0),orbit);
		normal.z=mandelboxDF(pos+float4(0.0,0.0,0.001,0.0),orbit)-mandelboxDF(pos-float4(0.0,0.0,0.001,0.0),orbit);
		normal=normalize(normal);

		lightVector=normalize(float3(1.0,1.0,1.0));
		output.xyz*=clamp(dot(normal,lightVector),0.0,1.0)*0.5+0.5;
	}
	return output;
}
            ENDCG
        }
    }
}

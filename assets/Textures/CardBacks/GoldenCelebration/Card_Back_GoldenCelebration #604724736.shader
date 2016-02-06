Shader "Custom/CardBack/GoldenCelebration" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _MaskTex ("Mask (RGB)", 2D) = "black" {}
 _FxTex1 ("FX 1 Map", 2D) = "black" {}
 _FXIntensity1 ("FX 1 Intensity", Float) = 1
 _FXScale1 ("FX 1 Scale", Float) = 1
 _FXScrollX1 ("FX 1 Scroll X", Float) = 1
 _FXScrollY1 ("FX 1 Scroll Y", Float) = 1
 _FxTex2 ("FX 2 Map", 2D) = "black" {}
 _FXIntensity2 ("FX 2 Intensity", Float) = 1
 _FXScale2 ("FX 2 Scale", Float) = 1
 _FXScrollX2 ("FX 2 Scroll X", Float) = 1
 _FXScrollY2 ("FX 2 Scroll Y", Float) = 1
 _FxColor1 ("FX 1 Color", Color) = (1,1,1,1)
 _FXIntensity ("FX Intensity", Float) = 1
 _FXFlicker ("FX Flicker Speed", Float) = 1
 _SpecIntensityR ("(Red) Specular Intensity", Float) = 1
 _SpecShininessR ("(Red) Specular Shininess", Float) = 25
 _SpecIntensityG ("(Green) Specular Intensity", Float) = 1
 _SpecShininessG ("(Green) Specular Shininess", Float) = 25
 _LightingBlend ("Ambient Lighting Blend", Float) = 0
 _Seed ("Seed", Float) = 0
 _TimeScale ("Time Scale", Float) = 1
}
SubShader { 
 Tags { "RenderType"="Opaque" "Highlight"="true" }
 Pass {
  Tags { "RenderType"="Opaque" "Highlight"="true" }
  Fog {
   Color (0,0,0,1)
  }
Program "vp" {
SubProgram "opengl " {
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
Vector 2 [_Time]
Vector 3 [_WorldSpaceCameraPos]
Vector 4 [_WorldSpaceLightPos0]
Vector 17 [_LightColor0]
Float 18 [_FXScale1]
Float 19 [_FXScrollX1]
Float 20 [_FXScrollY1]
Float 21 [_FXScale2]
Float 22 [_FXScrollX2]
Float 23 [_FXScrollY2]
Float 24 [_SpecIntensityR]
Float 25 [_SpecShininessR]
Float 26 [_SpecIntensityG]
Float 27 [_SpecShininessG]
Float 28 [_Seed]
Float 29 [_ShaderTime]
Float 30 [_TimeScale]
Float 31 [_LightingBlend]
Vector 32 [_MainTex_ST]
"!!ARBvp1.0
PARAM c[33] = { { 0, 2, 1 },
		state.lightmodel.ambient,
		program.local[2..12],
		state.matrix.mvp,
		program.local[17..32] };
TEMP R0;
TEMP R1;
TEMP R2;
MUL R0.xyz, vertex.normal.y, c[10];
MAD R0.xyz, vertex.normal.x, c[9], R0;
MAD R0.xyz, vertex.normal.z, c[11], R0;
ADD R1.xyz, R0, c[0].x;
DP3 R0.x, R1, R1;
RSQ R0.y, R0.x;
MUL R1.xyz, R0.y, R1;
DP3 R0.x, c[4], c[4];
RSQ R0.w, R0.x;
MUL R0.w, R0, c[4].x;
DP3 R2.x, R1, -R0.w;
MUL R1.xyz, R1, R2.x;
MAD R1.xyz, -R1, c[0].y, -R0.w;
DP4 R0.z, vertex.position, c[7];
DP4 R0.x, vertex.position, c[5];
DP4 R0.y, vertex.position, c[6];
ADD R0.xyz, -R0, c[3];
DP3 R1.w, R0, R0;
RSQ R1.w, R1.w;
MUL R0.xyz, R1.w, R0;
DP3 R0.x, R1, R0;
POW R0.y, R0.x, c[27].x;
MAX R0.y, R0, c[0].x;
POW R0.x, R0.x, c[25].x;
MUL result.texcoord[1].y, R0, c[26].x;
MAX R0.y, R0.x, c[0].x;
MOV R0.x, c[0].y;
MUL R1, R0.x, c[1];
ADD R2, R1, c[17];
MOV R0.x, c[29];
SLT R1.x, c[0], R0;
MUL R1.y, R1.x, c[29].x;
MUL result.texcoord[1].x, R0.y, c[24];
ABS R1.x, R1;
ADD R0, R2, -c[0].z;
SGE R1.x, c[0], R1;
ADD R1.y, R1, c[28].x;
MAD R1.y, R1.x, c[2].x, R1;
MOV R1.x, c[0].z;
MUL R1.y, R1, c[30].x;
MAD result.color, R0, c[31].x, R1.x;
MUL R0.y, R1, c[23].x;
MUL R0.x, R1.y, c[22];
MAD R0.y, vertex.texcoord[0], c[21].x, R0;
MAD R0.x, vertex.texcoord[0], c[21], R0;
MOV result.texcoord[2].zw, R0.xyxy;
MUL R0.x, R1.y, c[19];
MUL R0.y, R1, c[20].x;
MAD result.texcoord[0].xy, vertex.texcoord[0], c[32], c[32].zwzw;
MAD result.texcoord[2].x, vertex.texcoord[0], c[18], R0;
MAD result.texcoord[2].y, vertex.texcoord[0], c[18].x, R0;
DP4 result.position.w, vertex.position, c[16];
DP4 result.position.z, vertex.position, c[15];
DP4 result.position.y, vertex.position, c[14];
DP4 result.position.x, vertex.position, c[13];
END
# 55 instructions, 3 R-regs
"
}
SubProgram "d3d9 " {
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
Vector 12 [glstate_lightmodel_ambient]
Vector 13 [_Time]
Vector 14 [_WorldSpaceCameraPos]
Vector 15 [_WorldSpaceLightPos0]
Vector 16 [_LightColor0]
Float 17 [_FXScale1]
Float 18 [_FXScrollX1]
Float 19 [_FXScrollY1]
Float 20 [_FXScale2]
Float 21 [_FXScrollX2]
Float 22 [_FXScrollY2]
Float 23 [_SpecIntensityR]
Float 24 [_SpecShininessR]
Float 25 [_SpecIntensityG]
Float 26 [_SpecShininessG]
Float 27 [_Seed]
Float 28 [_ShaderTime]
Float 29 [_TimeScale]
Float 30 [_LightingBlend]
Vector 31 [_MainTex_ST]
"vs_2_0
def c32, 0.00000000, 2.00000000, -1.00000000, 1.00000000
dcl_position0 v0
dcl_texcoord0 v1
dcl_normal0 v2
mul r0.xyz, v2.y, c9
mad r0.xyz, v2.x, c8, r0
mad r0.xyz, v2.z, c10, r0
add r1.xyz, r0, c32.x
dp3 r0.x, r1, r1
rsq r0.y, r0.x
mul r1.xyz, r0.y, r1
dp3 r0.x, c15, c15
rsq r0.w, r0.x
mul r0.w, r0, c15.x
dp3 r2.x, r1, -r0.w
mul r1.xyz, r1, r2.x
mad r1.xyz, -r1, c32.y, -r0.w
mov r2, c12
dp4 r0.z, v0, c6
dp4 r0.x, v0, c4
dp4 r0.y, v0, c5
add r0.xyz, -r0, c14
dp3 r1.w, r0, r0
rsq r1.w, r1.w
mul r0.xyz, r1.w, r0
dp3 r1.x, r1, r0
pow r0, r1.x, c26.x
max r1.y, r0.x, c32.x
pow r0, r1.x, c24.x
mov r0.z, r0.x
max r0.w, r0.z, c32.x
mov r0.y, c28.x
slt r0.x, c32, r0.y
sge r0.z, c32.x, r0.x
sge r0.y, r0.x, c32.x
mul r0.y, r0, r0.z
max r0.z, -r0.x, r0.x
max r0.y, -r0, r0
slt r0.x, c32, r0.y
slt r0.y, c32.x, r0.z
mul oT1.x, r0.w, c23
add r0.w, -r0.y, c32
mov r0.z, c27.x
add r1.x, -r0, c32.w
mul r0.w, r0, c27.x
add r0.z, c28.x, r0
mad r0.y, r0, r0.z, r0.w
mul r0.z, r0.y, r1.x
add r0.y, r0, c13.x
mad r0.x, r0, r0.y, r0.z
mul r1.x, r0, c29
mul r0, c32.y, r2
add r2, r0, c16
mul r1.z, r1.x, c22.x
mul oT1.y, r1, c25.x
mul r0.y, r1.x, c19.x
mad r1.w, v1.y, c20.x, r1.z
mul r1.y, r1.x, c21.x
mad r1.z, v1.x, c20.x, r1.y
mov r0.x, c32.w
add r2, r2, c32.z
mad oD0, r2, c30.x, r0.x
mul r0.x, r1, c18
mov oT2.zw, r1
mad oT2.x, v1, c17, r0
mad oT2.y, v1, c17.x, r0
mad oT0.xy, v1, c31, c31.zwzw
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}
SubProgram "d3d11 " {
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
ConstBuffer "$Globals" 160
Vector 16 [_LightColor0]
Float 68 [_FXScale1]
Float 72 [_FXScrollX1]
Float 76 [_FXScrollY1]
Float 84 [_FXScale2]
Float 88 [_FXScrollX2]
Float 92 [_FXScrollY2]
Float 104 [_SpecIntensityR]
Float 108 [_SpecShininessR]
Float 112 [_SpecIntensityG]
Float 116 [_SpecShininessG]
Float 120 [_Seed]
Float 124 [_ShaderTime]
Float 128 [_TimeScale]
Float 132 [_LightingBlend]
Vector 144 [_MainTex_ST]
ConstBuffer "UnityPerCamera" 128
Vector 0 [_Time]
Vector 64 [_WorldSpaceCameraPos] 3
ConstBuffer "UnityLighting" 720
Vector 0 [_WorldSpaceLightPos0]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
ConstBuffer "UnityPerFrame" 208
Vector 64 [glstate_lightmodel_ambient]
BindCB  "$Globals" 0
BindCB  "UnityPerCamera" 1
BindCB  "UnityLighting" 2
BindCB  "UnityPerDraw" 3
BindCB  "UnityPerFrame" 4
"vs_4_0
eefiecedijpmeceapcaikihaaclgiadbjpghfdnoabaaaaaabaaiaaaaadaaaaaa
cmaaaaaalmaaaaaagaabaaaaejfdeheoiiaaaaaaaeaaaaaaaiaaaaaagiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaahbaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaahkaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaa
apaaaaaaiaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaahahaaaafaepfdej
feejepeoaafeeffiedepepfceeaaedepemepfcaaeoepfcenebemaaklepfdeheo
jmaaaaaaafaaaaaaaiaaaaaaiaaaaaaaaaaaaaaaabaaaaaaadaaaaaaaaaaaaaa
apaaaaaaimaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadamaaaaimaaaaaa
abaaaaaaaaaaaaaaadaaaaaaabaaaaaaamadaaaaimaaaaaaacaaaaaaaaaaaaaa
adaaaaaaacaaaaaaapaaaaaajfaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaa
apaaaaaafdfgfpfagphdgjhegjgpgoaafeeffiedepepfceeaaedepemepfcaakl
fdeieefckiagaaaaeaaaabaakkabaaaafjaaaaaeegiocaaaaaaaaaaaakaaaaaa
fjaaaaaeegiocaaaabaaaaaaafaaaaaafjaaaaaeegiocaaaacaaaaaaabaaaaaa
fjaaaaaeegiocaaaadaaaaaabdaaaaaafjaaaaaeegiocaaaaeaaaaaaafaaaaaa
fpaaaaadpcbabaaaaaaaaaaafpaaaaaddcbabaaaabaaaaaafpaaaaadhcbabaaa
adaaaaaaghaaaaaepccabaaaaaaaaaaaabaaaaaagfaaaaaddccabaaaabaaaaaa
gfaaaaadmccabaaaabaaaaaagfaaaaadpccabaaaacaaaaaagfaaaaadpccabaaa
adaaaaaagiaaaaacacaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaaaaaaaaaa
egiocaaaadaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaadaaaaaa
aaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaaaaaaaaaa
egiocaaaadaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaak
pccabaaaaaaaaaaaegiocaaaadaaaaaaadaaaaaapgbpbaaaaaaaaaaaegaobaaa
aaaaaaaabaaaaaaibcaabaaaaaaaaaaaegbcbaaaadaaaaaaegiccaaaadaaaaaa
baaaaaaabaaaaaaiccaabaaaaaaaaaaaegbcbaaaadaaaaaaegiccaaaadaaaaaa
bbaaaaaabaaaaaaiecaabaaaaaaaaaaaegbcbaaaadaaaaaaegiccaaaadaaaaaa
bcaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaaaaaaaaa
eeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaaaaaaaaaa
pgapbaaaaaaaaaaaegacbaaaaaaaaaaabaaaaaajicaabaaaaaaaaaaaegiccaaa
acaaaaaaaaaaaaaaegiccaaaacaaaaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaa
dkaabaaaaaaaaaaadiaaaaaiicaabaaaaaaaaaaadkaabaaaaaaaaaaaakiacaaa
acaaaaaaaaaaaaaabaaaaaaibcaabaaaabaaaaaapgapbaiaebaaaaaaaaaaaaaa
egacbaaaaaaaaaaaaaaaaaahbcaabaaaabaaaaaaakaabaaaabaaaaaaakaabaaa
abaaaaaadcaaaaalhcaabaaaaaaaaaaaegacbaaaaaaaaaaaagaabaiaebaaaaaa
abaaaaaapgapbaiaebaaaaaaaaaaaaaadiaaaaaihcaabaaaabaaaaaafgbfbaaa
aaaaaaaaegiccaaaadaaaaaaanaaaaaadcaaaaakhcaabaaaabaaaaaaegiccaaa
adaaaaaaamaaaaaaagbabaaaaaaaaaaaegacbaaaabaaaaaadcaaaaakhcaabaaa
abaaaaaaegiccaaaadaaaaaaaoaaaaaakgbkbaaaaaaaaaaaegacbaaaabaaaaaa
dcaaaaakhcaabaaaabaaaaaaegiccaaaadaaaaaaapaaaaaapgbpbaaaaaaaaaaa
egacbaaaabaaaaaaaaaaaaajhcaabaaaabaaaaaaegacbaiaebaaaaaaabaaaaaa
egiccaaaabaaaaaaaeaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaaabaaaaaa
egacbaaaabaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaah
hcaabaaaabaaaaaapgapbaaaaaaaaaaaegacbaaaabaaaaaabaaaaaahbcaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaacpaaaaafbcaabaaaaaaaaaaa
akaabaaaaaaaaaaadiaaaaaiccaabaaaaaaaaaaaakaabaaaaaaaaaaadkiacaaa
aaaaaaaaagaaaaaadiaaaaaibcaabaaaaaaaaaaaakaabaaaaaaaaaaabkiacaaa
aaaaaaaaahaaaaaabjaaaaafbcaabaaaaaaaaaaaakaabaaaaaaaaaaadiaaaaai
iccabaaaabaaaaaaakaabaaaaaaaaaaaakiacaaaaaaaaaaaahaaaaaabjaaaaaf
bcaabaaaaaaaaaaabkaabaaaaaaaaaaadiaaaaaieccabaaaabaaaaaaakaabaaa
aaaaaaaackiacaaaaaaaaaaaagaaaaaadcaaaaaldccabaaaabaaaaaaegbabaaa
abaaaaaaegiacaaaaaaaaaaaajaaaaaaogikcaaaaaaaaaaaajaaaaaadbaaaaai
bcaabaaaaaaaaaaaabeaaaaaaaaaaaaadkiacaaaaaaaaaaaahaaaaaaaaaaaaaj
ccaabaaaaaaaaaaadkiacaaaaaaaaaaaahaaaaaackiacaaaaaaaaaaaahaaaaaa
aaaaaaajecaabaaaaaaaaaaackiacaaaaaaaaaaaahaaaaaaakiacaaaabaaaaaa
aaaaaaaadhaaaaajbcaabaaaaaaaaaaaakaabaaaaaaaaaaabkaabaaaaaaaaaaa
ckaabaaaaaaaaaaadiaaaaaibcaabaaaaaaaaaaaakaabaaaaaaaaaaaakiacaaa
aaaaaaaaaiaaaaaadiaaaaaigcaabaaaaaaaaaaaagaabaaaaaaaaaaakgilcaaa
aaaaaaaaaeaaaaaadiaaaaaijcaabaaaaaaaaaaaagaabaaaaaaaaaaakgiocaaa
aaaaaaaaafaaaaaadcaaaaakmccabaaaacaaaaaaagbebaaaabaaaaaafgifcaaa
aaaaaaaaafaaaaaaagambaaaaaaaaaaadcaaaaakdccabaaaacaaaaaaegbabaaa
abaaaaaafgifcaaaaaaaaaaaaeaaaaaajgafbaaaaaaaaaaadcaaaaaopcaabaaa
aaaaaaaaegiocaaaaeaaaaaaaeaaaaaaaceaaaaaaaaaaaeaaaaaaaeaaaaaaaea
aaaaaaeaegiocaaaaaaaaaaaabaaaaaaaaaaaaakpcaabaaaaaaaaaaaegaobaaa
aaaaaaaaaceaaaaaaaaaialpaaaaialpaaaaialpaaaaialpdcaaaaanpccabaaa
adaaaaaafgifcaaaaaaaaaaaaiaaaaaaegaobaaaaaaaaaaaaceaaaaaaaaaiadp
aaaaiadpaaaaiadpaaaaiadpdoaaaaab"
}
SubProgram "d3d11_9x " {
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
ConstBuffer "$Globals" 160
Vector 16 [_LightColor0]
Float 68 [_FXScale1]
Float 72 [_FXScrollX1]
Float 76 [_FXScrollY1]
Float 84 [_FXScale2]
Float 88 [_FXScrollX2]
Float 92 [_FXScrollY2]
Float 104 [_SpecIntensityR]
Float 108 [_SpecShininessR]
Float 112 [_SpecIntensityG]
Float 116 [_SpecShininessG]
Float 120 [_Seed]
Float 124 [_ShaderTime]
Float 128 [_TimeScale]
Float 132 [_LightingBlend]
Vector 144 [_MainTex_ST]
ConstBuffer "UnityPerCamera" 128
Vector 0 [_Time]
Vector 64 [_WorldSpaceCameraPos] 3
ConstBuffer "UnityLighting" 720
Vector 0 [_WorldSpaceLightPos0]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
ConstBuffer "UnityPerFrame" 208
Vector 64 [glstate_lightmodel_ambient]
BindCB  "$Globals" 0
BindCB  "UnityPerCamera" 1
BindCB  "UnityLighting" 2
BindCB  "UnityPerDraw" 3
BindCB  "UnityPerFrame" 4
"vs_4_0_level_9_1
eefiecednlckooafnnddpfhcbmoalkdekbdemgppabaaaaaanialaaaaaeaaaaaa
daaaaaaapeadaaaakeakaaaadealaaaaebgpgodjlmadaaaalmadaaaaaaacpopp
deadaaaaiiaaaaaaaiaaceaaaaaaieaaaaaaieaaaaaaceaaabaaieaaaaaaabaa
abaaabaaaaaaaaaaaaaaaeaaagaaacaaaaaaaaaaabaaaaaaabaaaiaaaaaaaaaa
abaaaeaaabaaajaaaaaaaaaaacaaaaaaabaaakaaaaaaaaaaadaaaaaaaeaaalaa
aaaaaaaaadaaamaaahaaapaaaaaaaaaaaeaaaeaaabaabgaaaaaaaaaaaaaaaaaa
aaacpoppfbaaaaafbhaaapkaaaaaaaaaaaaaaaeaaaaaialpaaaaiadpbpaaaaac
afaaaaiaaaaaapjabpaaaaacafaaabiaabaaapjabpaaaaacafaaadiaadaaapja
aiaaaaadaaaaabiaadaaoejabdaaoekaaiaaaaadaaaaaciaadaaoejabeaaoeka
aiaaaaadaaaaaeiaadaaoejabfaaoekaceaaaaacabaaahiaaaaaoeiaaiaaaaad
aaaaabiaakaaoekaakaaoekaahaaaaacaaaaabiaaaaaaaiaafaaaaadaaaaabia
aaaaaaiaakaaaakaaiaaaaadaaaaaciaaaaaaaibabaaoeiaacaaaaadaaaaacia
aaaaffiaaaaaffiaaeaaaaaeaaaaahiaabaaoeiaaaaaffibaaaaaaibafaaaaad
abaaahiaaaaaffjabaaaoekaaeaaaaaeabaaahiaapaaoekaaaaaaajaabaaoeia
aeaaaaaeabaaahiabbaaoekaaaaakkjaabaaoeiaaeaaaaaeabaaahiabcaaoeka
aaaappjaabaaoeiaacaaaaadabaaahiaabaaoeibajaaoekaceaaaaacacaaahia
abaaoeiaaiaaaaadaaaaabiaaaaaoeiaacaaoeiaapaaaaacaaaaabiaaaaaaaia
afaaaaadaaaaaciaaaaaaaiaaeaappkaafaaaaadaaaaabiaaaaaaaiaafaaffka
aoaaaaacaaaaabiaaaaaaaiaafaaaaadaaaaaeoaaaaaaaiaafaaaakaaoaaaaac
aaaaabiaaaaaffiaafaaaaadaaaaaioaaaaaaaiaaeaakkkaaeaaaaaeaaaaadoa
abaaoejaahaaoekaahaaookaabaaaaacaaaaaliabhaaoekaamaaaaadaaaaabia
aaaaaaiaafaappkaacaaaaadaaaaaeiaafaappkaafaakkkaabaaaaacabaaaeia
afaakkkaacaaaaadabaaabiaabaakkiaaiaaaakabcaaaaaeacaaabiaaaaaaaia
aaaakkiaabaaaaiaafaaaaadaaaaabiaacaaaaiaagaaaakaafaaaaadabaaadia
aaaaaaiaacaaookaafaaaaadaaaaafiaaaaaaaiaadaapgkaaeaaaaaeabaaamoa
abaaeejaadaaffkaaaaaieiaaeaaaaaeabaaadoaabaaoejaacaaffkaabaaoeia
abaaaaacabaaapiabgaaoekaaeaaaaaeabaaapiaabaaoeiaaaaaffiaabaaoeka
acaaaaadabaaapiaabaaoeiabhaakkkaaeaaaaaeacaaapoaagaaffkaabaaoeia
aaaappiaafaaaaadaaaaapiaaaaaffjaamaaoekaaeaaaaaeaaaaapiaalaaoeka
aaaaaajaaaaaoeiaaeaaaaaeaaaaapiaanaaoekaaaaakkjaaaaaoeiaaeaaaaae
aaaaapiaaoaaoekaaaaappjaaaaaoeiaaeaaaaaeaaaaadmaaaaappiaaaaaoeka
aaaaoeiaabaaaaacaaaaammaaaaaoeiappppaaaafdeieefckiagaaaaeaaaabaa
kkabaaaafjaaaaaeegiocaaaaaaaaaaaakaaaaaafjaaaaaeegiocaaaabaaaaaa
afaaaaaafjaaaaaeegiocaaaacaaaaaaabaaaaaafjaaaaaeegiocaaaadaaaaaa
bdaaaaaafjaaaaaeegiocaaaaeaaaaaaafaaaaaafpaaaaadpcbabaaaaaaaaaaa
fpaaaaaddcbabaaaabaaaaaafpaaaaadhcbabaaaadaaaaaaghaaaaaepccabaaa
aaaaaaaaabaaaaaagfaaaaaddccabaaaabaaaaaagfaaaaadmccabaaaabaaaaaa
gfaaaaadpccabaaaacaaaaaagfaaaaadpccabaaaadaaaaaagiaaaaacacaaaaaa
diaaaaaipcaabaaaaaaaaaaafgbfbaaaaaaaaaaaegiocaaaadaaaaaaabaaaaaa
dcaaaaakpcaabaaaaaaaaaaaegiocaaaadaaaaaaaaaaaaaaagbabaaaaaaaaaaa
egaobaaaaaaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaadaaaaaaacaaaaaa
kgbkbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpccabaaaaaaaaaaaegiocaaa
adaaaaaaadaaaaaapgbpbaaaaaaaaaaaegaobaaaaaaaaaaabaaaaaaibcaabaaa
aaaaaaaaegbcbaaaadaaaaaaegiccaaaadaaaaaabaaaaaaabaaaaaaiccaabaaa
aaaaaaaaegbcbaaaadaaaaaaegiccaaaadaaaaaabbaaaaaabaaaaaaiecaabaaa
aaaaaaaaegbcbaaaadaaaaaaegiccaaaadaaaaaabcaaaaaabaaaaaahicaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaa
dkaabaaaaaaaaaaadiaaaaahhcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaa
aaaaaaaabaaaaaajicaabaaaaaaaaaaaegiccaaaacaaaaaaaaaaaaaaegiccaaa
acaaaaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaai
icaabaaaaaaaaaaadkaabaaaaaaaaaaaakiacaaaacaaaaaaaaaaaaaabaaaaaai
bcaabaaaabaaaaaapgapbaiaebaaaaaaaaaaaaaaegacbaaaaaaaaaaaaaaaaaah
bcaabaaaabaaaaaaakaabaaaabaaaaaaakaabaaaabaaaaaadcaaaaalhcaabaaa
aaaaaaaaegacbaaaaaaaaaaaagaabaiaebaaaaaaabaaaaaapgapbaiaebaaaaaa
aaaaaaaadiaaaaaihcaabaaaabaaaaaafgbfbaaaaaaaaaaaegiccaaaadaaaaaa
anaaaaaadcaaaaakhcaabaaaabaaaaaaegiccaaaadaaaaaaamaaaaaaagbabaaa
aaaaaaaaegacbaaaabaaaaaadcaaaaakhcaabaaaabaaaaaaegiccaaaadaaaaaa
aoaaaaaakgbkbaaaaaaaaaaaegacbaaaabaaaaaadcaaaaakhcaabaaaabaaaaaa
egiccaaaadaaaaaaapaaaaaapgbpbaaaaaaaaaaaegacbaaaabaaaaaaaaaaaaaj
hcaabaaaabaaaaaaegacbaiaebaaaaaaabaaaaaaegiccaaaabaaaaaaaeaaaaaa
baaaaaahicaabaaaaaaaaaaaegacbaaaabaaaaaaegacbaaaabaaaaaaeeaaaaaf
icaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaaabaaaaaapgapbaaa
aaaaaaaaegacbaaaabaaaaaabaaaaaahbcaabaaaaaaaaaaaegacbaaaaaaaaaaa
egacbaaaabaaaaaacpaaaaafbcaabaaaaaaaaaaaakaabaaaaaaaaaaadiaaaaai
ccaabaaaaaaaaaaaakaabaaaaaaaaaaadkiacaaaaaaaaaaaagaaaaaadiaaaaai
bcaabaaaaaaaaaaaakaabaaaaaaaaaaabkiacaaaaaaaaaaaahaaaaaabjaaaaaf
bcaabaaaaaaaaaaaakaabaaaaaaaaaaadiaaaaaiiccabaaaabaaaaaaakaabaaa
aaaaaaaaakiacaaaaaaaaaaaahaaaaaabjaaaaafbcaabaaaaaaaaaaabkaabaaa
aaaaaaaadiaaaaaieccabaaaabaaaaaaakaabaaaaaaaaaaackiacaaaaaaaaaaa
agaaaaaadcaaaaaldccabaaaabaaaaaaegbabaaaabaaaaaaegiacaaaaaaaaaaa
ajaaaaaaogikcaaaaaaaaaaaajaaaaaadbaaaaaibcaabaaaaaaaaaaaabeaaaaa
aaaaaaaadkiacaaaaaaaaaaaahaaaaaaaaaaaaajccaabaaaaaaaaaaadkiacaaa
aaaaaaaaahaaaaaackiacaaaaaaaaaaaahaaaaaaaaaaaaajecaabaaaaaaaaaaa
ckiacaaaaaaaaaaaahaaaaaaakiacaaaabaaaaaaaaaaaaaadhaaaaajbcaabaaa
aaaaaaaaakaabaaaaaaaaaaabkaabaaaaaaaaaaackaabaaaaaaaaaaadiaaaaai
bcaabaaaaaaaaaaaakaabaaaaaaaaaaaakiacaaaaaaaaaaaaiaaaaaadiaaaaai
gcaabaaaaaaaaaaaagaabaaaaaaaaaaakgilcaaaaaaaaaaaaeaaaaaadiaaaaai
jcaabaaaaaaaaaaaagaabaaaaaaaaaaakgiocaaaaaaaaaaaafaaaaaadcaaaaak
mccabaaaacaaaaaaagbebaaaabaaaaaafgifcaaaaaaaaaaaafaaaaaaagambaaa
aaaaaaaadcaaaaakdccabaaaacaaaaaaegbabaaaabaaaaaafgifcaaaaaaaaaaa
aeaaaaaajgafbaaaaaaaaaaadcaaaaaopcaabaaaaaaaaaaaegiocaaaaeaaaaaa
aeaaaaaaaceaaaaaaaaaaaeaaaaaaaeaaaaaaaeaaaaaaaeaegiocaaaaaaaaaaa
abaaaaaaaaaaaaakpcaabaaaaaaaaaaaegaobaaaaaaaaaaaaceaaaaaaaaaialp
aaaaialpaaaaialpaaaaialpdcaaaaanpccabaaaadaaaaaafgifcaaaaaaaaaaa
aiaaaaaaegaobaaaaaaaaaaaaceaaaaaaaaaiadpaaaaiadpaaaaiadpaaaaiadp
doaaaaabejfdeheoiiaaaaaaaeaaaaaaaiaaaaaagiaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapapaaaahbaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaa
adadaaaahkaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaaapaaaaaaiaaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaadaaaaaaahahaaaafaepfdejfeejepeoaafeeffi
edepepfceeaaedepemepfcaaeoepfcenebemaaklepfdeheojmaaaaaaafaaaaaa
aiaaaaaaiaaaaaaaaaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaimaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadamaaaaimaaaaaaabaaaaaaaaaaaaaa
adaaaaaaabaaaaaaamadaaaaimaaaaaaacaaaaaaaaaaaaaaadaaaaaaacaaaaaa
apaaaaaajfaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapaaaaaafdfgfpfa
gphdgjhegjgpgoaafeeffiedepepfceeaaedepemepfcaakl"
}
}
Program "fp" {
SubProgram "opengl " {
Vector 0 [_FxColor1]
Float 1 [_FXIntensity1]
Float 2 [_FXIntensity2]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_MaskTex] 2D 1
SetTexture 2 [_FxTex1] 2D 2
SetTexture 3 [_FxTex2] 2D 3
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
OPTION ARB_precision_hint_fastest;
PARAM c[4] = { program.local[0..2],
		{ 0.30000001, 0.58999997, 0.11 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEX R0, fragment.texcoord[2], texture[2], 2D;
TEX R1, fragment.texcoord[2].zwzw, texture[3], 2D;
TEX R3.xyz, fragment.texcoord[0], texture[1], 2D;
TEX R2, fragment.texcoord[0], texture[0], 2D;
MUL R1, R1, c[2].x;
MUL R0, R0, c[1].x;
MUL R0, R0, R1;
MUL R1.x, R3.y, fragment.texcoord[1].y;
DP3 R1.y, R2, c[3];
MAD R1.x, R3, fragment.texcoord[1], R1;
MUL R3.x, R1, R1.y;
MAD R1, R2, R3.x, R2;
MUL R0, R0, R3.z;
MAD R2, R0, c[0], R2;
ADD R2, R2, -R1;
MAD R1, R3.z, R2, R1;
MAD R1, R1, R3.x, R1;
MAD R0, R0, c[0], R1;
MUL result.color, R0, fragment.color.primary;
END
# 19 instructions, 4 R-regs
"
}
SubProgram "d3d9 " {
Vector 0 [_FxColor1]
Float 1 [_FXIntensity1]
Float 2 [_FXIntensity2]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_MaskTex] 2D 1
SetTexture 2 [_FxTex1] 2D 2
SetTexture 3 [_FxTex2] 2D 3
"ps_2_0
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
def c3, 0.30000001, 0.58999997, 0.11000000, 0
dcl t0.xy
dcl t1.xy
dcl t2
dcl v0
texld r1, t0, s0
texld r3, t0, s1
texld r2, t2, s2
mul r4.x, r3.y, t1.y
mad r3.x, r3, t1, r4
mov r0.y, t2.w
mov r0.x, t2.z
mul r2, r2, c1.x
texld r0, r0, s3
mul r0, r0, c2.x
mul_pp r2, r2, r0
dp3 r0.x, r1, c3
mul r0.x, r3, r0
mad_pp r4, r1, r0.x, r1
mul_pp r2, r2, r3.z
mad_pp r1, r2, c0, r1
add_pp r1, r1, -r4
mad_pp r1, r3.z, r1, r4
mad_pp r0, r1, r0.x, r1
mad_pp r0, r2, c0, r0
mul_pp r0, r0, v0
mov_pp oC0, r0
"
}
SubProgram "d3d11 " {
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_MaskTex] 2D 1
SetTexture 2 [_FxTex1] 2D 2
SetTexture 3 [_FxTex2] 2D 3
ConstBuffer "$Globals" 160
Vector 48 [_FxColor1]
Float 64 [_FXIntensity1]
Float 80 [_FXIntensity2]
BindCB  "$Globals" 0
"ps_4_0
eefiecednfmjlncipdfjmnkahjfifbbmgkffpjkcabaaaaaadmaeaaaaadaaaaaa
cmaaaaaanaaaaaaaaeabaaaaejfdeheojmaaaaaaafaaaaaaaiaaaaaaiaaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaimaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaaimaaaaaaabaaaaaaaaaaaaaaadaaaaaaabaaaaaa
amamaaaaimaaaaaaacaaaaaaaaaaaaaaadaaaaaaacaaaaaaapapaaaajfaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapapaaaafdfgfpfagphdgjhegjgpgoaa
feeffiedepepfceeaaedepemepfcaaklepfdeheocmaaaaaaabaaaaaaaiaaaaaa
caaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgf
heaaklklfdeieefcdaadaaaaeaaaaaaammaaaaaafjaaaaaeegiocaaaaaaaaaaa
agaaaaaafkaaaaadaagabaaaaaaaaaaafkaaaaadaagabaaaabaaaaaafkaaaaad
aagabaaaacaaaaaafkaaaaadaagabaaaadaaaaaafibiaaaeaahabaaaaaaaaaaa
ffffaaaafibiaaaeaahabaaaabaaaaaaffffaaaafibiaaaeaahabaaaacaaaaaa
ffffaaaafibiaaaeaahabaaaadaaaaaaffffaaaagcbaaaaddcbabaaaabaaaaaa
gcbaaaadmcbabaaaabaaaaaagcbaaaadpcbabaaaacaaaaaagcbaaaadpcbabaaa
adaaaaaagfaaaaadpccabaaaaaaaaaaagiaaaaacaeaaaaaaefaaaaajpcaabaaa
aaaaaaaaegbabaaaacaaaaaaeghobaaaacaaaaaaaagabaaaacaaaaaadiaaaaai
pcaabaaaaaaaaaaaegaobaaaaaaaaaaaagiacaaaaaaaaaaaaeaaaaaaefaaaaaj
pcaabaaaabaaaaaaogbkbaaaacaaaaaaeghobaaaadaaaaaaaagabaaaadaaaaaa
diaaaaaipcaabaaaabaaaaaaegaobaaaabaaaaaaagiacaaaaaaaaaaaafaaaaaa
diaaaaahpcaabaaaaaaaaaaaegaobaaaaaaaaaaaegaobaaaabaaaaaaefaaaaaj
pcaabaaaabaaaaaaegbabaaaabaaaaaaeghobaaaabaaaaaaaagabaaaabaaaaaa
diaaaaahpcaabaaaaaaaaaaaegaobaaaaaaaaaaakgakbaaaabaaaaaaefaaaaaj
pcaabaaaacaaaaaaegbabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
dcaaaaakpcaabaaaadaaaaaaegaobaaaaaaaaaaaegiocaaaaaaaaaaaadaaaaaa
egaobaaaacaaaaaaapaaaaahbcaabaaaabaaaaaaogbkbaaaabaaaaaaegaabaaa
abaaaaaabaaaaaakccaabaaaabaaaaaaaceaaaaajkjjjjdodnakbhdpkoehobdn
aaaaaaaaegacbaaaacaaaaaadiaaaaahbcaabaaaabaaaaaabkaabaaaabaaaaaa
akaabaaaabaaaaaadcaaaaajpcaabaaaacaaaaaaegaobaaaacaaaaaaagaabaaa
abaaaaaaegaobaaaacaaaaaaaaaaaaaipcaabaaaadaaaaaaegaobaiaebaaaaaa
acaaaaaaegaobaaaadaaaaaadcaaaaajpcaabaaaacaaaaaakgakbaaaabaaaaaa
egaobaaaadaaaaaaegaobaaaacaaaaaadcaaaaajpcaabaaaabaaaaaaegaobaaa
acaaaaaaagaabaaaabaaaaaaegaobaaaacaaaaaadcaaaaakpcaabaaaaaaaaaaa
egaobaaaaaaaaaaaegiocaaaaaaaaaaaadaaaaaaegaobaaaabaaaaaadiaaaaah
pccabaaaaaaaaaaaegaobaaaaaaaaaaaegbobaaaadaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_MaskTex] 2D 1
SetTexture 2 [_FxTex1] 2D 2
SetTexture 3 [_FxTex2] 2D 3
ConstBuffer "$Globals" 160
Vector 48 [_FxColor1]
Float 64 [_FXIntensity1]
Float 80 [_FXIntensity2]
BindCB  "$Globals" 0
"ps_4_0_level_9_1
eefiecedigbokbmgmolajpdnklmcmjigblmlanipabaaaaaafiagaaaaaeaaaaaa
daaaaaaaeiacaaaaiaafaaaaceagaaaaebgpgodjbaacaaaabaacaaaaaaacpppp
naabaaaaeaaaaaaaabaadeaaaaaaeaaaaaaaeaaaaeaaceaaaaaaeaaaaaaaaaaa
abababaaacacacaaadadadaaaaaaadaaadaaaaaaaaaaaaaaaaacppppfbaaaaaf
adaaapkajkjjjjdodnakbhdpkoehobdnaaaaaaaabpaaaaacaaaaaaiaaaaaapla
bpaaaaacaaaaaaiaabaaaplabpaaaaacaaaaaaiaacaacplabpaaaaacaaaaaaja
aaaiapkabpaaaaacaaaaaajaabaiapkabpaaaaacaaaaaajaacaiapkabpaaaaac
aaaaaajaadaiapkaabaaaaacaaaaabiaabaakklaabaaaaacaaaaaciaabaappla
ecaaaaadabaacpiaaaaaoelaabaioekaecaaaaadacaacpiaaaaaoelaaaaioeka
ecaaaaadaaaaapiaaaaaoeiaadaioekaecaaaaadadaaapiaabaaoelaacaioeka
afaaaaadabaaaciaabaaffiaaaaakklaaeaaaaaeabaaabiaaaaapplaabaaaaia
abaaffiaaiaaaaadabaaaciaadaaoekaacaaoeiaafaaaaadabaacbiaabaaffia
abaaaaiaaeaaaaaeaeaacpiaacaaoeiaabaaaaiaacaaoeiaafaaaaadaaaacpia
aaaaoeiaacaaaakaafaaaaadadaacpiaadaaoeiaabaaaakaafaaaaadaaaacpia
aaaaoeiaadaaoeiaafaaaaadaaaacpiaabaakkiaaaaaoeiaaeaaaaaeacaacpia
aaaaoeiaaaaaoekaacaaoeiabcaaaaaeadaacpiaabaakkiaacaaoeiaaeaaoeia
aeaaaaaeabaacpiaadaaoeiaabaaaaiaadaaoeiaaeaaaaaeaaaacpiaaaaaoeia
aaaaoekaabaaoeiaafaaaaadaaaacpiaaaaaoeiaacaaoelaabaaaaacaaaicpia
aaaaoeiappppaaaafdeieefcdaadaaaaeaaaaaaammaaaaaafjaaaaaeegiocaaa
aaaaaaaaagaaaaaafkaaaaadaagabaaaaaaaaaaafkaaaaadaagabaaaabaaaaaa
fkaaaaadaagabaaaacaaaaaafkaaaaadaagabaaaadaaaaaafibiaaaeaahabaaa
aaaaaaaaffffaaaafibiaaaeaahabaaaabaaaaaaffffaaaafibiaaaeaahabaaa
acaaaaaaffffaaaafibiaaaeaahabaaaadaaaaaaffffaaaagcbaaaaddcbabaaa
abaaaaaagcbaaaadmcbabaaaabaaaaaagcbaaaadpcbabaaaacaaaaaagcbaaaad
pcbabaaaadaaaaaagfaaaaadpccabaaaaaaaaaaagiaaaaacaeaaaaaaefaaaaaj
pcaabaaaaaaaaaaaegbabaaaacaaaaaaeghobaaaacaaaaaaaagabaaaacaaaaaa
diaaaaaipcaabaaaaaaaaaaaegaobaaaaaaaaaaaagiacaaaaaaaaaaaaeaaaaaa
efaaaaajpcaabaaaabaaaaaaogbkbaaaacaaaaaaeghobaaaadaaaaaaaagabaaa
adaaaaaadiaaaaaipcaabaaaabaaaaaaegaobaaaabaaaaaaagiacaaaaaaaaaaa
afaaaaaadiaaaaahpcaabaaaaaaaaaaaegaobaaaaaaaaaaaegaobaaaabaaaaaa
efaaaaajpcaabaaaabaaaaaaegbabaaaabaaaaaaeghobaaaabaaaaaaaagabaaa
abaaaaaadiaaaaahpcaabaaaaaaaaaaaegaobaaaaaaaaaaakgakbaaaabaaaaaa
efaaaaajpcaabaaaacaaaaaaegbabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaa
aaaaaaaadcaaaaakpcaabaaaadaaaaaaegaobaaaaaaaaaaaegiocaaaaaaaaaaa
adaaaaaaegaobaaaacaaaaaaapaaaaahbcaabaaaabaaaaaaogbkbaaaabaaaaaa
egaabaaaabaaaaaabaaaaaakccaabaaaabaaaaaaaceaaaaajkjjjjdodnakbhdp
koehobdnaaaaaaaaegacbaaaacaaaaaadiaaaaahbcaabaaaabaaaaaabkaabaaa
abaaaaaaakaabaaaabaaaaaadcaaaaajpcaabaaaacaaaaaaegaobaaaacaaaaaa
agaabaaaabaaaaaaegaobaaaacaaaaaaaaaaaaaipcaabaaaadaaaaaaegaobaia
ebaaaaaaacaaaaaaegaobaaaadaaaaaadcaaaaajpcaabaaaacaaaaaakgakbaaa
abaaaaaaegaobaaaadaaaaaaegaobaaaacaaaaaadcaaaaajpcaabaaaabaaaaaa
egaobaaaacaaaaaaagaabaaaabaaaaaaegaobaaaacaaaaaadcaaaaakpcaabaaa
aaaaaaaaegaobaaaaaaaaaaaegiocaaaaaaaaaaaadaaaaaaegaobaaaabaaaaaa
diaaaaahpccabaaaaaaaaaaaegaobaaaaaaaaaaaegbobaaaadaaaaaadoaaaaab
ejfdeheojmaaaaaaafaaaaaaaiaaaaaaiaaaaaaaaaaaaaaaabaaaaaaadaaaaaa
aaaaaaaaapaaaaaaimaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadadaaaa
imaaaaaaabaaaaaaaaaaaaaaadaaaaaaabaaaaaaamamaaaaimaaaaaaacaaaaaa
aaaaaaaaadaaaaaaacaaaaaaapapaaaajfaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
adaaaaaaapapaaaafdfgfpfagphdgjhegjgpgoaafeeffiedepepfceeaaedepem
epfcaaklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklkl"
}
}
 }
}
Fallback "Diffuse"
}
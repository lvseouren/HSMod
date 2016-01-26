Shader "Custom/CardBack/Birthday" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _MaskTex ("Mask (RGB)", 2D) = "white" {}
 _FX1Tex ("FX1 (RGB)", 2D) = "white" {}
 _FX2Tex ("FX2 (RGB)", 2D) = "white" {}
 _FX3Tex ("FX3 (RGB)", 2D) = "white" {}
 _SpecularIntensity ("Specular Intensity", Float) = 1
 _SpecularColorR ("Specular Color (R)", Color) = (1,1,1,1)
 _SpecularIntensityR ("Specular Intensity (R)", Float) = 2
 _ShininessR ("Shininess (R)", Float) = 10
 _SpecularColorG ("Specular Color (G)", Color) = (1,1,1,1)
 _SpecularIntensityG ("Specular Intensity (G)", Float) = 2
 _ShininessG ("Shininess (G)", Float) = 10
 _FxColor ("Monocolor Sparkle Color (B)", Color) = (1,1,1,1)
 _FxIntensity ("Sparkle Intensity", Float) = 1
 _ScrollX1 ("Sparkle1 Scroll X Speed", Float) = 0
 _ScrollY1 ("Sparkle1 Scroll Y Speed", Float) = 0
 _Scale1 ("Multicolor Sparkle Scale", Float) = 1
 _ScrollX2 ("Sparkle2 Scroll X Speed", Float) = 0
 _ScrollY2 ("Sparkle2 Scroll Y Speed", Float) = 0
 _Scale2 ("Monocolor Sparkle Scale", Float) = 1
 _ScrollX3 ("Multicolor Sparkle Scroll X Speed", Float) = 0
 _ScrollY3 ("Multicolor Scroll Y Speed", Float) = 0
 _Scale3 ("Color FX Scale", Float) = 1
 _LightingBlend ("Ambient Lighting Blend", Float) = 0
 _Seed ("Seed", Float) = 0
 _TimeScale ("Time Scale", Float) = 1
}
SubShader { 
 Tags { "RenderType"="Opaque" "Highlight"="true" }
 Pass {
  Tags { "RenderType"="Opaque" "Highlight"="true" }
  BindChannels {
   Bind "color", Color
   Bind "texcoord", TexCoord
  }
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
Float 18 [_SpecularIntensity]
Float 19 [_ShininessR]
Float 20 [_SpecularIntensityR]
Float 21 [_ShininessG]
Float 22 [_SpecularIntensityG]
Float 23 [_ScrollX1]
Float 24 [_ScrollY1]
Float 25 [_Scale1]
Float 26 [_ScrollX2]
Float 27 [_ScrollY2]
Float 28 [_Scale2]
Float 29 [_ScrollX3]
Float 30 [_ScrollY3]
Float 31 [_Scale3]
Float 32 [_LightingBlend]
Float 33 [_Seed]
Float 34 [_ShaderTime]
Float 35 [_TimeScale]
Vector 36 [_MainTex_ST]
"!!ARBvp1.0
PARAM c[38] = { { 0, 0.69999999, 0.89990234, 2 },
		state.lightmodel.ambient,
		program.local[2..12],
		state.matrix.mvp,
		program.local[17..36],
		{ 1 } };
TEMP R0;
TEMP R1;
TEMP R2;
MUL R0.xyz, vertex.normal.y, c[10];
MAD R2.xyz, vertex.normal.x, c[9], R0;
MOV R1.xyz, c[3];
MOV R1.w, c[37].x;
DP4 R0.w, vertex.position, c[8];
DP4 R0.z, vertex.position, c[7];
DP4 R0.x, vertex.position, c[5];
DP4 R0.y, vertex.position, c[6];
ADD R0, R1, -R0;
MAD R1.xyz, vertex.normal.z, c[11], R2;
ADD R2.xyz, R1, c[0].x;
DP4 R0.w, R0, R0;
DP3 R1.y, R2, R2;
RSQ R1.w, R1.y;
RSQ R0.w, R0.w;
DP3 R1.x, c[4], c[4];
RSQ R1.x, R1.x;
MUL R0.xyz, R0.w, R0;
MUL R2.xyz, R1.w, R2;
MUL R1.xyz, R1.x, c[4];
DP3 R1.w, R2, -R1;
MUL R2.xyz, R2, R1.w;
MAD R1.xyz, -R2, c[0].w, -R1;
DP3 R0.z, R1, R0;
POW R0.x, R0.z, c[21].x;
MUL R0.y, R0.x, c[22].x;
MOV R0.x, c[34];
MOV R1.x, c[0].w;
MUL R1, R1.x, c[1];
ADD R1, R1, c[17];
POW R0.w, R0.z, c[19].x;
SLT R0.x, c[0], R0;
MUL R0.z, R0.x, c[34].x;
ABS R0.x, R0;
MAX R0.y, R0, c[0].x;
ADD R0.z, R0, c[33].x;
SGE R0.x, c[0], R0;
MAD R0.x, R0, c[2], R0.z;
MUL R2.x, R0, c[35];
MUL R0.z, R0.w, c[20].x;
MAX R0.x, R0.z, c[0];
MAD R0.z, R2.x, c[29].x, vertex.texcoord[0].x;
MAD R0.w, R2.x, c[30].x, vertex.texcoord[0].y;
MUL R0.zw, R0, c[31].x;
MUL result.texcoord[3], R0, c[18].x;
MUL R0.y, R2.x, c[24].x;
MOV R0.x, c[37];
ADD R1, R1, -c[37].x;
MAD result.color, R1, c[32].x, R0.x;
MUL R0.x, R2, c[23];
MAD R0.y, R0, c[0], vertex.texcoord[0];
MAD R0.x, R0, c[0].y, vertex.texcoord[0];
MUL result.texcoord[1].zw, R0.xyxy, c[28].x;
MAD R0.y, R2.x, c[24].x, vertex.texcoord[0];
MAD R0.x, R2, c[23], vertex.texcoord[0];
MUL result.texcoord[1].xy, R0, c[25].x;
MUL R0.y, R2.x, c[27].x;
MUL R0.x, R2, c[26];
MAD R0.y, R0, c[0], vertex.texcoord[0];
MAD R0.x, R0, c[0].y, vertex.texcoord[0];
MUL R0.zw, R0.xyxy, c[28].x;
MAD R0.y, R2.x, c[27].x, vertex.texcoord[0];
MAD R0.x, R2, c[26], vertex.texcoord[0];
MUL R0.xy, R0, c[25].x;
MUL result.texcoord[2].zw, R0, c[0].z;
MUL result.texcoord[2].xy, R0, c[0].z;
MAD result.texcoord[0].xy, vertex.texcoord[0], c[36], c[36].zwzw;
DP4 result.position.w, vertex.position, c[16];
DP4 result.position.z, vertex.position, c[15];
DP4 result.position.y, vertex.position, c[14];
DP4 result.position.x, vertex.position, c[13];
END
# 71 instructions, 3 R-regs
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
Float 17 [_SpecularIntensity]
Float 18 [_ShininessR]
Float 19 [_SpecularIntensityR]
Float 20 [_ShininessG]
Float 21 [_SpecularIntensityG]
Float 22 [_ScrollX1]
Float 23 [_ScrollY1]
Float 24 [_Scale1]
Float 25 [_ScrollX2]
Float 26 [_ScrollY2]
Float 27 [_Scale2]
Float 28 [_ScrollX3]
Float 29 [_ScrollY3]
Float 30 [_Scale3]
Float 31 [_LightingBlend]
Float 32 [_Seed]
Float 33 [_ShaderTime]
Float 34 [_TimeScale]
Vector 35 [_MainTex_ST]
"vs_2_0
def c36, 0.00000000, 0.69999999, 0.89990234, 1.00000000
def c37, 2.00000000, -1.00000000, 0, 0
dcl_position0 v0
dcl_texcoord0 v1
dcl_normal0 v2
mul r0.xyz, v2.y, c9
mad r2.xyz, v2.x, c8, r0
mov r1.xyz, c14
mov r1.w, c36
dp4 r0.w, v0, c7
dp4 r0.z, v0, c6
dp4 r0.x, v0, c4
dp4 r0.y, v0, c5
add r0, r1, -r0
mad r1.xyz, v2.z, c10, r2
add r2.xyz, r1, c36.x
dp4 r0.w, r0, r0
dp3 r1.y, r2, r2
rsq r1.w, r1.y
rsq r0.w, r0.w
dp3 r1.x, c15, c15
rsq r1.x, r1.x
mul r2.xyz, r1.w, r2
mul r1.xyz, r1.x, c15
dp3 r1.w, r2, -r1
mul r2.xyz, r2, r1.w
mad r1.xyz, -r2, c37.x, -r1
mul r0.xyz, r0.w, r0
dp3 r1.x, r1, r0
pow r0, r1.x, c20.x
mul r0.y, r0.x, c21.x
mov r0.x, c33
slt r1.z, c36.x, r0.x
max r1.y, r0, c36.x
pow r0, r1.x, c18.x
sge r0.z, c36.x, r1
sge r0.y, r1.z, c36.x
mul r0.y, r0, r0.z
max r0.z, -r1, r1
slt r0.z, c36.x, r0
add r1.x, -r0.z, c36.w
max r0.y, -r0, r0
slt r0.y, c36.x, r0
mov r0.w, c32.x
add r1.z, -r0.y, c36.w
mul r1.x, r1, c32
add r0.w, c33.x, r0
mad r0.z, r0, r0.w, r1.x
mul r0.w, r0.z, r1.z
add r0.z, r0, c13.x
mad r0.y, r0, r0.z, r0.w
mul r2.x, r0.y, c34
mul r0.z, r0.x, c19.x
max r1.x, r0.z, c36
mad r0.y, r2.x, c29.x, v1
mad r0.x, r2, c28, v1
mul r1.zw, r0.xyxy, c30.x
mul oT3, r1, c17.x
mul r0.y, r2.x, c23.x
mul r0.x, r2, c22
mad r0.y, r0, c36, v1
mad r0.x, r0, c36.y, v1
mul oT1.zw, r0.xyxy, c27.x
mul r0.y, r2.x, c26.x
mul r0.x, r2, c25
mad r0.y, r0, c36, v1
mad r0.x, r0, c36.y, v1
mul r0.zw, r0.xyxy, c27.x
mad r0.y, r2.x, c23.x, v1
mad r0.x, r2, c22, v1
mul oT1.xy, r0, c24.x
mul oT2.zw, r0, c36.z
mov r0, c12
mul r0, c37.x, r0
mad r1.y, r2.x, c26.x, v1
mad r1.x, r2, c25, v1
mul r1.xy, r1, c24.x
mul oT2.xy, r1, c36.z
add r1, r0, c16
mov r0.x, c36.w
add r1, r1, c37.y
mad oD0, r1, c31.x, r0.x
mad oT0.xy, v1, c35, c35.zwzw
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
ConstBuffer "$Globals" 208
Vector 16 [_LightColor0]
Float 48 [_SpecularIntensity]
Float 64 [_ShininessR]
Float 68 [_SpecularIntensityR]
Float 92 [_ShininessG]
Float 96 [_SpecularIntensityG]
Float 132 [_ScrollX1]
Float 136 [_ScrollY1]
Float 140 [_Scale1]
Float 144 [_ScrollX2]
Float 148 [_ScrollY2]
Float 152 [_Scale2]
Float 156 [_ScrollX3]
Float 160 [_ScrollY3]
Float 164 [_Scale3]
Float 168 [_LightingBlend]
Float 172 [_Seed]
Float 176 [_ShaderTime]
Float 180 [_TimeScale]
Vector 192 [_MainTex_ST]
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
eefiecedfdpiggbbiekagaefohhjkdmdonhioejpabaaaaaadmakaaaaadaaaaaa
cmaaaaaaneaaaaaajaabaaaaejfdeheokaaaaaaaafaaaaaaaiaaaaaaiaaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaaijaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaaijaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaa
adaaaaaajcaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapaaaaaajiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaeaaaaaaahahaaaafaepfdejfeejepeoaafeeffi
edepepfceeaaedepemepfcaaeoepfcenebemaaklepfdeheoleaaaaaaagaaaaaa
aiaaaaaajiaaaaaaaaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaakeaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadamaaaakeaaaaaaabaaaaaaaaaaaaaa
adaaaaaaacaaaaaaapaaaaaakeaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaa
apaaaaaakeaaaaaaadaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapaaaaaaknaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaafaaaaaaapaaaaaafdfgfpfagphdgjhegjgpgoaa
feeffiedepepfceeaaedepemepfcaaklfdeieefckeaiaaaaeaaaabaacjacaaaa
fjaaaaaeegiocaaaaaaaaaaaanaaaaaafjaaaaaeegiocaaaabaaaaaaafaaaaaa
fjaaaaaeegiocaaaacaaaaaaabaaaaaafjaaaaaeegiocaaaadaaaaaabdaaaaaa
fjaaaaaeegiocaaaaeaaaaaaafaaaaaafpaaaaadpcbabaaaaaaaaaaafpaaaaad
dcbabaaaabaaaaaafpaaaaadhcbabaaaaeaaaaaaghaaaaaepccabaaaaaaaaaaa
abaaaaaagfaaaaaddccabaaaabaaaaaagfaaaaadpccabaaaacaaaaaagfaaaaad
pccabaaaadaaaaaagfaaaaadpccabaaaaeaaaaaagfaaaaadpccabaaaafaaaaaa
giaaaaacaeaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaaaaaaaaaaegiocaaa
adaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaadaaaaaaaaaaaaaa
agbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaa
adaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpccabaaa
aaaaaaaaegiocaaaadaaaaaaadaaaaaapgbpbaaaaaaaaaaaegaobaaaaaaaaaaa
dcaaaaaldccabaaaabaaaaaaegbabaaaabaaaaaaegiacaaaaaaaaaaaamaaaaaa
ogikcaaaaaaaaaaaamaaaaaadbaaaaaibcaabaaaaaaaaaaaabeaaaaaaaaaaaaa
akiacaaaaaaaaaaaalaaaaaaaaaaaaajccaabaaaaaaaaaaadkiacaaaaaaaaaaa
akaaaaaaakiacaaaaaaaaaaaalaaaaaaaaaaaaajecaabaaaaaaaaaaadkiacaaa
aaaaaaaaakaaaaaaakiacaaaabaaaaaaaaaaaaaadhaaaaajbcaabaaaaaaaaaaa
akaabaaaaaaaaaaabkaabaaaaaaaaaaackaabaaaaaaaaaaadiaaaaaibcaabaaa
aaaaaaaaakaabaaaaaaaaaaabkiacaaaaaaaaaaaalaaaaaadiaaaaaigcaabaaa
aaaaaaaaagaabaaaaaaaaaaafgigcaaaaaaaaaaaaiaaaaaadcaaaaamgcaabaaa
aaaaaaaafgagbaaaaaaaaaaaaceaaaaaaaaaaaaadddddddpdddddddpaaaaaaaa
agbbbaaaabaaaaaadiaaaaaimccabaaaacaaaaaafgajbaaaaaaaaaaakgikcaaa
aaaaaaaaajaaaaaadcaaaaakgcaabaaaaaaaaaaafgigcaaaaaaaaaaaaiaaaaaa
agaabaaaaaaaaaaaagbbbaaaabaaaaaadiaaaaaidccabaaaacaaaaaajgafbaaa
aaaaaaaapgipcaaaaaaaaaaaaiaaaaaadiaaaaaigcaabaaaaaaaaaaaagaabaaa
aaaaaaaaagibcaaaaaaaaaaaajaaaaaadcaaaaamgcaabaaaaaaaaaaafgagbaaa
aaaaaaaaaceaaaaaaaaaaaaadddddddpdddddddpaaaaaaaaagbbbaaaabaaaaaa
diaaaaaigcaabaaaaaaaaaaafgagbaaaaaaaaaaakgikcaaaaaaaaaaaajaaaaaa
diaaaaakmccabaaaadaaaaaafgajbaaaaaaaaaaaaceaaaaaaaaaaaaaaaaaaaaa
ggggggdpggggggdpdcaaaaakhcaabaaaabaaaaaaegidcaaaaaaaaaaaajaaaaaa
agaabaaaaaaaaaaaegbabaaaabaaaaaadcaaaaakicaabaaaabaaaaaaakiacaaa
aaaaaaaaakaaaaaaakaabaaaaaaaaaaabkbabaaaabaaaaaadiaaaaaimcaabaaa
aaaaaaaakgaobaaaabaaaaaafgifcaaaaaaaaaaaakaaaaaadiaaaaaidcaabaaa
abaaaaaaegaabaaaabaaaaaapgipcaaaaaaaaaaaaiaaaaaadiaaaaakdccabaaa
adaaaaaaegaabaaaabaaaaaaaceaaaaaggggggdpggggggdpaaaaaaaaaaaaaaaa
baaaaaaibcaabaaaabaaaaaaegbcbaaaaeaaaaaaegiccaaaadaaaaaabaaaaaaa
baaaaaaiccaabaaaabaaaaaaegbcbaaaaeaaaaaaegiccaaaadaaaaaabbaaaaaa
baaaaaaiecaabaaaabaaaaaaegbcbaaaaeaaaaaaegiccaaaadaaaaaabcaaaaaa
baaaaaahicaabaaaabaaaaaaegacbaaaabaaaaaaegacbaaaabaaaaaaeeaaaaaf
icaabaaaabaaaaaadkaabaaaabaaaaaadiaaaaahhcaabaaaabaaaaaapgapbaaa
abaaaaaaegacbaaaabaaaaaabaaaaaajicaabaaaabaaaaaaegiccaaaacaaaaaa
aaaaaaaaegiccaaaacaaaaaaaaaaaaaaeeaaaaaficaabaaaabaaaaaadkaabaaa
abaaaaaadiaaaaaihcaabaaaacaaaaaapgapbaaaabaaaaaaegiccaaaacaaaaaa
aaaaaaaabaaaaaaiicaabaaaabaaaaaaegacbaiaebaaaaaaacaaaaaaegacbaaa
abaaaaaaaaaaaaahicaabaaaabaaaaaadkaabaaaabaaaaaadkaabaaaabaaaaaa
dcaaaaalhcaabaaaabaaaaaaegacbaaaabaaaaaapgapbaiaebaaaaaaabaaaaaa
egacbaiaebaaaaaaacaaaaaadiaaaaaipcaabaaaacaaaaaafgbfbaaaaaaaaaaa
egiocaaaadaaaaaaanaaaaaadcaaaaakpcaabaaaacaaaaaaegiocaaaadaaaaaa
amaaaaaaagbabaaaaaaaaaaaegaobaaaacaaaaaadcaaaaakpcaabaaaacaaaaaa
egiocaaaadaaaaaaaoaaaaaakgbkbaaaaaaaaaaaegaobaaaacaaaaaadcaaaaak
pcaabaaaacaaaaaaegiocaaaadaaaaaaapaaaaaapgbpbaaaaaaaaaaaegaobaaa
acaaaaaaaaaaaaaiicaabaaaadaaaaaadkaabaiaebaaaaaaacaaaaaaabeaaaaa
aaaaiadpaaaaaaajhcaabaaaadaaaaaaegacbaiaebaaaaaaacaaaaaaegiccaaa
abaaaaaaaeaaaaaabbaaaaahicaabaaaabaaaaaaegaobaaaadaaaaaaegaobaaa
adaaaaaaeeaaaaaficaabaaaabaaaaaadkaabaaaabaaaaaadiaaaaahhcaabaaa
acaaaaaapgapbaaaabaaaaaaegacbaaaadaaaaaabaaaaaahbcaabaaaabaaaaaa
egacbaaaabaaaaaaegacbaaaacaaaaaacpaaaaafbcaabaaaabaaaaaaakaabaaa
abaaaaaadiaaaaaiccaabaaaabaaaaaaakaabaaaabaaaaaaakiacaaaaaaaaaaa
aeaaaaaadiaaaaaibcaabaaaabaaaaaaakaabaaaabaaaaaadkiacaaaaaaaaaaa
afaaaaaabjaaaaafbcaabaaaabaaaaaaakaabaaaabaaaaaadiaaaaaibcaabaaa
abaaaaaaakaabaaaabaaaaaaakiacaaaaaaaaaaaagaaaaaadeaaaaahccaabaaa
aaaaaaaaakaabaaaabaaaaaaabeaaaaaaaaaaaaabjaaaaafbcaabaaaabaaaaaa
bkaabaaaabaaaaaadiaaaaaibcaabaaaabaaaaaaakaabaaaabaaaaaabkiacaaa
aaaaaaaaaeaaaaaadeaaaaahbcaabaaaaaaaaaaaakaabaaaabaaaaaaabeaaaaa
aaaaaaaadiaaaaaipccabaaaaeaaaaaaegaobaaaaaaaaaaaagiacaaaaaaaaaaa
adaaaaaadcaaaaaopcaabaaaaaaaaaaaegiocaaaaeaaaaaaaeaaaaaaaceaaaaa
aaaaaaeaaaaaaaeaaaaaaaeaaaaaaaeaegiocaaaaaaaaaaaabaaaaaaaaaaaaak
pcaabaaaaaaaaaaaegaobaaaaaaaaaaaaceaaaaaaaaaialpaaaaialpaaaaialp
aaaaialpdcaaaaanpccabaaaafaaaaaakgikcaaaaaaaaaaaakaaaaaaegaobaaa
aaaaaaaaaceaaaaaaaaaiadpaaaaiadpaaaaiadpaaaaiadpdoaaaaab"
}
SubProgram "d3d11_9x " {
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
ConstBuffer "$Globals" 208
Vector 16 [_LightColor0]
Float 48 [_SpecularIntensity]
Float 64 [_ShininessR]
Float 68 [_SpecularIntensityR]
Float 92 [_ShininessG]
Float 96 [_SpecularIntensityG]
Float 132 [_ScrollX1]
Float 136 [_ScrollY1]
Float 140 [_Scale1]
Float 144 [_ScrollX2]
Float 148 [_ScrollY2]
Float 152 [_Scale2]
Float 156 [_ScrollX3]
Float 160 [_ScrollY3]
Float 164 [_Scale3]
Float 168 [_LightingBlend]
Float 172 [_Seed]
Float 176 [_ShaderTime]
Float 180 [_TimeScale]
Vector 192 [_MainTex_ST]
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
eefiecediphiabphhcocpnjjnfmljhhdijchelkoabaaaaaabeapaaaaaeaaaaaa
daaaaaaaaeafaaaalaanaaaafiaoaaaaebgpgodjmmaeaaaammaeaaaaaaacpopp
diaeaaaajeaaaaaaajaaceaaaaaajaaaaaaajaaaaaaaceaaabaajaaaaaaaabaa
abaaabaaaaaaaaaaaaaaadaaaeaaacaaaaaaaaaaaaaaaiaaafaaagaaaaaaaaaa
abaaaaaaabaaalaaaaaaaaaaabaaaeaaabaaamaaaaaaaaaaacaaaaaaabaaanaa
aaaaaaaaadaaaaaaaeaaaoaaaaaaaaaaadaaamaaahaabcaaaaaaaaaaaeaaaeaa
abaabjaaaaaaaaaaaaaaaaaaaaacpoppfbaaaaafbkaaapkaaaaaaaaadddddddp
ggggggdpaaaaaaeafbaaaaafblaaapkaaaaaialpaaaaiadpaaaaaaaaaaaaaaaa
bpaaaaacafaaaaiaaaaaapjabpaaaaacafaaabiaabaaapjabpaaaaacafaaaeia
aeaaapjaaeaaaaaeaaaaadoaabaaoejaakaaoekaakaaookaabaaaaacaaaaajia
bkaaoekaamaaaaadaaaaabiaaaaaaaiaajaaaakaabaaaaacabaaamiaaiaaoeka
acaaaaadaaaaaciaabaappiaajaaaakaacaaaaadaaaaaeiaabaappiaalaaaaka
bcaaaaaeabaaabiaaaaaaaiaaaaaffiaaaaakkiaafaaaaadaaaaabiaabaaaaia
ajaaffkaaeaaaaaeaaaaagiaagaaoekaaaaaaaiaabaanajaafaaaaadabaaadoa
aaaaojiaagaappkaafaaaaadaaaaagiaaaaaaaiaagaaoekaaeaaaaaeaaaaagia
aaaaoeiabkaaffkaabaanajaafaaaaadabaaamoaaaaajeiaahaakkkaaeaaaaae
acaaahiaahaapekaaaaaaaiaabaamejaafaaaaadaaaaagiaacaanaiaagaappka
afaaaaadacaaadoaaaaaojiabkaakkkaafaaaaadaaaaagiaaaaaaaiaahaanaka
aeaaaaaeacaaaiiaaiaaaakaaaaaaaiaabaaffjaafaaaaadacaaamiaacaaoeia
aiaaffkaaeaaaaaeaaaaadiaaaaaojiabkaaffkaabaaoejaafaaaaadaaaaadia
aaaaoeiaahaakkkaafaaaaadacaaamoaaaaaeeiabkaakkkaabaaaaacadaaapia
bjaaoekaaeaaaaaeaaaaapiaadaaoeiaaaaappiaabaaoekaacaaaaadaaaaapia
aaaaoeiablaaaakaaeaaaaaeaeaaapoaabaakkiaaaaaoeiablaaffkaafaaaaad
aaaaapiaaaaaffjabdaaoekaaeaaaaaeaaaaapiabcaaoekaaaaaaajaaaaaoeia
aeaaaaaeaaaaapiabeaaoekaaaaakkjaaaaaoeiaaeaaaaaeaaaaapiabfaaoeka
aaaappjaaaaaoeiaacaaaaadabaaaiiaaaaappibblaaffkaacaaaaadabaaahia
aaaaoeibamaaoekaajaaaaadaaaaabiaabaaoeiaabaaoeiaahaaaaacaaaaabia
aaaaaaiaafaaaaadaaaaahiaaaaaaaiaabaaoeiaaiaaaaadabaaabiaaeaaoeja
bgaaoekaaiaaaaadabaaaciaaeaaoejabhaaoekaaiaaaaadabaaaeiaaeaaoeja
biaaoekaceaaaaacadaaahiaabaaoeiaceaaaaacabaaahiaanaaoekaaiaaaaad
aaaaaiiaabaaoeibadaaoeiaacaaaaadaaaaaiiaaaaappiaaaaappiaaeaaaaae
abaaahiaadaaoeiaaaaappibabaaoeibaiaaaaadaaaaabiaabaaoeiaaaaaoeia
apaaaaacaaaaabiaaaaaaaiaafaaaaadaaaaaciaaaaaaaiaadaaaakaafaaaaad
aaaaabiaaaaaaaiaaeaappkaaoaaaaacaaaaabiaaaaaaaiaafaaaaadaaaaabia
aaaaaaiaafaaaakaalaaaaadacaaaciaaaaaaaiabkaaaakaaoaaaaacaaaaabia
aaaaffiaafaaaaadaaaaabiaaaaaaaiaadaaffkaalaaaaadacaaabiaaaaaaaia
bkaaaakaafaaaaadadaaapoaacaaoeiaacaaaakaafaaaaadaaaaapiaaaaaffja
apaaoekaaeaaaaaeaaaaapiaaoaaoekaaaaaaajaaaaaoeiaaeaaaaaeaaaaapia
baaaoekaaaaakkjaaaaaoeiaaeaaaaaeaaaaapiabbaaoekaaaaappjaaaaaoeia
aeaaaaaeaaaaadmaaaaappiaaaaaoekaaaaaoeiaabaaaaacaaaaammaaaaaoeia
ppppaaaafdeieefckeaiaaaaeaaaabaacjacaaaafjaaaaaeegiocaaaaaaaaaaa
anaaaaaafjaaaaaeegiocaaaabaaaaaaafaaaaaafjaaaaaeegiocaaaacaaaaaa
abaaaaaafjaaaaaeegiocaaaadaaaaaabdaaaaaafjaaaaaeegiocaaaaeaaaaaa
afaaaaaafpaaaaadpcbabaaaaaaaaaaafpaaaaaddcbabaaaabaaaaaafpaaaaad
hcbabaaaaeaaaaaaghaaaaaepccabaaaaaaaaaaaabaaaaaagfaaaaaddccabaaa
abaaaaaagfaaaaadpccabaaaacaaaaaagfaaaaadpccabaaaadaaaaaagfaaaaad
pccabaaaaeaaaaaagfaaaaadpccabaaaafaaaaaagiaaaaacaeaaaaaadiaaaaai
pcaabaaaaaaaaaaafgbfbaaaaaaaaaaaegiocaaaadaaaaaaabaaaaaadcaaaaak
pcaabaaaaaaaaaaaegiocaaaadaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaa
aaaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaadaaaaaaacaaaaaakgbkbaaa
aaaaaaaaegaobaaaaaaaaaaadcaaaaakpccabaaaaaaaaaaaegiocaaaadaaaaaa
adaaaaaapgbpbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaaldccabaaaabaaaaaa
egbabaaaabaaaaaaegiacaaaaaaaaaaaamaaaaaaogikcaaaaaaaaaaaamaaaaaa
dbaaaaaibcaabaaaaaaaaaaaabeaaaaaaaaaaaaaakiacaaaaaaaaaaaalaaaaaa
aaaaaaajccaabaaaaaaaaaaadkiacaaaaaaaaaaaakaaaaaaakiacaaaaaaaaaaa
alaaaaaaaaaaaaajecaabaaaaaaaaaaadkiacaaaaaaaaaaaakaaaaaaakiacaaa
abaaaaaaaaaaaaaadhaaaaajbcaabaaaaaaaaaaaakaabaaaaaaaaaaabkaabaaa
aaaaaaaackaabaaaaaaaaaaadiaaaaaibcaabaaaaaaaaaaaakaabaaaaaaaaaaa
bkiacaaaaaaaaaaaalaaaaaadiaaaaaigcaabaaaaaaaaaaaagaabaaaaaaaaaaa
fgigcaaaaaaaaaaaaiaaaaaadcaaaaamgcaabaaaaaaaaaaafgagbaaaaaaaaaaa
aceaaaaaaaaaaaaadddddddpdddddddpaaaaaaaaagbbbaaaabaaaaaadiaaaaai
mccabaaaacaaaaaafgajbaaaaaaaaaaakgikcaaaaaaaaaaaajaaaaaadcaaaaak
gcaabaaaaaaaaaaafgigcaaaaaaaaaaaaiaaaaaaagaabaaaaaaaaaaaagbbbaaa
abaaaaaadiaaaaaidccabaaaacaaaaaajgafbaaaaaaaaaaapgipcaaaaaaaaaaa
aiaaaaaadiaaaaaigcaabaaaaaaaaaaaagaabaaaaaaaaaaaagibcaaaaaaaaaaa
ajaaaaaadcaaaaamgcaabaaaaaaaaaaafgagbaaaaaaaaaaaaceaaaaaaaaaaaaa
dddddddpdddddddpaaaaaaaaagbbbaaaabaaaaaadiaaaaaigcaabaaaaaaaaaaa
fgagbaaaaaaaaaaakgikcaaaaaaaaaaaajaaaaaadiaaaaakmccabaaaadaaaaaa
fgajbaaaaaaaaaaaaceaaaaaaaaaaaaaaaaaaaaaggggggdpggggggdpdcaaaaak
hcaabaaaabaaaaaaegidcaaaaaaaaaaaajaaaaaaagaabaaaaaaaaaaaegbabaaa
abaaaaaadcaaaaakicaabaaaabaaaaaaakiacaaaaaaaaaaaakaaaaaaakaabaaa
aaaaaaaabkbabaaaabaaaaaadiaaaaaimcaabaaaaaaaaaaakgaobaaaabaaaaaa
fgifcaaaaaaaaaaaakaaaaaadiaaaaaidcaabaaaabaaaaaaegaabaaaabaaaaaa
pgipcaaaaaaaaaaaaiaaaaaadiaaaaakdccabaaaadaaaaaaegaabaaaabaaaaaa
aceaaaaaggggggdpggggggdpaaaaaaaaaaaaaaaabaaaaaaibcaabaaaabaaaaaa
egbcbaaaaeaaaaaaegiccaaaadaaaaaabaaaaaaabaaaaaaiccaabaaaabaaaaaa
egbcbaaaaeaaaaaaegiccaaaadaaaaaabbaaaaaabaaaaaaiecaabaaaabaaaaaa
egbcbaaaaeaaaaaaegiccaaaadaaaaaabcaaaaaabaaaaaahicaabaaaabaaaaaa
egacbaaaabaaaaaaegacbaaaabaaaaaaeeaaaaaficaabaaaabaaaaaadkaabaaa
abaaaaaadiaaaaahhcaabaaaabaaaaaapgapbaaaabaaaaaaegacbaaaabaaaaaa
baaaaaajicaabaaaabaaaaaaegiccaaaacaaaaaaaaaaaaaaegiccaaaacaaaaaa
aaaaaaaaeeaaaaaficaabaaaabaaaaaadkaabaaaabaaaaaadiaaaaaihcaabaaa
acaaaaaapgapbaaaabaaaaaaegiccaaaacaaaaaaaaaaaaaabaaaaaaiicaabaaa
abaaaaaaegacbaiaebaaaaaaacaaaaaaegacbaaaabaaaaaaaaaaaaahicaabaaa
abaaaaaadkaabaaaabaaaaaadkaabaaaabaaaaaadcaaaaalhcaabaaaabaaaaaa
egacbaaaabaaaaaapgapbaiaebaaaaaaabaaaaaaegacbaiaebaaaaaaacaaaaaa
diaaaaaipcaabaaaacaaaaaafgbfbaaaaaaaaaaaegiocaaaadaaaaaaanaaaaaa
dcaaaaakpcaabaaaacaaaaaaegiocaaaadaaaaaaamaaaaaaagbabaaaaaaaaaaa
egaobaaaacaaaaaadcaaaaakpcaabaaaacaaaaaaegiocaaaadaaaaaaaoaaaaaa
kgbkbaaaaaaaaaaaegaobaaaacaaaaaadcaaaaakpcaabaaaacaaaaaaegiocaaa
adaaaaaaapaaaaaapgbpbaaaaaaaaaaaegaobaaaacaaaaaaaaaaaaaiicaabaaa
adaaaaaadkaabaiaebaaaaaaacaaaaaaabeaaaaaaaaaiadpaaaaaaajhcaabaaa
adaaaaaaegacbaiaebaaaaaaacaaaaaaegiccaaaabaaaaaaaeaaaaaabbaaaaah
icaabaaaabaaaaaaegaobaaaadaaaaaaegaobaaaadaaaaaaeeaaaaaficaabaaa
abaaaaaadkaabaaaabaaaaaadiaaaaahhcaabaaaacaaaaaapgapbaaaabaaaaaa
egacbaaaadaaaaaabaaaaaahbcaabaaaabaaaaaaegacbaaaabaaaaaaegacbaaa
acaaaaaacpaaaaafbcaabaaaabaaaaaaakaabaaaabaaaaaadiaaaaaiccaabaaa
abaaaaaaakaabaaaabaaaaaaakiacaaaaaaaaaaaaeaaaaaadiaaaaaibcaabaaa
abaaaaaaakaabaaaabaaaaaadkiacaaaaaaaaaaaafaaaaaabjaaaaafbcaabaaa
abaaaaaaakaabaaaabaaaaaadiaaaaaibcaabaaaabaaaaaaakaabaaaabaaaaaa
akiacaaaaaaaaaaaagaaaaaadeaaaaahccaabaaaaaaaaaaaakaabaaaabaaaaaa
abeaaaaaaaaaaaaabjaaaaafbcaabaaaabaaaaaabkaabaaaabaaaaaadiaaaaai
bcaabaaaabaaaaaaakaabaaaabaaaaaabkiacaaaaaaaaaaaaeaaaaaadeaaaaah
bcaabaaaaaaaaaaaakaabaaaabaaaaaaabeaaaaaaaaaaaaadiaaaaaipccabaaa
aeaaaaaaegaobaaaaaaaaaaaagiacaaaaaaaaaaaadaaaaaadcaaaaaopcaabaaa
aaaaaaaaegiocaaaaeaaaaaaaeaaaaaaaceaaaaaaaaaaaeaaaaaaaeaaaaaaaea
aaaaaaeaegiocaaaaaaaaaaaabaaaaaaaaaaaaakpcaabaaaaaaaaaaaegaobaaa
aaaaaaaaaceaaaaaaaaaialpaaaaialpaaaaialpaaaaialpdcaaaaanpccabaaa
afaaaaaakgikcaaaaaaaaaaaakaaaaaaegaobaaaaaaaaaaaaceaaaaaaaaaiadp
aaaaiadpaaaaiadpaaaaiadpdoaaaaabejfdeheokaaaaaaaafaaaaaaaiaaaaaa
iaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaaijaaaaaaaaaaaaaa
aaaaaaaaadaaaaaaabaaaaaaadadaaaaijaaaaaaabaaaaaaaaaaaaaaadaaaaaa
acaaaaaaadaaaaaajcaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapaaaaaa
jiaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaeaaaaaaahahaaaafaepfdejfeejepeo
aafeeffiedepepfceeaaedepemepfcaaeoepfcenebemaaklepfdeheoleaaaaaa
agaaaaaaaiaaaaaajiaaaaaaaaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaa
keaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadamaaaakeaaaaaaabaaaaaa
aaaaaaaaadaaaaaaacaaaaaaapaaaaaakeaaaaaaacaaaaaaaaaaaaaaadaaaaaa
adaaaaaaapaaaaaakeaaaaaaadaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapaaaaaa
knaaaaaaaaaaaaaaaaaaaaaaadaaaaaaafaaaaaaapaaaaaafdfgfpfagphdgjhe
gjgpgoaafeeffiedepepfceeaaedepemepfcaakl"
}
}
Program "fp" {
SubProgram "opengl " {
Vector 0 [_SpecularColorR]
Vector 1 [_SpecularColorG]
Vector 2 [_FxColor]
Float 3 [_FxIntensity]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_MaskTex] 2D 1
SetTexture 2 [_FX1Tex] 2D 2
SetTexture 3 [_FX2Tex] 2D 3
SetTexture 4 [_FX3Tex] 2D 4
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
PARAM c[4] = { program.local[0..3] };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
TEMP R5;
TEMP R6;
TEX R6.xyz, fragment.texcoord[0], texture[1], 2D;
TEX R3, fragment.texcoord[2].zwzw, texture[2], 2D;
TEX R1, fragment.texcoord[1].zwzw, texture[2], 2D;
TEX R2, fragment.texcoord[2], texture[3], 2D;
TEX R5, fragment.texcoord[0], texture[0], 2D;
TEX R4, fragment.texcoord[3].zwzw, texture[4], 2D;
TEX R0, fragment.texcoord[1], texture[2], 2D;
MUL R1, R6.y, R1;
MAD R0, R0, R6.z, R1;
MUL R3, R6.y, R3;
MAD R1, R6.z, R2, R3;
MUL R0, R0, R1;
MUL R2, R6.y, c[2];
MAD R1, R6.z, R4, R2;
MUL R0, R0, R1;
MUL R2.xyz, fragment.texcoord[3].y, c[1];
MAD R0, R0, c[3].x, R5;
MUL R1.xyz, R6.y, R2;
MUL R2.xyz, R0, R1;
MUL R1.xyz, fragment.texcoord[3].x, c[0];
MAD R1.xyz, R6.x, R1, R2;
ADD R0.xyz, R0, R1;
MUL result.color, R0, fragment.color.primary;
END
# 23 instructions, 7 R-regs
"
}
SubProgram "d3d9 " {
Vector 0 [_SpecularColorR]
Vector 1 [_SpecularColorG]
Vector 2 [_FxColor]
Float 3 [_FxIntensity]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_MaskTex] 2D 1
SetTexture 2 [_FX1Tex] 2D 2
SetTexture 3 [_FX2Tex] 2D 3
SetTexture 4 [_FX3Tex] 2D 4
"ps_2_0
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
dcl_2d s4
dcl t0.xy
dcl t1
dcl t2
dcl t3
dcl v0
texld r5, t1, s2
texld r3, t2, s3
texld r7, t0, s1
mov r0.y, t2.w
mov r0.x, t2.z
mov r2.xy, r0
mov r0.y, t3.w
mov r0.x, t3.z
mov r1.xy, r0
mov r0.y, t1.w
mov r0.x, t1.z
mov r4.xy, r0
mul_pp r6, r7.y, c2
texld r0, t0, s0
texld r4, r4, s2
texld r1, r1, s4
texld r2, r2, s2
mul r2, r7.y, r2
mad r2, r7.z, r3, r2
mul r3, r7.y, r4
mad r3, r5, r7.z, r3
mul_pp r2, r3, r2
mad r1, r7.z, r1, r6
mul_pp r1, r2, r1
mul r2.xyz, t3.y, c1
mad_pp r0, r1, c3.x, r0
mul r2.xyz, r7.y, r2
mul r2.xyz, r0, r2
mul r1.xyz, t3.x, c0
mad r1.xyz, r7.x, r1, r2
add_pp r0.xyz, r0, r1
mul_pp r0, r0, v0
mov_pp oC0, r0
"
}
SubProgram "d3d11 " {
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_MaskTex] 2D 1
SetTexture 2 [_FX1Tex] 2D 2
SetTexture 3 [_FX2Tex] 2D 3
SetTexture 4 [_FX3Tex] 2D 4
ConstBuffer "$Globals" 208
Vector 52 [_SpecularColorR] 3
Vector 80 [_SpecularColorG] 3
Vector 112 [_FxColor]
Float 128 [_FxIntensity]
BindCB  "$Globals" 0
"ps_4_0
eefiecedgealifinifdddlhhkonamjckgcgddeaeabaaaaaabaafaaaaadaaaaaa
cmaaaaaaoiaaaaaabmabaaaaejfdeheoleaaaaaaagaaaaaaaiaaaaaajiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaa
apapaaaakeaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaapapaaaakeaaaaaa
adaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapapaaaaknaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaafaaaaaaapapaaaafdfgfpfagphdgjhegjgpgoaafeeffiedepepfcee
aaedepemepfcaaklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaa
aaaaaaaaadaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefc
omadaaaaeaaaaaaaplaaaaaafjaaaaaeegiocaaaaaaaaaaaajaaaaaafkaaaaad
aagabaaaaaaaaaaafkaaaaadaagabaaaabaaaaaafkaaaaadaagabaaaacaaaaaa
fkaaaaadaagabaaaadaaaaaafkaaaaadaagabaaaaeaaaaaafibiaaaeaahabaaa
aaaaaaaaffffaaaafibiaaaeaahabaaaabaaaaaaffffaaaafibiaaaeaahabaaa
acaaaaaaffffaaaafibiaaaeaahabaaaadaaaaaaffffaaaafibiaaaeaahabaaa
aeaaaaaaffffaaaagcbaaaaddcbabaaaabaaaaaagcbaaaadpcbabaaaacaaaaaa
gcbaaaadpcbabaaaadaaaaaagcbaaaadpcbabaaaaeaaaaaagcbaaaadpcbabaaa
afaaaaaagfaaaaadpccabaaaaaaaaaaagiaaaaacaeaaaaaaefaaaaajpcaabaaa
aaaaaaaaegbabaaaacaaaaaaeghobaaaacaaaaaaaagabaaaacaaaaaaefaaaaaj
pcaabaaaabaaaaaaogbkbaaaacaaaaaaeghobaaaacaaaaaaaagabaaaacaaaaaa
efaaaaajpcaabaaaacaaaaaaegbabaaaabaaaaaaeghobaaaabaaaaaaaagabaaa
abaaaaaadiaaaaahpcaabaaaabaaaaaaegaobaaaabaaaaaafgafbaaaacaaaaaa
dcaaaaajpcaabaaaaaaaaaaaegaobaaaaaaaaaaakgakbaaaacaaaaaaegaobaaa
abaaaaaaefaaaaajpcaabaaaabaaaaaaegbabaaaadaaaaaaeghobaaaadaaaaaa
aagabaaaadaaaaaaefaaaaajpcaabaaaadaaaaaaogbkbaaaadaaaaaaeghobaaa
acaaaaaaaagabaaaacaaaaaadiaaaaahpcaabaaaadaaaaaafgafbaaaacaaaaaa
egaobaaaadaaaaaadcaaaaajpcaabaaaabaaaaaaegaobaaaabaaaaaakgakbaaa
acaaaaaaegaobaaaadaaaaaadiaaaaahpcaabaaaaaaaaaaaegaobaaaaaaaaaaa
egaobaaaabaaaaaaefaaaaajpcaabaaaabaaaaaaogbkbaaaaeaaaaaaeghobaaa
aeaaaaaaaagabaaaaeaaaaaadiaaaaaipcaabaaaadaaaaaafgafbaaaacaaaaaa
egiocaaaaaaaaaaaahaaaaaadcaaaaajpcaabaaaabaaaaaaegaobaaaabaaaaaa
kgakbaaaacaaaaaaegaobaaaadaaaaaadiaaaaahpcaabaaaaaaaaaaaegaobaaa
aaaaaaaaegaobaaaabaaaaaaefaaaaajpcaabaaaabaaaaaaegbabaaaabaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaadcaaaaakpcaabaaaaaaaaaaaegaobaaa
aaaaaaaaagiacaaaaaaaaaaaaiaaaaaaegaobaaaabaaaaaadiaaaaaihcaabaaa
abaaaaaafgbfbaaaaeaaaaaaegiccaaaaaaaaaaaafaaaaaadiaaaaahhcaabaaa
abaaaaaafgafbaaaacaaaaaaegacbaaaabaaaaaadiaaaaahhcaabaaaabaaaaaa
egacbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaaiocaabaaaacaaaaaaagbabaaa
aeaaaaaafgiocaaaaaaaaaaaadaaaaaadcaaaaajhcaabaaaabaaaaaajgahbaaa
acaaaaaaagaabaaaacaaaaaaegacbaaaabaaaaaaaaaaaaahhcaabaaaaaaaaaaa
egacbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaahpccabaaaaaaaaaaaegaobaaa
aaaaaaaaegbobaaaafaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_MaskTex] 2D 1
SetTexture 2 [_FX1Tex] 2D 2
SetTexture 3 [_FX2Tex] 2D 3
SetTexture 4 [_FX3Tex] 2D 4
ConstBuffer "$Globals" 208
Vector 52 [_SpecularColorR] 3
Vector 80 [_SpecularColorG] 3
Vector 112 [_FxColor]
Float 128 [_FxIntensity]
BindCB  "$Globals" 0
"ps_4_0_level_9_1
eefiecedpamdggmliigbbldjkkmpahdkmihiegncabaaaaaaoaahaaaaaeaaaaaa
daaaaaaapmacaaaapaagaaaakmahaaaaebgpgodjmeacaaaameacaaaaaaacpppp
giacaaaafmaaaaaaadaadiaaaaaafmaaaaaafmaaafaaceaaaaaafmaaaaaaaaaa
abababaaacacacaaadadadaaaeaeaeaaaaaaadaaabaaaaaaaaaaaaaaaaaaafaa
abaaabaaaaaaaaaaaaaaahaaacaaacaaaaaaaaaaaaacppppbpaaaaacaaaaaaia
aaaaadlabpaaaaacaaaaaaiaabaaaplabpaaaaacaaaaaaiaacaaaplabpaaaaac
aaaaaaiaadaaaplabpaaaaacaaaaaaiaaeaacplabpaaaaacaaaaaajaaaaiapka
bpaaaaacaaaaaajaabaiapkabpaaaaacaaaaaajaacaiapkabpaaaaacaaaaaaja
adaiapkabpaaaaacaaaaaajaaeaiapkaabaaaaacaaaaabiaabaakklaabaaaaac
aaaaaciaabaapplaabaaaaacabaaabiaacaakklaabaaaaacabaaaciaacaappla
abaaaaacacaaabiaadaakklaabaaaaacacaaaciaadaapplaecaaaaadadaaapia
abaaoelaacaioekaecaaaaadaaaaapiaaaaaoeiaacaioekaecaaaaadaeaacpia
aaaaoelaabaioekaecaaaaadafaaapiaacaaoelaadaioekaecaaaaadabaaapia
abaaoeiaacaioekaecaaaaadacaaapiaacaaoeiaaeaioekaecaaaaadagaacpia
aaaaoelaaaaioekaafaaaaadaaaaapiaaaaaoeiaaeaaffiaaeaaaaaeaaaacpia
adaaoeiaaeaakkiaaaaaoeiaafaaaaadabaaapiaaeaaffiaabaaoeiaaeaaaaae
abaacpiaafaaoeiaaeaakkiaabaaoeiaafaaaaadaaaacpiaaaaaoeiaabaaoeia
afaaaaadabaaapiaaeaaffiaacaaoekaaeaaaaaeabaacpiaacaaoeiaaeaakkia
abaaoeiaafaaaaadaaaacpiaaaaaoeiaabaaoeiaaeaaaaaeaaaacpiaaaaaoeia
adaaaakaagaaoeiaafaaaaadabaaahiaadaafflaabaaoekaafaaaaadabaaahia
aeaaffiaabaaoeiaafaaaaadabaaahiaaaaaoeiaabaaoeiaafaaaaadacaaadia
adaaaalaaaaamjkaafaaaaadacaaaeiaadaaaalaaaaappkaaeaaaaaeabaaahia
acaaoeiaaeaaaaiaabaaoeiaacaaaaadaaaachiaaaaaoeiaabaaoeiaafaaaaad
aaaacpiaaaaaoeiaaeaaoelaabaaaaacaaaicpiaaaaaoeiappppaaaafdeieefc
omadaaaaeaaaaaaaplaaaaaafjaaaaaeegiocaaaaaaaaaaaajaaaaaafkaaaaad
aagabaaaaaaaaaaafkaaaaadaagabaaaabaaaaaafkaaaaadaagabaaaacaaaaaa
fkaaaaadaagabaaaadaaaaaafkaaaaadaagabaaaaeaaaaaafibiaaaeaahabaaa
aaaaaaaaffffaaaafibiaaaeaahabaaaabaaaaaaffffaaaafibiaaaeaahabaaa
acaaaaaaffffaaaafibiaaaeaahabaaaadaaaaaaffffaaaafibiaaaeaahabaaa
aeaaaaaaffffaaaagcbaaaaddcbabaaaabaaaaaagcbaaaadpcbabaaaacaaaaaa
gcbaaaadpcbabaaaadaaaaaagcbaaaadpcbabaaaaeaaaaaagcbaaaadpcbabaaa
afaaaaaagfaaaaadpccabaaaaaaaaaaagiaaaaacaeaaaaaaefaaaaajpcaabaaa
aaaaaaaaegbabaaaacaaaaaaeghobaaaacaaaaaaaagabaaaacaaaaaaefaaaaaj
pcaabaaaabaaaaaaogbkbaaaacaaaaaaeghobaaaacaaaaaaaagabaaaacaaaaaa
efaaaaajpcaabaaaacaaaaaaegbabaaaabaaaaaaeghobaaaabaaaaaaaagabaaa
abaaaaaadiaaaaahpcaabaaaabaaaaaaegaobaaaabaaaaaafgafbaaaacaaaaaa
dcaaaaajpcaabaaaaaaaaaaaegaobaaaaaaaaaaakgakbaaaacaaaaaaegaobaaa
abaaaaaaefaaaaajpcaabaaaabaaaaaaegbabaaaadaaaaaaeghobaaaadaaaaaa
aagabaaaadaaaaaaefaaaaajpcaabaaaadaaaaaaogbkbaaaadaaaaaaeghobaaa
acaaaaaaaagabaaaacaaaaaadiaaaaahpcaabaaaadaaaaaafgafbaaaacaaaaaa
egaobaaaadaaaaaadcaaaaajpcaabaaaabaaaaaaegaobaaaabaaaaaakgakbaaa
acaaaaaaegaobaaaadaaaaaadiaaaaahpcaabaaaaaaaaaaaegaobaaaaaaaaaaa
egaobaaaabaaaaaaefaaaaajpcaabaaaabaaaaaaogbkbaaaaeaaaaaaeghobaaa
aeaaaaaaaagabaaaaeaaaaaadiaaaaaipcaabaaaadaaaaaafgafbaaaacaaaaaa
egiocaaaaaaaaaaaahaaaaaadcaaaaajpcaabaaaabaaaaaaegaobaaaabaaaaaa
kgakbaaaacaaaaaaegaobaaaadaaaaaadiaaaaahpcaabaaaaaaaaaaaegaobaaa
aaaaaaaaegaobaaaabaaaaaaefaaaaajpcaabaaaabaaaaaaegbabaaaabaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaadcaaaaakpcaabaaaaaaaaaaaegaobaaa
aaaaaaaaagiacaaaaaaaaaaaaiaaaaaaegaobaaaabaaaaaadiaaaaaihcaabaaa
abaaaaaafgbfbaaaaeaaaaaaegiccaaaaaaaaaaaafaaaaaadiaaaaahhcaabaaa
abaaaaaafgafbaaaacaaaaaaegacbaaaabaaaaaadiaaaaahhcaabaaaabaaaaaa
egacbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaaiocaabaaaacaaaaaaagbabaaa
aeaaaaaafgiocaaaaaaaaaaaadaaaaaadcaaaaajhcaabaaaabaaaaaajgahbaaa
acaaaaaaagaabaaaacaaaaaaegacbaaaabaaaaaaaaaaaaahhcaabaaaaaaaaaaa
egacbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaahpccabaaaaaaaaaaaegaobaaa
aaaaaaaaegbobaaaafaaaaaadoaaaaabejfdeheoleaaaaaaagaaaaaaaiaaaaaa
jiaaaaaaaaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaa
aaaaaaaaadaaaaaaabaaaaaaadadaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaa
acaaaaaaapapaaaakeaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaapapaaaa
keaaaaaaadaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapapaaaaknaaaaaaaaaaaaaa
aaaaaaaaadaaaaaaafaaaaaaapapaaaafdfgfpfagphdgjhegjgpgoaafeeffied
epepfceeaaedepemepfcaaklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklkl
"
}
}
 }
}
Fallback "Diffuse"
}
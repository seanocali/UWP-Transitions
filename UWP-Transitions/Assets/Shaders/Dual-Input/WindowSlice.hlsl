// Author: Sean O'Neil
// License: MIT

#define D2D_INPUT_COUNT 2
#define D2D_INPUT0_SIMPLE
#define D2D_INPUT1_SIMPLE
#define D2D_REQUIRES_SCENE_POSITION
#include "d2d1effecthelpers.hlsli"

float progress;
float dpi = 96;
float width;
uniform float count = 10.0;
uniform float smoothness = 2.5;

D2D_PS_ENTRY(main)
{
    float2 positionInPixels = D2DGetScenePosition().xy / (width);
    float2 p = positionInPixels * 96 / dpi;
    float4 a = D2DGetInput(0);
    float4 b = D2DGetInput(1);
    float pr = smoothstep(-smoothness, 0, p.x - progress * (1.0 + smoothness));
    float s = step(pr, frac(count * (p.x)));
    return lerp(a, b, s);
}

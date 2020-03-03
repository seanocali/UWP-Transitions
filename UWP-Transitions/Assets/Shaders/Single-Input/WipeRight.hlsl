// Author: Sean O'Neil
// License: MIT

#define D2D_INPUT_COUNT 1
#define D2D_INPUT0_SIMPLE
#define D2D_REQUIRES_SCENE_POSITION
#include "d2d1effecthelpers.hlsli"

float progress;
float dpi = 96;
float width;
float feather;


D2D_PS_ENTRY(main)
{
    float2 positionInPixels = D2DGetScenePosition().xy / (width + feather);
    float2 p = positionInPixels * 96 / dpi;
    float4 a = D2DGetInput(0);
    float pr = progress * (1 + feather);
    return lerp(a, 0, smoothstep(p.x, p.x + feather, pr));
}

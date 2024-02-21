Shader "GuiyuuuuuGame/Default-Background"
{
    Properties 
    {
        _hour ("hour", Float) = 0
        _minute ("minute", Float) = 0
        _second ("second", Float) = 0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            #define TAU 6.28318530718
            #define PI 3.14159265

            float _hour;
            float _minute;
            float _second;

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float2 rotation(float2 uv, float angle) {
                // Rotation, angle in radians (0 - 2PI)
                // Rotate the whole coordinate around the origin for 2D
                // The formula never change regardless the shape we are rotating
                float2x2 rotate2D = float2x2(
                    cos(angle), -sin(angle),
                    sin(angle), cos(angle)
                );

                // 应用 旋转矩阵
                uv = mul(uv, rotate2D);
                return uv;
            }

            float2 translation(float2 uv, float x, float y) {
                // Translation offset
                float2 translate = float2(x, y);

                float3x3 translate2D = float3x3(
                        1, 0, translate.x,
                        0, 1, translate.y,
                        0, 0, 1
                );

                // 应用以上的变换
                float3 uv3 = mul(translate2D, float3(uv, 1));

                return uv3.xy;
            }

            float circle(float2 uv, float radius, float x, float y) {
                uv = float2(uv.x - radius - x, uv.y - radius - y);
                return 1-step(radius, length(uv));
            }

            float rectangle(float2 uv, float x, float y, float width, float height) {
                return step(x, uv.x) * 
                step(y, uv.y) * 
                step(1-(x+width), 1-uv.x) * 
                step(1-(y+height), 1-uv.y);
            }

            float checkState(int standardNum, int actualNum) {
                return step(standardNum, actualNum) * (1 - step(standardNum+1, actualNum));
            }

            float drawZero(float2 uv, float x, float y) {
                float2 uv1 = float2(uv.x - x, uv.y - y);
                return rectangle(uv1, 0, 0, 0.1, 0.6) + rectangle(uv1, 0.3, 0, 0.1, 0.6) + rectangle(uv1, 0, 0, 0.3, 0.1) + rectangle(uv1, 0.1, 0.5, 0.2, 0.1);
            }

            float drawOne(float2 uv, float x, float y) {
                float2 uv1 = float2(uv.x - x, uv.y - y);
                return rectangle(uv1, 0, 0, 0.1, 0.6);
            }

            float drawTwo(float2 uv, float x, float y) {
                float2 uv1 = float2(uv.x - x, uv.y - y);
                float circleVar = circle(uv1, 0.2, 0, 0.2);
                float innerCircle = 1 - circle(uv1, 0.1, 0.1, 0.3);
                float stepCover = step(0.4, uv.y);

                float rectangle1 = rectangle(uv1, 0.3, 0.25, 0.1, 0.15);
                float rectangle2 = rectangle(uv1, 0, 0.2, 0.4, 0.1);
                float rectangle3 = rectangle(uv1, 0, 0, 0.1, 0.2);
                float rectangle4 = rectangle(uv1, 0, 0, 0.4, 0.1);

                return circleVar * innerCircle * stepCover + rectangle1 + rectangle2 + rectangle3 + rectangle4;
            }

            float drawThree(float2 uv, float x, float y) {
                float2 uv1 = float2(uv.x - x, uv.y - y);
                float rec1 = rectangle(uv1, 0, 0, 0.4, 0.1);
                float rec2 = rectangle(uv1, 0.3, 0, 0.1, 0.6);
                float rec3 = rectangle(uv1, 0, 0.5, 0.4, 0.1);
                float rec4 = rectangle(uv1, 0.05, 0.25, 0.35, 0.1);
                return rec1 + rec2 + rec3 + rec4;
            }

            float drawFour(float2 uv, float x, float y) {
                float2 uv1 = float2(uv.x - x, uv.y - y);
                float rec1 = rectangle(uv1, 0, 0.2, 0.4, 0.1);
                float rec2 = rectangle(uv1, 0.2, 0, 0.1, 0.6);
                float rec3 = rectangle(uv1, 0, 0.25, 0.1, 0.35);
                return rec1 + rec2 + rec3;
            }

            float drawFive(float2 uv, float x, float y) {
                float2 uv1 = float2(uv.x - x, uv.y - y);
                float circleVar = circle(uv1, 0.2, 0, 0);
                float innerCircle = 1 - circle(uv1, 0.1, 0.1, 0.1);
                float stepCover = 1-step(0.15, uv.y);

                float rectangle1 = rectangle(uv1, 0.3, 0.15, 0.1, 0.15);
                float rectangle2 = rectangle(uv1, 0, 0.3, 0.4, 0.1);
                float rectangle3 = rectangle(uv1, 0, 0.3, 0.1, 0.2);
                float rectangle4 = rectangle(uv1, 0, 0.5, 0.4, 0.1);

                return circleVar * innerCircle * stepCover + rectangle1 + rectangle2 + rectangle3 + rectangle4;
            }

            float drawSix(float2 uv, float x, float y) {
                float2 uv1 = float2(uv.x - x, uv.y - y);
                float circleVar = circle(uv1, 0.2, 0, 0);
                float innerCircle = 1 - circle(uv1, 0.1, 0.1, 0.1);

                float circleVar1 = circle(uv1, 0.2, 0, 0.2);
                float innerCircle1 = 1 - circle(uv1, 0.1, 0.1, 0.3);
                float stepCover1 = step(0.45, uv.y);

                float rectangle1 = rectangle(uv1, 0, 0.2, 0.1, 0.25);

                return circleVar * innerCircle + circleVar1 * innerCircle1 * stepCover1 + rectangle1;
            }

            float drawSeven(float2 uv, float x, float y) {
                float2 uv1 = float2(uv.x - x, uv.y - y);
                float rec1 = rectangle(uv1, 0, 0.5, 0.4, 0.1);
                float rec2 = rectangle(uv1, 0.3, 0, 0.1, 0.6);
                return rec1 + rec2;
            }

            float drawEight(float2 uv, float x, float y) {
                float2 uv1 = float2(uv.x - x, uv.y - y);
                float rec1 = rectangle(uv1, 0, 0, 0.4, 0.1);
                float rec2 = rectangle(uv1, 0.3, 0, 0.1, 0.6);
                float rec3 = rectangle(uv1, 0, 0.5, 0.4, 0.1);
                float rec4 = rectangle(uv1, 0.05, 0.25, 0.35, 0.1);
                float rec5 = rectangle(uv1, 0, 0, 0.1, 0.6);
                return rec1 + rec2 + rec3 + rec4 + rec5;
            }
            
            float drawNine(float2 uv, float x, float y) {
                float2 uv1 = float2(uv.x - x, uv.y - y);
                float circleVar = circle(uv1, 0.2, 0, 0.2);
                float innerCircle = 1 - circle(uv1, 0.1, 0.1, 0.3);

                float circleVar1 = circle(uv1, 0.2, 0, 0);
                float innerCircle1 = 1 - circle(uv1, 0.1, 0.1, 0.1);
                float stepCover1 = 1-step(0.15, uv.y);

                float rectangle1 = rectangle(uv1, 0.29, 0.14, 0.1, 0.25);

                return circleVar * innerCircle + circleVar1 * innerCircle1 * stepCover1 + rectangle1;
            }

            float4 frag (Interpolators i) : SV_Target {

                float2 uvBackup = i.uv;
                float2 uv = i.uv * 4;
                uv.y -= 4/6.0*5;

                float timeSpeeder = 1;

                int totalSecond = int(_Time.y) * timeSpeeder;
                int shownSecond = totalSecond % 60;
                int unitSecond = shownSecond % 10;
                int tenthSecond = int(shownSecond / 10);

                int totalMinute = int(totalSecond / 60);
                int shownMinute = totalMinute % 60;
                int unitMinute = shownMinute % 10;
                int tenthMinute = int(shownMinute / 10);

                int totalHour = int(totalMinute / 60);
                int shownHour = totalHour % 24;
                int unitHour = shownHour % 10;
                int tenthHour = int(shownHour / 10);

                // unitSecond = int(frac(sin(uv.x) * 31582937));
                // tenthSecond = int(frac(sin(uv.x) * 31582937));
                // unitMinute = int(frac(sin(uv.x) * 31582937));
                // tenthMinute = int(frac(sin(uv.x) * 31582937));
                // unitHour = int(frac(sin(uv.x) * 31582937));
                // tenthHour = int(frac(sin(uv.x) * 31582937));

                int timeArray[6] = {tenthHour, unitHour, tenthMinute, unitMinute, tenthSecond, unitSecond};

                float clock = 0;

                float currentX = -0.325;
                float currentY = 0.03;
                for(int i = 0; i < 6; i++) {
                    int isEven = (i+1) % 2; // 是 even 的话 值 为 1，不是的话是 0
                    currentX += 0.2 * isEven;
                    currentX += 0.5;

                    float zeroStatus = checkState(0, timeArray[i]) * drawZero(uv, currentX, currentY);
                    float oneStatus = checkState(1, timeArray[i]) * drawOne(uv, currentX, currentY);
                    float twoStatus = checkState(2, timeArray[i]) * drawTwo(uv, currentX, currentY);
                    float threeStatus = checkState(3, timeArray[i]) * drawThree(uv, currentX, currentY);
                    float fourStatus = checkState(4, timeArray[i]) * drawFour(uv, currentX, currentY);
                    float fiveStatus = checkState(5, timeArray[i]) * drawFive(uv, currentX, currentY);
                    float sixStatus = checkState(6, timeArray[i]) * drawSix(uv, currentX, currentY);
                    float sevenStatus = checkState(7, timeArray[i]) * drawSeven(uv, currentX, currentY);
                    float eightStatus = checkState(8, timeArray[i]) * drawEight(uv, currentX, currentY);
                    float nineStatus = checkState(9, timeArray[i]) * drawNine(uv, currentX, currentY);
                    
                    float finalImage = zeroStatus + oneStatus + twoStatus + threeStatus + fourStatus + fiveStatus + sixStatus + sevenStatus + eightStatus + nineStatus;
                    clock += finalImage;
                }

                float3 clockColor = float3((230 - 245)/255.0, (226 - 245)/255.0, (190 - 245)/255.0);

                float3 background = rectangle(uv, 0, 0, 4, 4.0/6) * float3(245/255.0, 245/255.0, 245/255.0);
                float3 colorBoard1 = rectangle(uv, 0, 0.58 - 4.0/6, 4, 0.1) * float3(200/255.0, 200/255.0, 200/255.0);
                
                float height1 = 0.538888888;
                float3 colorBoard2 = rectangle(uv, 0, 0.58 - height1*2 - 4.0/6, 4, height1*2) * float3(1, 249/255.0, 196/255.0);
                float3 colorBoard3 = rectangle(uv, 0, 0.58 - height1*4 - 4.0/6, 4, height1*2) * float3(207/255.0, 236/255.0, 1);
                float3 colorBoard4 = rectangle(uv, 0, 0.58 - height1*6 - 4.0/6, 4, height1*2) * float3(200/255.0, 1, 240/255.0);

                float2 uvForObj2 = uvBackup * 4;
                uvForObj2.y -= 2.65;
                uvForObj2 = translation(uvForObj2, frac(_Time.y/60.0) * 4 - 4, 0);
                uvForObj2 = rotation(uvForObj2, ((_Time.y%10.0 + tenthSecond*10) / 60.0) * TAU);
                float obj2 = rectangle(uvForObj2, -0.25, -0.25, 0.5, 0.5);
                float3 obj2Color = float3(100, 100, 100);

                float2 uvForObj3 = uvBackup * 4;
                uvForObj3.y -= 1.65;
                uvForObj3 = translation(uvForObj3, frac(_Time.y/60.0/60.0) * 4 - 4, 0);
                uvForObj3 = rotation(uvForObj3, ((_Time.y%10.0 + tenthMinute*10) / 60.0 / 60.0) * TAU);
                float obj3 = rectangle(uvForObj3, -0.15, -0.15, 0.3, 0.3);
                float3 obj3Color = float3(100, 100, 100);

                float2 uvForObj4 = uvBackup * 4;
                uvForObj4.y -= 0.45;
                uvForObj4 = translation(uvForObj4, frac(_Time.y/60.0/60.0/24.0) * 4 - 4, 0);
                uvForObj4 = rotation(uvForObj4, ((_Time.y%10.0 + tenthHour*10) / 60.0 / 60.0 / 24.0) * TAU);
                float obj4 = rectangle(uvForObj4, -0.075, -0.075, 0.15, 0.15);
                float3 obj4Color = float3(100, 100, 100);

                return float4(clock.xxx*clockColor + background + colorBoard1 + colorBoard2 + colorBoard3 + colorBoard4 + obj2*obj2Color + obj3*obj3Color + obj4*obj4Color, 1.0f);
            }
            ENDCG
        }
    }
}

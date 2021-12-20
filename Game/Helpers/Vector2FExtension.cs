using SFML.System;

namespace HunterXSavageness.Game.Helpers;

public static class Vector2FExtension
{
    public static Vector2f Zero { get; } = new(0f, 0f);
    
    public static Vector2f Up { get; } = new(0f, 1f);

    public static Vector2f GetRotationVector(float angle) => Up * angle;
    
    public static float GetRotationAngle(this Vector2f first, Vector2f second)
    {
        float dx = second.X - first.X;
        float dy = second.Y - first.Y;
        return (float) (Math.Atan2(dy, dx) * (180 / Math.PI));
    }

    public static float GetMagnitude(this Vector2f vector) => (float) Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);

    public static float GetSquaredMagnitude(this Vector2f vector) => vector.X * vector.X + vector.Y * vector.Y;
    
    public static Vector2f GetNormalized(this Vector2f vector)
    {
        float magnitude = GetMagnitude(vector);
        return new Vector2f(vector.X / magnitude, vector.Y / magnitude);
    }

    // Implementation taken from the Unity game engine, god bless Unity
    public static Vector2f SmoothDamp(Vector2f current, Vector2f target, ref Vector2f currentVelocity, float smoothTime)
    {
        float deltaTime = GameLoop.DeltaTime;
        const float maxSpeed = Single.PositiveInfinity;
        
        smoothTime = Math.Max(0.0001f, smoothTime);
        float omega = 2 / smoothTime;

        float x = omega * deltaTime;
        float exp = 1 / (1 + x + 0.48f * x * x + 0.235f * x * x * x);

        float changeX = current.X - target.X;
        float changeY = current.Y - target.Y;
        var originalTo = target;
        
        float maxChange = maxSpeed * smoothTime;

        float maxChangeSq = maxChange * maxChange;
        float sqDist = changeX * changeX + changeY * changeY;
        if (sqDist > maxChangeSq)
        {
            var mag = (float) Math.Sqrt(sqDist);
            changeX = changeX / mag * maxChange;
            changeY = changeY / mag * maxChange;
        }

        target.X = current.X - changeX;
        target.Y = current.Y - changeY;

        float tempX = (currentVelocity.X + omega * changeX) * deltaTime;
        float tempY = (currentVelocity.Y + omega * changeY) * deltaTime;

        currentVelocity.X = (currentVelocity.X - omega * tempX) * exp;
        currentVelocity.Y = (currentVelocity.Y - omega * tempY) * exp;

        float outputX = target.X + (changeX + tempX) * exp;
        float outputY = target.Y + (changeY + tempY) * exp;
        
        float origMinusCurrentX = originalTo.X - current.X;
        float origMinusCurrentY = originalTo.Y - current.Y;
        float outMinusOrigX = outputX - originalTo.X;
        float outMinusOrigY = outputY - originalTo.Y;

        if (origMinusCurrentX * outMinusOrigX + origMinusCurrentY * outMinusOrigY > 0)
        {
            outputX = originalTo.X;
            outputY = originalTo.Y;

            currentVelocity.X = (outputX - originalTo.X) / deltaTime;
            currentVelocity.Y = (outputY - originalTo.Y) / deltaTime;
        }
        
        return new Vector2f(outputX, outputY);
    }    
}

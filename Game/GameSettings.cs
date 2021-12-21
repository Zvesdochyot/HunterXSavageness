using HunterXSavageness.Game.Helpers;
using SFML.Graphics;
using SFML.System;

namespace HunterXSavageness.Game;

public class GameSettings
{
    public const uint HareCount = 20, DeerCount = 40, WolfCount = 8;

    public static float FieldRadius { get; private set; }

    public View GameView { get; }

    public Region HaresSpawn { get; }
    
    public Region DeerSpawn { get; }
    
    public Region WolvesSpawn { get; }
    
    public static Vector2f BulletSize { get; private set; }
    
    private static float SideLength { get; set; }
    
    public GameSettings(RenderTarget window)
    {
        const float centerX = 0, centerY = 0;
        FieldRadius = window.Size.X;
        
        const float viewMultiplier = 1.2f;
        GameView = new View(new Vector2f(centerX, centerY),
            new Vector2f(viewMultiplier * window.Size.X, viewMultiplier * window.Size.Y));
        window.SetView(GameView);

        float startX = -window.Size.X / 4f, startY = -window.Size.Y / 4f;
        float endX = window.Size.X / 4f, endY = window.Size.Y / 4f;
        
        HaresSpawn = new Region(startX, centerY, centerX, endY);
        DeerSpawn = new Region(startX, startY, endX, centerY);
        WolvesSpawn = new Region(centerX, centerY, endX, endY);
        
        BulletSize = new Vector2f(window.Size.X / 256f, window.Size.Y / 144f);
        SideLength = window.Size.X / 64f;
    }

    public static float GetTriangleCircumradius() => (float) (Math.Sqrt(3) / 3 * SideLength);

    public static float GetDiagonal() => (float) (2 * (Math.Sqrt(3) / 3) * SideLength);
    
    public static Vector2f GetRandomPointWithinCircle()
    {
        double alpha = 2 * Math.PI * GameLoop.RandomGenerator.NextDouble();

        float radius = FieldRadius * (float) Math.Sqrt(GameLoop.RandomGenerator.NextSingle());

        float x = radius * (float) Math.Cos(alpha);
        float y = radius * (float) Math.Sin(alpha);
        return new Vector2f(x, y);
    }
}

using HunterXSavageness.Game.Helpers;
using SFML.Graphics;
using SFML.System;

namespace HunterXSavageness.Game;

public class GameSettings
{
    public const uint HareCount = 20, DeerCount = 70, WolfCount = 8;

    public static float FieldRadius { get; private set; }

    public static float SideLength { get; private set; }
    
    public View GameView { get; }

    public Region HaresSpawn { get; }
    
    public Region DeerSpawn { get; }
    
    public Region WolvesSpawn { get; }
    
    public GameSettings(RenderTarget window)
    {
        const float centerX = 0, centerY = 0;
        FieldRadius = window.Size.X;
        
        const float viewMultiplier = 1.5f;
        GameView = new View(new Vector2f(centerX, centerY),
            new Vector2f(viewMultiplier * window.Size.X, viewMultiplier * window.Size.Y));
        window.SetView(GameView);

        float startX = -window.Size.X / 1.5f, startY = -window.Size.Y / 1.5f;
        float endX = window.Size.X / 1.5f, endY = window.Size.Y / 1.5f;
        
        HaresSpawn = new Region(startX, centerY, centerX, endY);
        DeerSpawn = new Region(startX, startY, endX, centerY);
        WolvesSpawn = new Region(centerX, centerY, endX, endY);
        
        SideLength = window.Size.X / 64f;
    }

    public static float GetTriangleCircumradius() => (float) (Math.Sqrt(3) / 3 * SideLength);

    public static float GetDiagonal() => (float) (2 * (Math.Sqrt(3) / 3) * SideLength);
}

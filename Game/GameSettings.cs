using HunterXSavageness.Game.Helpers;
using SFML.Graphics;
using SFML.System;

namespace HunterXSavageness.Game;

public class GameSettings
{
    public const uint HareCount = 10, DeerCount = 50, WolfCount = 5;

    public static float FieldWidth { get; private set; }
    
    public static float FieldHeight { get; private set; }
    
    public static float SideLength { get; private set; }
    
    public View GameView { get; }
    
    public Region FieldBorders { get; }
    
    public Region HaresSpawn { get; }
    
    public Region DeerSpawn { get; }
    
    public Region WolvesSpawn { get; }
    
    public GameSettings(RenderTarget window)
    {
        const float centerX = 0, centerY = 0;
        FieldWidth = 2 * window.Size.X;
        FieldHeight = 2 * window.Size.Y;

        const int frameOffset = 200;
        const float viewMultiplier = 2 / 3f;
        GameView = new View(new Vector2f(centerX, centerY),
            new Vector2f(viewMultiplier * window.Size.X, viewMultiplier * window.Size.Y));
        window.SetView(GameView);

        float startX = -window.Size.X, startY = -window.Size.Y;
        float endX = window.Size.X, endY = window.Size.Y;

        const int fieldOffset = 50;
        FieldBorders = new Region(-FieldWidth / 2 - fieldOffset, -FieldHeight / 2 - fieldOffset,
            FieldWidth / 2 + fieldOffset, FieldHeight / 2 + fieldOffset);
        HaresSpawn = new Region(startX, centerY, centerX, endY);
        DeerSpawn = new Region(startX, startY, endX, centerY);
        WolvesSpawn = new Region(centerX, centerY, endX, endY);
        
        SideLength = window.Size.X / 64f;
    }

    public static float GetTriangleCircumradius() => (float) (Math.Sqrt(3) / 3 * SideLength);
}

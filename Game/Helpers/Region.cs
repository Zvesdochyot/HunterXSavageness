using SFML.System;

namespace HunterXSavageness.Game.Helpers;

public class Region
{
    public Vector2f StartPoint { get; }
    
    public Vector2f EndPoint { get; }

    public Region(float startX, float startY, float endX, float endY)
    {
        StartPoint = new Vector2f(startX, startY);
        EndPoint = new Vector2f(endX, endY);
    }
}

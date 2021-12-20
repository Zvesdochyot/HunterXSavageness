using HunterXSavageness.Game.Shapes.Abstractions;
using SFML.Graphics;
using SFML.System;

namespace HunterXSavageness.Game.Shapes;

public sealed class Square : ShapeBase
{
    protected override VertexArray Roots { get; }
    
    public Square(float originX, float originY, float sideLength, uint color)
    {
        Roots = new VertexArray(PrimitiveType.Quads, 4);
        Roots[0] = new Vertex(new Vector2f(originX, originY))
        {
            Color = new Color(color)
        };
        Roots[1] = new Vertex(new Vector2f(originX + sideLength, originY));
        Roots[2] = new Vertex(new Vector2f(originX + sideLength, originY + sideLength));
        Roots[3] = new Vertex(new Vector2f(originX, originY + sideLength));
    }
}

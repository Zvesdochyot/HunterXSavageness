using HunterXSavageness.Game.Shapes.Abstractions;
using SFML.Graphics;
using SFML.System;

namespace HunterXSavageness.Game.Shapes;

public sealed class Triangle : ShapeBase
{
    protected override VertexArray Roots { get; }
    
    public Triangle(float originX, float originY, float sideLength, uint color)
    {
        Roots = new VertexArray(PrimitiveType.Triangles, 3);

        float height = sideLength * ((float) Math.Sqrt(3) / 2);
        Roots[0] = new Vertex(new Vector2f(originX, originY))
        {
            Color = new Color(color)
        };
        Roots[1] = new Vertex(new Vector2f(originX + sideLength / 2, originY + height));
        Roots[2] = new Vertex(new Vector2f(originX - sideLength / 2, originY + height));
    }
}

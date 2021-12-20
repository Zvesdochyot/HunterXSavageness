using SFML.Graphics;

namespace HunterXSavageness.Game.Shapes.Abstractions;

public abstract class ShapeBase : Drawable
{
    public Transformable Movement { get; }
    
    protected abstract VertexArray Roots { get; }

    protected ShapeBase()
    {
        Movement = new Transformable();
    }
    
    public void Draw(RenderTarget target, RenderStates states)
    {
        states.Transform *= Movement.Transform;
        target.Draw(Roots, states);
    }
}

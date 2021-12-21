using SFML.Graphics;
using SFML.System;

namespace HunterXSavageness.Game.Particles.Abstractions;

public abstract class ParticleBase
{
    public abstract Shape GameObject { get; }
    
    public abstract bool IsDestroyed { get; protected set; }

    protected abstract Vector2f InitialPosition { get; set; }
    
    protected abstract Vector2f Destination { get; }
    
    protected abstract float Speed { get; }

    public abstract void FixedUpdate();
}

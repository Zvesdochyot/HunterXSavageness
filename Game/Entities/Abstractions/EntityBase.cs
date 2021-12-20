using SFML.Graphics;
using SFML.System;

namespace HunterXSavageness.Game.Entities.Abstractions;

public abstract class EntityBase
{
    public abstract Shape GameObject { get; }
    
    public abstract EntityType Type { get; }
    
    public abstract Vector2f Velocity { get; }

    protected abstract float WanderingSpeed { get; }
    
    protected abstract float RunningSpeed { get; }
    
    public abstract bool IsDead { get; protected set; }
}

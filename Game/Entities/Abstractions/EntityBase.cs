using HunterXSavageness.Game.Helpers;
using SFML.Graphics;
using SFML.System;

namespace HunterXSavageness.Game.Entities.Abstractions;

public abstract class EntityBase
{
    public abstract Shape GameObject { get; }

    public abstract float WanderingSpeed { get; }
    
    public abstract float RunningSpeed { get; }
    
    public abstract Vector2f Velocity { get; set; }

    public abstract void FixedUpdate();
    
    public abstract bool IsDead { get; protected set; }
    
    protected virtual void HandleIfOutsideCircle()
    {
        float squaredRadius = GameSettings.FieldRadius * GameSettings.FieldRadius;
        if (GameObject.Position.GetSquaredMagnitude() < squaredRadius) return;
        GameObject.Position = Vector2FExtension.Zero; // Start from center, again
    }
}

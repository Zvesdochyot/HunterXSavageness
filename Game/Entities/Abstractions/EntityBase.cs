using HunterXSavageness.Game.Helpers;
using SFML.Graphics;
using SFML.System;

namespace HunterXSavageness.Game.Entities.Abstractions;

public abstract class EntityBase
{
    // wolf collides with entity ->
    // case entity typeof(Player): teleport Player to starting point
    // case entity typeof(NPC): destroy this NPC
    
    // bullet collides with typeof(NPC) ->
    // destroy this NPC
    
    // wolf checks if collides
    // bullet checks if collides
    
    public abstract Shape GameObject { get; }

    public abstract float WanderingSpeed { get; }
    
    public abstract float RunningSpeed { get; }
    
    public abstract Vector2f Velocity { get; set; }

    public abstract bool IsDead { get; set; }
    
    public abstract void FixedUpdate();

    public void MoveToStart()
    {
        GameObject.Position = Vector2FExtension.Zero;
    }
    
    protected virtual void HandleIfOutsideCircle()
    {
        float squaredRadius = GameSettings.FieldRadius * GameSettings.FieldRadius;
        if (GameObject.Position.GetSquaredMagnitude() < squaredRadius) return;
        MoveToStart(); // Start from center, again
    }
}

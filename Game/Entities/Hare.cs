using HunterXSavageness.Game.Behaviors;
using HunterXSavageness.Game.Behaviors.BehaviorScripts;
using HunterXSavageness.Game.Entities.Abstractions;
using HunterXSavageness.Game.Helpers;
using SFML.Graphics;
using SFML.System;

namespace HunterXSavageness.Game.Entities;

public class Hare : NpcBase
{
    public override Shape GameObject { get; }
    
    public override EntityType Type => EntityType.Hare;

    public override Vector2f Velocity { get; } = new();

    protected override float WanderingSpeed => 12f;

    protected override float RunningSpeed => 15f;
    
    public override bool IsDead { get; protected set; }

    protected override Region SpawnArea { get; }
    
    protected override FlockBehaviorBase Behavior { get; } = new AvoidanceBehavior();

    protected override float ActivationRadius { get; }

    public Hare(Region spawnArea)
    {
        SpawnArea = spawnArea;
        
        float spawnX = spawnArea.StartPoint.X + GameLoop.RandomGenerator.NextSingle() * (spawnArea.EndPoint.X - spawnArea.StartPoint.X);
        float spawnY = GameLoop.RandomGenerator.NextSingle() * (spawnArea.EndPoint.Y - spawnArea.StartPoint.Y);

        float triangleRadius = GameSettings.GetTriangleCircumradius();
        GameObject = new CircleShape(triangleRadius, 3)
        {
            Position = new Vector2f(spawnX, spawnY),
            Origin = new Vector2f(triangleRadius, triangleRadius),
            FillColor = GameRenderer.FieldColor,
            OutlineColor = Color.Blue,
            OutlineThickness = 2f
        };
    }
    
    public override void FixedUpdate()
    {
        return;
    }
}

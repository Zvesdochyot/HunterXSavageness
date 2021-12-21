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
    
    public override NpcType Type => NpcType.Hare;

    public override float WanderingSpeed => 0.6f;

    public override float RunningSpeed => 1.4f;
    
    public override Vector2f Velocity { get; set; }
    
    public override bool IsDead { get; protected set; }

    public override float ActivationRadius { get; } = 150 * GameSettings.GetDiagonal() * GameSettings.GetDiagonal();
    
    protected override Region SpawnArea { get; }
    
    protected override FlockBehaviorBase Behavior { get; } = new CompositeBehavior(
        new FlockBehaviorBase[] { new SearchVictimBehavior(), new AvoidEveryoneBehavior(), new StayInCircleBehavior() },
        new[] { 1f, 1f, 1f });

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
        HandleIfOutsideCircle();
        GameObject.Position += Velocity * GameLoop.DeltaTime;
    }
}

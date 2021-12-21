using HunterXSavageness.Game.Behaviors;
using HunterXSavageness.Game.Behaviors.BehaviorScripts;
using HunterXSavageness.Game.Entities.Abstractions;
using HunterXSavageness.Game.Helpers;
using SFML.Graphics;
using SFML.System;

namespace HunterXSavageness.Game.Entities;

public sealed class Deer : NpcBase
{
    public override Shape GameObject { get; }

    public override EntityType Type => EntityType.Deer;

    public override Vector2f Velocity { get; set; }

    protected override float WanderingSpeed => 50f;

    protected override float RunningSpeed => 75f;
    
    public override bool IsDead { get; protected set; }

    public override float ActivationRadius { get; } = 100 * GameSettings.GetDiagonal() * GameSettings.GetDiagonal();
    
    protected override Region SpawnArea { get; }

    protected override FlockBehaviorBase Behavior { get; } = new CompositeBehavior(
        new FlockBehaviorBase[] { new AvoidanceBehavior(), new AlignmentBehavior(), new AlignmentBehavior(), new AvoidWolvesBehavior() },
        new[] { 3f, 2f, 5f, 3f });

    public Deer(Region spawnArea)
    {
        SpawnArea = spawnArea;

        float spawnX = spawnArea.StartPoint.X + GameLoop.RandomGenerator.NextSingle() * (spawnArea.EndPoint.X - spawnArea.StartPoint.X);
        float spawnY = spawnArea.StartPoint.Y + GameLoop.RandomGenerator.NextSingle() * (spawnArea.EndPoint.Y - spawnArea.StartPoint.Y);

        float triangleRadius = GameSettings.GetTriangleCircumradius();
        GameObject = new CircleShape(triangleRadius, 3)
        {
            Position = new Vector2f(spawnX, spawnY),
            Origin = new Vector2f(triangleRadius, triangleRadius),
            FillColor = GameRenderer.FieldColor,
            OutlineColor = Color.Green,
            OutlineThickness = 2f
        };
    }
    
    public override void FixedUpdate()
    {
        GameObject.Position += Velocity * GameLoop.DeltaTime;
    }
}

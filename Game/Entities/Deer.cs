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

    public override Vector2f Velocity { get; } = new();

    protected override float WanderingSpeed => 8f;

    protected override float RunningSpeed => 10f;

    protected override Region SpawnArea { get; }

    protected override FlockBehaviorBase Behavior { get; } = new CompositeBehavior(
        new FlockBehaviorBase[] { new AvoidanceBehavior(), new SteeredCohesionBehavior(), new AlignmentBehavior(), new AvoidWolvesBehavior() },
        new[] { 10f, 3f, 3f, 5f });
    
    protected override float ActivationRadius { get; }

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
}

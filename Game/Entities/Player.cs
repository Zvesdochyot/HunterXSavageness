using HunterXSavageness.Game.Entities.Abstractions;
using HunterXSavageness.Game.Guns;
using HunterXSavageness.Game.Guns.Abstractions;
using HunterXSavageness.Game.Helpers;
using SFML.Graphics;
using SFML.System;

namespace HunterXSavageness.Game.Entities;

public class Player : EntityBase
{
    public override Shape GameObject { get; }
    
    public override float WanderingSpeed => 4000f;
    
    public override float RunningSpeed => 40f; // Let it be for future mechanics
    
    public override Vector2f Velocity { get; set; } = new();
    
    public override bool IsDead { get; protected set; } = false;

    public GunBase Gun { get; }
    
    public Player()
    {
        float triangleRadius = GameSettings.GetTriangleCircumradius();
        GameObject = new CircleShape(triangleRadius, 3)
        {
            Position = new Vector2f(), // Center of the map
            Origin = new Vector2f(triangleRadius, triangleRadius), // Origin in the center of the triangle  
            FillColor = GameRenderer.FieldColor,
            OutlineColor = Color.Magenta,
            OutlineThickness = 2f
        };

        Gun = new Pistol();
    }
    
    public override void FixedUpdate()
    {
        HandleIfOutsideCircle();
    }
    
    public void HandleRotation(Vector2f mousePosition)
    {
        GameObject.Rotation = GameObject.Position.GetRotationAngle(mousePosition) + 90; // + 90 because of triangle shape
    }
    
    public void HandleMovement(Vector2f mousePosition)
    {
        var destinationPoint = mousePosition - GameObject.Position;
        if (destinationPoint.GetMagnitude() < 2f)
        {
            // Do nothing, we are already at destination point
            return;
        }
        
        float length = destinationPoint.GetMagnitude();
        var unitVector = destinationPoint / length;
        GameObject.Position += WanderingSpeed * GameLoop.DeltaTime * unitVector;
    }

    public void HandleShooting(Vector2f destination)
    {
        var destinationPoint = destination - GameObject.Position;
        float difference = destinationPoint.GetMagnitude() - Gun.ShootingRange;
        
        if (difference > 0f)
        {
            destinationPoint = destinationPoint.GetNormalized() * Gun.ShootingRange;
        }

        destinationPoint += GameObject.Position; // Restore offset
        Gun.ShootOnce(GameObject.Position, destinationPoint);
    }
}

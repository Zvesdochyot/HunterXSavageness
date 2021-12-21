using HunterXSavageness.Game.Entities.Abstractions;
using HunterXSavageness.Game.Guns.Abstractions;
using HunterXSavageness.Game.Helpers;
using HunterXSavageness.Game.Particles.Abstractions;
using SFML.Graphics;
using SFML.System;

namespace HunterXSavageness.Game.Particles;

public sealed class Bullet : ParticleBase
{
    public override Shape GameObject { get; }
    
    public override bool IsDestroyed { get; set; }
    
    protected override Vector2f InitialPosition { get; set; }
    
    protected override Vector2f Destination { get; }

    protected override float Speed => 300f;
    
    private readonly GunBase _gun;
    
    public Bullet(Vector2f initialPosition, Vector2f destination, GunBase gun)
    {
        GameObject = new RectangleShape(GameSettings.BulletSize)
        {
            Position = initialPosition,
            FillColor = Color.Yellow
        };

        InitialPosition = initialPosition;
        Destination = destination;
        _gun = gun;
    }
    
    public override void FixedUpdate()
    {
        HandleIfOutsideCircle();

        var destinationPoint = Destination - InitialPosition;
        var velocity = destinationPoint.GetNormalized() * Speed;
        
        var deltaVelocity = velocity * GameLoop.DeltaTime;
        if (destinationPoint.GetMagnitude() < deltaVelocity.GetMagnitude())
        {
            IsDestroyed = true;
        }
        else
        {
            GameObject.Position += deltaVelocity;
            InitialPosition = GameObject.Position;
        }
        
        if (IsDestroyed)
        {
            RemoveBullet();
        }
    }
    
    public List<NpcBase> GetNearbyEntities(List<NpcBase> entities)
    {
        var context = new List<NpcBase>();
        var boundingBulletBox = GameObject.GetGlobalBounds();

        foreach (var entity in entities)
        {
            if (boundingBulletBox.Intersects(entity.GameObject.GetGlobalBounds()))
            {
                context.Add(entity);
            }
        }
        
        return context;
    }
    
    private void HandleIfOutsideCircle()
    {
        float squaredRadius = GameSettings.FieldRadius * GameSettings.FieldRadius;
        if (GameObject.Position.GetSquaredMagnitude() < squaredRadius) return;
        IsDestroyed = true;
    }

    private void RemoveBullet()
    {
        _gun.FiredBullets.Remove(this);
        GameObject.Dispose();
    }
}

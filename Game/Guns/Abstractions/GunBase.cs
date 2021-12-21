using HunterXSavageness.Game.Particles;
using SFML.System;

namespace HunterXSavageness.Game.Guns.Abstractions;

public abstract class GunBase
{
    public abstract float ShootingRange { get; }

    public abstract List<Bullet> FiredBullets { get; }

    protected abstract uint BulletCount { get; set; }
    
    public abstract void ShootOnce(Vector2f initialPosition, Vector2f destination);
}

using HunterXSavageness.Game.Guns.Abstractions;
using HunterXSavageness.Game.Particles;
using SFML.System;

namespace HunterXSavageness.Game.Guns;

public class Pistol : GunBase
{
    public override float ShootingRange => 1000f;
    
    public override List<Bullet> FiredBullets => _firedBullets;

    protected override uint BulletCount { get; set; } = 66;
    
    private volatile List<Bullet> _firedBullets = new();
    
    public override void ShootOnce(Vector2f initialPosition, Vector2f destination)
    {
        if (BulletCount == 0)
        {
            return;
        }

        --BulletCount;
        _firedBullets.Add(new Bullet(initialPosition, destination, this));
    }
}

using HunterXSavageness.Game.Guns.Abstractions;
using HunterXSavageness.Game.Particles;
using SFML.System;

namespace HunterXSavageness.Game.Guns;

public class Pistol : GunBase
{
    public override float ShootingRange => 1000f;

    public override List<Bullet> FiredBullets { get; } = new();

    protected override uint BulletCount { get; set; } = 66;

    public override void ShootOnce(Vector2f initialPosition, Vector2f destination)
    {
        if (BulletCount == 0)
        {
            return;
        }

        --BulletCount;
        FiredBullets.Add(new Bullet(initialPosition, destination, this));
    }
}

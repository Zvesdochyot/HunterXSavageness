using HunterXSavageness.Game.Entities.Abstractions;
using HunterXSavageness.Game.Helpers;
using SFML.System;

namespace HunterXSavageness.Game.Behaviors;

public class FlockAgent
{
    public NpcBase Entity { get; }
    
    public FlockAgent(NpcBase npcEntity)
    {
        Entity = npcEntity;
    }
    
    public void Move(Vector2f velocity)
    {
        if (velocity != Vector2FExtension.Zero)
        {
            Entity.GameObject.Rotation = Entity.Velocity.GetRotationAngle(velocity) + 90; // + 90 because of triangle shape
        }
        Entity.Velocity = velocity;
    }
}

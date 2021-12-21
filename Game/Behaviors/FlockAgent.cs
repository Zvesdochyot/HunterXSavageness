using HunterXSavageness.Game.Entities;
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
        Entity.Velocity = velocity;
    }
}

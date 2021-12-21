using HunterXSavageness.Game.Entities.Abstractions;
using SFML.System;

namespace HunterXSavageness.Game.Behaviors.BehaviorScripts;

public class AlignmentBehavior : FlockBehaviorBase
{
    public override Vector2f CalculateMove(FlockAgent agent, List<NpcBase> context)
    {
        if (!context.Any())
        {
            return agent.Entity.Velocity;
        }
        
        var alignmentMove = new Vector2f();
        
        int groupCount = 0;
        foreach (var other in context.Where(other => agent.Entity.Type == other.Type))
        {
            alignmentMove += other.Velocity;
            ++groupCount;
        }

        if (groupCount == 0)
        {
            return agent.Entity.Velocity;
        }
        
        alignmentMove /= groupCount;
        return alignmentMove * agent.Entity.WanderingSpeed;
    }
}

using HunterXSavageness.Game.Entities.Abstractions;
using HunterXSavageness.Game.Helpers;
using SFML.System;

namespace HunterXSavageness.Game.Behaviors.BehaviorScripts;

public class AvoidanceBehavior : FlockBehaviorBase
{
    public override Vector2f CalculateMove(FlockAgent agent, List<NpcBase> context)
    {
        var avoidanceMove = new Vector2f();

        if (context.Count == 0)
        {
            return avoidanceMove;
        }
        
        int groupCount = 0;
        foreach (var other in context.Where(other => agent.Entity.Type == other.Type))
        {
            var resultingVector = other.GameObject.Position - agent.Entity.GameObject.Position;
            if (resultingVector.GetSquaredMagnitude() >= agent.Entity.SquareAvoidanceRadius) continue;
            avoidanceMove += -resultingVector;
            ++groupCount;
        }

        if (groupCount == 0)
        {
            return avoidanceMove;
        }
        
        avoidanceMove /= groupCount;
        return avoidanceMove;
    }
}

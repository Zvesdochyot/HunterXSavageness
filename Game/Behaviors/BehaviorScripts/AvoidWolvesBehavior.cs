using HunterXSavageness.Game.Entities;
using HunterXSavageness.Game.Entities.Abstractions;
using HunterXSavageness.Game.Helpers;
using SFML.System;

namespace HunterXSavageness.Game.Behaviors.BehaviorScripts;

public class AvoidWolvesBehavior : FlockBehaviorBase
{
    private const float SpeedMultiplayer = 2f;  
    
    public override Vector2f CalculateMove(FlockAgent agent, List<NpcBase> context)
    {
        var avoidanceMove = new Vector2f();

        if (context.Count == 0)
        {
            return avoidanceMove;
        }
        
        int groupCount = 0;
        foreach (var other in context.Where(other => other.Type == EntityType.Wolf))
        {
            var resultingVector = agent.Entity.GameObject.Position - other.GameObject.Position;
            if (resultingVector.GetSquaredMagnitude() >= agent.Entity.SquareAvoidanceRadius) continue;
            avoidanceMove += resultingVector;
            ++groupCount;
        }

        if (groupCount == 0)
        {
            return avoidanceMove;
        }
        
        // avoidanceMove /= groupCount;
        return avoidanceMove * SpeedMultiplayer;
    }
}
using HunterXSavageness.Game.Entities.Abstractions;
using HunterXSavageness.Game.Helpers;
using SFML.System;

namespace HunterXSavageness.Game.Behaviors.BehaviorScripts;

public class SteeredCohesionBehavior : FlockBehaviorBase
{
    private Vector2f _currentVelocity;
    private const float AgentSmoothTime = 0.5f;
    
    public override Vector2f CalculateMove(FlockAgent agent, List<NpcBase> context)
    {
        var cohesionMove = new Vector2f();
        
        if (!context.Any())
        {
            return cohesionMove;
        }
        
        int groupCount = 0;
        foreach (var other in context.Where(other => agent.Entity.Type == other.Type))
        {
            cohesionMove += other.GameObject.Position;
            ++groupCount;
        }

        if (groupCount == 0)
        {
            return cohesionMove;
        }
        
        cohesionMove /= groupCount;
        cohesionMove = Vector2FExtension.SmoothDamp(agent.Entity.Velocity, cohesionMove, ref _currentVelocity, AgentSmoothTime);
        return cohesionMove;
    }
}

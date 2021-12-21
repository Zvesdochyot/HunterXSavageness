using HunterXSavageness.Game.Entities.Abstractions;
using HunterXSavageness.Game.Helpers;
using SFML.System;

namespace HunterXSavageness.Game.Behaviors.BehaviorScripts;

public class SearchVictimBehavior : FlockBehaviorBase
{
    private Vector2f _destinationPoint = GameSettings.GetRandomPointWithinCircle();
    
    public override Vector2f CalculateMove(FlockAgent agent, List<NpcBase> context)
    {
        var distance = _destinationPoint - agent.Entity.GameObject.Position;
        var velocity = distance.GetNormalized();
        
        if (distance.GetMagnitude() < 2 * velocity.GetMagnitude())
        {
            _destinationPoint = GameSettings.GetRandomPointWithinCircle();
        }
        
        return velocity * agent.Entity.WanderingSpeed;
    }
}

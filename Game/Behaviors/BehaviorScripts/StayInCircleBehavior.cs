using HunterXSavageness.Game.Entities.Abstractions;
using HunterXSavageness.Game.Helpers;
using SFML.System;

namespace HunterXSavageness.Game.Behaviors.BehaviorScripts;

public class StayInCircleBehavior : FlockBehaviorBase
{
    private readonly Vector2f _circleCenter = Vector2FExtension.Zero;
    private readonly float _circleRadius = GameSettings.FieldRadius;
    
    public override Vector2f CalculateMove(FlockAgent agent, List<NpcBase> context)
    {
        var centerOffset = _circleCenter - agent.Entity.GameObject.Position;
        
        float t = centerOffset.GetMagnitude() / _circleRadius;
        if (t < 0.9f)
        {
            return Vector2FExtension.Zero;
        }

        return centerOffset * t * t * agent.Entity.WanderingSpeed;
    }
}

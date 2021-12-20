using HunterXSavageness.Game.Entities.Abstractions;
using HunterXSavageness.Game.Helpers;
using SFML.System;

namespace HunterXSavageness.Game.Behaviors.BehaviorScripts;

public class CompositeBehavior : FlockBehaviorBase
{
    private readonly FlockBehaviorBase[] _behaviors;
    private readonly float[] _weights;

    public CompositeBehavior(FlockBehaviorBase[] behaviors, float[] weights)
    {
        _behaviors = behaviors;
        _weights = weights;
    }
    
    public override Vector2f CalculateMove(FlockAgent agent, List<NpcBase> context)
    {
        var compositeMove = new Vector2f();
        
        if (_behaviors.Length != _weights.Length)
        {
            return compositeMove;
        }

        for (int i = 0; i < _behaviors.Length; i++)
        {
            var partialMove = _behaviors[i].CalculateMove(agent, context) * _weights[i];

            if (partialMove == Vector2FExtension.Zero) continue;
            if (partialMove.GetSquaredMagnitude() > _weights[i] * _weights[i])
            {
                partialMove = partialMove.GetNormalized();
                partialMove *= _weights[i];
            }

            compositeMove += partialMove;
        }

        return compositeMove;
    }
}

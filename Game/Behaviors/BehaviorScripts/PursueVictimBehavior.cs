using HunterXSavageness.Game.Entities.Abstractions;
using SFML.System;

namespace HunterXSavageness.Game.Behaviors.BehaviorScripts;

public class PursueVictimBehavior : FlockBehaviorBase
{
    public override Vector2f CalculateMove(FlockAgent agent, List<NpcBase> context)
    {
        return new Vector2f();
    }
}

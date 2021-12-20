using HunterXSavageness.Game.Entities.Abstractions;
using SFML.System;

namespace HunterXSavageness.Game.Behaviors;

public abstract class FlockBehaviorBase
{
    public abstract Vector2f CalculateMove(FlockAgent agent, List<NpcBase> context);
}

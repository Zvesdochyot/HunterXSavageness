using HunterXSavageness.Game.Behaviors;
using HunterXSavageness.Game.Helpers;

namespace HunterXSavageness.Game.Entities.Abstractions;

public abstract class NpcBase : EntityBase
{
    public float SquareAvoidanceRadius { get; }
    
    protected abstract Region SpawnArea { get; }
    
    protected abstract FlockBehaviorBase Behavior { get; }
    
    protected abstract float ActivationRadius { get; }

    private readonly FlockAgent _agent;
    private static readonly List<FlockAgent> Agents = new();
    
    private const float MaxSpeed = 50f;
    private const float DriveFactor = 5f;
    private const float SquareMaxSpeed = MaxSpeed * MaxSpeed;

    private const float NeighborRadius = 2f;
    private readonly float _avoidanceRadiusMultiplier = 2.5f * GameSettings.SideLength;

    protected NpcBase()
    {
        SquareAvoidanceRadius = _avoidanceRadiusMultiplier * _avoidanceRadiusMultiplier;
        
        _agent = new FlockAgent(this);
        Agents.Add(_agent);
    }

    public void Update()
    {
        var context = GetNearbyEntities(_agent);
        var move = Behavior.CalculateMove(_agent, context);
        move *= DriveFactor;
        if (move.GetSquaredMagnitude() > SquareMaxSpeed)
        {
            move = move.GetNormalized() * MaxSpeed;
        }
        _agent.Move(move);
    }

    private static List<NpcBase> GetNearbyEntities(FlockAgent agent)
    {
        var context = new List<NpcBase>();
        var boundingBox = agent.Entity.GameObject.GetGlobalBounds();
        // Expand the boundaries a bit for more greedy finding neighbors
        boundingBox.Left -= NeighborRadius;
        boundingBox.Top -= NeighborRadius;
        boundingBox.Width += 2 * NeighborRadius;
        boundingBox.Height += 2 * NeighborRadius;
        
        foreach (var other in Agents)
        {
            if (agent == other) continue;
            if (boundingBox.Intersects(other.Entity.GameObject.GetGlobalBounds()))
            {
                context.Add(other.Entity);
            }
        }
        return context;
    }
}

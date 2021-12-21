using HunterXSavageness.Game.Behaviors;
using HunterXSavageness.Game.Helpers;

namespace HunterXSavageness.Game.Entities.Abstractions;

public abstract class NpcBase : EntityBase
{
    public float SquaredFriendlyAvoidanceRadius => FriendlyCrossingThreshold * FriendlyCrossingThreshold;

    public abstract NpcType Type { get; }
    
    public abstract float ActivationRadius { get; }

    protected abstract Region SpawnArea { get; }

    protected abstract FlockBehaviorBase Behavior { get; }
    
    private readonly FlockAgent _agent;
    private static readonly List<FlockAgent> Agents = new();
    
    private const float DriveFactor = 30f;
    private const float MaxSpeed = 52f;
    private const float SquaredMaxSpeed = MaxSpeed * MaxSpeed;

    private const float CrossingThreshold = 1.5f;
    private static readonly float ContextCrossingThreshold = 2 * CrossingThreshold * GameSettings.GetDiagonal();
    private static readonly float FriendlyCrossingThreshold = CrossingThreshold * GameSettings.GetDiagonal() * GameSettings.GetDiagonal();

    protected NpcBase()
    {
        _agent = new FlockAgent(this);
        Agents.Add(_agent);
    }

    public void FixedUpdateAgent()
    {
        var context = GetNearbyEntities(this);
        var move = Behavior.CalculateMove(_agent, context);
        move *= DriveFactor;
        
        if (move.GetSquaredMagnitude() > SquaredMaxSpeed)
        {
            move = move.GetNormalized() * MaxSpeed;
        }

        _agent.Move(move);
    }
    
    protected override void HandleIfOutsideCircle()
    {
        float squaredRadius = GameSettings.FieldRadius * GameSettings.FieldRadius;
        if (GameObject.Position.GetSquaredMagnitude() < squaredRadius) return;
        IsDead = true;
        RemoveAgent(this);
    }
    
    public static void RemoveAgent(NpcBase agentToDestroy)
    {
        Agents.Remove(agentToDestroy._agent);
        agentToDestroy.GameObject.Dispose();
    }
    
    public static List<NpcBase> GetNearbyEntities(NpcBase agent)
    {
        var context = new List<NpcBase>();
        var boundingBox = agent.GameObject.GetGlobalBounds();
        // Expand the boundaries a bit for more greedy finding neighbors
        boundingBox.Left -= ContextCrossingThreshold;
        boundingBox.Top -= ContextCrossingThreshold;
        boundingBox.Width += 2 * ContextCrossingThreshold;
        boundingBox.Height += 2 * ContextCrossingThreshold;
        
        foreach (var other in Agents)
        {
            if (agent == other.Entity) continue;
            if (boundingBox.Intersects(other.Entity.GameObject.GetGlobalBounds()))
            {
                context.Add(other.Entity);
            }
        }
        return context;
    }
}

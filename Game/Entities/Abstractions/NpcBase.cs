using HunterXSavageness.Game.Behaviors;
using HunterXSavageness.Game.Helpers;

namespace HunterXSavageness.Game.Entities.Abstractions;

public abstract class NpcBase : EntityBase
{
    public float SquaredAvoidanceRadius => CrossingThreshold * _crossingThresholdMultiplier * _crossingThresholdMultiplier;
    
    public abstract float ActivationRadius { get; }

    protected abstract Region SpawnArea { get; }

    protected abstract FlockBehaviorBase Behavior { get; }
    
    private readonly FlockAgent _agent;
    private static readonly List<FlockAgent> Agents = new();
    
    private const float DriveFactor = 30f;
    private const float MaxSpeed = 52f;
    private const float SquaredMaxSpeed = MaxSpeed * MaxSpeed;

    private const float CrossingThreshold = 20f;
    private readonly float _crossingThresholdMultiplier = GameSettings.GetDiagonal();

    protected NpcBase()
    {
        _agent = new FlockAgent(this);
        Agents.Add(_agent);
    }

    public void FixedUpdateAgent()
    {
        var context = GetNearbyEntities(_agent);
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
    
    protected static void RemoveAgent(NpcBase agentToDestroy)
    {
        Agents.Remove(agentToDestroy._agent);
        agentToDestroy.GameObject.Dispose();
    }
    
    private static List<NpcBase> GetNearbyEntities(FlockAgent agent)
    {
        var context = new List<NpcBase>();
        var boundingBox = agent.Entity.GameObject.GetGlobalBounds();
        // Expand the boundaries a bit for more greedy finding neighbors
        boundingBox.Left -= CrossingThreshold;
        boundingBox.Top -= CrossingThreshold;
        boundingBox.Width += 2 * CrossingThreshold;
        boundingBox.Height += 2 * CrossingThreshold;
        
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

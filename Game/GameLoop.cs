using HunterXSavageness.Game.Entities;
using HunterXSavageness.Game.Entities.Abstractions;
using HunterXSavageness.Game.Particles;
using HunterXSavageness.Game.Particles.Abstractions;
using SFML.Graphics;
using SFML.System;

namespace HunterXSavageness.Game;

public class GameLoop
{
    public const float DeltaTime = 1 / 60f;
    
    public static Random RandomGenerator { get; } = new();

    private readonly RenderWindow _window;
    private readonly GameSettings _settings;
    private readonly Shape _fieldBorders;

    private readonly Player _player;
    private readonly List<EntityBase> _entities = new(64);

    private Vector2f _lastRecordedMousePosition;
    
    public GameLoop(RenderWindow window)
    {
        _window = window;
        _settings = new GameSettings(window);
        
        _fieldBorders = new CircleShape(GameSettings.FieldRadius, 200)
        {
            Position = new Vector2f(-GameSettings.FieldRadius, -GameSettings.FieldRadius),
            FillColor = GameRenderer.FieldColor
        };

        _player = new Player();
        
        _entities.Add(_player);

        for (int i = 0; i < GameSettings.HareCount; i++)
        {
            _entities.Add(new Hare(_settings.HaresSpawn));
        }
        
        for (int i = 0; i < GameSettings.DeerCount; i++)
        {
            _entities.Add(new Deer(_settings.DeerSpawn));
        }
        
        for (int i = 0; i < GameSettings.WolfCount; i++)
        {
            _entities.Add(new Wolf(_settings.WolvesSpawn));
        }
    }
    
    public void FixedUpdate()
    {
        while (_window.IsOpen)
        {
            _entities.ForEach(target => (target as NpcBase)?.FixedUpdateAgent());
            _entities.ForEach(target => target.FixedUpdate());

            var wolfTargets = _entities.OfType<Wolf>();
            foreach (var target in wolfTargets)
            {
                var entities = NpcBase.GetNearbyEntities(target)
                    .Where(entity => entity.Type != NpcType.Wolf && !entity.IsDead).ToList();
                
                if (entities.Count == 0) continue;
                var targetBound = target.GameObject.GetGlobalBounds();

                for (int i = 0; i < entities.Count; i++)
                {
                    if (entities[i].GameObject.GetGlobalBounds().Intersects(targetBound))
                    {
                        entities[i].IsDead = true;
                        NpcBase.RemoveAgent(entities[i]);
                    }
                }
            }

            _entities.RemoveAll(target => target.IsDead);

            for (int i = 0; i < _player.Gun.FiredBullets.Count; i++)
            {
                _player.Gun.FiredBullets[i].FixedUpdate();
            }
            
            var npcs = _entities.OfType<NpcBase>().ToList();
            foreach (var target in _player.Gun.FiredBullets)
            {
                var entities = target.GetNearbyEntities(npcs);

                if (entities.Count == 0) continue;
                var targetBound = target.GameObject.GetGlobalBounds();

                for (int i = 0; i < entities.Count; i++)
                {
                    if (entities[i].GameObject.GetGlobalBounds().Intersects(targetBound))
                    {
                        entities[i].IsDead = true;
                        target.IsDestroyed = true;
                        npcs.Remove(entities[i]);
                        NpcBase.RemoveAgent(entities[i]);
                        break;
                    }
                }
            }
            
            _entities.RemoveAll(target => target.IsDead);
            _player.Gun.FiredBullets.RemoveAll(target => target.IsDestroyed);
            
            var allTargets = _entities.Select(target => target.GameObject)
                .Concat(_player.Gun.FiredBullets.Select(target => target.GameObject));
            GameRenderer.RenderFrame(_window, allTargets, _fieldBorders);
        }
    }

    public void OnMouseMoved(Vector2f mousePosition)
    {
        _lastRecordedMousePosition = mousePosition;
        _player.HandleRotation(mousePosition);
    }

    public void OnMouseKeyPressed(Vector2f mousePosition)
    {
        _player.HandleShooting(mousePosition);
    }
    
    public void OnKeyboardKeyPressed()
    {
        _player.HandleMovement(_lastRecordedMousePosition);
        _settings.GameView.Center = _player.GameObject.Position;
        _window.SetView(_settings.GameView);
    }
}

using HunterXSavageness.Game.Entities;
using HunterXSavageness.Game.Entities.Abstractions;
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

            _entities.RemoveAll(target => target.IsDead);
            
            GameRenderer.RenderFrame(_window, _entities.Select(target => target.GameObject), _fieldBorders);
        }
    }

    public void OnMouseMoved(Vector2f mousePosition)
    {
        _lastRecordedMousePosition = mousePosition;
        _player.HandleRotation(mousePosition);
    }

    public void OnKeyPressed()
    {
        _player.HandleMovement(_lastRecordedMousePosition);
        _settings.GameView.Center = _player.GameObject.Position;
        _window.SetView(_settings.GameView);
    }
}

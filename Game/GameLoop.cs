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
    private readonly Hare[] _hares;
    private readonly Deer[] _deer;
    private readonly Wolf[] _wolves;
    
    private readonly List<NpcBase> _npcs;
    private readonly List<Shape> _renderTargets;
    
    public GameLoop(RenderWindow window)
    {
        _window = window;
        _settings = new GameSettings(window);
        _fieldBorders = new RectangleShape(_settings.FieldBorders.EndPoint - _settings.FieldBorders.StartPoint)
        {
            Position = _settings.FieldBorders.StartPoint,
            FillColor = GameRenderer.FieldColor
        };
            
        _player = new Player();
        
        _hares = new Hare[GameSettings.HareCount];
        for (int i = 0; i < GameSettings.HareCount; i++)
        {
            _hares[i] = new Hare(_settings.HaresSpawn);
        }
        
        _deer = new Deer[GameSettings.DeerCount];
        for (int i = 0; i < GameSettings.DeerCount; i++)
        {
            _deer[i] = new Deer(_settings.DeerSpawn);
        }
        
        _wolves = new Wolf[GameSettings.WolfCount];
        for (int i = 0; i < GameSettings.WolfCount; i++)
        {
            _wolves[i] = new Wolf(_settings.WolvesSpawn);
        }
        
        _renderTargets = new List<Shape> { _fieldBorders, _player.GameObject }
            .Concat(_hares.Select(target => target.GameObject))
            .Concat(_wolves.Select(target => target.GameObject))
            .Concat(_deer.Select(target => target.GameObject)).ToList();
        _npcs = new List<NpcBase>().Concat(_hares).Concat(_wolves).Concat(_deer).ToList();

    }
    
    public void FixedUpdate()
    {
        while (_window.IsOpen)
        {
            _npcs.ForEach(target => target.Update());
            
            GameRenderer.RenderFrame(_window, _renderTargets);
        }
    }

    public void OnMouseMoved(Vector2f mousePosition) => _player.HandleRotation(mousePosition);

    public void OnKeyPressed()
    {
        _player.HandleMovement();
        _settings.GameView.Center = _player.GameObject.Position;
        _window.SetView(_settings.GameView);
    }
}

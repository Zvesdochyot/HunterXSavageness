using HunterXSavageness.Game;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace HunterXSavageness;

internal static class GameClient
{
    private const string WindowName = "HunterXSavageness";
    private static readonly ContextSettings DefaultSettings = new() { AntialiasingLevel = 8 };

    private static void Main(string[] args)
    {
        var gameWindow = new RenderWindow(VideoMode.DesktopMode, WindowName, Styles.Fullscreen, DefaultSettings);

        gameWindow.SetFramerateLimit(60);

        var gameLoop = new GameLoop(gameWindow);
        
        gameWindow.InitEventHandlers(gameLoop);
        
        gameWindow.SetActive(false);

        var calcRenderThread = new Thread(gameLoop.FixedUpdate);
        calcRenderThread.Start();

        while (gameWindow.IsOpen)
        {
            gameWindow.DispatchEvents();
        }
    }

    private static void InitEventHandlers(this RenderWindow window, GameLoop worker)
    {
        window.Closed += (_, _) => window.Close();
        
        window.KeyPressed += (_, eventArgs) =>
        {
            switch (eventArgs.Code)
            {
                case Keyboard.Key.Escape:
                    window.Close();
                    break;
            }
        };
        
        window.MouseMoved += (_, eventArgs) =>
        {
            var convertedVector = window.MapPixelToCoords(new Vector2i(eventArgs.X, eventArgs.Y));
            worker.OnMouseMoved(convertedVector);
        };
        
        window.MouseButtonPressed += (_, eventArgs) =>
        {
            switch (eventArgs.Button)
            {
                case Mouse.Button.Left:
                    var convertedVector = window.MapPixelToCoords(new Vector2i(eventArgs.X, eventArgs.Y));
                    worker.OnMouseKeyPressed(convertedVector);
                    break;
            }
        };
        
        window.KeyPressed += (_, eventArgs) =>
        {
            switch (eventArgs.Code)
            {
                case Keyboard.Key.W:
                    worker.OnKeyboardKeyPressed();
                    break;
            }
        };
    }
}

using Godot;
using System.Collections.Generic;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }
    public Dictionary<string, object> Scenes { get; set; } = new Dictionary<string, object>
        {
            { "Main", GD.Load<PackedScene>("res://Scenes/Main/Main.tscn") },
            { "Game", GD.Load<PackedScene>("res://Scenes/Game/Game.tscn") }
        };

    public override void _Ready()
    {
        Instance = this;
        ProcessMode = ProcessModeEnum.Always; // prevent game manager from pausing
    }

    public static void Load(string sceneName)
    {
        if (Instance.Scenes.TryGetValue(sceneName, out var scene))
        {
            Instance.GetTree().ChangeSceneToPacked((PackedScene)scene);
            return;
        }
        GD.PrintErr($"Scene '{sceneName}' not found in Scenes.");
    }
}

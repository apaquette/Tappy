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
    
    private PackedScene _complexChangeScene = GD.Load<PackedScene>("res://Scenes/Changes/ComplexChange.tscn");
    private PackedScene _nextScene;
    private ComplexChange _complexChange;

    public override void _Ready()
    {
        Instance = this;
        _complexChange = _complexChangeScene.Instantiate<ComplexChange>();
        AddChild(_complexChange);
        ProcessMode = ProcessModeEnum.Always; // prevent game manager from pausing
    }

    public static void Load(string sceneName)
    {
        if (Instance.Scenes.TryGetValue(sceneName, out var scene))
        {
            Instance._nextScene = (PackedScene)scene;
            Instance._complexChange.PlayAnimation();
            return;
        }
        GD.PrintErr($"Scene '{sceneName}' not found in Scenes.");
    }

    public static void LoadNextScene()
    {
        if(Instance._nextScene is not null)
        {
            Instance.GetTree().ChangeSceneToPacked(Instance._nextScene);
        }
    }
}

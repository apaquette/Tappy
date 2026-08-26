using Godot;
using System;

public partial class Game : Node
{
	[Export] private PackedScene _pipesScene;
	[Export] private Timer _spawnTimer;
	[Export] private Node _pipesHolder;
	[Export] private Marker2D[] _spawns;

	

	public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel"))
        {
            GameManager.Load("Main");
        }
    }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_spawnTimer.Timeout += SpawnPipes;
		SpawnPipes();
	}

	private void SpawnPipes()
	{
		var pipes = _pipesScene.Instantiate<Pipes>();
		float yPos = (float)GD.RandRange(_spawns[0].Position.Y, _spawns[1].Position.Y);
		pipes.Position = new Vector2(_spawns[0].Position.X, yPos);
		_pipesHolder.AddChild(pipes);
	}
}

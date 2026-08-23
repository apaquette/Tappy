using Godot;
using System;

public partial class Game : Node
{
	[Export] private PackedScene _pipesScene;
	[Export] private Timer _spawnTimer;
	[Export] private Node _pipesHolder;
	[Export] private Marker2D _upperSpawn;
	[Export] private Marker2D _lowerSpawn;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_spawnTimer.Timeout += SpawnPipes;
		SpawnPipes();
	}

	private void SpawnPipes()
	{
		var pipes = _pipesScene.Instantiate<Pipes>();
		float yPos = (float)GD.RandRange(_upperSpawn.Position.Y, _lowerSpawn.Position.Y);
		pipes.Position = new Vector2(_upperSpawn.Position.X, yPos);
		_pipesHolder.AddChild(pipes);
	}
}

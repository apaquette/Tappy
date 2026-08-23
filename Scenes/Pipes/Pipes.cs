using Godot;
using System;

public partial class Pipes : Node2D
{
	const float SCROLL_SPEED = 120f;
	[Export] private VisibleOnScreenNotifier2D _notifier;
	[Export] private Timer _lifeTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_notifier.ScreenExited += QueueFree;
		_lifeTimer.Timeout += QueueFree;	// Cover cases where notifier doesn't work
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Position -= new Vector2(SCROLL_SPEED * (float)delta, 0);
	}
}

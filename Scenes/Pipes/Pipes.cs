using Godot;
using System;

public partial class Pipes : Node2D
{
	private const float SCROLL_SPEED = 120f;
	[Export] private VisibleOnScreenNotifier2D _notifier;
	[Export] private Timer _lifeTimer;
	[Export] private Area2D _upperPipe;
	[Export] private Area2D _lowerPipe;
	[Export] private Area2D _laser;

	private bool _laserActive = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_notifier.ScreenExited += QueueFree;
		_lifeTimer.Timeout += QueueFree;	// Cover cases where notifier doesn't work
		_upperPipe.BodyEntered += OnPipeBodyEntered;
		_lowerPipe.BodyEntered += OnPipeBodyEntered;
		_laser.BodyExited += Score;
		SignalHub.Instance.Connect(SignalHub.SignalName.OnTappyDied,Callable.From(DisconnectLaser));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
	{
		Position -= new Vector2(SCROLL_SPEED * (float)delta, 0);
	}

    private void Score(Node2D body)
    {
        if (body is Tappy) 
		{
			SignalHub.EmitOnScored();
			DisconnectLaser(); // prevent multiple scoring from the same pipe
		}
    }

    private void OnPipeBodyEntered(Node2D body)
    {
        if (body is Tappy) SignalHub.EmitOnTappyDied();
    }

	private void DisconnectLaser()
	{
		if(_laserActive)
		{
			_laser.BodyExited -= Score;
			_laserActive = false;
		}
	}
}

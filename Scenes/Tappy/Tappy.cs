using Godot;
using System;

public partial class Tappy : CharacterBody2D
{
	public const string GROUP_NAME = "tappy";
	private const float JUMP_POWER = -350.0f;

	//[Signal] public delegate void OnTappyDiedEventHandler();

	[Export] private AnimatedSprite2D _animatedSprite2D;
	[Export] private AnimationPlayer _animationPlayer;
	[Export] private AudioStreamPlayer _engineSound;

	private bool _jumped = false;
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _UnhandledInput(InputEvent @event)
    {
		_jumped = @event.IsActionPressed("Jump");
    }

    public override void _EnterTree()
    {
        AddToGroup(GROUP_NAME);
    }


	public override void _PhysicsProcess(double delta)
	{
		Fly(delta);
		MoveAndSlide();
		if(IsOnFloor()) SignalHub.EmitOnTappyDied();
	}

	private void Fly(double delta)
	{
		Vector2 velocity = Velocity;
		velocity.Y += _gravity * (float)delta;
		if (_jumped)
		{
			_jumped = false;
			_animationPlayer.Play("Tilt");
			velocity.Y = JUMP_POWER;
		}
		Velocity = velocity;
	}
}

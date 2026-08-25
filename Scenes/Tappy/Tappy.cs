using Godot;
using System;

public partial class Tappy : CharacterBody2D
{
	const float JUMP_POWER = -350.0f;

	[Export] private AnimatedSprite2D _animatedSprite2D;
	[Export] private AnimationPlayer _animationPlayer;

	private bool _jumped = false;
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _UnhandledInput(InputEvent @event)
    {
		_jumped = @event.IsActionPressed("Jump");
    }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}


	public override void _PhysicsProcess(double delta)
	{
		Fly(delta);
		MoveAndSlide();

		if(IsOnFloor()) Die();
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

	public void Die()
	{
		// SetPhysicsProcess(false);
		// _animatedSprite2D.Stop();
		GetTree().Paused = true;
	}

	public void Score()
	{
		GD.Print("Scored!");
	}

}

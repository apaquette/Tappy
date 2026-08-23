using Godot;
using System;

public partial class Tappy : CharacterBody2D
{
	const float JUMP_POWER = -350.0f;

	[Export] private AnimatedSprite2D _animatedSprite2D;

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
		Vector2 velocity = Velocity;
		velocity.Y = _jumped ? JUMP_POWER : velocity.Y + (_gravity * (float)delta);
		Velocity = velocity;

		MoveAndSlide();

		if(IsOnFloor())
		{
			Die();
		}
	}

	private void Die()
	{
		SetPhysicsProcess(false);
		_animatedSprite2D.Stop();
	}

}

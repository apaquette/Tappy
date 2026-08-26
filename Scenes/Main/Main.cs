using Godot;
using System;

public partial class Main : Control
{
    

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("Jump"))
        {
            GameManager.Load("Game");
        }
    }
    public override void _Ready()
    {
        GetTree().Paused = false;
    }
}

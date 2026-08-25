using Godot;
using System;

public partial class Main : Control
{
    

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel"))
        {
            GameManager.Load("Game");
        }
    }
}

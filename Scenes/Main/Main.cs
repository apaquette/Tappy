using Godot;
using System;

public partial class Main : Control
{
    [Export] private Label _highScoreLabel;

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
        _highScoreLabel.Text = ScoreManager.Instance.HighScore.ToString("D3");
    }
}

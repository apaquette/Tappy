using Godot;
using System;
using System.Threading.Tasks;

public partial class GameUi : Control
{
    [Export] private Label _gameOverLabel;
    [Export] private Label _pressSpaceLabel;
    [Export] private Timer _gameOverTimer;
    [Export] private AudioStreamPlayer _gameOverSound;
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel") || (@event.IsActionPressed("Jump") && _pressSpaceLabel.Visible))
        {
            GameManager.Load("Main");
        }
    }
    public override void _Ready()
    {
        SignalHub.Instance.Connect(
            SignalHub.SignalName.OnTappyDied,
            Callable.From(OnTappyDied)
        );
        _gameOverTimer.Timeout += ShowPressSpaceLabel;
    }
    private void OnTappyDied(){
        _gameOverLabel.Show();
        _gameOverSound.Play();
        _gameOverTimer.Start();
    }

    private void ShowPressSpaceLabel()
    {
        _gameOverLabel.Hide();
        _pressSpaceLabel.Show();
    }
}

using Godot;
using System;
using System.Threading.Tasks;

public partial class GameUi : Control
{
    [Export] private Label _scoreLabel;
    [Export] private Label _gameOverLabel;
    [Export] private Label _pressSpaceLabel;
    [Export] private Timer _gameOverTimer;
    [Export] private AudioStreamPlayer _gameOverSound;
    

    private int _score = 0;

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel") || 
            (@event.IsActionPressed("Jump") && _pressSpaceLabel.Visible))
        {
            GameManager.Load("Main");
        }
    }
    public override void _Ready()
    {
        UpdateScoreLabel();
        
        // Connect signals
        SignalHub.Instance.Connect(SignalHub.SignalName.OnTappyDied,Callable.From(() =>
        {
            _gameOverLabel.Show();
            _gameOverSound.Play();
            _gameOverTimer.Start();
            GetTree().Paused = true;
        }));
        SignalHub.Instance.Connect(SignalHub.SignalName.OnScored,Callable.From(() =>
        {
            ScoreManager.Instance.HighScore = ++_score;
            UpdateScoreLabel();
        }));
        _gameOverTimer.Timeout += () =>
        {
            _gameOverLabel.Hide();
            _pressSpaceLabel.Show();
        };
    }

    private void UpdateScoreLabel() => _scoreLabel.Text = _score.ToString("D3");
}

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
    [Export] private AudioStreamPlayer _scoreSound;

    private int _score = 0;

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel") || (@event.IsActionPressed("Jump") && _pressSpaceLabel.Visible))
        {
            GameManager.Load("Main");
        }
    }
    public override void _Ready()
    {
        UpdateScoreLabel();
        SignalHub.Instance.Connect(
            SignalHub.SignalName.OnTappyDied,
            Callable.From(OnTappyDied)
        );
        SignalHub.Instance.Connect(
            SignalHub.SignalName.OnScored,
            Callable.From(Score)
        );
        _gameOverTimer.Timeout += ShowPressSpaceLabel;
    }
    private void OnTappyDied()
    {
        _gameOverLabel.Show();
        _gameOverSound.Play();
        _gameOverTimer.Start();
    }

    private void Score()
    {
        _scoreSound.Play();
        _score++;
        UpdateScoreLabel();
        ScoreManager.Instance.HighScore = _score;
    }

    private void UpdateScoreLabel()
    {
        _scoreLabel.Text = _score.ToString("D3");
    }

    private void ShowPressSpaceLabel()
    {
        _gameOverLabel.Hide();
        _pressSpaceLabel.Show();
    }
}

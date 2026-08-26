using Godot;
using System;

public partial class ScoreManager : Node
{
    public static ScoreManager Instance { get; private set; }
    private int _highScore = 0;
    public int HighScore
    {
        get => _highScore;
        set
        {
            if (value > _highScore)
            {
                _highScore = value;
            }
        }
    }
    public override void _Ready()
    {
        Instance = this;
    }
}

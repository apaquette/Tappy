using Godot;

public partial class ScoreManager : Node
{
    private const string SCORE_FILE_PATH = "user://tappy.res";
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
                SaveScoreToFile();
            }
        }
    }
    public override void _Ready()
    {
        Instance = this;
        LoadScoreFromFile();
    }

    private void SaveScoreToFile()
    {
        HighScoreResource hsr = new()
        {
            HighScore = _highScore
        };
        ResourceSaver.Save(hsr, SCORE_FILE_PATH);
    }

    private void LoadScoreFromFile()
    {
        if(!ResourceLoader.Exists(SCORE_FILE_PATH)) return;
        HighScoreResource hsr = ResourceLoader.Load<HighScoreResource>(SCORE_FILE_PATH);
        if(hsr != null) _highScore = hsr.HighScore;
    }
}

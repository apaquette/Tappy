using Godot;
using System;

public partial class ComplexChange : CanvasLayer
{
    [Export] private AnimationPlayer _animationPlayer;

    public void PlayAnimation()
    {
        _animationPlayer.Play("flash");
    }

    public void SwitchScene()
    {
        GameManager.LoadNextScene();
    }
}

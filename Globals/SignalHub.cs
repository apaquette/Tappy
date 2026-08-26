using Godot;
using System;

public partial class SignalHub : Node
{
    public static SignalHub Instance { get; private set; }
    [Signal] public delegate void OnTappyDiedEventHandler();
    [Signal] public delegate void OnScoredEventHandler();

    public override void _Ready()
    {
        Instance = this;
    }

    public static void EmitOnTappyDied() => Instance.EmitSignal(SignalName.OnTappyDied);
    public static void EmitOnScored() =>Instance.EmitSignal(SignalName.OnScored);
}

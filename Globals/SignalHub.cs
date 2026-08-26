using Godot;
using System;

public partial class SignalHub : Node
{
    public static SignalHub Instance { get; private set; }
    [Signal] public delegate void OnTappyDiedEventHandler();

    public override void _Ready()
    {
        Instance = this;
    }

    public static void EmitOnTappyDied()
    {
        Instance.EmitSignal(SignalName.OnTappyDied);
    }
}

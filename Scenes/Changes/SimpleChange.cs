using System.Threading.Tasks;
using Godot;

public partial class SimpleChange : Control
{
	public override async void _Ready()
	{
		await ToSignal(GetTree().CreateTimer(1.5), Timer.SignalName.Timeout); // wait for 1.5s
		GameManager.LoadNextScene();
	}
}

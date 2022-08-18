using Godot;

public partial class LinkButton : Button
{
    [Export]
    public string Link { get; set; }

    public override void _Ready()
    {
        Connect("pressed", this, nameof(OnPressed));
    }

    private void OnPressed()
    {
        OS.ShellOpen(Link);
    }
}

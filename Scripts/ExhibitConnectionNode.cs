using Godot;

[Tool]
public partial class ExhibitConnectionNode : Node2D
{
    [Export(PropertyHint.MultilineText)]
    private string labelText;
    public string LabelText
    {
        get => labelText;
        set
        {
            labelText = value;
            if (Label != null)
                Label.BbcodeText = $"[center][b]{value}[/b][/center]";
        }
    }
    [Export]
    private NodePath labelPath;
    public RichTextLabel Label => GetNodeOrNull<RichTextLabel>(labelPath);
    [Export]
    public NodePath targetExhibitPath;
    public Exhibit TargetExhibit
    {
        get => GetNodeOrNull<Exhibit>(targetExhibitPath);
        set
        {
            targetExhibitPath = GetPathTo(value);
            if (visit != null)
                visit.TargetExhibitPath = visit.GetPathTo(value);
        }
    }
    [Export]
    private NodePath visitPath;
    public VisitExhibit visit => GetNodeOrNull<VisitExhibit>(visitPath);
    [Export]
    private NodePath animationTreePath;
    public AnimationTree animationTree => GetNodeOrNull<AnimationTree>(animationTreePath);
    [Export]
    private NodePath clickSFXPath;
    private AudioStreamPlayer clickSFX => GetNodeOrNull<AudioStreamPlayer>(clickSFXPath);

    public override void _Ready()
    {
        if (Engine.EditorHint) return;

        visit.HoverChanged += (hovered) =>
        {
            animationTree?.Set("parameters/conditions/hovered", hovered);
            animationTree?.Set("parameters/conditions/not_hovered", !hovered);
        };

        visit.MouseDownChanged += (mouseDown) =>
        {
            animationTree?.Set("parameters/conditions/pressed", mouseDown);
            animationTree?.Set("parameters/conditions/not_pressed", !mouseDown);
        };

        visit.VisitedExhibit += () =>
        {
            clickSFX.Play();
        };
    }

    public override void _Process(float delta)
    {
        if (Engine.EditorHint)
        {
            if (Label != null && Label.Text != LabelText)
                Label.Text = labelText;
        }
    }
}

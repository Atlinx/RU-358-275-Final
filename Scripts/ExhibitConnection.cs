using Godot;
using GodotOnReady.Attributes;

[Tool]
public partial class ExhibitConnection : Line2D
{
    [Export(PropertyHint.MultilineText)]
    private string startText = "";
    public string StartText
    {
        get
        {
            if (startText == "" && EndExhibit != null)
                return EndExhibit.Name;
            return startText;
        }
        set => startText = value;
    }
    [Export(PropertyHint.MultilineText)]
    private string endText = "";
    public string EndText
    {
        get
        {
            if (endText == "" && StartExhibit != null)
                return StartExhibit.Name;
            return endText;
        }
        set => endText = value;
    }
    [Export]
    private NodePath startExhibitPath;
    public Exhibit StartExhibit => GetNodeOrNull<Exhibit>(startExhibitPath);
    [Export]
    private NodePath endExhibitPath;
    public Exhibit EndExhibit => GetNodeOrNull<Exhibit>(endExhibitPath);

    [Export]
    private NodePath startNodePath;
    private Node2D startNode => GetNode<Node2D>(startNodePath);
    [Export]
    private NodePath endNodePath;
    private Node2D endNode => GetNode<Node2D>(endNodePath);
    [Export]
    private NodePath startLabelPath;
    private RichTextLabel startLabel => GetNode<RichTextLabel>(startLabelPath);
    [Export]
    private NodePath endLabelPath;
    private RichTextLabel endLabel => GetNode<RichTextLabel>(endLabelPath);
    [Export]
    private NodePath visitStartPath;
    private VisitExhibit visitStart => GetNode<VisitExhibit>(visitStartPath);
    [Export]
    private NodePath visitEndPath;
    private VisitExhibit visitEnd => GetNode<VisitExhibit>(visitEndPath);

    public override void _Process(float delta)
    {
        if (Engine.EditorHint)
        {
            if (visitStart != null && EndExhibit != null && visitStart.TargetExhibitPath != visitStart.GetPathTo(EndExhibit))
                visitStart.TargetExhibitPath = visitStart.GetPathTo(EndExhibit);
            if (visitEnd != null && StartExhibit != null && visitEnd.TargetExhibitPath != visitEnd.GetPathTo(StartExhibit))
                visitEnd.TargetExhibitPath = visitStart.GetPathTo(StartExhibit);
            if (startLabel != null && startLabel.Text != StartText)
                startLabel.BbcodeText = $"[center][b]{StartText}[/b][/center]";
            if (endLabel != null && endLabel.Text != EndText)
                endLabel.BbcodeText = $"[center][b]{EndText}[/b][/center]";
            if ((startNode != null && endNode != null) && (Points == null || Points.Length == 0 || startNode.Position != Points[0] || endNode.Position != Points[1]))
                Points = new Vector2[] { startNode.Position, endNode.Position };
        }
    }
}

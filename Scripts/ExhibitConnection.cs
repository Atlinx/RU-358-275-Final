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
    private ExhibitConnectionNode startNode => GetNodeOrNull<ExhibitConnectionNode>(startNodePath);
    [Export]
    private NodePath endNodePath;
    private ExhibitConnectionNode endNode => GetNodeOrNull<ExhibitConnectionNode>(endNodePath);

    public override void _Ready()
    {
        if (Engine.EditorHint) return;
    }

    public override void _Process(float delta)
    {
        if (Engine.EditorHint)
        {
            if (startNode != null)
            {
                if (EndExhibit != null && startNode.TargetExhibit != EndExhibit)
                    startNode.TargetExhibit = EndExhibit;
                if (startNode.LabelText != StartText)
                    startNode.LabelText = StartText;
            }
            if (endNode != null)
            {
                if (StartExhibit != null && endNode.TargetExhibit != StartExhibit)
                    endNode.TargetExhibit = StartExhibit;
                if (endNode.LabelText != EndText)
                    endNode.LabelText = EndText;
            }
            if ((startNode != null && endNode != null) && (Points == null || Points.Length == 0 || startNode.Position != Points[0] || endNode.Position != Points[1]))
                Points = new Vector2[] { startNode.Position, endNode.Position };
        }
    }
}

using System;
using Godot;
using GodotOnReady.Attributes;

public partial class VisitExhibit : Area2D
{
    public event Action VisitedExhibit;
    [Export]
    public float VisitAnimDuration { get; set; } = 0.5f;

    [OnReadyGet]
    public Exhibit TargetExhibit;

    public override void _InputEvent(Godot.Object viewport, InputEvent inputEvent, int shapeIdx)
    {
        if (inputEvent is InputEventMouseButton mouseButtonEvent && mouseButtonEvent.Pressed && mouseButtonEvent.ButtonIndex == (int)ButtonList.Left)
        {
            VisitedExhibit?.Invoke();
        }
    }
}

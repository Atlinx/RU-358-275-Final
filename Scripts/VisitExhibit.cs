using System;
using Godot;
using GodotOnReady.Attributes;

[Tool]
public partial class VisitExhibit : Area2D
{
    public event Action<bool> HoverChanged;
    public event Action<bool> MouseDownChanged;
    public event Action VisitedExhibit;

    public bool IsMouseDown { get; private set; } = false;
    public bool IsHovering { get; private set; } = false;

    [Export]
    public float VisitAnimDuration { get; set; } = 0.5f;

    [OnReadyGet]
    public Exhibit TargetExhibit;

    [OnReady]
    private void RealReady()
    {
        Connect("mouse_entered", this, nameof(OnMouseEntered));
        Connect("mouse_exited", this, nameof(OnMouseExited));
    }

    private void OnMouseEntered()
    {
        IsHovering = true;
        HoverChanged?.Invoke(IsHovering);
    }

    private void OnMouseExited()
    {
        IsMouseDown = false;
        IsHovering = false;
        HoverChanged?.Invoke(IsHovering);
        MouseDownChanged?.Invoke(IsMouseDown);
    }

    public override void _InputEvent(Godot.Object viewport, InputEvent inputEvent, int shapeIdx)
    {
        if (inputEvent is InputEventMouseButton mouseButtonEvent && mouseButtonEvent.ButtonIndex == (int)ButtonList.Left)
        {
            if (mouseButtonEvent.Pressed)
            {
                VisitedExhibit?.Invoke();
                IsMouseDown = true;
                MouseDownChanged?.Invoke(IsMouseDown);
            }
            else
            {
                IsMouseDown = false;
                MouseDownChanged?.Invoke(IsMouseDown);
            }
        }
    }
}

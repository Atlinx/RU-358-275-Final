using Godot;
using GodotOnReady.Attributes;

public partial class CameraController : Camera2D
{
    [Export]
    public float Speed { get; set; }
    [Export]
    public Vector2 Direction { get; private set; }
    [Export]
    public float MinZoom { get; set; }
    [Export]
    public float MaxZoom { get; set; }
    [Export]
    public float ZoomFactor;
    [Export]
    public float ZoomDuration;
    [Export]
    public bool CanMove { get; set; }
    private float zoomLevel;
    [Export]
    public float ZoomLevel
    {
        get => zoomLevel;
        set
        {
            if (zoomTween == null)
                return;
            zoomLevel = Mathf.Clamp(value, MinZoom, MaxZoom);
            zoomTween.InterpolateProperty(this, "zoom", Zoom, new Vector2(ZoomLevel, ZoomLevel), ZoomDuration, Tween.TransitionType.Sine, Tween.EaseType.Out);
            zoomTween.Start();
        }
    }

    [OnReadyGet]
    private Tween visitTween;
    [OnReadyGet]
    private Tween zoomTween;

    private bool panning;
    private Vector2 prevMousePos;

    [OnReady]
    private void RealReady()
    {
        Zoom = new Vector2(ZoomLevel, ZoomLevel);
    }

    public override void _Process(float delta)
    {
        var dir = Vector2.Zero;
        if (!visitTween.IsActive() && CanMove)
        {
            if (Input.IsMouseButtonPressed((int)ButtonList.Left))
            {
                // Mouse movement
                if (!panning)
                {
                    prevMousePos = GetGlobalMousePosition();
                    panning = true;
                }
                else
                {
                    Vector2 mouseDelta = GetGlobalMousePosition() - prevMousePos;
                    Position -= mouseDelta * Zoom;
                }
            }
            else
            {
                if (panning) panning = false;
                if (!Input.IsMouseButtonPressed((int)ButtonList.Left))
                {
                    // WASD movement
                    if (Input.IsActionPressed("left")) dir.x -= 1;
                    if (Input.IsActionPressed("right")) dir.x += 1;
                    if (Input.IsActionPressed("up")) dir.y -= 1;
                    if (Input.IsActionPressed("down")) dir.y += 1;
                    Direction = dir.Normalized();

                    Position += dir * Speed;
                }
            }

            if (Input.IsActionJustReleased("zoom_in"))
                ZoomLevel -= ZoomFactor;
            else if (Input.IsActionJustReleased("zoom_out"))
                ZoomLevel += ZoomFactor;
        }
    }

    public void Visit(Vector2 newPosition, float duration = 0.25f)
    {
        if (visitTween.IsActive())
            visitTween.Stop(this);
        visitTween.InterpolateProperty(this, "position", Position, newPosition, duration);
        visitTween.Start();
    }
}

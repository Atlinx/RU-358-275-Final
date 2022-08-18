using Godot;
using GodotOnReady.Attributes;

public partial class VisitExhibitUIButton : Button
{
    [OnReadyGet]
    private CameraController cameraController;
    [OnReadyGet]
    private Exhibit exhibit;

    [OnReady]
    public void RealReady()
    {
        Connect("pressed", this, nameof(OnPressed));
    }

    private void OnPressed()
    {
        cameraController.Visit(exhibit.Position);
    }
}

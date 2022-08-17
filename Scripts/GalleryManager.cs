using System.Collections.Generic;
using Godot;
using GodotOnReady.Attributes;
using Fractural.Utils;

public partial class GalleryManager : Node2D
{
    [OnReadyGet]
    private CameraController cameraController;

    public List<Exhibit> Exhibits { get; private set; } = new List<Exhibit>();
    public List<VisitExhibit> VisitExhibits { get; private set; } = new List<VisitExhibit>();

    [OnReady]
    public void RealReady()
    {
        Exhibits = this.GetDescendants<Exhibit>();
        VisitExhibits = this.GetDescendants<VisitExhibit>();
        foreach (var visit in VisitExhibits)
            visit.VisitedExhibit += () => cameraController.Visit(visit.TargetExhibit.Position, visit.VisitAnimDuration);
    }

    public override void _Process(float delta)
    {
        foreach (var exhibit in Exhibits)
            exhibit.UpdateParallaxBackgroundZoom(cameraController.Zoom.x);
    }
}

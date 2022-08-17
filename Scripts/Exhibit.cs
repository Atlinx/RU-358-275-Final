using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using GodotOnReady.Attributes;

[Tool]
public partial class Exhibit : Node2D
{
    [OnReadyGet]
    private ParallaxBackground parallaxBackground;
    [Export]
    private NodePath[] canvasLayerPaths;
    private IEnumerable<CanvasLayer> canvasLayers => canvasLayerPaths.Select(x => GetNode<CanvasLayer>(x));
    private Vector2 originalParallaxBackgroundBaseOffset;

    [OnReady]
    public void RealReady()
    {
        if (!Engine.EditorHint)
        {
            SetProcess(false);
            parallaxBackground.Offset = Vector2.Zero;

            originalParallaxBackgroundBaseOffset = parallaxBackground.ScrollBaseOffset;
            parallaxBackground.ScrollBaseOffset = Position;
            foreach (var layer in canvasLayers)
                layer.Offset = Position;
        }
    }

    public override void _Process(float delta)
    {
        if (Engine.EditorHint)
        {
            parallaxBackground.Offset = Position;
            foreach (var layer in canvasLayers)
                layer.Offset = Position;
        }
    }

    public void UpdateParallaxBackgroundZoom(float zoom)
    {
        parallaxBackground.ScrollBaseOffset = originalParallaxBackgroundBaseOffset / zoom;
    }
}

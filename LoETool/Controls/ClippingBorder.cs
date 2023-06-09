using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LoETool.Controls;

public class ClippingBorder : Border
{
    private readonly RectangleGeometry _clipRectangle = new();
    private object? _oldClip;

    protected override void OnRender(DrawingContext dc)
    {
        OnApplyChildClip();
        base.OnRender(dc);
    }

    public override UIElement Child
    {
        get => base.Child;
        set
        {
            Child?.SetValue(ClipProperty, _oldClip);
            _oldClip = value?.ReadLocalValue(ClipProperty);
            base.Child = value;
        }
    }

    protected virtual void OnApplyChildClip()
    {
        var child = Child;
        if (child is not null)
        {
            _clipRectangle.RadiusX = _clipRectangle.RadiusY = Math.Max(0.0, CornerRadius.TopLeft - (BorderThickness.Left * 0.5));
            _clipRectangle.Rect = new(child.RenderSize);
            child.Clip = _clipRectangle;
        }
    }
}

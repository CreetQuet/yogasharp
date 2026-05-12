namespace YogaSharp;

public interface ILayoutNode : IDisposable
{
    float Width { get; set; }
    float Height { get; set; }
    float MinWidth { get; set; }
    float MinHeight { get; set; }
    float MaxWidth { get; set; }
    float MaxHeight { get; set; }

    float Padding { set; }
    float PaddingLeft { get; set; }
    float PaddingTop { get; set; }
    float PaddingRight { get; set; }
    float PaddingBottom { get; set; }

    float Margin { set; }
    float MarginLeft { get; set; }
    float MarginTop { get; set; }
    float MarginRight { get; set; }
    float MarginBottom { get; set; }

    float Gap { get; set; }

    FlexDirection FlexDirection { get; set; }
    JustifyContent JustifyContent { get; set; }
    AlignItems AlignItems { get; set; }
    PositionType PositionType { get; set; }

    float FlexGrow { get; set; }
    float FlexShrink { get; set; }
    float AspectRatio { get; set; }

    float PositionLeft { get; set; }
    float PositionTop { get; set; }
    float PositionRight { get; set; }
    float PositionBottom { get; set; }

    LayoutRect Layout { get; }

    void AddChild(ILayoutNode child, int index = -1);
    void RemoveChild(ILayoutNode child);
    void ClearChildren();

    void CalculateLayout(float availableWidth = float.NaN, float availableHeight = float.NaN);
}
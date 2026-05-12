namespace YogaSharp.Layout;

public readonly struct YogaLayout {
    public readonly float X;
    public readonly float Y;
    public readonly float Width;
    public readonly float Height;

    public YogaLayout(float x, float y, float width, float height) {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }
}

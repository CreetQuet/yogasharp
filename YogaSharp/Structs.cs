namespace YogaSharp;

public struct LayoutRect
{
    public float X;
    public float Y;
    public float Width;
    public float Height;

    public LayoutRect(float x, float y, float width, float height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public override string ToString()
    {
        return $"{{X={X}, Y={Y}, Width={Width}, Height={Height}}}";
    }
}
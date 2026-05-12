namespace YogaSharp.Core;

public enum YogaMeasureMode {
    Undefined = 0,
    Exactly = 1,
    AtMost = 2
}

public readonly struct YogaMeasureResult {
    public readonly float Width;
    public readonly float Height;

    public YogaMeasureResult(float width, float height) {
        Width = width;
        Height = height;
    }
}

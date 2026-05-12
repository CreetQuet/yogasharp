using System.Runtime.InteropServices;

namespace YogaSharp.Core;

public enum YogaUnit {
    Undefined = 0,
    Point = 1,
    Percent = 2,
    Auto = 3
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct YogaValue {
    public readonly float Value;
    public readonly YogaUnit Unit;

    public YogaValue(float value, YogaUnit unit) {
        Value = value;
        Unit = unit;
    }

    public static YogaValue Undefined() => new YogaValue(float.NaN, YogaUnit.Undefined);
    public static YogaValue Point(float value) => new YogaValue(value, YogaUnit.Point);
    public static YogaValue Percent(float value) => new YogaValue(value, YogaUnit.Percent);
    public static YogaValue Auto() => new YogaValue(float.NaN, YogaUnit.Auto);

    public bool Equals(YogaValue other) {
        if (Unit != other.Unit) return false;
        if (Unit == YogaUnit.Undefined || Unit == YogaUnit.Auto) return true;
        return Value.Equals(other.Value);
    }
}

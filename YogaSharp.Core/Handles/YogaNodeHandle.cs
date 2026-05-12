using Microsoft.Win32.SafeHandles;
using YogaSharp.Core.Internal;

namespace YogaSharp.Core.Handles;

public sealed class YogaNodeHandle : SafeHandleZeroOrMinusOneIsInvalid {
    public YogaNodeHandle() : base(true) {
        SetHandle(YogaNative.YGNodeNew());
    }

    internal YogaNodeHandle(IntPtr configPtr) : base(true) {
        SetHandle(YogaNative.YGNodeNewWithConfig(configPtr));
    }

    protected override bool ReleaseHandle() {
        YogaNative.YGNodeFree(handle);
        return true;
    }
}

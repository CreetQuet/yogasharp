using Microsoft.Win32.SafeHandles;
using YogaSharp.Core.Internal;

namespace YogaSharp.Core.Handles;

public sealed class YogaConfigHandle : SafeHandleZeroOrMinusOneIsInvalid {
    public YogaConfigHandle() : base(true) {
        SetHandle(YogaNative.YGConfigNew());
    }

    protected override bool ReleaseHandle() {
        YogaNative.YGConfigFree(handle);
        return true;
    }
}

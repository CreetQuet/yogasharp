using YogaSharp.Core.Handles;

namespace YogaSharp.Core;

public sealed class YogaConfig : IDisposable {
    internal YogaConfigHandle Handle { get; }

    public YogaConfig() {
        Handle = new YogaConfigHandle();
    }

    public void Dispose() {
        Handle.Dispose();
    }
}

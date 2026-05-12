namespace YogaSharp;

public class YogaLayoutEngine : ILayoutEngine
{
    private bool _isDisposed;

    public YogaLayoutEngine()
    {
        NativeLoader.Initialize();
    }

    public ILayoutNode CreateNode()
    {
        CheckDisposed();
        return new YogaLayoutNode();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed) return;
        _isDisposed = true;
    }

    private void CheckDisposed()
    {
        if (_isDisposed)
            throw new ObjectDisposedException(nameof(YogaLayoutEngine));
    }
}
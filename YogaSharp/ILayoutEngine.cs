namespace YogaSharp;

public interface ILayoutEngine : IDisposable
{
    ILayoutNode CreateNode();
}
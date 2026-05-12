namespace YogaSharp;

public class YogaLayoutNode : ILayoutNode
{
    private IntPtr _nativeNode;
    private readonly List<YogaLayoutNode> _children = new();
    private bool _isDisposed;

    public YogaLayoutNode()
    {
        _nativeNode = YogaNative.YGNodeNew();
    }

    ~YogaLayoutNode()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed) return;

        if (disposing)
        {
            // Dispose managed state (children)
            foreach (var child in _children)
            {
                child.Dispose();
            }

            _children.Clear();
        }

        // Free native resource
        if (_nativeNode != IntPtr.Zero)
        {
            YogaNative.YGNodeFree(_nativeNode);
            _nativeNode = IntPtr.Zero;
        }

        _isDisposed = true;
    }

    private void CheckDisposed()
    {
        if (_isDisposed)
            throw new ObjectDisposedException(nameof(YogaLayoutNode));
    }

    public float Width
    {
        get => YogaNative.YGNodeLayoutGetWidth(_nativeNode);
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetWidth(_nativeNode, value);
        }
    }

    public float Height
    {
        get => YogaNative.YGNodeLayoutGetHeight(_nativeNode);
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetHeight(_nativeNode, value);
        }
    }

    public float MinWidth
    {
        get => 0; // YGNodeStyleGetMinWidth not bound for brevity
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetMinWidth(_nativeNode, value);
        }
    }

    public float MinHeight
    {
        get => 0;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetMinHeight(_nativeNode, value);
        }
    }

    public float MaxWidth
    {
        get => 0;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetMaxWidth(_nativeNode, value);
        }
    }

    public float MaxHeight
    {
        get => 0;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetMaxHeight(_nativeNode, value);
        }
    }

    public float Padding
    {
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetPadding(_nativeNode, YGEdge.All, value);
        }
    }

    public float PaddingLeft
    {
        get => 0;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetPadding(_nativeNode, YGEdge.Left, value);
        }
    }

    public float PaddingTop
    {
        get => 0;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetPadding(_nativeNode, YGEdge.Top, value);
        }
    }

    public float PaddingRight
    {
        get => 0;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetPadding(_nativeNode, YGEdge.Right, value);
        }
    }

    public float PaddingBottom
    {
        get => 0;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetPadding(_nativeNode, YGEdge.Bottom, value);
        }
    }

    public float Margin
    {
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetMargin(_nativeNode, YGEdge.All, value);
        }
    }

    public float MarginLeft
    {
        get => 0;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetMargin(_nativeNode, YGEdge.Left, value);
        }
    }

    public float MarginTop
    {
        get => 0;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetMargin(_nativeNode, YGEdge.Top, value);
        }
    }

    public float MarginRight
    {
        get => 0;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetMargin(_nativeNode, YGEdge.Right, value);
        }
    }

    public float MarginBottom
    {
        get => 0;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetMargin(_nativeNode, YGEdge.Bottom, value);
        }
    }

    public float Gap
    {
        get => 0;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetGap(_nativeNode, YGGutter.All, value);
        }
    }

    public FlexDirection FlexDirection
    {
        get => FlexDirection.Row; // Getter omitted for brevity
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetFlexDirection(_nativeNode, (int)value);
        }
    }

    public JustifyContent JustifyContent
    {
        get => JustifyContent.FlexStart;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetJustifyContent(_nativeNode, (int)value);
        }
    }

    public AlignItems AlignItems
    {
        get => AlignItems.Stretch;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetAlignItems(_nativeNode, (int)value);
        }
    }

    public PositionType PositionType
    {
        get => PositionType.Relative;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetPositionType(_nativeNode, (int)value);
        }
    }

    public float FlexGrow
    {
        get => 0;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetFlexGrow(_nativeNode, value);
        }
    }

    public float FlexShrink
    {
        get => 0;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetFlexShrink(_nativeNode, value);
        }
    }

    public float AspectRatio
    {
        get => 0;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetAspectRatio(_nativeNode, value);
        }
    }

    public float PositionLeft
    {
        get => 0;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetPosition(_nativeNode, YGEdge.Left, value);
        }
    }

    public float PositionTop
    {
        get => 0;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetPosition(_nativeNode, YGEdge.Top, value);
        }
    }

    public float PositionRight
    {
        get => 0;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetPosition(_nativeNode, YGEdge.Right, value);
        }
    }

    public float PositionBottom
    {
        get => 0;
        set
        {
            CheckDisposed();
            YogaNative.YGNodeStyleSetPosition(_nativeNode, YGEdge.Bottom, value);
        }
    }

    public LayoutRect Layout
    {
        get
        {
            CheckDisposed();
            return new LayoutRect(
                YogaNative.YGNodeLayoutGetLeft(_nativeNode),
                YogaNative.YGNodeLayoutGetTop(_nativeNode),
                YogaNative.YGNodeLayoutGetWidth(_nativeNode),
                YogaNative.YGNodeLayoutGetHeight(_nativeNode)
            );
        }
    }

    public void AddChild(ILayoutNode child, int index = -1)
    {
        CheckDisposed();
        if (child is not YogaLayoutNode yogaChild)
            throw new ArgumentException("Child must be a YogaLayoutNode", nameof(child));

        int insertIndex = index >= 0 ? index : _children.Count;
        _children.Insert(insertIndex, yogaChild);
        YogaNative.YGNodeInsertChild(_nativeNode, yogaChild._nativeNode, (uint)insertIndex);
    }

    public void RemoveChild(ILayoutNode child)
    {
        CheckDisposed();
        if (child is not YogaLayoutNode yogaChild) return;

        if (_children.Remove(yogaChild))
        {
            YogaNative.YGNodeRemoveChild(_nativeNode, yogaChild._nativeNode);
        }
    }

    public void ClearChildren()
    {
        CheckDisposed();
        _children.Clear();
        YogaNative.YGNodeRemoveAllChildren(_nativeNode);
    }

    public void CalculateLayout(float availableWidth = float.NaN, float availableHeight = float.NaN)
    {
        CheckDisposed();
        YogaNative.YGNodeCalculateLayout(_nativeNode, availableWidth, availableHeight, YGDirection.LTR);
    }
}
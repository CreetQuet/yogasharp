using System.Runtime.InteropServices;
using YogaSharp.Core;
using YogaSharp.Core.Handles;
using YogaSharp.Core.Internal;

namespace YogaSharp.Layout;

public sealed class YogaNode : IDisposable {
    private readonly YogaNodeHandle _handle;
    private readonly List<YogaNode> _children;
    private YogaNative.YGMeasureFunc? _measureFuncDelegate;
    private MeasureFunction? _managedMeasureFunc;

    public delegate YogaMeasureResult MeasureFunction(YogaNode node, float width, YogaMeasureMode widthMode,
        float height, YogaMeasureMode heightMode);

    public YogaNode() {
        _handle = new YogaNodeHandle();
        _children = new List<YogaNode>();
    }

    public YogaNode(YogaConfig config) {
        _handle = new YogaNodeHandle(config.Handle.DangerousGetHandle());
        _children = new List<YogaNode>();
    }

    public void Dispose() {
        foreach (var child in _children) {
            child.Dispose();
        }

        _children.Clear();
        _handle.Dispose();
    }

    public void AddChild(YogaNode child, int index = -1) {
        int insertIndex = index >= 0 ? index : _children.Count;
        _children.Insert(insertIndex, child);
        YogaNative.YGNodeInsertChild(_handle.DangerousGetHandle(), child._handle.DangerousGetHandle(),
            (uint)insertIndex);
    }

    public void RemoveChild(YogaNode child) {
        if (_children.Remove(child)) {
            YogaNative.YGNodeRemoveChild(_handle.DangerousGetHandle(), child._handle.DangerousGetHandle());
        }
    }

    public void ClearChildren() {
        _children.Clear();
        YogaNative.YGNodeRemoveAllChildren(_handle.DangerousGetHandle());
    }

    public void CalculateLayout(float availableWidth = float.NaN, float availableHeight = float.NaN) {
        YogaNative.YGNodeCalculateLayout(_handle.DangerousGetHandle(), availableWidth, availableHeight,
            YGDirection.LTR);
    }

    public YogaLayout Layout => new YogaLayout(
        YogaNative.YGNodeLayoutGetLeft(_handle.DangerousGetHandle()),
        YogaNative.YGNodeLayoutGetTop(_handle.DangerousGetHandle()),
        YogaNative.YGNodeLayoutGetWidth(_handle.DangerousGetHandle()),
        YogaNative.YGNodeLayoutGetHeight(_handle.DangerousGetHandle())
    );

    public void SetMeasureFunction(MeasureFunction measureFunc) {
        _managedMeasureFunc = measureFunc;
        _measureFuncDelegate = NativeMeasureFunc;
        YogaNative.YGNodeSetMeasureFunc(_handle.DangerousGetHandle(), _measureFuncDelegate);
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct MeasureResultPacker {
        [FieldOffset(0)] public float Width;
        [FieldOffset(4)] public float Height;
        [FieldOffset(0)] public ulong Result;
    }

    private ulong NativeMeasureFunc(IntPtr node, float width, int widthMode, float height, int heightMode) {
        if (_managedMeasureFunc == null) return 0;
        var result = _managedMeasureFunc(this, width, (YogaMeasureMode)widthMode, height, (YogaMeasureMode)heightMode);

        var packer = new MeasureResultPacker { Width = result.Width, Height = result.Height };
        return packer.Result;
    }

    public float Width {
        set => YogaNative.YGNodeStyleSetWidth(_handle.DangerousGetHandle(), value);
    }

    public float Height {
        set => YogaNative.YGNodeStyleSetHeight(_handle.DangerousGetHandle(), value);
    }

    public float MinWidth {
        set => YogaNative.YGNodeStyleSetMinWidth(_handle.DangerousGetHandle(), value);
    }

    public float MinHeight {
        set => YogaNative.YGNodeStyleSetMinHeight(_handle.DangerousGetHandle(), value);
    }

    public float MaxWidth {
        set => YogaNative.YGNodeStyleSetMaxWidth(_handle.DangerousGetHandle(), value);
    }

    public float MaxHeight {
        set => YogaNative.YGNodeStyleSetMaxHeight(_handle.DangerousGetHandle(), value);
    }

    public float Padding {
        set => YogaNative.YGNodeStyleSetPadding(_handle.DangerousGetHandle(), YGEdge.All, value);
    }

    public float Margin {
        set => YogaNative.YGNodeStyleSetMargin(_handle.DangerousGetHandle(), YGEdge.All, value);
    }

    public float Gap {
        set => YogaNative.YGNodeStyleSetGap(_handle.DangerousGetHandle(), YGGutter.All, value);
    }

    public FlexDirection FlexDirection {
        set => YogaNative.YGNodeStyleSetFlexDirection(_handle.DangerousGetHandle(), (int)value);
    }

    public JustifyContent JustifyContent {
        set => YogaNative.YGNodeStyleSetJustifyContent(_handle.DangerousGetHandle(), (int)value);
    }

    public AlignItems AlignItems {
        set => YogaNative.YGNodeStyleSetAlignItems(_handle.DangerousGetHandle(), (int)value);
    }

    public AlignSelf AlignSelf {
        set => YogaNative.YGNodeStyleSetAlignSelf(_handle.DangerousGetHandle(), (int)value);
    }

    public PositionType PositionType {
        set => YogaNative.YGNodeStyleSetPositionType(_handle.DangerousGetHandle(), (int)value);
    }

    public Wrap Wrap {
        set => YogaNative.YGNodeStyleSetFlexWrap(_handle.DangerousGetHandle(), (int)value);
    }

    public Overflow Overflow {
        set => YogaNative.YGNodeStyleSetOverflow(_handle.DangerousGetHandle(), (int)value);
    }

    public Display Display {
        set => YogaNative.YGNodeStyleSetDisplay(_handle.DangerousGetHandle(), (int)value);
    }

    public float FlexGrow {
        set => YogaNative.YGNodeStyleSetFlexGrow(_handle.DangerousGetHandle(), value);
    }

    public float FlexShrink {
        set => YogaNative.YGNodeStyleSetFlexShrink(_handle.DangerousGetHandle(), value);
    }

    public float FlexBasis {
        set => YogaNative.YGNodeStyleSetFlexBasis(_handle.DangerousGetHandle(), value);
    }

    public float AspectRatio {
        set => YogaNative.YGNodeStyleSetAspectRatio(_handle.DangerousGetHandle(), value);
    }
}

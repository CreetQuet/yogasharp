using System.Runtime.InteropServices;
using YogaSharp.Core.Interop;

namespace YogaSharp.Core.Internal;

internal enum YGEdge { Left, Top, Right, Bottom, Start, End, Horizontal, Vertical, All }

internal enum YGGutter { Column, Row, All }

internal enum YGDirection { Inherit, LTR, RTL }

internal enum YGUnit { Undefined, Point, Percent, Auto }

internal static class YogaNative {
    private const string DllName = "yoga";

    static YogaNative() {
        NativeLoader.Initialize();
    }

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr YGConfigNew();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGConfigFree(IntPtr config);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr YGNodeNew();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr YGNodeNewWithConfig(IntPtr config);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeFree(IntPtr node);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeInsertChild(IntPtr node, IntPtr child, uint index);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeRemoveChild(IntPtr node, IntPtr child);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeRemoveAllChildren(IntPtr node);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeCalculateLayout(IntPtr node, float availableWidth, float availableHeight,
        YGDirection parentDirection);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float YGNodeLayoutGetLeft(IntPtr node);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float YGNodeLayoutGetTop(IntPtr node);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float YGNodeLayoutGetWidth(IntPtr node);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float YGNodeLayoutGetHeight(IntPtr node);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetWidth(IntPtr node, float width);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetWidthPercent(IntPtr node, float width);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetWidthAuto(IntPtr node);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetHeight(IntPtr node, float height);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetHeightPercent(IntPtr node, float height);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetHeightAuto(IntPtr node);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetMinWidth(IntPtr node, float minWidth);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetMinHeight(IntPtr node, float minHeight);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetMaxWidth(IntPtr node, float maxWidth);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetMaxHeight(IntPtr node, float maxHeight);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetPadding(IntPtr node, YGEdge edge, float padding);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetMargin(IntPtr node, YGEdge edge, float margin);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetGap(IntPtr node, YGGutter gutter, float gapLength);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetPosition(IntPtr node, YGEdge edge, float position);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetFlexDirection(IntPtr node, int flexDirection);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetJustifyContent(IntPtr node, int justifyContent);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetAlignItems(IntPtr node, int alignItems);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetAlignSelf(IntPtr node, int alignSelf);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetPositionType(IntPtr node, int positionType);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetFlexWrap(IntPtr node, int flexWrap);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetOverflow(IntPtr node, int overflow);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetDisplay(IntPtr node, int display);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetFlexGrow(IntPtr node, float flexGrow);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetFlexShrink(IntPtr node, float flexShrink);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetFlexBasis(IntPtr node, float flexBasis);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetFlexBasisPercent(IntPtr node, float flexBasis);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetFlexBasisAuto(IntPtr node);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetAspectRatio(IntPtr node, float aspectRatio);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ulong YGMeasureFunc(IntPtr node, float width, int widthMode, float height, int heightMode);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeSetMeasureFunc(IntPtr node, YGMeasureFunc? measureFunc);
}

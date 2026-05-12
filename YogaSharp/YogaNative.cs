using System.Runtime.InteropServices;

namespace YogaSharp;

internal enum YGEdge
{
    Left = 0,
    Top = 1,
    Right = 2,
    Bottom = 3,
    Start = 4,
    End = 5,
    Horizontal = 6,
    Vertical = 7,
    All = 8
}

internal enum YGGutter
{
    Column = 0,
    Row = 1,
    All = 2
}

internal enum YGDirection
{
    Inherit = 0,
    LTR = 1,
    RTL = 2
}

internal static class YogaNative
{
    private const string DllName = "yoga";

    static YogaNative()
    {
        NativeLoader.Initialize();
    }

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr YGNodeNew();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeFree(IntPtr node);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeFreeRecursive(IntPtr node);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeInsertChild(IntPtr node, IntPtr child, uint index);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeRemoveChild(IntPtr node, IntPtr child);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeRemoveAllChildren(IntPtr node);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeCalculateLayout(IntPtr node, float availableWidth, float availableHeight,
        YGDirection parentDirection);

    // Layout
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float YGNodeLayoutGetLeft(IntPtr node);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float YGNodeLayoutGetTop(IntPtr node);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float YGNodeLayoutGetWidth(IntPtr node);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float YGNodeLayoutGetHeight(IntPtr node);

    // Style - Dimensions
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetWidth(IntPtr node, float width);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetHeight(IntPtr node, float height);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetMinWidth(IntPtr node, float minWidth);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetMinHeight(IntPtr node, float minHeight);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetMaxWidth(IntPtr node, float maxWidth);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetMaxHeight(IntPtr node, float maxHeight);

    // Style - Margins / Padding / Gap
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetPadding(IntPtr node, YGEdge edge, float padding);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetMargin(IntPtr node, YGEdge edge, float margin);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetGap(IntPtr node, YGGutter gutter, float gapLength);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetPosition(IntPtr node, YGEdge edge, float position);

    // Style - Flex
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetFlexDirection(IntPtr node, int flexDirection);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetJustifyContent(IntPtr node, int justifyContent);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetAlignItems(IntPtr node, int alignItems);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetPositionType(IntPtr node, int positionType);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetFlexGrow(IntPtr node, float flexGrow);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetFlexShrink(IntPtr node, float flexShrink);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void YGNodeStyleSetAspectRatio(IntPtr node, float aspectRatio);
}
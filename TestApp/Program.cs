using YogaSharp.Core;
using YogaSharp.Layout;

namespace TestApp;

class Program {
    static void Main(string[] args) {
        Console.WriteLine("Initializing Yoga Engine...");

        // Root Node (Only dispose root, children are owned by it)
        using var root = new YogaNode {
            Width = 500,
            Height = 500,
            Padding = 20,
            FlexDirection = FlexDirection.Column,
            JustifyContent = JustifyContent.SpaceBetween
        };

        // Header
        var header = new YogaNode {
            Height = 50,
            FlexDirection = FlexDirection.Row
        };

        // Content Area
        var content = new YogaNode {
            FlexGrow = 1,
            FlexDirection = FlexDirection.Row
        };

        // Sidebar inside Content
        var sidebar = new YogaNode {
            Width = 100
        };

        // Main body inside Content
        var mainBody = new YogaNode {
            FlexGrow = 1
        };

        content.AddChild(sidebar);
        content.AddChild(mainBody);

        // Footer
        var footer = new YogaNode {
            Height = 50
        };

        root.AddChild(header);
        root.AddChild(content);
        root.AddChild(footer);

        Console.WriteLine("Calculating Layout...");
        root.CalculateLayout();

        Console.WriteLine(
            $"Root Layout: X={root.Layout.X}, Y={root.Layout.Y}, W={root.Layout.Width}, H={root.Layout.Height}");
        Console.WriteLine(
            $"Header Layout: X={header.Layout.X}, Y={header.Layout.Y}, W={header.Layout.Width}, H={header.Layout.Height}");
        Console.WriteLine(
            $"Content Layout: X={content.Layout.X}, Y={content.Layout.Y}, W={content.Layout.Width}, H={content.Layout.Height}");
        Console.WriteLine(
            $"Sidebar Layout: X={sidebar.Layout.X}, Y={sidebar.Layout.Y}, W={sidebar.Layout.Width}, H={sidebar.Layout.Height}");
        Console.WriteLine(
            $"MainBody Layout: X={mainBody.Layout.X}, Y={mainBody.Layout.Y}, W={mainBody.Layout.Width}, H={mainBody.Layout.Height}");
        Console.WriteLine(
            $"Footer Layout: X={footer.Layout.X}, Y={footer.Layout.Y}, W={footer.Layout.Width}, H={footer.Layout.Height}");

        Console.WriteLine("Layout Calculated Successfully.");
    }
}

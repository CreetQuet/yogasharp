using YogaSharp;

namespace TestApp;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Initializing Yoga Engine...");

        using var engine = new YogaLayoutEngine();

        // Root Node
        using var root = engine.CreateNode();
        root.Width = 500;
        root.Height = 500;
        root.Padding = 20;
        root.FlexDirection = FlexDirection.Column;
        root.JustifyContent = JustifyContent.SpaceBetween;

        // Header
        using var header = engine.CreateNode();
        header.Height = 50;
        header.FlexDirection = FlexDirection.Row;

        // Content Area
        using var content = engine.CreateNode();
        content.FlexGrow = 1;
        content.FlexDirection = FlexDirection.Row;

        // Sidebar inside Content
        using var sidebar = engine.CreateNode();
        sidebar.Width = 100;

        // Main body inside Content
        using var mainBody = engine.CreateNode();
        mainBody.FlexGrow = 1;

        content.AddChild(sidebar);
        content.AddChild(mainBody);

        // Footer
        using var footer = engine.CreateNode();
        footer.Height = 50;

        root.AddChild(header);
        root.AddChild(content);
        root.AddChild(footer);

        Console.WriteLine("Calculating Layout...");
        root.CalculateLayout();

        Console.WriteLine($"Root Layout: {root.Layout}");
        Console.WriteLine($"Header Layout: {header.Layout}");
        Console.WriteLine($"Content Layout: {content.Layout}");
        Console.WriteLine($"Sidebar Layout: {sidebar.Layout}");
        Console.WriteLine($"MainBody Layout: {mainBody.Layout}");
        Console.WriteLine($"Footer Layout: {footer.Layout}");

        Console.WriteLine("Layout Calculated Successfully.");
    }
}
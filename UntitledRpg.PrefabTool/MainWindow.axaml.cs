using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace UntitledRpg.PrefabTool;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        BuildGrid();
        JsonTextBox.Text = "{ \"example\": \"This is JSON data\" }";
    }

    private void BuildGrid()
    {
        PrefabGrid.RowDefinitions.Clear();
        PrefabGrid.ColumnDefinitions.Clear();
        PrefabGrid.Children.Clear();

        for (var i = 0; i < 16; i++)
        {
            PrefabGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star));
            PrefabGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        }

        for (var row = 0; row < 16; row++)
        for (var col = 0; col < 16; col++)
        {
            var border = new Border
            {
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(0.5),
                Background = Brushes.White
            };
            border.PointerPressed += (s, e) => { border.Background = Brushes.Red; };
            Grid.SetRow(border, row);
            Grid.SetColumn(border, col);
            PrefabGrid.Children.Add(border);
        }
    }

    private void PrefabGrid_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var position = e.GetPosition(PrefabGrid);
        // Get cell size
        var cellWidth = PrefabGrid.Bounds.Width / PrefabGrid.ColumnDefinitions.Count;
        var cellHeight = PrefabGrid.Bounds.Height / PrefabGrid.RowDefinitions.Count;

        // Calculate row and column
        var col = (int)(position.X / cellWidth);
        var row = (int)(position.Y / cellHeight);

        // Find the child at (row, col)
        foreach (var child in PrefabGrid.Children)
            if (Grid.GetRow(child) == row && Grid.GetColumn(child) == col)
                if (child is Border border)
                    border.Background = Brushes.Red; // Change background color
    }
}
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

        for (int i = 0; i < 16; i++)
        {
            PrefabGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star));
            PrefabGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        }

        for (int row = 0; row < 16; row++)
        for (int col = 0; col < 16; col++)
        {
            Border border = new()
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
        Point position = e.GetPosition(PrefabGrid);
        // Get cell size
        double cellWidth = PrefabGrid.Bounds.Width / PrefabGrid.ColumnDefinitions.Count;
        double cellHeight = PrefabGrid.Bounds.Height / PrefabGrid.RowDefinitions.Count;

        // Calculate row and column
        int col = (int)(position.X / cellWidth);
        int row = (int)(position.Y / cellHeight);

        // Find the child at (row, col)
        foreach (Control? child in PrefabGrid.Children)
            if (Grid.GetRow(child) == row && Grid.GetColumn(child) == col)
                if (child is Border border)
                    border.Background = Brushes.Red; // Change background color
    }
}

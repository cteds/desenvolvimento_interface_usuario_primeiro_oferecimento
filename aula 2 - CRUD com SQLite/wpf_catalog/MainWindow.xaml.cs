using System.Linq;
using System.Windows;
using wpf_catalog.Data;

namespace wpf_catalog;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly Context context;
    Product NewProduct = new();
    Product selectedProduct = new();

    public MainWindow(Context context)
    {
        this.context = context;
        InitializeComponent();
        GetProducts();
        NewProductGrid.DataContext = NewProduct;
    }

    private void GetProducts()
    {
        ProductDataGrid.ItemsSource = context.Products.ToList();
    }

    private void AddItem(object s, RoutedEventArgs e)
    {
        context.Products.Add(NewProduct);
        context.SaveChanges();
        GetProducts();
        NewProduct = new Product();
        NewProductGrid.DataContext = NewProduct;
    }

    private void UpdateItem(object s, RoutedEventArgs e)
    {
        context.Update(selectedProduct);
        context.SaveChanges();
        GetProducts();
        UpdateProductGrid.DataContext = null;
    }

    private void SelectProductToEdit(object s, RoutedEventArgs e)
    {
        selectedProduct = (s as FrameworkElement).DataContext as Product;
        UpdateProductGrid.DataContext = selectedProduct;
    }

    private void DeleteProduct(object s, RoutedEventArgs e)
    {
        var productToDelete = (s as FrameworkElement).DataContext as Product;
        context.Products.Remove(productToDelete);
        context.SaveChanges();
        GetProducts();
    }
}

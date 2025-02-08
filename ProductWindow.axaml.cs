using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.EntityFrameworkCore;
using kurs.Entities;

namespace kurs;

public partial class ProductWindow : Window
{
    PostgresContext BDContext;
    public List<Product> Products { get; set; }
    public ProductWindow()
    {
        InitializeComponent();
        DataContext = this;
        BDContext = new PostgresContext();
        ShowTable();
    }
    public void ShowTable()
    {
        Products = BDContext.Products
            .Include(x => x.Suppliers)
            .Include(x => x.Category)
            .ToList();
        DataGridProduct.ItemsSource = Products;
    }
    private void BtnFilter_OnClick(object? sender, RoutedEventArgs e)
    {
    var priceFilter = this.FindControl<TextBox>("PriceFilter");
    var cenaText = priceFilter.Text?.Trim();

    if (!string.IsNullOrEmpty(cenaText) && decimal.TryParse(cenaText, out decimal cena))
    {
        Products = BDContext.Products
            .Where(x => x.Price == cena)
            .ToList();
    }
    else
    {
        Products = BDContext.Products.ToList();
    }

    DataGridProduct.ItemsSource = Products;
    }

    private void BtnAll_OnClick(object? sender, RoutedEventArgs e)
    {
        ShowTable();
    }
    private void ClientWindowBtn_Click(object? sender, RoutedEventArgs e)
    {
        var newClientWindow = new ClientWindow();
        newClientWindow.WindowStartupLocation = this.WindowStartupLocation;
        newClientWindow.Show();
        
        this.Close();
    }
    private void StatusBtn_Click(object? sender, RoutedEventArgs e)
    {
        var newStatusWindow = new MainWindow();
        newStatusWindow.WindowStartupLocation = this.WindowStartupLocation;
        newStatusWindow.Show();
        
        this.Close();
    }
    private void BtnCreate_OnClick(object? sender, RoutedEventArgs e)
    {
        ShowTable();
    }
    private void BtnDelete_OnClick(object? sender, RoutedEventArgs e)
    {
        var SelItem = DataGridProduct.SelectedItem as Product;
        if (SelItem == null){
            return;
        }

        try
        {
            BDContext.Products.Remove(SelItem);
            BDContext.SaveChanges();
            ShowTable();
        }
        catch (System.Exception)
        {
            Console.Write("Error");
        }
    }
}
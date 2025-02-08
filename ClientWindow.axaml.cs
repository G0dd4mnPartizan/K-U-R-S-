using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.EntityFrameworkCore;
using kurs.Entities;

namespace kurs;

public partial class ClientWindow : Window
{
    PostgresContext BDContext;
    public List<Client> Clients { get; set; }
    public ClientWindow()
    {
        InitializeComponent();
        DataContext = this;
        BDContext = new PostgresContext();
        ShowTable();
    }
    
     public void ShowTable()
    {
        Clients = BDContext.Clients
            .Include(x => x.PaymentType)
            .ToList();
        DataGridClient.ItemsSource = Clients;
    }
    private void BtnFilter_OnClick(object? sender, RoutedEventArgs e)
    {
    var fioFilter = this.FindControl<TextBox>("FioFilter");

    var fioText = fioFilter.Text?.Trim();

    var query = BDContext.Clients.AsQueryable();

    if (!string.IsNullOrEmpty(fioText))
    {
        query = query.Where(x => x.Fio.Contains(fioText));
    }

    DataGridClient.ItemsSource = Clients;
    }

    private void BtnAll_OnClick(object? sender, RoutedEventArgs e)
    {
        ShowTable();
    }
    private void ProductBtn_Click(object? sender, RoutedEventArgs e)
    {
        var newProductWindow = new ProductWindow();
        newProductWindow.WindowStartupLocation = this.WindowStartupLocation;
        newProductWindow.Show();
        
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
        var SelItem = DataGridClient.SelectedItem as Client;
        if (SelItem == null){
            return;
        }

        try
        {
            BDContext.Clients.Remove(SelItem);
            BDContext.SaveChanges();
            ShowTable();
        }
        catch (System.Exception)
        {
            Console.Write("Error");
        }
    }
}
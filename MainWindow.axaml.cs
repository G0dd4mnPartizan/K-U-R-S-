using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.EntityFrameworkCore;
using kurs.Entities;

namespace kurs;

public partial class MainWindow : Window
{
    PostgresContext BDContext;
    public List<Status> Statuses { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        BDContext = new PostgresContext();
        ShowTable();
    }
    public void ShowTable()
    {
        Statuses = BDContext.Statuses
            .Include(x => x.Client)
            .Include(x => x.Delivery)
            .Include(x => x.Order)
            .ToList();
        DataGridStatus.ItemsSource = Statuses;
    }
    private void BtnFilter_OnClick(object? sender, RoutedEventArgs e)
    {
        var statusFilter = this.FindControl<TextBox>("StatusFilter");

        var status = statusFilter.Text?.Trim();

        if (!string.IsNullOrEmpty(status))
        {
            Statuses = BDContext.Statuses
                .Where(x => x.Delivery.Name.Contains(status))
                .Include(x => x.Client)
                .Include(x => x.Delivery)
                .Include(x => x.Order)
                .ToList();
        }
        else
        {
            Statuses = BDContext.Statuses
                .Include(x => x.Client)
                .Include(x => x.Delivery)
                .Include(x => x.Order)
                .ToList();
        }

        DataGridStatus.ItemsSource = Statuses;
    }

    private void BtnAll_OnClick(object? sender, RoutedEventArgs e)
    {
        ShowTable();
    }
    private void ClientBtn_Click(object? sender, RoutedEventArgs e)
    {
        var newClientWindow = new ClientWindow();
        newClientWindow.WindowStartupLocation = this.WindowStartupLocation;
        newClientWindow.Show();
        
        this.Close();
    }
    private void ProductBtn_Click(object? sender, RoutedEventArgs e)
    {
        var newProductWindow = new ProductWindow();
        newProductWindow.WindowStartupLocation = this.WindowStartupLocation;
        newProductWindow.Show();
        
        this.Close();
    }
    private void CreateBtn_Click(object? sender, RoutedEventArgs e)
    {
        var newStatus = new Status();
        BDContext.Statuses.Add(newStatus);

        var newStatusAddWindow = new StatusAddWindow(this.BDContext, newStatus);
        newStatusAddWindow.WindowStartupLocation = this.WindowStartupLocation;
        newStatusAddWindow.Show();
    
        this.Close();
    }

    private void BtnDelete_OnClick(object? sender, RoutedEventArgs e)
    {
        var SelItem = DataGridStatus.SelectedItem as Status;
        if (SelItem == null){
            return;
        }

        try
        {
            BDContext.Statuses.Remove(SelItem);
            BDContext.SaveChanges();
            ShowTable();
        }
        catch (System.Exception)
        {
            Console.Write("Error");
        }
    }
}
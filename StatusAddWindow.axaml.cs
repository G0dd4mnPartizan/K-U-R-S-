using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using kurs.Entities;

namespace kurs;

public partial class StatusAddWindow : Window
{
    public PostgresContext BDContext { get; set; }
    public Status CurrentStatus { get; set; }
    public StatusAddWindow(PostgresContext bdcontext, Status currentStatus)
    {
        InitializeComponent();
        this.BDContext = bdcontext;
        this.CurrentStatus = currentStatus;
        FillClient();
        FillOrder();
        FillDelivery();
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void FillClient()
    {
        if (BDContext == null) return;

        var client = BDContext.Clients.ToList();
        var cmbClient = this.FindControl<ComboBox>("cmbClient");
        cmbClient.ItemsSource = client;
    }
    private void FillOrder()
    {
        if (BDContext == null) return;

        var order = BDContext.Categories.ToList();
        var cmbOrderList = this.FindControl<ComboBox>("cmbOrder");
        cmbOrderList.ItemsSource = order;
    }
    private void FillDelivery()
    {
        if (BDContext == null) return;

        var delivery = BDContext.Deliveries.ToList();
        var cmbDeliveryList = this.FindControl<ComboBox>("cmbDelivery");
        cmbDeliveryList.ItemsSource = delivery;
    }

    private void BackButton_Click(object? sender, RoutedEventArgs e)
    {
        var newDeliveryWindow = new MainWindow();
        newDeliveryWindow.WindowStartupLocation = this.WindowStartupLocation;
        newDeliveryWindow.Show();
        
        this.Close();
    }
    private void BtnSave_OnClick(object? sender, RoutedEventArgs e)
    {
    var cmbclient = this.FindControl<ComboBox>("cmbClient");
    var cmborder = this.FindControl<ComboBox>("cmbOrder");
    var cmbdelivery = this.FindControl<ComboBox>("cmbDelivery");
    var txtBoxQuantity = this.FindControl<TextBox>("TxtBoxQuantity");
    var txtBoxAddress = this.FindControl<TextBox>("TxtBoxAddress");
    var txtBoxAmount = this.FindControl<TextBox>("TxtBoxAmount");

    var client = cmbclient.SelectedItem as Client;
    var order = cmborder.SelectedItem as Order;
    var delivery = cmbdelivery.SelectedItem as Delivery;

    var address = txtBoxAddress.Text;

    if (int.TryParse(txtBoxQuantity.Text, out int quantity) &&
        decimal.TryParse(txtBoxAmount.Text, out decimal amount))
    {
        try
        {
            if (client != null && delivery != null)
            {
                if (CurrentStatus != null)
                {
                    this.CurrentStatus.Client = client;
                    this.CurrentStatus.Order = order;
                    this.CurrentStatus.Delivery = delivery;
                    this.CurrentStatus.OrderId = quantity;
                    this.CurrentStatus.DeliveryAddress = address;
                    this.CurrentStatus.Amount = amount;

                    BDContext.SaveChanges();
                        
                    var newStatusWindow = new MainWindow();
                    newStatusWindow.WindowStartupLocation = this.WindowStartupLocation;
                    newStatusWindow.Show();
        
                    this.Close();
                }
            }
            else 
            { 
                Console.WriteLine("Пожалуйста, заполните не заполненные поля.");
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    else
    {
        Console.WriteLine("Пожалуйста, введите корректные числовые значения.");
    }
    }
}
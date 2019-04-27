using CustomerDatabase.Commands;
using CustomerDatabase.Interfaces;
using CustomerDatabase.Models;
using CustomerDatabase.Services;
using CustomerDatabase.Views;
using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;

namespace CustomerDatabase.ViewModels
{
  public class CustomerListViewModel : INotifyPropertyChanged
  {
    #region Fields

    private DataRowView _selectedCustomer;
    private DataTable _customers;
    private readonly ICustomerService _service;
    #endregion    

    #region Constructors

    public CustomerListViewModel()
    {
      _service = new CustomerService();
      AddCommand = new BaseCommand(ExecuteAddCommand);
      EditCommand = new BaseCommand(ExecuteEditCommand, CanExecuteEditCommand);
      CustomerSaved += OnCustomerSavedHandler;
      DisplayTable();
    }    
    #endregion

    #region Dependency Properties

    public DataRowView SelectedCustomer
    {
      get => _selectedCustomer;

      set
      {
        _selectedCustomer = value;
        NotifyPropertyChanged();
      }
    }

    public DataTable Customers
    {
      get => _customers;

      set
      {
        _customers = value;
        NotifyPropertyChanged();
      }
    }
    #endregion

    #region Commands

    public BaseCommand AddCommand { get; set; }

    private void ExecuteAddCommand(object obj)
    {
      var handler = CustomerSaved;
      var addCustomerView = new AddCustomerView(Customers.NewRow(), Mode.Add, handler);
      SelectedCustomer = null;
      addCustomerView.ShowDialog();
    }

    public BaseCommand EditCommand { get; set; }

    private bool CanExecuteEditCommand(object obj)
    {
      return SelectedCustomer != null;
    }

    private void ExecuteEditCommand(object obj)
    {
      var handler = CustomerSaved;
      var editCustomerView = new AddCustomerView(SelectedCustomer.Row, Mode.Edit, handler);
      editCustomerView.Title = "Edit customer";
      SelectedCustomer = null;
      editCustomerView.ShowDialog();
    }
    #endregion

    #region Events

    public event EventHandler CustomerSaved;
    #endregion

    #region Methods

    private void DisplayTable()
    {
      Customers = _service.SelectAll();
    }

    private void OnCustomerSavedHandler(object sender, EventArgs e)
    {
      DisplayTable();
    }
    #endregion

    #region INotifyPropertyChanged

    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler PropertyChanged;
    #endregion
  }
}

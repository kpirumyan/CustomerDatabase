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
    private string _search;
    private DataTable _allCustomers;
    private readonly ICustomerService _service;
    #endregion    

    #region Constructors

    public CustomerListViewModel()
    {
      _service = new CustomerService();

      // Register command handlers
      AddCommand = new BaseCommand(ExecuteAddCommand);
      EditCommand = new BaseCommand(ExecuteEditCommand, CanExecuteEditCommand);

      CustomerSaved += OnCustomerSavedHandler;
      _allCustomers = _service.SelectAll();
      DisplayAllTable();
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

    // This is an active DataTable property which depends on search filter.
    // If the filter is empty than table is equal to _allCustomers table.
    public DataTable Customers
    {
      get => _customers;

      set
      {
        _customers = value;
        NotifyPropertyChanged();
      }
    }

    public string Search
    {
      get => _search;

      set
      {
        _search = value;
        DisplayFilteredTable();
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
      addCustomerView.ShowDialog();
      SelectedCustomer = null;
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
      editCustomerView.ShowDialog();
      SelectedCustomer = null;
    }
    #endregion

    #region Events

    public event EventHandler CustomerSaved;
    #endregion

    #region Methods

    private void DisplayAllTable()
    {
      Customers = _allCustomers;
    }

    private void DisplayFilteredTable()
    {
      // Filter rows by search field value
      DataRow[] rows = _allCustomers.Select($"FirstName LIKE '%{Search}%' OR LastName LIKE '%{Search}%'");

      // If rows have found then display filtered table else display empty table
      if (rows.Length > 0)
      {
        Customers = rows.CopyToDataTable();
      }
      else
      {
        Customers = _allCustomers.Copy();
        Customers.Clear();
      }
    }

    // This event hadler will being called on 'save' button click
    private void OnCustomerSavedHandler(object sender, EventArgs e)
    {
      _allCustomers = _service.SelectAll();

      if (string.IsNullOrEmpty(Search))
        DisplayAllTable();
      else
        DisplayFilteredTable();
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

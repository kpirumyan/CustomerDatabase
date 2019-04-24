using CustomerDatabase.Commands;
using CustomerDatabase.Models;
using CustomerDatabase.Services;
using CustomerDatabase.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CustomerDatabase.ViewModels
{
  public class CustomerListViewModel : INotifyPropertyChanged
  {
    #region Fields

    private DataRowView _selectedCustomer;
    private DataTable _customers;
    private readonly CustomerService _service;
    #endregion    

    #region Constructors

    public CustomerListViewModel()
    {
      _service = new CustomerService();
      AddCommand = new BaseCommand(ExecuteAddCommand);
      EditCommand = new BaseCommand(ExecuteEditCommand, CanExecuteEditCommand);
      DisplayTable();
    }    
    #endregion

    #region DependencyProperties

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

    public static BaseCommand AddCommand { get; set; }

    private void ExecuteAddCommand(object obj)
    {
      var addCustomerView = new AddCustomerView();
      addCustomerView.Show();
    }

    public static BaseCommand EditCommand { get; set; }

    private bool CanExecuteEditCommand(object obj)
    {
      return SelectedCustomer != null;
    }

    private void ExecuteEditCommand(object obj)
    {
      var editCustomerView = new AddCustomerView();
      editCustomerView.Title = "Edit customer";
      editCustomerView.Show();
    }
    #endregion

    #region Methods

    private void DisplayTable()
    {
      Customers = _service.SelectAll();
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

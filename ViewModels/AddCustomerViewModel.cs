using CustomerDatabase.Commands;
using CustomerDatabase.Models;
using CustomerDatabase.Services;
using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;

namespace CustomerDatabase.ViewModels
{
  public class AddCustomerViewModel : INotifyPropertyChanged
  {
    #region Fields

    private Mode _mode = Mode.Add;
    private DataRow _customer;
    private readonly CustomerService _customerService;
    #endregion

    #region Constructor

    public AddCustomerViewModel(DataRow row, Mode mode)
    {
      _customerService = new CustomerService();
      _mode = mode;
      Customer = row;
      SaveCommand = new BaseCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
      CancelCommand = new BaseCommand(ExecuteCancelCommand);
    }
    #endregion

    #region Dependency Properties

    public DataRow Customer
    {
      get => _customer;

      set
      {
        _customer = value;
        NotifyPropertyChanged();
      }
    }
    #endregion

    #region Commands

    public BaseCommand SaveCommand { get; set; }

    private bool CanExecuteSaveCommand(object obj)
    {
      return true;
    }

    private void ExecuteSaveCommand(object obj)
    {
      if (_mode == Mode.Edit)
      {
        _customerService.Update(Customer);
      }
      else
      {
        _customerService.Insert(Customer);
      }
      App.Current.Windows[2].Close();
    }

    public BaseCommand CancelCommand { get; set; }

    private void ExecuteCancelCommand(object obj)
    {
      App.Current.Windows[2].Close();
    }
    #endregion

    #region Methods

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

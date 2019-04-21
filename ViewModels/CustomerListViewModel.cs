using CustomerDatabase.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CustomerDatabase.ViewModels
{
  public class CustomerListViewModel : INotifyPropertyChanged
  {
    #region Fields
    private Customer _customer;
    #endregion    

    #region Constructors

    public CustomerListViewModel()
    {
      Customer = new Customer();
    }
    #endregion

    #region DependencyProperties
    public Customer Customer
    {
      get => _customer;

      set
      {
        _customer = value;
        NotifyPropertyChanged();
      }
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

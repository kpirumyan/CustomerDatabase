using CustomerDatabase.Commands;
using CustomerDatabase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDatabase.ViewModels
{
  public class AddCustomerViewModel : INotifyPropertyChanged
  {
    #region Fields

    private int _id;
    private string _firstName;
    private string _lastName;
    private int _age;
    private Sex _sex;
    private string _passportId;
    private string _address;
    private Mode _mode = Mode.Add;
    #endregion

    #region Constructor

    public AddCustomerViewModel()
    {
      SaveCommand = new BaseCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
      CancelCommand = new BaseCommand(ExecuteCancelCommand);
    }

    public AddCustomerViewModel(object selectedCustomer, Mode mode) : this()
    {
      _mode = mode;
      ConvertRowToCustomer(selectedCustomer as DataRowView);
    }
    #endregion

    #region Dependency Properties

    public int Id
    {
      get => _id;

      set
      {
        _id = value;
        NotifyPropertyChanged();
      }
    }

    public string FirstName
    {
      get => _firstName;

      set
      {
        _firstName = value;
        NotifyPropertyChanged();
      }
    }

    public string LastName
    {
      get => _lastName;

      set
      {
        _lastName = value;
        NotifyPropertyChanged();
      }
    }

    public int Age
    {
      get => _age;

      set
      {
        _age = value;
        NotifyPropertyChanged();
      }
    }

    public string PassportId
    {
      get => _passportId;

      set
      {
        _passportId = value;
        NotifyPropertyChanged();
      }
    }

    public Sex Sex
    {
      get => _sex;

      set
      {
        _sex = value;
        NotifyPropertyChanged();
      }
    }

    public string Address
    {
      get => _address;

      set
      {
        _address = value;
        NotifyPropertyChanged();
      }
    }
    #endregion

    #region Commands

    public BaseCommand SaveCommand { get; set; }

    private bool CanExecuteSaveCommand(object obj)
    {
      return false;
    }

    private void ExecuteSaveCommand(object obj)
    {
      throw new NotImplementedException();
    }

    public BaseCommand CancelCommand { get; set; }

    private void ExecuteCancelCommand(object obj)
    {
      App.Current.Windows[2].Close();
    }
    #endregion

    #region Methods

    private void ConvertRowToCustomer(DataRowView row)
    {
      if (row != null)
      {
        if (row["Id"] is int)
        {
          Id = (int)row["Id"];
        }
        if (row["FirstName"] is string)
        {
          FirstName = (string)row["FirstName"];
        }
        if (row["LastName"] is string)
        {
          LastName = (string)row["LastName"];
        }
        if (row["Age"] is int)
        {
          Age = (int)row["Age"];
        }
        if (row["PassportId"] is string)
        {
          PassportId = (string)row["PassportId"];
        }
        if (row["Sex"] is Sex)
        {
          Sex = (Sex)row["Sex"];
        }
        if (row["Address"] is string)
        {
          Address = (string)row["Address"];
        }
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

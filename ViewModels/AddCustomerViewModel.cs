using CustomerDatabase.Commands;
using CustomerDatabase.Models;
using CustomerDatabase.Services;
using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace CustomerDatabase.ViewModels
{
  public class AddCustomerViewModel : INotifyPropertyChanged, IDataErrorInfo
  {
    #region Fields
    private Mode _mode = Mode.Add;
    private string _firstName;
    private string _lastName;
    private string _age;
    private string _passportId;
    private string _sex;
    public bool _hasError;
    private string _address;
    private DataRow _row;
    private readonly CustomerService _customerService;
    private EventHandler _handler;
    #endregion

    #region Constructor

    public AddCustomerViewModel(DataRow row, Mode mode, EventHandler handler)
    {
      _customerService = new CustomerService();
      _mode = mode;
      _handler = handler;
      _row = row;
      InitProperties(_row);
      SaveCommand = new BaseCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
      CancelCommand = new BaseCommand(ExecuteCancelCommand);
    }
    #endregion

    #region Dependency Properties

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

    public string Age
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

    public string Sex
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
    public bool HasError
    {
      get { return _hasError; }
      set
      {
        _hasError = value;
        NotifyPropertyChanged();
      }
    }

    #endregion

    #region Methods

    private void InitProperties(DataRow row)
    {
      FirstName = row["FirstName"].ToString();
      LastName = row["LastName"].ToString();
      Age = row["Age"].ToString();
      PassportId = row["PassportId"].ToString();
      Sex = row["Sex"].ToString();
      Address = row["Address"].ToString();
    }

    private DataRow BuildDataRow()
    {
      _row["FirstName"] = FirstName;
      _row["LastName"] = LastName;
      _row["Age"] = Age;
      _row["PassportId"] = PassportId;
      _row["Sex"] = Sex;
      _row["Address"] = Address;

      return _row;
    }
    #endregion

    #region Commands

    public BaseCommand SaveCommand { get; set; }

    private bool CanExecuteSaveCommand(object obj)
    {
      if (string.IsNullOrEmpty(FirstName)
        || string.IsNullOrEmpty(LastName)
        || string.IsNullOrEmpty(PassportId)
        || HasError)
      {
        return false;
      }

      return true;
    }

    private void ExecuteSaveCommand(object obj)
    {
      DataRow row = BuildDataRow();

      if (_mode == Mode.Edit)
      {
        _customerService.Update(row);
      }
      else
      {
        _customerService.Insert(row);
      }

      _handler?.Invoke(this, null);
      App.Current.Windows[2].Close();
    }

    public BaseCommand CancelCommand { get; set; }

    private void ExecuteCancelCommand(object obj)
    {
      App.Current.Windows[2].Close();
    }
    #endregion

    #region INotifyPropertyChanged

    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler PropertyChanged;
    #endregion

    #region IDataErrorInfo

    public string Error
    {
      get
      {
        return null;
      }
    }

    public string this[string columnName]
    {
      get
      {
        HasError = false;
        string msg = null;

        switch (columnName)
        {
          case "FirstName":
            break;
          case "LastName":
            break;
          case "Age":
            if (Age != null && Age.Length > 3)
            {
              msg = "Max length = 3";
              HasError = true;
            }
            break;
          case "PassportId":
            var regex = new Regex(@"^[a-zA-Z]{2}\d{6}$");
            if (!string.IsNullOrEmpty(PassportId) && !regex.IsMatch(PassportId))
            {
              msg = "ID format: XX000000";
              HasError = true;
            }
            break;
          case "Sex":
            break;
          case "Address":
            break;

          default:
            throw new ArgumentException("Unrecognized property: " + columnName);
        }

        return msg;
      }
    }
    #endregion
  }
}

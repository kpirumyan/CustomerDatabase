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
    private bool _hasError;
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

      // Set form fields default values by initializing dependency properties 
      InitProperties(_row);

      // Register command handlers
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

    // This property will being used to enable or disable 'Save' button depending on the validation
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

    // Converts DataRow to separated dependency properties for binding and validation
    private void InitProperties(DataRow row)
    {
      FirstName = row["FirstName"].ToString();
      LastName = row["LastName"].ToString();
      Age = row["Age"].ToString();
      PassportId = row["PassportId"].ToString();
      Sex = row["Sex"].ToString();
      Address = row["Address"].ToString();
    }

    // Builds DataRow from dependency properties for database insert or update commands usage
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

    // Enables 'Save' button when FirstName, LastName and PassportId fields are not empty 
    // and there are no validation errors.
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

      // Invoke CustomerSaved event handler
      _handler?.Invoke(this, null);

      // Close child window
      App.Current.Windows[2].Close();
    }

    public BaseCommand CancelCommand { get; set; }

    private void ExecuteCancelCommand(object obj)
    {
      // Close child window
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

    // The property will not be used
    public string Error
    {
      get
      {
        return null;
      }
    }

    // Validation rules
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
            // Validation fails if 'Age' field contains more than 3 digits
            if (Age != null && Age.Length > 3)
            {
              msg = "Max length = 3";
              HasError = true;
            }
            break;
          case "PassportId":
            // Validation fails if input format does not match to 'XX000000'
            var regex = new Regex(@"^[a-zA-Z]{2}\d{6}$");
            if (!string.IsNullOrEmpty(PassportId) && !regex.IsMatch(PassportId))
            {
              msg = "ID format: XX000000";
              HasError = true;
            }

            // Check if Passport Id is unique
            DataTable table = _customerService.SelectAll();
            if ((_mode == Mode.Add && table.Select($"PassportId = '{PassportId}'").Length > 0) 
              || (_mode == Mode.Edit && table.Select($"Id <> {_row["Id"]} AND PassportId = '{PassportId}'").Length > 0))
            {
              msg = "ID must be unique";
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

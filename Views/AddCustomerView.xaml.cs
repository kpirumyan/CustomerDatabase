using CustomerDatabase.Models;
using CustomerDatabase.ViewModels;
using System;
using System.Data;
using System.Linq;
using System.Windows;

namespace CustomerDatabase.Views
{
  /// <summary>
  /// Interaction logic for AddUserView.xaml
  /// </summary>
  public partial class AddCustomerView : Window
  {
    public AddCustomerView(DataRow row, Mode mode)
    {
      InitializeComponent();
      DataContext = new AddCustomerViewModel(row, mode);
      sexComboBox.ItemsSource = Enum.GetValues(typeof(Sex)).Cast<Sex>();
    }
  }
}

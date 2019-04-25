using CustomerDatabase.Models;
using CustomerDatabase.ViewModels;
using System;
using System.Linq;
using System.Windows;

namespace CustomerDatabase.Views
{
  /// <summary>
  /// Interaction logic for AddUserView.xaml
  /// </summary>
  public partial class AddCustomerView : Window
  {
    public AddCustomerView()
    {
      InitializeComponent();
      DataContext = new AddCustomerViewModel();
      sexComboBox.ItemsSource = Enum.GetValues(typeof(Sex)).Cast<Sex>();
    }

    public AddCustomerView(object param, Mode mode)
    {
      InitializeComponent();
      DataContext = new AddCustomerViewModel(param, mode);
      sexComboBox.ItemsSource = Enum.GetValues(typeof(Sex)).Cast<Sex>();
    }
  }
}

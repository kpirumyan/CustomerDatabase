using CustomerDatabase.Models;
using CustomerDatabase.ViewModels;
using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace CustomerDatabase.Views
{
  /// <summary>
  /// Interaction logic for AddUserView.xaml
  /// </summary>
  public partial class AddCustomerView : Window
  {
    public AddCustomerView(DataRow row, Mode mode, EventHandler handler)
    {
      InitializeComponent();
      DataContext = new AddCustomerViewModel(row, mode, handler);
      sexComboBox.ItemsSource = Enum.GetValues(typeof(Sex)).Cast<Sex>();
    }

    private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
      var regex = new Regex("[^0-9]+");
      e.Handled = regex.IsMatch(e.Text);
    }
  }
}

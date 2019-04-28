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

      // ViewModel constructor takes 'selectedItem row', 'mode'(add or edit) and CustomerSaved event as parameters. 
      // The event will being invoked on save button click.
      DataContext = new AddCustomerViewModel(row, mode, handler);

      // Add enum 'Sex' to combobox items source
      sexComboBox.ItemsSource = Enum.GetValues(typeof(Sex)).Cast<Sex>();
    }

    // This is an event handler for 'Age' field. It can only accept numbers.
    private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
      var regex = new Regex("[^0-9]+");
      e.Handled = regex.IsMatch(e.Text);
    }
  }
}

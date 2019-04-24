using CustomerDatabase.ViewModels;
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
    }
  }
}

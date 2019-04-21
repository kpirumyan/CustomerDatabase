using CustomerDatabase.ViewModels;
using System.Windows;

namespace CustomerDatabase.Views
{
  /// <summary>
  /// Interaction logic for CustomerListView.xaml
  /// </summary>
  public partial class CustomerListView : Window
  {
    public CustomerListView()
    {
      InitializeComponent();
      this.DataContext = new CustomerListViewModel();
    }
  }
}

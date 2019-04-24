using CustomerDatabase.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;

namespace CustomerDatabase.Interfaces
{
  public interface ICustomerService
  {
    Task Insert(Customer customer);

    Task Update(Customer customer);

    //Task<ObservableCollection<Customer>> SelectAll();
    DataTable SelectAll();

    Task<ObservableCollection<Customer>> FindByFullName(string str);
  }
}

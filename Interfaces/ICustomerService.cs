using CustomerDatabase.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;

namespace CustomerDatabase.Interfaces
{
  public interface ICustomerService
  {
    void Insert(DataRow row);

    void Update(DataRow row);

    DataTable SelectAll();

    DataRowView FindByFullName(string str);
  }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDatabase.Models
{
  public enum Sex
  {
    [Description("None")]
    None,

    [Description("Male")]
    Male,

    [Description("Female")]
    Female
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DerinatManagement.Services.DataAccess;
public interface IDao
{
    
    public List<T> GetData<T>();
}

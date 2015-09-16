using System.Collections.Generic;

namespace Termine.Promises.Base.Interfaces
{
    public interface IAmAPromiseUser
    {
        string UserName { get; set; }
        IEnumerable<string> Claims { get; set;  }
    }
}

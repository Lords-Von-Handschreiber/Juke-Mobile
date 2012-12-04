using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juke_Mobile_Model.Database
{
    /// <summary>
    /// Database Observer
    /// </summary>
    public interface IDbReceiver
    {
        void Update();
    }
}

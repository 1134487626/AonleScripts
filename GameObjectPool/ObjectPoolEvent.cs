using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anole
{
    public interface IObjectPoolEvent
    {
        Action OnEnable { get; set; }

        Action OnDisable { get; set; }

        Action OnDestroy { get; set; }
    }
}

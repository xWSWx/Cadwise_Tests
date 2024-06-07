using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProcessingTextFiles.Wrappers
{
    public class CancellationTokenWrapper : CancellationTokenSource
    {
        
        public Guid Id = Guid.Empty;
        
        private CancellationTokenWrapper() : base() { }
        public CancellationTokenWrapper(Guid newId) : this()
        {
            Id = newId;
        }
    }
}

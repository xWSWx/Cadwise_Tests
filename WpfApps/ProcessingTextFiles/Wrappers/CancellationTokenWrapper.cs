using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProcessingTextFiles.Wrappers
{
    public class CustomCancellationToken
    {
        
        public Guid Id = Guid.Empty;
        public bool Cancelled = false;
        public bool Started = false;        

        public void Stop ()  => Cancelled = true;
        public void Start () => Started = true;
        

        public CustomCancellationToken() : base() 
        {
            Id = Guid.Empty;
        }
        public CustomCancellationToken(Guid newId) : this()
        {
            Id = newId;
        }
    }
}

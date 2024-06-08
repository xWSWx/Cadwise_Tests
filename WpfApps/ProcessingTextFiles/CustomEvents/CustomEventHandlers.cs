using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingTextFiles.CustomEvents
{
    public class CustomEventHandlers
    {
        public delegate void GuidEventHandler(object? sender, GuidEventArgs e);
        public class GuidEventArgs : EventArgs
        {
            public Guid id;
            private GuidEventArgs() { }
            public GuidEventArgs(Guid id) : this() 
            {
                this.id = id;
            }
        }

        public delegate void ProcessingHandler(object? sender, ProcessingArgs e);
        public class ProcessingArgs : EventArgs
        {
            public Guid tokenId;
            public int CompletedPercent;
            private ProcessingArgs() { }
            public ProcessingArgs(Guid id, int completedPercent) : this()
            {
                this.tokenId = id;                
                CompletedPercent = completedPercent;
            }
        }

        public delegate void PathEventHandler(object? sender, PathEventArgs e);
        public class PathEventArgs
        {
            public string path;
            private PathEventArgs():base() { }
            public PathEventArgs(string path) :this()
            {
                this.path = path;
            }
        }

        public delegate void StringGuidEventHandler(object? sender, StringGuidEventArgs e);
        public class StringGuidEventArgs 
        {
            public string str;
            public Guid id;
            private StringGuidEventArgs() : base(){}
            public StringGuidEventArgs(string str, Guid id):this()
            {
                this.str = str;
                this.id = id;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionTests
{
    public class NullScpoe : IDisposable
    {
        public static NullScpoe Instance { get; } = new NullScpoe();
        private NullScpoe() { }
        public void Dispose(){}
    }
}

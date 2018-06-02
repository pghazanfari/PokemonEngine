using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModelUnitTests.MoveImpl;

namespace ModelUnitTests
{
    public partial class Move
    {
        public static readonly Tackle Tackle = Tackle.Instance;
        public static readonly Growl Growl = Growl.Instance;

        private Move() { }
    }
}

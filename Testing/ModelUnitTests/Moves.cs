using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModelUnitTests.Moves;

namespace ModelUnitTests
{
    public partial class Move
    {
        public static readonly MTackle Tackle = MTackle.Instance;

        private Move() { }
    }
}

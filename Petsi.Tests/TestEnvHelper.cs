using Petsi.Filing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsi.Tests
{
    public class TestEnvHelper
    {
        public FileBehavior fb;
        public TestEnvHelper()
        {
            fb = new FileBehavior("Test_Env_Helper");
        }
    }
}

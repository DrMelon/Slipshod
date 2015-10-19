using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace Slipshod
{
    class Global
    {
        // Public Static Refs go in this file. It's ugly, but gets the job done.
        public static Game theGame;
        public static Session thePlayerSession;
        public static ControllerXbox360 theController;
    }
}

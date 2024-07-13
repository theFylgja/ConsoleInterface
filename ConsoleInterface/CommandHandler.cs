using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInterface
{
    public class Handler
    {
        public void CIHandle(Command cmd)
        {
            Next.Adv("at 'CIHandle'");
            switch(cmd.command[1])
            {
                case "settings":
                    break;
            }
        }
    }
}

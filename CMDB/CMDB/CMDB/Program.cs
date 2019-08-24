using CMDB.Managers;
using System;

namespace CMDB
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleManager manager = new ConsoleManager();
            manager.Start();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TP_Link_Router_Auto_Restart
{
    internal class Program
    {
        private static void Main()
        {
            Router router = new Router();
            router.Go();
        }
    }
}

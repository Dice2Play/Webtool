using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebHelperProject
{

    // This class is responsible for all browser related tasks
    public static partial class WebHelper
    {
        public static void OpenWebPageInBrowser(Uri urlToOpen)
        {
            var chromeDriverApplicationPath = $"{Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ChromeDriver")}";

            ChromeDriver chromeDriver = new ChromeDriver(@chromeDriverApplicationPath);
            chromeDriver.Navigate().GoToUrl("https://www.google.nl");

        }

    }
}

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
        #region Public methods
        /// <summary>
        /// Opens Uri in browser using selenium library.
        /// </summary>
        /// <param name="urlToOpen">Uri to open.</param>
        public static void OpenWebPageInBrowser(Uri urlToOpen)
        {
            // Browse to input uri
            ReturnChromeDriver().Navigate().GoToUrl(urlToOpen);
        }

        /// <summary>
        /// Opens Uri in browser using selenium library.
        /// </summary>
        /// <param name="urlToOpen">Uri to open.</param>
        public static void OpenWebPageInBrowser(Uri urlToOpen, HttpCookie cookiesToUse)
        {
            // Set cookie(s)


            // Browse to input uri
            ReturnChromeDriver().Navigate().GoToUrl(urlToOpen);
        }

        /// <summary>
        /// Opens Uri in browser using selenium library.
        /// </summary>
        /// <param name="urlToOpen">Uri to open.</param>
        public static void OpenWebPageInBrowser(Uri urlToOpen, HttpCookie cookiesToUse, UserAgent userAgentToUse )
        {
            // Set cookie(s)


            // Set user agent


            // Browse to input uri
            ReturnChromeDriver().Navigate().GoToUrl(urlToOpen);
        }




        #endregion


        #region Private methods

        private static ChromeDriver ReturnChromeDriver()
        {
            var chromeDriverApplicationPath = $"{Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ChromeDriver")}";
            return new ChromeDriver(@chromeDriverApplicationPath);
        }

        #endregion




    }
}

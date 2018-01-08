using System.Net;

namespace WebHelperProject
{
    using WebHelperTool.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HtmlAgilityPack;
    using FileHelperTool;
    using System.IO;
    using System.Reflection;
    using FileHelperTool.CRUD;
    
    /// <summary>
    /// This class is responsible for loading anything related to the web
    /// </summary>
    public static partial class WebHelper
    {
        #region Public methods

        /// <summary>
        /// Returns content of webpage
        /// </summary>
        /// <param name="webURL">URL of webpage to load</param>
        /// <returns>HtmlDocument</returns>
        public static HtmlDocument ReturnContentOfPageHtmlDocument(Uri webURL)
        {
            var web = new HtmlWeb();
            var doc = web.Load(webURL);

            return doc;
        }
        /// <summary>
        /// Returns content of webpage 
        /// </summary>
        /// <param name="webURL">URL of webpage to load.</param>
        /// <param name="contentType">What content to return.</param>
        /// <param name="options">What options to return.</param>
        /// <returns>(Filtered) HtmlDocument</returns>
        public static HtmlDocument ReturnContentOfPageHtmlDocument(Uri webURL, HtmlDocumentFilterContentType contentType, HtmlDocumentFilterOptions options)
        {
            var web = new HtmlWeb();
            var doc = web.Load(webURL);

            return doc;
        }

        public static HttpWebResponse ReturnWebHttpWebResponse(Uri webURL)
        {
            // Creates an HttpWebRequest for the specified URL. 
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(webURL);
            // Sends the HttpWebRequest and waits for response.
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

            // Print headers to console
            //PrintResponseHeaders(myHttpWebResponse);


            return myHttpWebResponse;
        }

        #endregion


        #region Private methods



        #region Diagnostics
        /// <summary>
        /// Prints all headers from input WebResponse to console
        /// </summary>
        /// <param name="httpWebResponse"></param>
        private static void PrintResponseHeaders(HttpWebResponse httpWebResponse)
        {
            // Displays all the headers present in the response received from the URI.
            Console.WriteLine("\r\nThe following headers were received in the response:");


            for (int i = 0; i < httpWebResponse.Headers.Count; ++i)
                Console.WriteLine("\nHeader Name:{0}, Value :{1}", httpWebResponse.Headers.Keys[i], httpWebResponse.Headers[i]);
        }


        #endregion




        #endregion



    }
}

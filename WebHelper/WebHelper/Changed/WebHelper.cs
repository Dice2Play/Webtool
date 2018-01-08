using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WebHelperTool.Model.Enums;

// This class is responsible for notifying the user when a webpage has changed

namespace WebHelperProject
{
    
    public static partial class WebHelper
    {

        #region Public methods
        public static bool HasWebpageChanged(HtmlDocument currentHtmlDocument,
                                             HtmlDocument reloadedHtmlDocument,
                                             HttpWebResponse currentWebResponse,
                                             HttpWebResponse reloadedHttpWebResponse)
        {
            // Initialize tasks
            Task<bool>[] webPageChangedCheckers = new[]
            {
                CheckIfLastModifiedHeaderChanged(currentWebResponse, reloadedHttpWebResponse),
                CheckIfWebPageBodyChanged(currentHtmlDocument, reloadedHtmlDocument)
            };

            // Wait for all check tasks to be finished
            Task.WaitAll(webPageChangedCheckers);


            // Check if all of the changeCheckers returns true and return outcome
            return webPageChangedCheckers.All(x => x.Result == true);
        }

        #endregion

        #region Private methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="webURL"></param>
        /// <returns></returns>
        private async static Task<bool> CheckIfLastModifiedHeaderChanged(HttpWebResponse currentHttpWebResponse, HttpWebResponse reloadedHttpWebResponse)
        {
            var dateTimeCurrentHttpWebResponse = currentHttpWebResponse.LastModified;
            var dateTimeReloadedHttpWebResponse = reloadedHttpWebResponse.LastModified;

            // Compare date
            // If changed close 'old' headers 
            // if not changed close reloaded response header

            // Unchanged
            if (DateTime.Compare(dateTimeCurrentHttpWebResponse, dateTimeReloadedHttpWebResponse) == 0)
            {

                reloadedHttpWebResponse.Close();
                return false;
            }


            // Changed
            else
            {
                currentHttpWebResponse.Close();
                return true;
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webURL"></param>
        /// <returns></returns>
        private async static Task<bool> CheckIfWebPageBodyChanged(HtmlDocument currentHtmlDocument, HtmlDocument reloadedDocument)
        {
            // Retrieve body of HtmlDocuments
            var currentHtmlDocumentBody = FilterHtmlDocument(currentHtmlDocument, HtmlDocumentFilterContentType.Body, HtmlDocumentFilterOptions.All);
            var reloadedHtmlDocumentBody = FilterHtmlDocument(reloadedDocument, HtmlDocumentFilterContentType.Body, HtmlDocumentFilterOptions.All);

            // Retrieve html body of body's
            var currentHtmlDocumentBodyHtml = currentHtmlDocumentBody.InnerHtml;
            var reloadedHtmlDocumentBodyHtml = reloadedHtmlDocumentBody.InnerHtml;

            // Compare html body's
            // return true if body's arent equal
            return !currentHtmlDocumentBodyHtml.Equals(reloadedHtmlDocumentBodyHtml) ? true: false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webURL"></param>
        /// <returns></returns>
        private async static Task<bool> CheckIfCacheChanged(string webURL)
        {
            throw new NotImplementedException();
        }








        #endregion


    }



}

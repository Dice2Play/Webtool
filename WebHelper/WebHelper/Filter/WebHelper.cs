using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WebHelperTool.Model.Enums;

namespace WebHelperProject
{

    /// <summary>
    /// This class is responsible for all Filters needed in the webhelper project
    /// </summary>

    public static partial class WebHelper
    {

        #region Private methods
        private static HtmlNode FilterHtmlDocument(HtmlDocument htmlDocumentToFilter, HtmlDocumentFilterContentType whichContentToRetrieve, HtmlDocumentFilterOptions filterOptions)
        {
            switch (whichContentToRetrieve)
            {
                case HtmlDocumentFilterContentType.Body:
                    // Retrieve Body
                    var htmlBody = htmlDocumentToFilter.DocumentNode.SelectSingleNode("//body");

                    // Apply additional filter options
                    return FilterAttributes(htmlBody, filterOptions);

                case HtmlDocumentFilterContentType.Header:

                    // Retrieve Header
                    var htmlHeader = htmlDocumentToFilter.DocumentNode.SelectSingleNode("//header");

                    // Apply additional filter options
                    return FilterAttributes(htmlHeader, filterOptions);


                default:
                        return null;
            }
        }

        private static HtmlNode FilterAttributes(HtmlNode htmlNodeToFilter, HtmlDocumentFilterOptions filterOptions)
        {
            switch (filterOptions)
            {
                case HtmlDocumentFilterOptions.All:
                    return htmlNodeToFilter;

                case HtmlDocumentFilterOptions.WithoutScript:
                    return htmlNodeToFilter;




                default: return null;
            }
        }

        #endregion
    }
}

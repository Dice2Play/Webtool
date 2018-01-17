using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebHelperTool;
using HtmlAgilityPack;
using MoreLinq;
using WebHelperProject;
using WebHelperProject.Model;


namespace WebToolFeaturesPrototype
{
    class Program
    {
        private static List<SubscribedWebsite> subscribedWebsiteses = new List<SubscribedWebsite>();


        static void Main(string[] args)
        {
            // Websites which program should check
            string[] websitesToSubscribeToo = new string[] { "https://www.nu.nl/",
                                                             "http://localhost:8080/" };

            // Add to subscribe list and subscribe to event when page changes
            websitesToSubscribeToo.ForEach(x => SubscribeToWebsite(new Uri(x)));


            // Wait for all check tasks to be finished
            var allTasks = subscribedWebsiteses.Select(x => x._checkForChangesTask).ToArray();
            Task.WaitAll(allTasks);
            
        }


        // Will notify user when the content of a webpage changed for all subscribed websites
        static void SubscribeToWebsite(Uri url)
        {
            // Initialize new subscribedWebsite object and subscribe to PageChanged event
            var newSubscribedWebsite = new SubscribedWebsite(url);
            newSubscribedWebsite.PageChanged += NotifyUserThatPageChanged;


            // Add to subscribedWebsiteses list
            subscribedWebsiteses.Add(newSubscribedWebsite);



            Debug.WriteLine($"added the following website {url}..");

        }

        static void NotifyUserThatPageChanged(object obj,EventArgs e)
        {
            var changedWebsite = (SubscribedWebsite) obj;
            var readFriendlyHostName = changedWebsite._uri.Host.Split('.')[1];


            // Print to console
            Console.WriteLine($" [{DateTime.Now.ToString("HH:mm:ss")}] Page: {changedWebsite._uri} changed!");

            // Save website
            FileHelper.CRUD.CREATE_Help.SaveHTMLFile(changedWebsite.CurrentWebSite.HtmlDocument, Path.Combine(Environment.CurrentDirectory, "HtmlStorage"), $"{readFriendlyHostName}.html");

            // Navigate to Uri
            WebHelper.OpenWebPageInBrowser(changedWebsite._uri, changedWebsite.CurrentWebSite.WebResponse.Cookies.Cast<HttpCookie>().ToArray());

        }



    }

    public class SubscribedWebsite
    {
        #region private fields
        // Current website (Will get updated when the website has changed(See HasPageChanged method))
        private WebSite _currentWebsite;

        // Url of subscribed website
        public Uri _uri { get; set; }

        // Task which checks for changes in webpage
        public Task _checkForChangesTask { get; set; }

        // Enables/Disables checkForChangesTask
        private bool _runCheckForChangesTask = true;

        public event EventHandler PageChanged = delegate {};

        
        

        #endregion

        public SubscribedWebsite(Uri uri)
        {
            this._uri = uri;
            _currentWebsite = WebHelper.ReturnWebSite(uri);
            _checkForChangesTask = new Task(() => HasPageChanged());

            // Start task
            _checkForChangesTask.Start();
        }

        #region public properties
        public WebSite CurrentWebSite
        {
            get { return _currentWebsite; }
            set { _currentWebsite = value; }
        }
    
        public bool RunCheckForChangesTask
        {
            get { return _runCheckForChangesTask; }
            set { _runCheckForChangesTask = value; }
        }

        public HtmlDocument HtmlDocument
        {
            get { return _currentWebsite.HtmlDocument; }
        }

        public WebResponse WebResponse
        {
            get { return _currentWebsite.WebResponse; }
        }

        #endregion


        #region private helpers

        private void HasPageChanged()
        {
            while (_runCheckForChangesTask)
            {
                _checkForChangesTask.Wait(2000);

                // Reloaded website
                var reloadedWebsite = WebHelper.ReturnWebSite(_uri);

                // Set HtmlDocuments and WebResponse
                var reloadedHttpWebResponse = reloadedWebsite.WebResponse;
                var reloadedHtmlDocument = reloadedWebsite.HtmlDocument;

                var currentHtmlWebDocument = _currentWebsite.HtmlDocument;
                var currentHttpWebResponse = _currentWebsite.WebResponse;

                // Check if page changed
                bool hasWebSiteChanged = WebHelper.HasWebpageChanged(   currentHtmlWebDocument,
                                                                        reloadedHtmlDocument,
                                                                        currentHttpWebResponse,
                                                                        reloadedHttpWebResponse);




                if (hasWebSiteChanged)
                {
                    // Set updated website
                    _currentWebsite = reloadedWebsite;

                    // Notify Subscribers!
                    PageChanged.Invoke(this,EventArgs.Empty);
                }

                else
                {
                    Debug.WriteLine($"URL {_uri} hasn't changed.");
                }


            }


        }




        #endregion
    }
}

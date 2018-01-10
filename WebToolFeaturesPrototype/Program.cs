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
using WebHelperTool;
using HtmlAgilityPack;
using WebHelperProject;


namespace WebToolFeaturesPrototype
{
    class Program
    {
        private static List<SubscribedWebsite> subscribedWebsiteses;


        static void Main(string[] args)
        {

            WebHelper.OpenWebPageInBrowser(new Uri("http://localhost:8080/"));



            subscribedWebsiteses = new List<SubscribedWebsite>();

            string[] websitesToSubscribeToo = new string[] { "https://www.nu.nl/",
                                                             "http://localhost:8080/" };

            foreach (string url in websitesToSubscribeToo)
            {
                SubscribeToWebsite(new Uri(url));
            }

            var allTasks = subscribedWebsiteses.Select(x => x._checkForChangesTask).ToArray();


            // Wait for all check tasks to be finished
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
            var getReadFriendlyHostName = changedWebsite._url.Host.Split('.')[1];


            // Print to console
            Console.WriteLine($" [{DateTime.Now.ToString("HH:mm:ss")}] Page: {changedWebsite._url} changed!");

            // Save file
            FileHelper.CRUD.CREATE_Help.SaveHTMLFile(changedWebsite.CurrentHtmlWebDocument, Path.Combine(Environment.CurrentDirectory, "HtmlStorage"), $"{getReadFriendlyHostName}.html");

            // Navigate to Uri
            //var cookies = 

        }



    }

    public class SubscribedWebsite
    {
        #region private fields
        // Website where class is subscribed too. (Will get updated when the website has changed(See HasPageChanged method))
        private HtmlDocument _currentHtmlWebDocument;

        // Response header. (Will get updated when the website has changed(See HasPageChanged method))
        private HttpWebResponse _currentHttpWebResponse;

        // Url of subscribed website
        public Uri _url { get; set; }

        // Task which checks for changes in webpage
        public Task _checkForChangesTask { get; set; }

        // Enables/Disables checkForChangesTask
        private bool _runCheckForChangesTask = true;

        public event EventHandler PageChanged = delegate {};

        
        

        #endregion

        public SubscribedWebsite(Uri url)
        {
            

            this._url = url;
            _currentHtmlWebDocument = WebHelper.ReturnContentOfPageHtmlDocument(url);
            _currentHttpWebResponse = WebHelper.ReturnWebHttpWebResponse(url);
            _checkForChangesTask = new Task(() => HasPageChanged());

            // Start task
            _checkForChangesTask.Start();
        }

        #region public properties
        public HtmlDocument CurrentHtmlWebDocument
        {
            get { return _currentHtmlWebDocument; }
            set { _currentHtmlWebDocument = value; }
        }
    
        public bool runCheckForChangesTask
        {
            get { return _runCheckForChangesTask; }
            set { _runCheckForChangesTask = value; }
        }

        #endregion


        #region private helpers

        private void HasPageChanged()
        {
            while (_runCheckForChangesTask)
            {
                _checkForChangesTask.Wait(2000);

                // Reload HtmlDocument and HttpWebResponse
                var reloadedHtmlDocument = WebHelper.ReturnContentOfPageHtmlDocument(_url);
                var reloadedHttpWebResponse = WebHelper.ReturnWebHttpWebResponse(_url);

                // Check if page changed
                bool hasWebSiteChanged = WebHelper.HasWebpageChanged(   _currentHtmlWebDocument,
                                                                        reloadedHtmlDocument,
                                                                        _currentHttpWebResponse,
                                                                        reloadedHttpWebResponse);




                if (hasWebSiteChanged)
                {
                    // Set updated webpage and webresponse
                    _currentHtmlWebDocument = reloadedHtmlDocument;
                    _currentHttpWebResponse = reloadedHttpWebResponse;

                    // Notify Subscribers!
                    PageChanged.Invoke(this,EventArgs.Empty);
                }

                else
                {
                    Debug.WriteLine($"URL {_url} hasn't changed.");
                }


            }


        }

        // Get reloaded version of htmlpage
        private HtmlDocument ReturnReloadedPage()
        {
            return WebHelper.ReturnContentOfPageHtmlDocument(_url);
        }



        #endregion
    }
}

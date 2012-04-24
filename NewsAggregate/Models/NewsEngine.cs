using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

using System.Net;
using System.IO;

using Amazon.SimpleDB;
using Amazon.S3;
using Amazon.S3.Model;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Drawing;
using NewsAggregate.Properties;


namespace RssNewsEngine.Models
{
    public class NewsEngine
    {
        private static AmazonSimpleDBClient _sdbClient;
        public static AmazonSimpleDBClient sdbClient
        {
            get
            {
                if (_sdbClient == null)
                    _sdbClient = new AmazonSimpleDBClient();
                return _sdbClient;
            }
        }
        private static AmazonS3Client _s3Client;
        public static AmazonS3Client s3Client
        {
            get
            {
                if (_s3Client == null)
                    _s3Client = new AmazonS3Client();
                return _s3Client;
            }
        }
        public static string BUCKETNAME = "rssnewsengine";
        public static String URL = "https://s3-sa-east-1.amazonaws.com/rssnewsengine/";
        public static  XmlReader reader;
        public static void LoadNewsintoTables(NewsComponents components, string domainName)
        {
            foreach (var img in components.Images)
            {
                components.NewsPhotoUrl = components.NewsPhotoUrl + "|" + img.Url;
            }

            NewsMethods.SaveNewItems(domainName, Settings.Default.BucketName + "/" + components.Country.ToLower(), components, sdbClient, s3Client);
            
        }
        public static void SaveMultimedia(Multimedia multimedia,string domainName)
        {
            NewsMethods.SaveVideoItems(domainName, multimedia,sdbClient,s3Client);
        }
        public static void SaveTags(Tags tags, string domainName)
        {
            NewsMethods.SaveTagNames(domainName, tags, sdbClient);
        }
        public static List<Tags> GetTags(string domainName)
        {
           return  NewsMethods.GetTags(domainName, sdbClient);
        }
        private static Stream CreateThumbNail(MemoryStream image)
        {
            Bitmap orig = new Bitmap(image);
            int width;
            int height;
            if (orig.Width > orig.Height)
            {
                width = 152;
                height = 152 * orig.Height / orig.Width;
            }
            else
            {
                height = 152;
                width = 152 * orig.Width / orig.Height;

            }
            Bitmap thumb = new Bitmap(width, height);
            using (Graphics graphic = Graphics.FromImage(thumb))
            {
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                graphic.DrawImage(orig, 0, 0, width, height);
                MemoryStream ms = new MemoryStream();
                thumb.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                ms.Seek(0, SeekOrigin.Begin);
                return ms;
            }

        }
        //public static void LoadNewsintoTables(List<Urls> newsurls, string domainName)
        //{


        //    List<NewsComponents> listNews = new List<NewsComponents>();
        //    Dictionary<string, List<NewsComponents>> newsdictionary = new Dictionary<string, List<NewsComponents>>();
        //    //Zambian watchdog

        //    foreach (var item in newsurls)
        //    {


        //        try
        //        {
        //            LoadXml(listNews, newsdictionary, item);
        //        }
        //        catch (XmlException e)
        //        {
        //            var xmlDoc = XDocument.Load(reader);
        //            var newsitems = (from channel in xmlDoc.Root.Elements("rdf")
        //                             from itemx in channel.Elements("item")
        //                             select new NewsComponents
        //                             {
        //                                 Source = item.key,
        //                                 Section = "",
        //                                 NewsItem = LoadNewsContent(itemx.Element("Link").Value),
        //                                 NewsHeadline = itemx.Element("title").Value,
        //                                 NewsAdded = Convert.ToDateTime(itemx.Element("dc:date").Value),
        //                                 NewsPhotoUrl = "",
        //                                 Summary = itemx.Element("description").Value,

        //                                 Category = item.Category

        //                             }).Take(10);



        //        }
        //        catch (WebException ex)
        //        {
        //            Thread.Sleep(1000);
        //            LoadXml(listNews, newsdictionary, item);
        //        }
        //    }
        //    FindOccurence(newsdictionary, domainName);
        //}

        //private static void LoadXml(List<NewsComponents> listNews, Dictionary<string, List<NewsComponents>> newsdictionary, Urls item)
        //{
        //    reader = XmlReader.Create(item.url);
        //    UsingSyndicationFeed(listNews, newsdictionary, item, reader);
        //}

        private static string LoadNewsContent(string p)
        {
            StreamReader strReader = new StreamReader(GetStreamInternet(p));
            return strReader.ReadToEnd();

        }

        //private static void UsingSyndicationFeed(List<NewsComponents> listNews, Dictionary<string, List<NewsComponents>> newsdictionary, Urls item, XmlReader reader)
        //{
        //    SyndicationFeed feed = SyndicationFeed.Load(reader);


        //    var fed = (from f in feed.Items
        //              orderby f.LastUpdatedTime descending

        //              select f).Take(10);

        //    foreach (var newsItem in fed)
        //    {
        //        NewsComponents newsComponents = new NewsComponents();
        //        newsComponents.NewsID = Guid.NewGuid();
        //        newsComponents.NewsHeadline = newsItem.Title.Text;
        //        newsComponents.Summary = string.IsNullOrWhiteSpace(newsItem.Summary.Text) ? "" : newsItem.Summary.Text;
        //        newsComponents.Source = item.key;
        //        newsComponents.Section = "";
        //        //newsComponents. = "";
        //        newsComponents.Category = item.Category;
        //        newsComponents.NewsAdded = newsItem.PublishDate.DateTime;
        //        newsComponents.TimeStamp = DateTime.Now;

        //        try
        //        {
        //            foreach (var link in newsItem.Links)
        //            {


        //                if (link.MediaType == null)
        //                {

        //                    var stream = GetStreamInternet(link);
        //                    StreamReader strReader = new StreamReader(stream);
        //                    string newsContent = strReader.ReadToEnd();
        //                    newsComponents.NewsItem = StripHtmlStuff(newsContent, newsComponents.NewsHeadline);
        //                }
        //                else if (link.MediaType.Contains("image"))
        //                {
        //                    var stream = GetStreamInternet(link);
        //                    Images image = new Images();
        //                    image.photostreams = stream;
        //                    image.fileName = Guid.NewGuid() + ".jpg";
        //                    image.Url = URL + image.fileName;
        //                    newsComponents.Images.Add(image);
        //                    newsComponents.NewsPhotoUrl = image.Url + ";" + newsComponents.NewsPhotoUrl;
        //                }
        //            }
        //            SaveImageToSC(newsComponents.Images);


        //        }
        //        catch (Exception e)
        //        {
        //        }
        //        finally
        //        {
        //            listNews.Add(newsComponents);

        //            //}


        //        }

        //    }
        //    newsdictionary.Add(item.key, listNews);
        //}

        
        private static void SaveImageToSC(List<Images> images)
        {
            try
            {
                foreach (var img in images)
                {
                    PutObjectRequest request = new PutObjectRequest();
                    request.WithBucketName(BUCKETNAME)
                        .WithCannedACL(S3CannedACL.PublicRead)
                        .WithKey(img.fileName)
                        .WithInputStream(img.photostreams);

                    using (S3Response response = s3Client.PutObject(request))
                    {
                        WebHeaderCollection headers = response.Headers;
                        foreach (string key in headers.Keys)
                        {
                            //log headers ("Response Header: {0}, Value: {1}", key, headers.Get(key));
                        }
                    }
                }

            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null && (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") || amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    //log exception - ("Please check the provided AWS Credentials.");
                }
                else
                {
                    //log exception - ("An Error, number {0}, occurred when creating a bucket with the message '{1}", amazonS3Exception.ErrorCode, amazonS3Exception.Message);    
                }
            }
        }
        //private static Stream GetStreamInternet(SyndicationLink link)
        //{
        //    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(link.Uri);
        //    HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
        //    Stream stream = response.GetResponseStream();

        //    return stream;
        //}
        private static Stream GetStreamInternet(String link)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(link);
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            Stream stream = response.GetResponseStream();

            return stream;
        }
        private static string StripHtmlStuff(string newsContent, string headline)
        {
            //Html Parser
            //Regex objRegExp = new Regex("<(.|\n)+?>");
            //String strOutput = objRegExp.Replace(newsContent, String.Empty);

            //int position = newsContent.IndexOf(headline);
            //string realstuff = newsContent.Substring(position);
            //HtmlString str = new HtmlString("");



            return newsContent;
        }
        public static void FindOccurence(Dictionary<string, List<NewsComponents>> newsdictionary, string domainName)
        {
            foreach (var dic in newsdictionary)
            {


               // NewsMethods.SaveNewsStory(domainName, Convert.ToString(Guid.NewGuid()), dic, sdbClient, s3Client);

               
            }

            List<NewsComponents> finalList = new List<NewsComponents>();
            //List<string> keys = new List<string>();
            //int j = 0;
            //foreach (var item in newsdictionary)
            //{
            //    keys.Add(item.Key);
            //    if (j == 0)
            //    {
            //        finalList = item.Value;
            //    }
            //    j++;
            //}



            //for (int i = 0; i < keys.Count; i++)
            //{
            //    foreach (var list in finalList)
            //    {

            //        var listheadline = newsdictionary[keys[i + 1]];
            //        foreach (var headline in listheadline)
            //        {
            //            if (list.NewsHeadline.Contains(headline.NewsHeadline))
            //            {
            //                finalList.Add(headline);

            //            }
            //        }

            //    }
            //}
            //foreach (var list in finalList)
            //{
            //    NewsMethods.SaveNewsStory(domainName, Convert.ToString(Guid.NewGuid()), "", list, sdbClient, s3Client);
            //}

        }
    }
}
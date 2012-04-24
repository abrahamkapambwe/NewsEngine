using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RssNewsEngine.Models
{
    public class RssUrls
    {
        //South Africa
        public static string NEWS24 = "http://feeds.news24.com/articles/news24/SouthAfrica/rss";
        public static string NEWS24SPORT = "http://feeds.24.com/articles/sport/featured/topstories/rss";

        public static string MAILGUADIAN = "http://mg.co.za/rss/national";
        public static string MAILGUADIANBUS = "http://mg.co.za/rss/business";
        public static string MAILGUADIANSPORT = "http://mg.co.za/rss/sport";

        public static string IOLSPORT = "http://iol.co.za/cmlink/sport-category-rss-1.704";

        public static string TIMES = "http://avusa.feedsportal.com/c/33051/f/534658/index.rss";
        public static string TIMESLIFESTYLE = "http://avusa.feedsportal.com/c/33051/f/534661/index.rss";

        public static string SOWETAN = "http://www.sowetanlive.co.za/?service=rss";
        public static string SOWETANERTAIN = "http://avusa.feedsportal.com/c/33051/f/534662/index.rss";

        public static string BBCTECH = "http://feeds.bbci.co.uk/news/technology/rss.xml";

        //Kenya
        public static string STANDARDMEDIA = "http://www.standardmedia.co.ke/rss/headlines.php";
        public static string STANDARDMEDIASPORT = "http://www.standardmedia.co.ke/rss/sports.php";
        public static string STANDARDMEDIAPOLI = "http://www.standardmedia.co.ke/rss/politics.php";
        public static string NATIONKENYA = "http://www.nation.co.ke/-/1148/1148/-/view/asFeed/-/vtvnjq/-/index.xml";
        public static string THESTAR = "http://www.the-star.co.ke/?format=feed&type=rss";
        public static string THEBUSINESSDAILY = "http://www.businessdailyafrica.com/-/539444/539444/-/view/asFeed/-/xs2vhb/-/index.xml";
        public static string KBC = "http://www.kbc.co.ke/rss.asp";
        public static string ALLAFRICANEWSKENYA = "http://allafrica.com/tools/headlines/rdf/kenya/headlines.rdf";
        public static string BBCTECHKE = "http://feeds.bbci.co.uk/news/technology/rss.xml";
        //Tanzania
        public static string THECITIZEN = "http://thecitizen.co.tz/news.feed?type=rss";
        public static string IN2EASTAFRICA = "http://feeds.feedburner.com/In2eastafrica";
        public static string ALLAFRICANEWSTANZIA = "http://allafrica.com/tools/headlines/rdf/tanzania/headlines.rdf";
        public static string BBCTECHTZ = "http://feeds.bbci.co.uk/news/technology/rss.xml";
        //Uganda
        //public static string MONITOR = "http://www.monitor.co.ug/-/691150/691150/-/view/asFeed/-/11emxavz/-/index.xml";
        public static string NEWVISION = "http://www.newvision.co.ug/feed.aspx?cat_id=1";
        public static string NEWVISIONSPORT = "http://www.newvision.co.ug/feed.aspx?cat_id=5";
        public static string NEWVISIONENTERN = "http://www.newvision.co.ug/feed.aspx?cat_id=397";
        public static string ALLAFRICANEWSUGANDA = "http://allafrica.com/tools/headlines/rdf/uganda/headlines.rdf";
        public static string BBCTECHUG = "http://feeds.bbci.co.uk/news/technology/rss.xml";
        //Zambia
        public static string ZAMBIANWATCHDOG = "http://www.zambianwatchdog.com/index.php/feed/";
        public static string TUMFWEKO = "http://tumfweko.com/feed/";
        public static string MUVITV = "http://www.muvitv.com/?feed=rss2";
        public static string ALLAFRICANEWSZAMBIA = "http://allafrica.com/tools/headlines/rdf/zambia/headlines.rdf";
        public static string BBCTECHZM = "http://feeds.bbci.co.uk/news/technology/rss.xml";
        //zimbabwe
        public static string THEZIMBABWEUK = "http://www.thezimbabwean.co.uk/rss/news";
        public static string NEWZIMBABWE = "http://www.newzimbabwe.com/rss/rss.xml";
        public static string ALLAFRICANEWSZIM = "http://allafrica.com/tools/headlines/rdf/zimbabwe/headlines.rdf";
        public static string BBCTECHZW = "http://feeds.bbci.co.uk/news/technology/rss.xml";
    }
}
public class Urls
{
   
    public string url
    {
        get;
        set;
    }
    public string key
    {
        get;
        set;
    }
    public string Category
    {
        get;
        set;
    }
}
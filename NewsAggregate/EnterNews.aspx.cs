using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewsAggregate.Properties;
using RssNewsEngine.Models;

namespace NewsAggregate
{
    public partial class EnterNews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
               var lists= NewsEngine.GetTags(Settings.Default.TagTable);

                foreach (var tag in lists)
                {
                    ListItem list=new ListItem();
                    list.Value = Convert.ToString(tag.TagId);
                    list.Text = tag.TagName;
                    LstTags.Items.Add(list);

                }
            }
        }
        protected void Clear_Click(object sender, EventArgs e)
        {
            lblResults2.Visible =false;
            lblResult.Visible = false;
            txtNewsHeadline.Text = "";
            txtNewsItem.Content = "";
            txtSummaryContent.Content = "";
            txtSource.Text = "";
            txtImageLabel.Text = "";
        }

        protected void Submit_Click(object sender,EventArgs e)
        {
            NewsComponents newscomponents=new NewsComponents();
            newscomponents.Country = ddlCountry.SelectedValue;
            newscomponents.NewsHeadline = txtNewsHeadline.Text;
            newscomponents.NewsItem = txtNewsItem.Content;
            newscomponents.SummaryContent = txtSummaryContent.Content;
            newscomponents.Source = txtSource.Text;
            newscomponents.Category = ddlCategory.SelectedValue;
            newscomponents.Imagelabel = txtImageLabel.Text;
          
            if (rbnNo.Checked)
                newscomponents.Publish = "false";
            if (rbnYes.Checked)
                newscomponents.Publish = "true";
            //if (FileUpload1.HasFile)
            //{
            //    uploadFiles = new UploadFiles();
            //    uploadFiles.ContentLength = Convert.ToString(FileUpload1.PostedFile.ContentLength);
            //    uploadFiles.ContentType = FileUpload1.PostedFile.ContentType;
            //    uploadFiles.FileName = FileUpload1.PostedFile.FileName;
            //    uploadFiles.Stream = FileUpload1.PostedFile.InputStream;
            //    listOffiles.Add(uploadFiles);
            //}
            //if (FileUpload2.HasFile)
            //{
            //    uploadFiles = new UploadFiles();
            //    uploadFiles.ContentLength = Convert.ToString(FileUpload2.PostedFile.ContentLength);
            //    uploadFiles.ContentType = FileUpload2.PostedFile.ContentType;
            //    uploadFiles.FileName = FileUpload2.PostedFile.FileName;
            //    uploadFiles.Stream = FileUpload2.PostedFile.InputStream;
            //    listOffiles.Add(uploadFiles);
            //}

             foreach (string file in Request.Files)
            {
                HttpPostedFile hpf = Request.Files[file] as HttpPostedFile;
                if (hpf.ContentLength == 0)
                    continue;
                Images images = new Images();
                images.fileName = Convert.ToString(Guid.NewGuid()) + hpf.FileName;
                images.photostreams = hpf.InputStream;
                switch (newscomponents.Country)
                {
                    case "Kenya":

                        images.Url = Settings.Default.BucketNameURL + "/kenya/" + images.fileName;
                        break;
                    case "Malawi":
                        images.Url = Settings.Default.BucketNameURL + "/malawi/" + images.fileName;
                        break;
                    case "Tanzania":
                        images.Url = Settings.Default.BucketNameURL + "/tanzania/" + images.fileName;
                        break;
                    case "Uganda":
                        images.Url = Settings.Default.BucketNameURL + "/uganda/" + images.fileName;
                        break;
                    case "Zambia":
                        images.Url = Settings.Default.BucketNameURL + "/zambia/" + images.fileName;
                        break;
                    case "Zimbabwe":
                        images.Url = Settings.Default.BucketNameURL + "/zimbabwe/" + images.fileName;
                        break;
                }

                // images.Url=Settings.Default.
                newscomponents.Images.Add(images);
            }
            
            newscomponents.NewsAdded = DateTime.Now;
            newscomponents.NewsID = Guid.NewGuid();
            newscomponents.NewsItem = Server.HtmlEncode(newscomponents.NewsItem);

            switch (newscomponents.Country)
            {
                case "Kenya":
                    newscomponents.BucketName = Settings.Default.BucketName + "/" + Settings.Default.Kenya;
                    newscomponents.Summary = Settings.Default.BucketNameURL + "/" + Settings.Default.Kenya + "/" +
                                             newscomponents.NewsID;
                    NewsEngine.LoadNewsintoTables(newscomponents, Settings.Default.Kenya);
                    break;
                case "Malawi":
                    newscomponents.BucketName = Settings.Default.BucketName + "/" + Settings.Default.Malawi;
                    newscomponents.Summary = Settings.Default.BucketNameURL + "/" + Settings.Default.Malawi + "/" +
                                             newscomponents.NewsID;
                    NewsEngine.LoadNewsintoTables(newscomponents, Settings.Default.Malawi);

                    break;
                case "Tanzania":
                    newscomponents.BucketName = Settings.Default.BucketName + "/" + Settings.Default.Uganda;
                    newscomponents.Summary = Settings.Default.BucketNameURL + "/" + Settings.Default.Uganda + "/" +
                                             newscomponents.NewsID;
                    NewsEngine.LoadNewsintoTables(newscomponents, Settings.Default.Tanzania);
                    break;
                case "Uganda":
                    newscomponents.BucketName = Settings.Default.BucketName + "/" + Settings.Default.Uganda;
                    newscomponents.Summary = Settings.Default.BucketNameURL + "/" + Settings.Default.Uganda + " /" +
                                             newscomponents.NewsID;
                    NewsEngine.LoadNewsintoTables(newscomponents, Settings.Default.Uganda);
                    break;
                case "SouthAfrica":
                    newscomponents.BucketName = Settings.Default.BucketName + "/" + Settings.Default.SouthAfrica;
                    newscomponents.Summary = Settings.Default.BucketNameURL + "/" + Settings.Default.SouthAfrica + " /" +
                                             newscomponents.NewsID;
                    NewsEngine.LoadNewsintoTables(newscomponents, Settings.Default.SouthAfrica);
                    break;
                case "Bostwana":
                    newscomponents.BucketName = Settings.Default.BucketName + "/" + Settings.Default.Bostwana;
                    newscomponents.Summary = Settings.Default.BucketNameURL + "/" + Settings.Default.Bostwana + " /" +
                                             newscomponents.NewsID;
                    NewsEngine.LoadNewsintoTables(newscomponents, Settings.Default.SouthAfrica);
                    break;
                case "Zambia":
                    newscomponents.BucketName = Settings.Default.BucketName + "/" + Settings.Default.Zambia;
                    newscomponents.Summary = Settings.Default.BucketNameURL + "/" + Settings.Default.Zambia + "/" +
                                             newscomponents.NewsID;
                    NewsEngine.LoadNewsintoTables(newscomponents, Settings.Default.Zambia);
                    break;
                case "Zimbabwe":
                    newscomponents.BucketName = Settings.Default.BucketName + "/" + Settings.Default.Zimbabwe;
                    newscomponents.Summary = Settings.Default.BucketNameURL + "/" + Settings.Default.Zimbabwe + "/" +
                                             newscomponents.NewsID;
                    NewsEngine.LoadNewsintoTables(newscomponents, Settings.Default.Zimbabwe);
                    break;
            }
            lblResults2.Visible = true;
            lblResult.Visible = true;
        }
    }
}
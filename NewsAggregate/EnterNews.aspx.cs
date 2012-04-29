using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewsAggregate.Properties;
using RssNewsEngine.Models;
using System.IO;
using System.Drawing;

namespace NewsAggregate
{
    public partial class EnterNews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        protected void Selected_Country(object sender, EventArgs e)
        {

            var lists = NewsEngine.GetTags(Settings.Default.TagTable).Where(p => p.Country == ddlCountry.SelectedValue).ToList();

            foreach (var tag in lists)
            {
                ListItem list = new ListItem();
                list.Value = Convert.ToString(tag.TagId);
                list.Text = tag.TagName;
                LstTags.Items.Add(list);

            }

        }
        protected void Clear_Click(object sender, EventArgs e)
        {
            lblResults2.Visible = false;
            lblResult.Visible = false;
            txtNewsHeadline.Text = "";
            txtNewsItem.Content = "";
            txtSummaryContent.Content = "";
            txtSource.Text = "";
            txtImageLabel.Text = "";
        }
        private Stream CreateThumbNail(MemoryStream image)
        {
            Bitmap orig = new Bitmap(image);
            int width;
            int height;
            if (orig.Width > orig.Height)
            {
                width = 162;
                height = 162 * orig.Height / orig.Width;
            }
            else
            {
                height = 162;
                width = 162 * orig.Width / orig.Height;

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
        protected void Submit_Click(object sender, EventArgs e)
        {
            NewsComponents newscomponents = new NewsComponents();
            newscomponents.Country = ddlCountry.SelectedValue;

            newscomponents.NewsHeadline = txtNewsHeadline.Text;
            newscomponents.NewsItem = txtNewsItem.Content;
            newscomponents.SummaryContent = txtSummaryContent.Content;
            newscomponents.Source = txtSource.Text;
            newscomponents.Category = ddlCategory.SelectedValue;
            newscomponents.Imagelabel = txtImageLabel.Text;
            newscomponents.TagName = "|";
            for (int i = 0; i < LstTags.Items.Count; i++)
            {
                if (LstTags.Items[i].Selected)
                {
                    newscomponents.TagName = newscomponents.TagName + LstTags.Items[i].Text + "|";
                }
            }

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
                Stream imagethumb;
                using (MemoryStream image = new MemoryStream())
                {
                    byte[] buffer = new byte[hpf.InputStream.Length];
                    int count;
                    int totalBytes = 0;
                    int len = Int32.Parse(Convert.ToString(hpf.InputStream.Length));
                    while ((count = hpf.InputStream.Read(buffer, 0, len)) > 0)
                    {
                        image.Write(buffer, 0, count);
                        totalBytes += count;
                    }
                    image.Position = 0;
                    byte[] transparentPng = new byte[totalBytes];
                    image.Read(transparentPng, 0, totalBytes);
                    imagethumb = CreateThumbNail(image);
                    newscomponents.ThumbNail = imagethumb;

                }
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
                    case "South Africa":
                        images.Url = Settings.Default.BucketNameURL + "/southafrica/" + images.fileName;
                        break;
                }

                // images.Url=Settings.Default.
                newscomponents.Images.Add(images);
            }

            newscomponents.NewsAdded = DateTime.Now;
            newscomponents.NewsID = Guid.NewGuid();
            newscomponents.ThumbNailKey = Guid.NewGuid() + ".jpg";
            newscomponents.NewsItem = Server.HtmlEncode(newscomponents.NewsItem);

            switch (newscomponents.Country)
            {
                case "Kenya":
                    if (newscomponents.ThumbNail != null)
                    {
                        newscomponents.ThumbNailBucketName = Settings.Default.BucketName + "/" + Settings.Default.Kenya;
                        newscomponents.ThumbNailUrl = Settings.Default.BucketNameURL + "/" + Settings.Default.Kenya + "/" +
                                                 newscomponents.ThumbNailKey;
                    }

                    newscomponents.BucketName = Settings.Default.BucketName + "/" + Settings.Default.Kenya;
                    newscomponents.Summary = Settings.Default.BucketNameURL + "/" + Settings.Default.Kenya + "/" +
                                             newscomponents.NewsID;
                    NewsEngine.LoadNewsintoTables(newscomponents, Settings.Default.Kenya);
                    break;
                case "Malawi":
                    if (newscomponents.ThumbNail != null)
                    {
                        newscomponents.ThumbNailBucketName = Settings.Default.BucketName + "/" + Settings.Default.Malawi;
                        newscomponents.ThumbNailUrl = Settings.Default.BucketNameURL + "/" + Settings.Default.Malawi + "/" +
                                                 newscomponents.ThumbNailKey;
                    }
                    newscomponents.BucketName = Settings.Default.BucketName + "/" + Settings.Default.Malawi;
                    newscomponents.Summary = Settings.Default.BucketNameURL + "/" + Settings.Default.Malawi + "/" +
                                             newscomponents.NewsID;
                    NewsEngine.LoadNewsintoTables(newscomponents, Settings.Default.Malawi);

                    break;
                case "Tanzania":
                    if (newscomponents.ThumbNail != null)
                    {
                        newscomponents.ThumbNailBucketName = Settings.Default.BucketName + "/" + Settings.Default.Tanzania;
                        newscomponents.ThumbNailUrl = Settings.Default.BucketNameURL + "/" + Settings.Default.Tanzania + "/" +
                                                 newscomponents.ThumbNailKey;
                    }
                    newscomponents.BucketName = Settings.Default.BucketName + "/" + Settings.Default.Tanzania;
                    newscomponents.Summary = Settings.Default.BucketNameURL + "/" + Settings.Default.Tanzania + "/" +
                                             newscomponents.NewsID;
                    NewsEngine.LoadNewsintoTables(newscomponents, Settings.Default.Tanzania);
                    break;
                case "Uganda":
                    if (newscomponents.ThumbNail != null)
                    {
                        newscomponents.ThumbNailBucketName = Settings.Default.BucketName + "/" + Settings.Default.Uganda;
                        newscomponents.ThumbNailUrl = Settings.Default.BucketNameURL + "/" + Settings.Default.Uganda + "/" +
                                                newscomponents.ThumbNailKey;
                    }
                    newscomponents.BucketName = Settings.Default.BucketName + "/" + Settings.Default.Uganda;
                    newscomponents.Summary = Settings.Default.BucketNameURL + "/" + Settings.Default.Uganda + " /" +
                                             newscomponents.NewsID;
                    NewsEngine.LoadNewsintoTables(newscomponents, Settings.Default.Uganda);
                    break;
                case "South Africa":
                    if (newscomponents.ThumbNail != null)
                    {
                        newscomponents.ThumbNailBucketName = Settings.Default.BucketName + "/" + Settings.Default.SouthAfrica;
                        newscomponents.ThumbNailUrl = Settings.Default.BucketNameURL + "/" + Settings.Default.SouthAfrica + "/" +
                                                newscomponents.ThumbNailKey;
                    }
                    newscomponents.BucketName = Settings.Default.BucketName + "/" + Settings.Default.SouthAfrica;
                    newscomponents.Summary = Settings.Default.BucketNameURL + "/" + Settings.Default.SouthAfrica + " /" +
                                             newscomponents.NewsID;
                    NewsEngine.LoadNewsintoTables(newscomponents, Settings.Default.SouthAfrica);
                    break;
                case "Bostwana":
                    if (newscomponents.ThumbNail != null)
                    {
                        newscomponents.ThumbNailBucketName = Settings.Default.BucketName + "/" + Settings.Default.Bostwana;
                        newscomponents.ThumbNailUrl = Settings.Default.BucketNameURL + "/" + Settings.Default.Bostwana + "/" +
                                                newscomponents.ThumbNailKey;
                    }
                    newscomponents.BucketName = Settings.Default.BucketName + "/" + Settings.Default.Bostwana;
                    newscomponents.Summary = Settings.Default.BucketNameURL + "/" + Settings.Default.Bostwana + " /" +
                                             newscomponents.NewsID;
                    NewsEngine.LoadNewsintoTables(newscomponents, Settings.Default.SouthAfrica);
                    break;
                case "Zambia":
                    if (newscomponents.ThumbNail != null)
                    {
                        newscomponents.ThumbNailBucketName = Settings.Default.BucketName + "/" + Settings.Default.Zambia;
                        newscomponents.ThumbNailUrl = Settings.Default.BucketNameURL + "/" + Settings.Default.Zambia + "/" +
                                                newscomponents.ThumbNailKey;
                    }
                    newscomponents.BucketName = Settings.Default.BucketName + "/" + Settings.Default.Zambia;
                    newscomponents.Summary = Settings.Default.BucketNameURL + "/" + Settings.Default.Zambia + "/" +
                                             newscomponents.NewsID;
                    NewsEngine.LoadNewsintoTables(newscomponents, Settings.Default.Zambia);
                    break;
                case "Zimbabwe":
                    if (newscomponents.ThumbNail != null)
                    {
                        newscomponents.ThumbNailBucketName = Settings.Default.BucketName + "/" + Settings.Default.Zimbabwe;
                        newscomponents.ThumbNailUrl = Settings.Default.BucketNameURL + "/" + Settings.Default.Zimbabwe + "/" +
                                                newscomponents.ThumbNailKey;
                    }
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
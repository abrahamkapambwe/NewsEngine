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
    public partial class MultimediaPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Clear_Click(object sender,EventArgs e)
        {
            txtSummaryContent.Content = "";
            txtNewsHeadline.Text = "";
            txtSource.Text = "";
            lblResult.Visible = false;
            lblResults2.Visible = false;

        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            Multimedia multimedia = new Multimedia();
            multimedia.Category = ddlCategory.SelectedValue;
            multimedia.Content = txtSummaryContent.Content;
            multimedia.Country = ddlCountry.SelectedValue;
            multimedia.Title = txtNewsHeadline.Text;
            multimedia.YoutubeUrl = txtSource.Text;
            multimedia.YouTubeAdded = DateTime.Now;
            if (FileUpload2.HasFile)
            {
                //uploadFiles = new UploadFiles();
                //uploadFiles.ContentLength = Convert.ToString(FileUpload1.PostedFile.ContentLength);
                multimedia.ContentType = FileUpload2.PostedFile.ContentType;
                multimedia.fileName = FileUpload2.PostedFile.FileName;
                multimedia.photostreams = FileUpload2.PostedFile.InputStream;

            }
            if (rbnNo.Checked)
                multimedia.Publish = false;
            if (rbnYes.Checked)
                multimedia.Publish = true;
            multimedia.VideoId = Guid.NewGuid();
            switch (multimedia.Country)
            {
                case "kenya":
                    multimedia.BucketNameUrl = Settings.Default.BucketName + "/" + Settings.Default.Kenya;
                    multimedia.Url = Settings.Default.BucketNameURL + "/" + Settings.Default.Kenya + "/" + multimedia.fileName;
                    NewsEngine.SaveMultimedia(multimedia, Settings.Default.KenyaVideo);
                    break;
                case "malawi":
                    multimedia.BucketNameUrl = Settings.Default.BucketName + "/" + Settings.Default.Malawi;
                    multimedia.Url = Settings.Default.BucketNameURL + "/" + Settings.Default.Malawi + "/" + multimedia.fileName;
                    NewsEngine.SaveMultimedia(multimedia, Settings.Default.MalawiVideo);
                    break;
                case "tanzania":

                    multimedia.BucketNameUrl = Settings.Default.BucketName + "/" + Settings.Default.Tanzania;
                    multimedia.Url = Settings.Default.BucketNameURL + "/" + Settings.Default.Tanzania + "/" + multimedia.fileName;
                    NewsEngine.SaveMultimedia(multimedia, Settings.Default.TanzaniaVideo);
                    break;
                case "uganda":
                    multimedia.BucketNameUrl = Settings.Default.BucketName + "/" + Settings.Default.Uganda;
                    multimedia.Url = Settings.Default.BucketNameURL + "/" + Settings.Default.Uganda + "/" + multimedia.fileName;
                    NewsEngine.SaveMultimedia(multimedia, Settings.Default.UgandaVideo);
                    break;
                case "southafrica":
                    multimedia.BucketNameUrl = Settings.Default.BucketName + "/" + Settings.Default.SouthAfrica;
                    multimedia.Url = Settings.Default.BucketNameURL + "/" + Settings.Default.SouthAfrica + "/" + multimedia.fileName;
                    NewsEngine.SaveMultimedia(multimedia, Settings.Default.SouthAfricaVideo);
                    break;
                case "bostwana":
                    multimedia.BucketNameUrl = Settings.Default.BucketName + "/" + Settings.Default.Bostwana;
                    multimedia.Url = Settings.Default.BucketNameURL + "/" + Settings.Default.Bostwana + "/" + multimedia.fileName;
                    NewsEngine.SaveMultimedia(multimedia, Settings.Default.BostwanaVideo);
                    break;
                case "zambia":
                    multimedia.BucketNameUrl = Settings.Default.BucketName + "/" + Settings.Default.Zambia;
                    multimedia.Url = Settings.Default.BucketNameURL + "/" + Settings.Default.Zambia  + "/" + multimedia.fileName;
                    NewsEngine.SaveMultimedia(multimedia, Settings.Default.ZambiaVideo);
                    break;
                case "zimbabwe":
                    multimedia.BucketNameUrl = Settings.Default.BucketName + "/" + Settings.Default.Zimbabwe;
                    multimedia.Url = Settings.Default.BucketNameURL + "/" + Settings.Default.Zimbabwe + "/" + multimedia.fileName;
                    NewsEngine.SaveMultimedia(multimedia, Settings.Default.ZimbabweVideo);
                    break;

            }
            lblResult.Visible = true;
            lblResults2.Visible = true;
        }
    }
}
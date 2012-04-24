using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;
using System.IO;
using Amazon.S3;
using Amazon.S3.Model;
using System.Net;

namespace RssNewsEngine.Models
{
    public class NewsMethods
    {
        private AmazonS3 client;
        private string accessKeyID;
        private string secretAccessKeyID;
        private AmazonS3Config config;
        public NewsMethods()
        {
            accessKeyID = "AKIAIWFK6YSYC34OEFJQ";
            secretAccessKeyID = "STxTfkHrJTRcwFmrKAsN7eelCs81BhLiPlnIjdkq";
            config = new AmazonS3Config();
            config.ServiceURL = "s3.amazonaws.com";
        }
        #region Save
        public static void SaveNewsStory(string domainName, string bucketName, List<NewsComponents> newsItems, AmazonSimpleDBClient sdbClient, AmazonS3Client s3Client)
        {
            //BucketHelper.CheckForBucket(itemName, s3Client);

            //foreach (var stream in newsItems.Images)
            //{
            //    PutObjectRequest putObjectRequest = new PutObjectRequest();
            //    putObjectRequest.WithBucketName(bucketName);
            //    putObjectRequest.CannedACL = S3CannedACL.PublicRead;
            //    putObjectRequest.Key = stream.fileName;
            //    putObjectRequest.InputStream = stream.photostreams;
            //    S3Response response = s3Client.PutObject(putObjectRequest);
            //    response.Dispose();
            //}
            // DomainHelper.CheckForDomain(domainName, sdbClient);
            BatchPutAttributesRequest batchPutAttributesRequest = new BatchPutAttributesRequest();
            batchPutAttributesRequest.WithDomainName(domainName);
            ReplaceableItem replaceableItem;
            foreach (var list in newsItems)
            {
                replaceableItem = new ReplaceableItem();

                replaceableItem.WithItemName(Convert.ToString(list.NewsID));
                var list1 = new List<ReplaceableAttribute>{
                    new ReplaceableAttribute
                    {
                        Name = "NewsID",
                        Value = Convert.ToString(list.NewsID),
                        Replace = false
                    },
                    new ReplaceableAttribute
                    {
                        Name = "Source",
                        Value = list.Source,
                        Replace = false
                    },
                    new ReplaceableAttribute
                    {
                        Name = "Section",
                        Value = list.Section,
                        Replace = false
                    },
                    new ReplaceableAttribute
                    {
                        Name = "NewsItem",
                        Value = list.NewsItem,
                        Replace = false
                    },
                    new ReplaceableAttribute
                    {
                        Name = "NewsHeadline",
                        Value = list.NewsHeadline,
                        Replace = true
                    },
                    new ReplaceableAttribute
                    {
                        Name = "NewsAdded",
                        Value = Convert.ToString(list.NewsAdded),
                        Replace = true
                    },
                     new ReplaceableAttribute
                     {
                         Name = "Photos",
                         Value = list.NewsPhotoUrl,
                         Replace = true
                     } 
                     ,
                     new ReplaceableAttribute
                     {
                         Name = "Summary",
                         Value = list.Summary,
                         Replace = true
                     }, 
                     new ReplaceableAttribute
                     {
                         Name = "Category",
                         Value = list.Category,
                         Replace = true
                     }
                     ,
                     new ReplaceableAttribute
                     {
                         Name = "TimeStamp",
                         Value = Convert.ToString(list.TimeStamp),
                         Replace = true
                    }
                };
                replaceableItem.WithAttribute(list1.ToArray());
                batchPutAttributesRequest.Item.Add(replaceableItem);
            }



            //PutAttributesRequest  putAttrRequest = new PutAttributesRequest()
            //    .WithDomainName(domainName)
            //    .WithItemName(Convert.ToString(newsItems.NewsID));

            // sdbClient.PutAttributes(putAttrRequest);
            sdbClient.BatchPutAttributes(batchPutAttributesRequest);
        }
        public static void SaveNewItems(string domainName, string bucketName, NewsComponents newsItem, AmazonSimpleDBClient sdbClient, AmazonS3Client s3Client)
        {
            try
            {



                foreach (var stream in newsItem.Images)
                {
                    PutObjectRequest putObjectRequest = new PutObjectRequest();
                    putObjectRequest.WithBucketName(bucketName);
                    putObjectRequest.CannedACL = S3CannedACL.PublicRead;
                    putObjectRequest.Key = stream.fileName;
                    putObjectRequest.InputStream = stream.photostreams;


                    using (S3Response response = s3Client.PutObject(putObjectRequest))
                    {
                        WebHeaderCollection headers = response.Headers;
                        foreach (string key in headers.Keys)
                        {
                            //log headers ("Response Header: {0}, Value: {1}", key, headers.Get(key));
                        }
                    }
                }

                PutObjectRequest putObjectNewsItem = new PutObjectRequest();
                putObjectNewsItem.WithBucketName(newsItem.BucketName);
                putObjectNewsItem.CannedACL = S3CannedACL.PublicRead;
                putObjectNewsItem.Key = Convert.ToString(newsItem.NewsID);
                putObjectNewsItem.ContentType = "text/html";
                putObjectNewsItem.ContentBody = newsItem.NewsItem;



                using (S3Response response = s3Client.PutObject(putObjectNewsItem))
                {
                    WebHeaderCollection headers = response.Headers;
                    foreach (string key in headers.Keys)
                    {
                        //log headers ("Response Header: {0}, Value: {1}", key, headers.Get(key));
                    }
                }

                PutAttributesRequest putAttrRequest = new PutAttributesRequest()
                .WithDomainName(domainName)
                .WithItemName(Convert.ToString(newsItem.NewsID));

                putAttrRequest.WithAttribute(
                     new ReplaceableAttribute
                        {
                            Name = "NewsID",
                            Value = Convert.ToString(newsItem.NewsID),
                            Replace = false
                        },
                        new ReplaceableAttribute
                        {
                            Name = "Source",
                            Value = newsItem.Source,
                            Replace = false
                        },
                        new ReplaceableAttribute
                        {
                            Name = "NewsHeadline",
                            Value = newsItem.NewsHeadline,
                            Replace = true
                        },
                        new ReplaceableAttribute
                        {
                            Name = "NewsAdded",
                            Value = Convert.ToString(newsItem.NewsAdded),
                            Replace = true
                        }
                        ,
                         new ReplaceableAttribute
                         {
                             Name = "Summary",
                             Value = newsItem.Summary,
                             Replace = true
                         }
                         ,
                         new ReplaceableAttribute
                         {
                             Name = "SummaryContent",
                             Value = newsItem.SummaryContent,
                             Replace = true
                         }
                          ,
                         new ReplaceableAttribute
                         {
                             Name = "Imagelabel",
                             Value = string.IsNullOrWhiteSpace(newsItem.Imagelabel) ? "" : newsItem.Imagelabel,
                             Replace = true
                         },
                         new ReplaceableAttribute
                         {
                             Name = "Photos",
                             Value = newsItem.NewsPhotoUrl,
                             Replace = true
                         }
                        ,
                         new ReplaceableAttribute
                         {
                             Name = "Category",
                             Value = newsItem.Category,
                             Replace = true
                         }
                         ,
                         new ReplaceableAttribute
                         {
                             Name = "TimeStamp",
                             Value = Convert.ToString(newsItem.TimeStamp),
                             Replace = true
                         }
                          ,
                         new ReplaceableAttribute
                         {
                             Name = "Publish",
                             Value = Convert.ToString(newsItem.Publish),
                             Replace = true
                         });

                sdbClient.PutAttributes(putAttrRequest);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null && (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") || amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    //log exception - ("Please check the provided AWS Credentials.");
                }
                else
                {
                    //log exception -("An error occurred with the message '{0}' when writing an object", amazonS3Exception.Message);
                }
            }
            catch (AmazonSimpleDBException amazonSimpleDBException)
            {
                string val = amazonSimpleDBException.ErrorCode;
            }
        }
        public static void SaveVideoItems(string domainName, Multimedia videoItem, AmazonSimpleDBClient sdbClient, AmazonS3Client s3Client)
        {
            try
            {

                PutObjectRequest putObjectRequest = new PutObjectRequest();
                putObjectRequest.WithBucketName(videoItem.BucketNameUrl);
                putObjectRequest.CannedACL = S3CannedACL.PublicRead;
                putObjectRequest.Key = videoItem.fileName;
                putObjectRequest.InputStream = videoItem.photostreams;


                using (S3Response response = s3Client.PutObject(putObjectRequest))
                {
                    WebHeaderCollection headers = response.Headers;
                    foreach (string key in headers.Keys)
                    {
                        //log headers ("Response Header: {0}, Value: {1}", key, headers.Get(key));
                    }
                }


                PutAttributesRequest putVideoRequest = new PutAttributesRequest()
                .WithDomainName(domainName)
                .WithItemName(Convert.ToString(videoItem.VideoId));

                putVideoRequest.WithAttribute(
                     new ReplaceableAttribute
                     {
                         Name = "VideoId",
                         Value = Convert.ToString(videoItem.VideoId),
                         Replace = false
                     },
                        new ReplaceableAttribute
                        {
                            Name = "YoutubeUrl",
                            Value = videoItem.YoutubeUrl,
                            Replace = false
                        },
                        new ReplaceableAttribute
                        {
                            Name = "Country",
                            Value = videoItem.Country,
                            Replace = true
                        },
                        new ReplaceableAttribute
                        {
                            Name = "Title",
                            Value = Convert.ToString(videoItem.Title),
                            Replace = true
                        }
                        ,
                        new ReplaceableAttribute
                        {
                            Name = "Publish",
                            Value = Convert.ToString(videoItem.Publish),
                            Replace = true
                        }
                        ,
                         new ReplaceableAttribute
                         {
                             Name = "Content",
                             Value = videoItem.Content,
                             Replace = true
                         }
                          ,
                         new ReplaceableAttribute
                         {
                             Name = "Url",
                             Value = videoItem.Content,
                             Replace = true
                         });

                sdbClient.PutAttributes(putVideoRequest);
            }

            catch (AmazonSimpleDBException amazonSimpleDBException)
            {
                string val = amazonSimpleDBException.ErrorCode;
            }
        }
        public static void SaveNewsImages(string domainName, string itemName, string bucketName, string fileName, Stream fileContent, AmazonS3Client s3Client)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                return;
            }

            //BucketHelper.CheckForBucket(itemName, s3Client);
        }
        public static void SaveTagNames(string domainName, Tags tags, AmazonSimpleDBClient sdbClient)
        {
            PutAttributesRequest putTagRequest = new PutAttributesRequest()
                .WithDomainName(domainName)
                .WithItemName(Convert.ToString(tags.TagId));

            putTagRequest.WithAttribute(
                new ReplaceableAttribute
                {
                    Name = "TagId",
                    Value = Convert.ToString(tags.TagId),
                    Replace = false
                },
                new ReplaceableAttribute
                {
                    Name = "TagName",
                    Value = tags.TagName,
                    Replace = false
                });
            sdbClient.PutAttributes(putTagRequest);


        }
        #endregion
        #region Get
        public static List<Tags> GetTags(string domainName,AmazonSimpleDBClient sdbClient)
        {
            List<Tags> tagses=new List<Tags>();
            Tags tags;
             String selectExpression = "Select TagId,TagName From " + domainName;
            SelectRequest selectRequestAction = new SelectRequest().WithSelectExpression(selectExpression);
            SelectResponse selectResponse = sdbClient.Select(selectRequestAction);
            if (selectResponse.IsSetSelectResult())
            {
                SelectResult selectResult = selectResponse.SelectResult;
                foreach (Item item in selectResult.Item)
                {
                    tags =new Tags();;
                    if (item.IsSetName())
                    {

                    }
                    int i = 0;
                    foreach (Amazon.SimpleDB.Model.Attribute attribute in item.Attribute)
                    {


                        if (attribute.IsSetName())
                        {
                            string name = attribute.Name;
                        }
                        if (attribute.IsSetValue())
                        {
                            switch (attribute.Name)
                            {
                                case "TagId":
                                    tags.TagId = Guid.Parse(attribute.Value);
                                    break;
                                case "TagName":
                                    tags.TagName= attribute.Value;
                                    break;
                               
                            }
                            i++;
                        }
                    }
                    tagses.Add(tags);
                }
            }
            return tagses;
        }
        
        #endregion


    }
}
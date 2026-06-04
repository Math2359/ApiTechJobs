using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Services.Utils.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utils
{
    public class AwsService : IAwsService
    {
        private readonly string _bucketName = "s3-bucket-techjobs";

        public async Task<string> UploadFileAsync(IFormFile file, string folder)
        {
            var s3Client = new AmazonS3Client();

            if (file == null || file.Length == 0)
                throw new Exception("Arquivo inválido.");

            // Nome final no bucket
            var fileKey = $"{folder}/{Guid.NewGuid()}_{file.FileName}";

            // Faz o upload
            using (var newMemoryStream = new MemoryStream())
            {
                await file.CopyToAsync(newMemoryStream);

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = newMemoryStream,
                    Key = fileKey,
                    BucketName = _bucketName,
                    ContentType = file.ContentType
                };

                var transferUtility = new TransferUtility(s3Client);

                await transferUtility.UploadAsync(uploadRequest);
            }

            return fileKey;
        }

        public async Task RemoveFileAsync(string fileKey)
        {
            using var client = new AmazonS3Client();

            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = fileKey
            };

            await client.DeleteObjectAsync(deleteObjectRequest);
        }

        public async Task<string> PreSignedURL(string fileKey)
        {
            var s3client = new AmazonS3Client();

            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = fileKey,
                Expires = DateTime.UtcNow.AddMinutes(15),
            };

            // Gera a URL
            return await s3client.GetPreSignedURLAsync(request);
        }
    }
}

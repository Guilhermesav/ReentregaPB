using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;


namespace QueueFunction
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public async static Task Run([QueueTrigger("queue-image-insert")]
                      MessageImage message,
                      IBinder binder,
                      ILogger log)
        {
            log.LogInformation($"Função ativada!");

            using var webClient = new WebClient();

            byte[] imageBytes = webClient.DownloadData(message.ImageUri.ToString());

            var image = (Image)(new Bitmap(Image.FromStream(new MemoryStream(imageBytes)), new Size(150, 150)));

            var converter = new ImageConverter();
            var img = (byte[])converter.ConvertTo(image, typeof(byte[]));

            var blobAttribute = new BlobAttribute($"imagens/{Guid.NewGuid()}.jpg", FileAccess.Write);
            var cloudBlobStream = await binder.BindAsync<ICloudBlob>(blobAttribute);
            await cloudBlobStream.UploadFromByteArrayAsync(img, 0, img.Length);
            await cloudBlobStream.Container.SetPermissionsAsync(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Blob });

            var connectionString = Environment.GetEnvironmentVariable("PostContext");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var textSql = $@"UPDATE [dbo].[Posts] SET [UrlFoto] = '{cloudBlobStream.Uri}' WHERE Id = {message.Id}";

                using (SqlCommand cmd = new SqlCommand(textSql, conn))
                {
                    var rowsAffected = cmd.ExecuteNonQuery();
                    log.LogInformation($"rowsAffected: {rowsAffected}");
                }
            }

            log.LogInformation($"Função encerrada!");
        }

        public class MessageImage
        {
            public string ImageUri { get; set; }
            public string Id { get; set; }
        }
    }
}

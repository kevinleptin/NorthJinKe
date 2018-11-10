using Newtonsoft.Json;
using NorthJinKe.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace NorthJinKe.Services
{
    public interface IFileDownloaderService
    {
       Task<byte[]> DownloadWxFileAsync(string id);
        /// <summary>
        /// 保存数据到指定文件夹并返回绝对路径
        /// </summary>
        /// <param name="rootFolderPath"></param>
        /// <param name="fileData"></param>
        /// <returns></returns>
       Task<string> SaveFileAsync(string rootFolderPath, byte[] fileData);
    }

    public class FileDownloaderService : IFileDownloaderService
    {
        public async Task<byte[]> DownloadWxFileAsync(string id)
        {
            string appId = ConfigurationManager.AppSettings["wxAppId"];
            string appSecret = ConfigurationManager.AppSettings["wxAppSecret"];
            string tokenUrl = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}",
                                        appId, appSecret);
            HttpClient client = new HttpClient();
            //todo: access_token 应该放到一个独立，定时刷新的地方存取
            var tokenEntity = await client.GetStringAsync(tokenUrl).ContinueWith(c => JsonConvert.DeserializeObject<AccessTokenRespEntity>(c.Result));

            string accessToken = tokenEntity.Access_Token;

            string mediaUrl = string.Format("https://api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}",
                                            accessToken,
                                            id);

            var result = await client.GetByteArrayAsync(mediaUrl);
            return result;
        }

        public async Task<string> SaveFileAsync(string rootFolderPath, byte[] fileData)
        {
            string fileFolderPath = Path.Combine(rootFolderPath, DateTime.Now.ToString("YYYY"), DateTime.Now.ToString("MM-dd"));
            if (!Directory.Exists(fileFolderPath))
            {
                Directory.CreateDirectory(fileFolderPath);
            }
            string fileName = Guid.NewGuid().ToString()+".amr";
            string fullFilePath = Path.Combine(fileFolderPath, fileName);
            
            //todo: async
                File.WriteAllBytes(fullFilePath, fileData);
            
            return fullFilePath;
        }
    }
}
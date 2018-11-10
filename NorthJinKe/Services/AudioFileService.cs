using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NorthJinKe.Services
{
    public interface IAudioFileService
    {
        /// <summary>
        /// convert the arm format to mp3 format
        /// </summary>
        /// <param name="amrFilePath">amr file absolute path</param>
        /// <returns>the absolute file path of mp3 file</returns>
        Task<string> ConvertAmr2Mp3Async(string amrFilePath);
    }

    public class AudioFileService : IAudioFileService
    {
        public async Task<string> ConvertAmr2Mp3Async(string amrFilePath)
        {
            throw  new NotImplementedException();
        }
    }
}
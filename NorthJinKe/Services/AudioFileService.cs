using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        private readonly string toolPath = @"C:\ffmpeg\bin\ffmpeg.exe";
        public async Task<string> ConvertAmr2Mp3Async(string amrFilePath)
        {
            string result = await Task<string>.Factory.StartNew(() =>
            {
                string inputFileFullPath = amrFilePath;
                string inputFileName = Path.GetFileNameWithoutExtension(amrFilePath);
                string inputFileFolderPath = Path.GetDirectoryName(amrFilePath);
                string outputFileFullPath = Path.Combine(inputFileFolderPath, inputFileName, ".mp3");
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                psi.FileName = toolPath;
                psi.Arguments = "-i "+inputFileFullPath+" "+outputFileFullPath;
                psi.UseShellExecute = false;
                Process proc = Process.Start(psi);
                
                proc.WaitForExit(3000);
                return proc.StandardOutput.ReadToEnd();
            });
            
            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace NorthJinKe.Services
{
    public interface IHostingEnvironmentService
    {
        string AppMapPath(string relPath);
        string AppPhysicalPath();
    }

    public class HostingEnvironmentService : IHostingEnvironmentService
    {
        public string AppMapPath(string relPath)
        {
            return HostingEnvironment.MapPath(relPath);
        }

        public string AppPhysicalPath()
        {
            return HostingEnvironment.ApplicationPhysicalPath;
        }
    }
}
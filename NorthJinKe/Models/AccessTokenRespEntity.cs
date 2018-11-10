using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthJinKe.Models
{
    public class AccessTokenRespEntity
    {
        public string Access_Token { get; set; }
        public int Expires_In { get; set; }

        public int Errcode { get; set; }

        public string Errmsg { get; set; }
    }
}
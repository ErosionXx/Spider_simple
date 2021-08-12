using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaFormsApp
{
    public static class Formats
    {
        public static void DownLoad(string url, string path, string fileName) 
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.2) AppleWebKit/525.13 (KHTML, like Gecko) Chrome/0.2.149.27 Safari/525.13";
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "GET";
            // request.Referer = "http://pinyin.sogou.com/dict/list.php?c=180";
           // request.Referer = "http://pinyin.sogou.com/dict/cell.php?id=19431";

            request.KeepAlive = false;
            request.Timeout = 4000;
            //request.ContentType="text/plain";
            request.ProtocolVersion = HttpVersion.Version10;

            HttpWebResponse response;
            Stream resStream;
            if (request==null) 
            {
                return;
            }
            response = (HttpWebResponse)request.GetResponse();
            resStream = response.GetResponseStream();

            int count = (int)response.ContentLength;
            int offset = 0;
            byte[] buf = new byte[count];
            while (count > 0)
            {
                int n = resStream.Read(buf, offset, count);
                if (n == 0)
                    break;
                count -= n;
                offset += n;
            }

            if (!System.IO.File.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            FileStream fs = new FileStream(path + "\\" + fileName, FileMode.Create, FileAccess.Write);
            fs.Write(buf, 0, buf.Length);
            fs.Flush();
            fs.Close();
            //Thread.Sleep(8000);
        }
    }
}

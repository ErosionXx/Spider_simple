using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.textBox2.Text = "";
            string strurl = GetPageData(this.textBox1.Text);
            //WebRequest request = WebRequest.Create(strurl);
            //WebResponse response = request.GetResponse();
            //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")); //reader.ReadToEnd() 表示取得网页的源码
            //this.textBox2.Text = reader.ReadToEnd();
            string strRef = @"(href|HREF|src|SRC|action|ACTION|Action)[ ]*=[ ]*[""'][^""'#>]+[""']";
            MatchCollection matches = new Regex(strRef).Matches(strurl);//在strResponse匹配的字符串
           
            int y = 0;
            for (int i = 0; i < matches.Count; i++)
            {
                Match match = matches[i];
                string str = match.Value;

                if (str.Contains(".png")|| str.Contains(".jpg")) 
                {
                    string tee = @"(\w)+:[^\"+"\"]*";

                    Regex Mars = new Regex(tee);

                    Match m2 = Mars.Match(str);

                    //int s = str.IndexOf('"')+1;
                    //int l = str.Length-s-1;
                    //str =str.Substring(s,l);ttr
                    this.textBox2.Text += m2.Value + ",";
                    y++;
                }
            }
            MessageBox.Show("找到" + y+ "个链接");
        }

        private static string GetPageData(string url)//获取url字符串的方法
        {
            if (url == null || url.Trim() == "")
            return null;
            WebClient wc = new WebClient();
                    wc.Credentials = CredentialCache.DefaultCredentials;
            Byte[] pageData = wc.DownloadData(url);
            return Encoding.Default.GetString(pageData);//.ASCII.GetString
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog file = new FolderBrowserDialog();
            file.ShowDialog();
            string Filename= file.SelectedPath;
          
            string[] arrays = this.textBox2.Text.Split(',');
            int s = 0;
            foreach (string item in arrays)
            {
                if (s<12) 
                {
                    string path = item.Replace("'", "");
                    string endname = path.Substring(path.LastIndexOf('/')+1, path.Length - (path.LastIndexOf('/') + 1));
                    Formats.DownLoad(path, Filename, endname);
                }
                s++;
            }

        }
    }
}

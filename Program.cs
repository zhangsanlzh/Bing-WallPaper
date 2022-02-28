using System;
using System.Net.Http;
using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.Win32;

using System.Drawing;

namespace web
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            var html = client.GetStringAsync("https://cn.bing.com").GetAwaiter().GetResult();

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            
            string str = "//*[@id='preloadBg']";
            var imgPath = htmlDoc.DocumentNode.SelectSingleNode(str).Attributes["href"].Value;

            new WebClient().DownloadFile($"https://cn.bing.com{imgPath}", "bg.jpg");

            Image image = Image.FromFile("bg.jpg");
            image.Save("bg.bmp",System.Drawing.Imaging.ImageFormat.Bmp);
            SystemParametersInfo(20, 0, AppDomain.CurrentDomain.BaseDirectory + "bg.bmp", 1|2);

            Console.WriteLine("设置成功");
        }

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(
         int uAction,
         int uParam,
         string lpvParam,
         int fuWinIni
         ); 
    }
}


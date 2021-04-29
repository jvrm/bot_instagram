using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Chrome;
using CefSharp.WinForms;
using CefSharp;
using System.Threading;

namespace WindowsFormsApp1
{
    
    public partial class Form1 : Form
    {
        List<string> links = new List<string>();
        string result = "";
        List<string> Seguidores = new List<string>();
        List<string> Seguindo = new List<string>();

        public ChromiumWebBrowser chromeBrowser;
        public Form1()
        {
            links = System.IO.File.ReadLines(Environment.CurrentDirectory+"/nomes.txt").ToList();

            InitializeComponent();
            // Start the browser after initialize global component
            InitializeChromium();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //this.Activated += AfterLoading;
            
        }

        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            settings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\CEF";
            Cef.Initialize(settings);
            // Create a browser component
            chromeBrowser = new ChromiumWebBrowser("https://www.instagram.com.br/jvitorreis_m");
            
            
            // Add it to the form and fill it to the form window.
            this.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void ExecuteJavaScript(string script)
        {
            //this.webView.ExecuteScript(script);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int j = 1; j < links.Count; j++)
            {
                chromeBrowser.Load("http://www.instagram.com.br/" + links[j]);
                Task.Delay(5000).Wait();
                chromeBrowser.GetMainFrame().ExecuteJavaScriptAsync("document.getElementsByClassName('g47SY')[1].click()");
                Task.Delay(5000).Wait();
                chromeBrowser.GetMainFrame().ExecuteJavaScriptAsync("document.getElementsByClassName('isgrP')[0].scrollTop = document.getElementsByClassName('isgrP')[0].scrollHeight");
                Task.Delay(5000).Wait();

                for (int i = 0; i < 20; i++)
                {
                    chromeBrowser.GetMainFrame().ExecuteJavaScriptAsync("if (document.getElementsByClassName('sqdOP  L3NKy   y3zKF     ')[0] === undefined) { document.getElementsByClassName('isgrP')[0].scrollTop = document.getElementsByClassName('isgrP')[0].scrollHeight} else{document.getElementsByClassName('sqdOP  L3NKy   y3zKF     ')[0].click()}");
                    Random rd = new Random();
                    Thread.Sleep(rd.Next(1000,10000));
                }

                Task.Delay(600000).Wait();

            }

        }

        private async void deixarDeSeguir()
        {
            chromeBrowser.Load("https://www.instagram.com/jvitorreis_m/followers/");
            Task.Delay(5000).Wait();
            chromeBrowser.GetMainFrame().ExecuteJavaScriptAsync("document.getElementsByClassName('g47SY')[1].click()");
            Task.Delay(5000).Wait();

            for (int j = 0; j < 400; j++)
            {
                chromeBrowser.GetMainFrame().ExecuteJavaScriptAsync("document.getElementsByClassName('isgrP')[0].scrollTop = document.getElementsByClassName('isgrP')[0].scrollHeight");
                Task.Delay(600).Wait();
            }

            for (int i = 0; i < 2400; i++)
            {
                string script = "(function() {return document.getElementsByClassName('FPmhX notranslate  _0imsa ')[" + i + "].getAttribute('title')})();";
                string returnValue = "ola";

                var task = chromeBrowser.EvaluateScriptAsync(script);

                await task.ContinueWith(t =>
                {
                    if (!t.IsFaulted)
                    {
                        var response = t.Result;

                        if (response.Success && response.Result != null)
                        {
                            Seguidores.Add(response.Result.ToString());
                        }
                    }
                });
            }


            Console.WriteLine(Seguidores);

            chromeBrowser.Load("https://www.instagram.com/jvitorreis_m/followers/");
            Task.Delay(5000).Wait();
            chromeBrowser.GetMainFrame().ExecuteJavaScriptAsync("document.getElementsByClassName('g47SY')[2].click()");
            Task.Delay(5000).Wait();
            
            for (int k = 0; k < 400; k++)
            {
                chromeBrowser.GetMainFrame().ExecuteJavaScriptAsync("document.getElementsByClassName('isgrP')[0].scrollTop = document.getElementsByClassName('isgrP')[0].scrollHeight");
                Task.Delay(600).Wait();
            }

            for (int l = 0; l < 2000; l++)
            {
                string script = "(function() {return document.getElementsByClassName('FPmhX notranslate  _0imsa ')[" + l + "].getAttribute('title')})();";
                
                var task = chromeBrowser.EvaluateScriptAsync(script);

                await task.ContinueWith(t =>
                {
                    if (!t.IsFaulted)
                    {
                        var response = t.Result;

                        if (response.Success && response.Result != null)
                        {
                            Seguindo.Add(response.Result.ToString());
                        }
                    }
                });
            }
            Console.WriteLine(Seguindo);

            var list3 = Seguindo.Except(Seguidores).ToList(); //list3 contains only 1, 2

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            deixarDeSeguir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            chromeBrowser.Load("https://www.instagram.com/jvitorreis_m/followers/");
        }

        /*private async void AfterLoading(object sender, EventArgs e)
        {
            this.Activated -= AfterLoading;

            string script = "alert('ola')";

            //chromeBrowser.GetMainFrame().EvaluateScriptAsync(script);

            this.chromeBrowser.IsBrowserInitializedChanged += (s, b) =>
            {
                //Task.Delay(5000).Wait();
                
            };

            
        }*/
    }


}









/*private void Form1_Load(object sender, EventArgs e)
        {
            //Carrega();

            System.Uri uri = new System.Uri("https://www.instagram.com.br");
            //webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser2.Url = uri;
            label1.Text = webBrowser2.Version.ToString();
        }

        public static void Carrega()
        {

            
            


            /*bool _jsLoaded = false;
            string _directory = AppDomain.CurrentDomain.BaseDirectory;
            System.Diagnostics.Process.Start("https://www.instagram.com.br/froezoficial/followers");
            System.Diagnostics.ProcessStartInfo _sinfo = new
            System.Diagnostics.ProcessStartInfo(_directory + "botinsta.html");
            System.Diagnostics.Process.Start(_sinfo);

            while (_jsLoaded == false)
            {
                System.Diagnostics.Process[] _runningProcesses =
                System.Diagnostics.Process.GetProcesses();
                foreach (System.Diagnostics.Process _p in _runningProcesses)
                {

                    if (_p.MainWindowTitle.Contains("Instagram"))
                    {
                        _jsLoaded = true;
                        break;
                    }
                }

            }

            
            */
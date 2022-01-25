using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Xml;

namespace TP_Link_Router_Auto_Restart
{
    internal abstract class RouterSettings
    {
        public string Url { get; set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public ushort WaitingTime { get; private set; }
        public bool Headless { get; private set; }

        protected RouterSettings(string url = "http://192.168.0.1/", string login = "admin", string password = "admin", ushort waitingTime = 400, bool headless = false)
        {
            Url = url;
            Login = login;
            Password = password;
            WaitingTime = waitingTime;
            Headless = headless;
            CreateSettingsFile();
            CreateDriver();
        }

        private void CreateSettingsFile()
        {
            if (File.Exists("settings.xml")) return;

            var xmlTextWriter = new XmlTextWriter("settings.xml", Encoding.UTF8)
            {
                Formatting = Formatting.Indented
            };

            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("Settings");

            xmlTextWriter.WriteStartElement("Url");
            xmlTextWriter.WriteString($"{Url.Remove(0, Url.IndexOf(":", StringComparison.Ordinal) + 1).Trim('/')}");
            xmlTextWriter.WriteEndElement();

            xmlTextWriter.WriteStartElement("Login");
            xmlTextWriter.WriteString(Login);
            xmlTextWriter.WriteEndElement();

            xmlTextWriter.WriteStartElement("Password");
            xmlTextWriter.WriteString(Password);
            xmlTextWriter.WriteEndElement();

            xmlTextWriter.WriteStartElement("WaitingTime");
            xmlTextWriter.WriteString(WaitingTime.ToString());
            xmlTextWriter.WriteEndElement();

            xmlTextWriter.WriteStartElement("HeadlessMode");
            xmlTextWriter.WriteString(Headless.ToString());
            xmlTextWriter.WriteEndElement();

            xmlTextWriter.WriteEndDocument();
            xmlTextWriter.Close();
        }

        private void CreateDriver()
        {
            if (!File.Exists("msedgedriver.exe"))
            {
                using var client = new WebClient();
                client.DownloadFile("https://msedgedriver.azureedge.net/97.0.1072.69/edgedriver_win64.zip", "edgedriver_win64.zip");
                ZipFile.ExtractToDirectory("edgedriver_win64.zip", @".");
                File.Delete("edgedriver_win64.zip");
                Directory.Delete("Driver_Notes", true);
            }
            else
            {
                UpdateSettings();
            }
        }

        public void UpdateSettings()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("settings.xml");

            XmlElement xRootElement = xmlDocument.DocumentElement;

            foreach (XmlElement xNode in xRootElement)
            {
                switch (xNode.Name)
                {
                    case "Url":
                        Url = $"http://{xNode.InnerText}/";
                        break;
                    case "Login":
                        Login = xNode.InnerText;
                        break;
                    case "Password":
                        Password = xNode.InnerText;
                        break;
                    case "WaitingTime":
                        WaitingTime = ushort.Parse(xNode.InnerText);
                        break;
                    case "HeadlessMode":
                        if (xNode.InnerText is "true" or "True")
                            Headless = true;
                        else if (xNode.InnerText is "false" or "False")
                            Headless = false;
                        break;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
        }

        private void CreateSettingsFile()
        {
            if (File.Exists("Setting.txt"))
            {
                UpdateSettings();
                return;
            }

            try
            {
                using StreamWriter sw = new StreamWriter("Setting.txt", false, System.Text.Encoding.Default);

                sw.WriteLine($"Url = \"{Url.Remove(0, Url.IndexOf(":", StringComparison.Ordinal) + 1).Trim('/')}\"");
                sw.WriteLine($"Login = \"{Login}\"");
                sw.WriteLine($"Password = \"{Password}\"");
                sw.WriteLine($"WaitingTime = \"{WaitingTime}\"");
                sw.WriteLine($"HeadlessMode = \"{Headless}\"");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateSettings()
        {
            var lines = new List<string>();

            try
            {
                using StreamReader sr = new StreamReader("Setting.txt", Encoding.Default);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }

                foreach (var item in lines)
                {
                    line = item;

                    if (line.Contains("Url"))
                    {
                        Url = $"http://{line.Remove(0, line.IndexOf('\"')).Trim('\"')}/";
                    }
                    else if (line.Contains("Login"))
                    {
                        Login = line.Remove(0, line.IndexOf('\"')).Trim('\"');
                    }

                    else if (line.Contains("Password"))
                    {
                        Password = line.Remove(0, line.IndexOf('\"')).Trim('\"');
                    }

                    else if (line.Contains("WaitingTime"))
                    {
                        WaitingTime = ushort.Parse(line.Remove(0, line.IndexOf('\"')).Trim('\"'));
                    }

                    else if (line.Contains("HeadlessMode"))
                    {
                        string temp = line.Remove(0, line.IndexOf('\"')).Trim('\"');

                        if (temp == "true" || temp == "True")
                        {
                            Headless = true;
                        }
                        else if (temp == "false" || temp == "False")
                        {
                            Headless = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Settings.txt has a problem");
                Console.ResetColor();
            }
        }
    }
}

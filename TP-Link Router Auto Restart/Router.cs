using System;
using System.Net.Sockets;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace TP_Link_Router_Auto_Restart
{
    internal sealed class Router : RouterSettings
    {
        private readonly WebDriver _driver;
        private IWebElement _query;

        public Router(string url = "http://192.168.0.1/", string login = "admin", string password = "admin", ushort waitingTime = 400, bool headless = false) : base(url, login, password, waitingTime, headless)
        {
            var options = new EdgeOptions();

            if (Headless)
            {
                options.AddArgument("headless");
            }

            _driver = new EdgeDriver(options);
        }

        private void Restart()
        {
            _driver.SwitchTo().Frame("bottomLeftFrame");

            FindSettingsField();

            FindRestartField();

            _driver.SwitchTo().DefaultContent();
            _driver.SwitchTo().Frame("mainFrame");

            Thread.Sleep(WaitingTime);

            FindRestartButton();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n\n==============================");
            Console.WriteLine("Router reboots successfully...");
            Console.WriteLine("==============================");
            Console.ResetColor();

            Thread.Sleep(5000);

            _driver.Quit();

            void FindSettingsField()
            {
                bool isFind = false;

                foreach (var elementLink in RouterElements.ElementsSettingsField)
                {
                    try
                    {
                        _query = _driver.FindElement(By.XPath(elementLink));
                        _query.Click();
                        isFind = true;
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(e.Message);
                        Console.ResetColor();
                    }
                }

                if (!isFind)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    throw new Exception("Element Field Settings is not exist!");
                }
            }

            void FindRestartField()
            {
                bool isFind = false;

                foreach (var elementLink in RouterElements.ElementsRestartField)
                {
                    try
                    {
                        _query = _driver.FindElement(By.XPath(elementLink));
                        _query.Click();
                        isFind = true;
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(e.Message);
                        Console.ResetColor();
                    }
                }

                if (!isFind)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    throw new Exception("Element Field Restart is not exist!");
                }
            }

            void FindRestartButton()
            {
                bool isFind = false;

                foreach (var elementLink in RouterElements.ElementsRestartButton)
                {
                    try
                    {
                        _query = _driver.FindElement(By.XPath(elementLink));
                        _query.Click();
                        IAlert alert = _driver.SwitchTo().Alert();
                        alert.Accept();
                        isFind = true;
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(e.Message);
                        Console.ResetColor();
                    }
                }

                if (!isFind)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    throw new Exception("Button Restart is not exist!");
                }
            }
        }

        public void ConnectToAdminPanel()
        {
            FindLink();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Successful connection to {Url.Remove(0, Url.IndexOf(":", StringComparison.Ordinal) + 1).Trim('/')}");
            Console.WriteLine("Page title is: " + _driver.Title);
            Console.ResetColor();

            Thread.Sleep(WaitingTime);

            FindLoginField();

            Thread.Sleep(WaitingTime);

            FindPasswordField();

            Thread.Sleep(WaitingTime);

            FindEnterButton();

            Thread.Sleep(WaitingTime);

            void FindLink()
            {
                var tcpClient = new TcpClient();
                Console.Clear();
                Console.WriteLine("Trying to connect to your router...");

                try
                {
                    tcpClient.Connect(Url.Remove(0, Url.IndexOf(":", StringComparison.Ordinal) + 1).Trim('/'), 80);
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Your IP in file Setting.txt is dead!");
                    Console.WriteLine("Trying to guess the ip..");
                    Console.ResetColor();
                }

                if (!tcpClient.Connected)
                {
                    foreach (var link in RouterElements.Links)
                    {
                        try
                        {
                            tcpClient.Connect(link, 80);
                            Url = $"http://{link}/";
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }

                    if (!tcpClient.Connected)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        throw new Exception("Url is not exit! Enter correct link in file Settings.txt!");
                    }
                }

                _driver.Navigate().GoToUrl(Url);
            }

            void FindLoginField()
            {
                bool isFind = false;

                foreach (var elementLink in RouterElements.ElementsLoginField)
                {
                    try
                    {
                        _query = _driver.FindElement(By.XPath(elementLink));
                        _query.SendKeys(Login);
                        isFind = true;
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(e.Message);
                        Console.ResetColor();
                    }
                }

                if (!isFind)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Field Login is not exist. Trying input only password..");
                    Console.ResetColor();
                }
            }

            void FindPasswordField()
            {
                bool isFind = false;

                foreach (var elementLink in RouterElements.ElementsPasswordField)
                {
                    try
                    {
                        _query = _driver.FindElement(By.XPath(elementLink));
                        _query.SendKeys(Password);
                        isFind = true;
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(e.Message);
                        Console.ResetColor();
                    }
                }

                if (isFind) return;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                throw new Exception("Field Password is not exist!");
            }

            void FindEnterButton()
            {
                bool isFind = false;

                foreach (var elementLink in RouterElements.ElementsEnterButton)
                {
                    try
                    {
                        _query = _driver.FindElement(By.XPath(elementLink));
                        _query.Click();

                        isFind = true;
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(e.Message);
                        Console.ResetColor();
                    }
                }

                if (isFind) return;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                throw new Exception("Button Enter is not exist!");
            }
        }

        public void Go()
        {
            ConnectToAdminPanel();
            Restart();
        }
    }
}

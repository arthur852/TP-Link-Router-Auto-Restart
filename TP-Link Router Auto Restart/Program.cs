using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TP_Link_Router_Auto_Restart
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            bool testMode = false;
            ushort waitingTime = 400;
            var options = new ChromeOptions();

            foreach (var arg in args)
            {
                switch (arg)
                {
                    case "-headless":
                        options.AddArgument("headless");
                        break;
                    case "-test":
                        testMode = true;
                        break;
                }
            }

            string url = "http://192.168.0.1/";
            string login = "admin";
            string password = "admin";

            IWebDriver driver = new ChromeDriver(options);

            try
            {
                driver.Navigate().GoToUrl(url);
            }
            catch (WebDriverArgumentException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Incorrect link!");
                Console.WriteLine(e);
                Console.ResetColor();
                throw;
            }

            Console.Clear();
            Console.WriteLine($"Connect to {url}");
            Console.WriteLine("Page title is: " + driver.Title);

            IWebElement query;

            Thread.Sleep(waitingTime);

            try
            {
                query = driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/ul/li[1]/input"));
                query.SendKeys(login);
            }
            catch (NoSuchElementException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(e);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Field Login is not exist.");
                Console.ResetColor();
            }

            Thread.Sleep(waitingTime);

            query = driver.FindElement(By.Id("pcPassword"));
            query.SendKeys(password);

            Thread.Sleep(waitingTime);

            query = driver.FindElement(By.Id("loginBtn"));
            query.Click();

            driver.SwitchTo().Frame("bottomLeftFrame");

            Thread.Sleep(waitingTime);

            try
            {
                query = driver.FindElement(By.XPath("/html/body/menu/ol[45]"));
            }
            catch (NoSuchElementException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(e);
                Console.ResetColor();

                try
                {
                    query = driver.FindElement(By.XPath("/html/body/div/div[1]/ul/li[17]"));
                }
                catch (NoSuchElementException ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(ex);
                    Console.ResetColor();
                    throw;
                }
            }

            query.Click();

            Thread.Sleep(waitingTime);

            try
            {
                query = driver.FindElement(By.XPath("/html/body/menu/ol[51]/a"));
            }
            catch (NoSuchElementException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(e);
                Console.ResetColor();

                try
                {
                    query = driver.FindElement(By.XPath("/html/body/div/div[1]/ul/li[17]/ul/li[7]/a"));
                }
                catch (NoSuchElementException ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(ex);
                    Console.ResetColor();
                    throw;
                }
            }

            query.Click();

            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame("mainFrame");

            Thread.Sleep(waitingTime);

            try
            {
                query = driver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[4]/td/input"));
            }
            catch (NoSuchElementException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(e);
                Console.ResetColor();

                try
                {
                    query = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/p[3]/input"));
                }
                catch (NoSuchElementException ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(ex);
                    Console.ResetColor();
                    throw;
                }
            }

            query.Click();

            Thread.Sleep(waitingTime);

            IAlert alert = driver.SwitchTo().Alert();

            if (!testMode)
            {
                alert.Accept();
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("The router is rebooting...");
            Console.ResetColor();

            Thread.Sleep(5000);

            driver.Quit();
        }
    }
}

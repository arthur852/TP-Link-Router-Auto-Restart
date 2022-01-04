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
            var options = new ChromeOptions();

            foreach (var arg in args)
            {
                if (arg == "-headless")
                {
                    options.AddArgument("headless");
                    break;
                }
            }
            
            string url = "http://192.168.0.1/";
            string login = "admin";
            string password = "admin";

            IWebDriver driver = new ChromeDriver(options);
            Console.Clear();

            driver.Navigate().GoToUrl(url);
            Console.WriteLine($"Connect to {url}");
            Console.WriteLine("Page title is: " + driver.Title);

            IWebElement query;

            try
            {
                query = driver.FindElement(By.Id("userName"));
                query.SendKeys(login);
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Field Login is not exist.");
            }
            
            query = driver.FindElement(By.Id("pcPassword"));
            query.SendKeys(password);

            query = driver.FindElement(By.Id("loginBtn"));
            query.Click();

            driver.SwitchTo().Frame("bottomLeftFrame");

            try
            {
                query = driver.FindElement(By.XPath("/html/body/menu/ol[45]"));
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine(e);

                try
                {
                    query = driver.FindElement(By.XPath("/html/body/div/div[1]/ul/li[17]"));
                }
                catch (NoSuchElementException ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }

            query.Click();

            try
            {
                query = driver.FindElement(By.XPath("/html/body/menu/ol[51]/a"));
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine(e);

                try
                {
                    query = driver.FindElement(By.XPath("/html/body/div/div[1]/ul/li[17]/ul/li[7]/a"));
                }
                catch (NoSuchElementException ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }

            query.Click();

            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame("mainFrame");

            try
            {
                query = driver.FindElement(By.XPath("/html/body/center/form/table/tbody/tr[4]/td/input"));
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine(e);

                try
                {
                    query = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/p[3]/input"));
                }
                catch (NoSuchElementException ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }

            query.Click();

            IAlert alert = driver.SwitchTo().Alert();

            //alert.Accept();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("The router is rebooting...");
            Console.ResetColor();

            Thread.Sleep(5000);

            driver.Quit();
        }
    }
}

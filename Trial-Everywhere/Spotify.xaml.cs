using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Trial_Everywhere
{
    /// <summary>
    /// Interaction logic for Spotify.xaml
    /// </summary>
    public partial class Spotify : Window
    {
        public Spotify(bool runningSelenium)
        {
            this.RunningSelenium = runningSelenium;
            InitializeComponent();
        }

        private readonly ChromeDriverService _driverService = ChromeDriverService.CreateDefaultService(); // Create chrome (selenium) settings
        private ChromeDriver _driver;
        public bool RunningSelenium;
        private Thread _runSeleniumThread;

        public string FirstNameUser = "John";
        public string LastNameUser = "Onion";
        public string EmailUser = "test@test.com";
        public string PassUser = "Power132";

        public string CardNumberPrefix;

        private void OpenSelenium(object sender, RoutedEventArgs e)
        {
            if (RunningSelenium) return;
            _runSeleniumThread = new Thread(RunSelenium); // create thread with selenium
            _driverService.HideCommandPromptWindow = true; // Disabling cmd when run selenium

            if (GetUserCredits())
            {
                return;
            }

            _runSeleniumThread.Start();
        }

        private void CloseSelenium(object sender, RoutedEventArgs e)
        {
            if (!RunningSelenium) return;
            var exiThread = new Thread(CloseSeleniumThread);
            exiThread.Start();
            RunningSelenium = false;
        }

        private void CloseSeleniumThread()
        {
            _runSeleniumThread.Abort();
            _driver.Quit();
        }

        private void RunSelenium()
        {
            RunningSelenium = true;

            _driver = new ChromeDriver(_driverService, new ChromeOptions());

            _driver.Navigate().GoToUrl("https://accounts.spotify.com/en/login/");
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            Login();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            PlanPickerSelect();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            FillingOutPaymentMethod();

            ////if (!RunningSelenium) return;
            //PlanPickerSelect();
            //_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            ////if (!RunningSelenium) return;
            //FillingOutForm();
            //MessageBox.Show("Test");
            ////if (!RunningSelenium) return;
            //FillingOutPaymentMethod();
        }

        private bool GetUserCredits()
        {
            if (emaiTextBox.Text != "")
            {
                EmailUser = emaiTextBox.Text;
            }
            else
            {
                var result = MessageBox.Show("Do you want enter a email?", "Email!", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes) return true;
            }

            if (creditCardNumberTextBox.Text != "")
            {
                CardNumberPrefix = creditCardNumberTextBox.Text;
            }
            else
            {
                MessageBox.Show("Enter a card number!", "Card Number!");
                return true;
            }

            return false;
        }

        private void Login()
        {
            _driver.FindElement(By.Id("login-username")).Clear();
            _driver.FindElement(By.Id("login-username")).SendKeys(EmailUser);
            _driver.FindElement(By.Id("login-password")).Clear();
            _driver.FindElement(By.Id("login-password")).SendKeys(PassUser);
            _driver.FindElement(By.Id("login-button")).Click();
        }

        private void PlanPickerSelect()
        {
            //MessageBox.Show("Login succesfull!");

            bool wantAgain = true;
            while (wantAgain && RunningSelenium)
            {
                try
                {
                    _driver.Navigate().GoToUrl("https://www.spotify.com/us/purchase/offer/2022-midyear-trial-3m/?marketing-campaign-id=default");
                    _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                    _driver.FindElement(By.Id("address-street")).Clear();
                    _driver.FindElement(By.Id("address-street")).SendKeys("Street 101");

                    _driver.FindElement(By.Id("address-city")).Clear();
                    _driver.FindElement(By.Id("address-city")).SendKeys("New York");

                    // select the drop down list
                    var education = _driver.FindElement(By.Id("address-state"));
                    //create select element object 
                    var selectElement = new SelectElement(education);

                    //select by value
                    selectElement.SelectByValue("NY");

                    _driver.FindElement(By.Id("address-postal_code_short")).Clear();
                    _driver.FindElement(By.Id("address-postal_code_short")).SendKeys("10080");

                    wantAgain = false;
                }
                catch
                {
                    var result = MessageBox.Show("Error! Try again?", "MS", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.No) wantAgain = false;
                }
            }

            _driver.FindElement(By.XPath(@"/html/body/div[3]/div[3]/div/div[2]/button")).Click();

            var element = _driver.FindElement(By.XPath(@"/html/body/div[1]/main/div/div/section[2]/div/div/ul/li[1]/a"));
            Actions actions = new Actions(_driver);
            actions.MoveToElement(element);
            actions.Perform();

            _driver.FindElement(By.XPath(@"/html/body/div[1]/main/div/div/section[2]/div/div/ul/li[1]/a")).Click();

        }

        private void FillingOutPaymentMethod()
        {
            var cardNumberGenerator = new CreditCardNumberGenerator(CardNumberPrefix);
            Random rnd = new Random();

            int indexOfList = 0;
            cardNumberGenerator.GetCreditCardNumbers(50);

            while (true)
            {

                try
                {
                    _driver.SwitchTo().Frame(_driver.FindElement(By.XPath(@"/html/body/div[1]/main/div/div/section[2]/div/form/div[1]/div[1]/div[2]/iframe"))); // change iframe

                    _driver.FindElement(By.Id("cardnumber")).Clear();
                    _driver.FindElement(By.Id("cardnumber")).SendKeys(cardNumberGenerator.CardNumberFoundList[indexOfList]);
                    indexOfList++;
                    //_driver.SwitchTo().DefaultContent();

                    _driver.FindElement(By.Id("expiry-date")).Clear();
                    _driver.FindElement(By.Id("expiry-date")).SendKeys("0824");

                    //_driver.SwitchTo().Frame(_driver.FindElement(By.XPath(@"/html/body/div[1]/div/div[3]/div/div/div/div[2]/div/div[1]/div/form/div[3]/div[2]/div[1]/div[1]/iframe"))); // change iframe

                    _driver.FindElement(By.Id("security-code")).Clear();
                    _driver.FindElement(By.Id("security-code")).SendKeys("123");

                    _driver.SwitchTo().DefaultContent();


                    _driver.FindElement(By.Id("checkout_submit")).Click();
                    _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                    var ele = _driver.FindElement(By.Id("checkout_submit"));
                    _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                }
                catch
                {
                    Thread.Sleep(5000);
                    continue;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Trial_Everywhere
{
    /// <summary>
    /// Interaction logic for HboMax.xaml
    /// </summary>
    public partial class HboMax : Window
    {
        public HboMax(bool runningSelenium)
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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

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

            _driver.Navigate().GoToUrl("https://www.hbomax.com/subscribe/plan-picker");
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            //if (!RunningSelenium) return;
            PlanPickerSelect();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            //if (!RunningSelenium) return;
            FillingOutForm();
            MessageBox.Show("Test");
            //if (!RunningSelenium) return;
            FillingOutPaymentMethod();
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

        private void PlanPickerSelect()
        {
            try
            {
            _driver.FindElement(By.Id("onetrust-accept-btn-handler")).Click();
            }
            catch
            {

            }

            //_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            bool wantAgain = true;
            while (wantAgain && RunningSelenium)
            {
                try
                {
                    _driver.FindElement(By.XPath(@"/html/body/div[1]/div/div[2]/div/div/div[2]/div/div[2]/div/div/div/div[3]/div")).Click();
                    wantAgain = false;
                }
                catch (Exception)
                {
                    if (RunningSelenium)
                    {
                        var result = MessageBox.Show("I did not find a button! Do you want to try again?", "HBO MAX",
                            MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.No) wantAgain = false;
                    }
                }
            }
        }

        private void FillingOutForm()
        {
            _driver.FindElement(By.Id("firstName")).Clear();
            _driver.FindElement(By.Id("firstName")).SendKeys(FirstNameUser);
            _driver.FindElement(By.Id("lastName")).Clear();
            _driver.FindElement(By.Id("lastName")).SendKeys(LastNameUser);
            _driver.FindElement(By.Id("email")).Clear();
            _driver.FindElement(By.Id("email")).SendKeys(EmailUser);
            _driver.FindElement(By.Id("password")).Clear();
            _driver.FindElement(By.Id("password")).SendKeys(PassUser);

            //_driver.FindElement(By.XPath(@"/html/body/div[1]/div/div[3]/div/div/form/div[4]/div[2]/button")).Click(); // Clicking the button to confirm the entered data
        }

        private void FillingOutPaymentMethod()
        {
            var cardNumberGenerator = new CreditCardNumberGenerator(CardNumberPrefix);
            Random rnd = new Random();

            cardNumberGenerator.GetCreditCardNumbers(10);

            try
            {
            _driver.FindElement(By.XPath(@"/html/body/div[1]/div/div[3]/div/div/div/div[2]/div/div[1]/label")).Click(); // click option of paymant
            }
            catch
            {

            }

            _driver.FindElement(By.Id("cardHolderNameField")).Clear();
            _driver.FindElement(By.Id("cardHolderNameField")).SendKeys($"{FirstNameUser} {LastNameUser}");

            _driver.SwitchTo().Frame(_driver.FindElement(By.XPath(@"/html/body/div[1]/div/div[3]/div/div/div/div[2]/div/div[1]/div/form/div[2]/div[1]/div/iframe"))); // change iframe

            _driver.FindElement(By.Id("card_number")).Clear();
            _driver.FindElement(By.Id("card_number")).SendKeys(cardNumberGenerator.CardNumberFoundList[0]);
            _driver.SwitchTo().DefaultContent();

            _driver.FindElement(By.Id("cardExpiryDateField")).Clear();
            _driver.FindElement(By.Id("cardExpiryDateField")).SendKeys("0824");

            _driver.SwitchTo().Frame(_driver.FindElement(By.XPath(@"/html/body/div[1]/div/div[3]/div/div/div/div[2]/div/div[1]/div/form/div[3]/div[2]/div[1]/div[1]/iframe"))); // change iframe

            _driver.FindElement(By.Id("cvv")).Clear();
            _driver.FindElement(By.Id("cvv")).SendKeys("123");

            _driver.SwitchTo().DefaultContent();


            _driver.FindElement(By.Id("savePaymentMethod")).Click();
        }
    }
}

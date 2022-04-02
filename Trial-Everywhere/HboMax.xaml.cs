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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void OpenSelenium(object sender, RoutedEventArgs e)
        {
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
            Thread exiThread = new Thread(CloseSeleniumThread);
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

            if (!RunningSelenium) return;
            PlanPickerSelect();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            if (!RunningSelenium) return;
            FillingOutForm();
        }

        private bool GetUserCredits()
        {
            if (emailLabel.Text != "")
            {
                EmailUser = emailLabel.Text;
                return false;
            }

            var result = MessageBox.Show("Do you want enter a email?", "Email!", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes) return true;
            return false;
        }

        private void PlanPickerSelect()
        {
            _driver.FindElement(By.Id("onetrust-accept-btn-handler")).Click();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            bool wantAgain = true;
            while (wantAgain && RunningSelenium)
            {
                try
                {
                    _driver.FindElement(By.XPath(@"/html/body/div[1]/div/div[2]/div/div/div/div[1]/div[2]/button")).Click();
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

            _driver.FindElement(By.XPath(@"/html/body/div[1]/div/div[3]/div/div/form/div[4]/div[2]/button")).Click(); // Clicking the button to confirm the entered data
        }

    }
}

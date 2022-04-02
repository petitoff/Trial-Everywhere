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
        public HboMax()
        {
            InitializeComponent();
        }

        ChromeDriverService driverService = ChromeDriverService.CreateDefaultService(); // Create chrome (selenium) settings
        private ChromeDriver driver;

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var runSeleniumThread = new Thread(RunSelenium); // create thread with selenium
            driverService.HideCommandPromptWindow = true; // Disabling cmd when run selenium

            runSeleniumThread.Start();
        }

        private void RunSelenium()
        {
            driver = new ChromeDriver(driverService, new ChromeOptions());

            driver.Navigate().GoToUrl("https://www.hbomax.com/subscribe/plan-picker");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            PlanPickerSelect();
<<<<<<< Updated upstream
            
        }

=======

        }

>>>>>>> Stashed changes
        private void PlanPickerSelect()
        {
            driver.FindElement(By.Id("onetrust-accept-btn-handler")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
<<<<<<< Updated upstream
            driver.FindElement(By.XPath(@"/html/body/div[1]/div/div[2]/div/div/div/div[1]/div[2]/button")).Click();
=======

            bool wantAgain = true;

            while(wantAgain)
            try
            {
                driver.FindElement(By.XPath(@"/html/body/div[1]/div/div[2]/div/div/div/div[1]/div[2]")).Click();
            }
            catch(Exception e)
            {
                
            }
>>>>>>> Stashed changes
        }

        private void CloseSelenium(object sender, RoutedEventArgs e)
        {
            driver.Quit();
        }
    }
}

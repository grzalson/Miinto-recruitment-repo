
using MIINTOrecruitment.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MIINTOrecruitment.Services
{

    public class FormSubmissionService : IFormSubmissionService
    {
        private readonly string filePath = @"Resources\index.html";
        public async Task SubmitForm(Order order)
        {
            using var driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl($"file:///{Path.GetFullPath(filePath)}");

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                var orderIdElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("orderId")));
                orderIdElement.SendKeys(order.order_id.ToString());

                var orderDateElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("orderDate")));
                orderDateElement.SendKeys(order.order_date.ToString());

                var orderStatusElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("orderStatus")));
                var statusSelectElement = new SelectElement(orderStatusElement);
                statusSelectElement.SelectByText(order.order_status);

                var submitButtonElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Submit']")));
                submitButtonElement.Click();

                wait.Until(ExpectedConditions.UrlContains($"orderId={order.order_id}" +
                    $"&orderDate={order.order_date.ToString("yyyy-MM-dd")}" +
                    $"&orderStatus={order.order_status.ToLower()}"));
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}

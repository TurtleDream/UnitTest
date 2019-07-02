using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Windows.Forms;

namespace MyLibrary
{
    public class MyClass
    {
		private readonly IWebDriver driver;
		private readonly Logger logger = LogManager.GetCurrentClassLogger();
		WebDriverWait wait;

		public MyClass(IWebDriver driver, WebDriverWait wait, Logger logger)
		{
			this.driver = driver;
			this.logger = logger;
			this.wait = wait;
		}

		public void Clicker(IWebElement webElement)
		{
			try
			{
				wait.Until(ExpectedConditions.ElementToBeClickable(webElement));
				logger.Info("Нажатие на элемент " + webElement.Text);
				webElement.Click();
			}
			catch (Exception exception)
			{
				logger.Error("Ошибка " + exception);
				MessageBox.Show("Error " + exception);
			}

		}

		public void Sender(IWebElement webElement, string startingPrice)
		{
			try
			{
				webElement.SendKeys(startingPrice);
				logger.Info("Ввод в поле " + webElement.Text + " значения " + startingPrice);
			}
			catch (Exception exception)
			{
				logger.Error("Ошибка " + exception);
				MessageBox.Show("Error " + exception);
			}
		}

		public bool check(By by)
		{
			try
			{
				driver.FindElement(by);
				return true;
			}
			catch (NoSuchElementException)
			{
				return false;
			}
		}
	}
}

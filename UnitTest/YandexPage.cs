using MyLibrary;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace UnitTest
{
	public class YandexPage
	{
		private readonly IWebDriver driver;
		private readonly string url = "https://yandex.ru/";
		private readonly Logger logger = LogManager.GetCurrentClassLogger();
		WebDriverWait wait;
		MyClass myClass;

		[System.Obsolete]
		public YandexPage(IWebDriver driver)
		{
			this.driver = driver;
			this.driver.Manage().Window.Maximize();
			this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			PageFactory.InitElements(driver, this);
			logger.Info("Браузер запущен");
			myClass = new MyClass(driver, wait, logger);
		}

		/// <summary>
		/// Кнопка перехода в яндекс маркет
		/// </summary>
		[FindsBy(How = How.PartialLinkText, Using = "Маркет")]
		public IWebElement MarketButton { get; set; }

		
		public void Navigate()
		{
			driver.Navigate().GoToUrl(url);
			logger.Info("Переход по ссылке " + url);
		}

		public void GoToMarket()
		{
			myClass.Clicker(MarketButton);
		}
	}
}
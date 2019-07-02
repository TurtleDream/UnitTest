using MyLibrary;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace UnitTest
{
	class YandexMarketPage
	{
		private readonly Logger logger = LogManager.GetCurrentClassLogger();
		WebDriverWait wait;
		MyClass myClass;

		public YandexMarketPage(IWebDriver driver)
		{
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			PageFactory.InitElements(driver, this);
			myClass = new MyClass(driver, wait, logger);
		}

		/// <summary>
		/// Кнопка перехода в раздел Электроника
		/// </summary>
		[FindsBy(How = How.PartialLinkText, Using = "Электроника")]
		public IWebElement ElectronicsButton { get; set; }

		public void GoToElectronics()
		{
			myClass.Clicker(ElectronicsButton);
		}
	}
}

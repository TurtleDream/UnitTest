using MyLibrary;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace UnitTest
{
	class ElectronicsPage
	{
		private readonly Logger logger = LogManager.GetCurrentClassLogger();
		WebDriverWait wait;
		MyClass myClass;

		public ElectronicsPage(IWebDriver driver)
		{
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			PageFactory.InitElements(driver, this);
			myClass = new MyClass(driver, wait, logger);
		}

		/// <summary>
		/// Кнопка перехода в раздел Телевизоры
		/// </summary>
		[FindsBy(How = How.PartialLinkText, Using = "Телевизоры")]
		public IWebElement TVsButton { get; set; }

		/// <summary>
		/// Кнопка перехода в раздел Наушники
		/// </summary>
		[FindsBy(How = How.PartialLinkText, Using = "Наушники")]
		public IWebElement HeadphonesButton { get; set; }

		public void GoToTVs()
		{
			myClass.Clicker(TVsButton);
		}
		public void GoToHeadphones()
		{
			myClass.Clicker(HeadphonesButton);
		}
	}
}

using MyLibrary;
using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace UnitTest
{
	class ProductPage
	{
		private readonly IWebDriver driver;
		private readonly Logger logger = LogManager.GetCurrentClassLogger();
		WebDriverWait wait;
		MyClass myClass;

		public ProductPage(IWebDriver driver)
		{
			this.driver = driver;
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			PageFactory.InitElements(driver, this);
			myClass = new MyClass(driver, wait, logger);
		}

		/// <summary>
		/// Название товара, найденого с помощью поисковой строки
		/// </summary>
		[FindsBy(How = How.CssSelector, Using = "[class='n-title__text']")]
		public IWebElement FinalTitle { get; set; }

		public void VolidateResults(string expected)
		{
			string resultTitle = FinalTitle.GetAttribute("textContent");
			resultTitle = resultTitle.Substring(resultTitle.IndexOf(' ') + 1);
			logger.Info("Получено имя найденного продукта - " + resultTitle);

			Assert.AreEqual(expected, resultTitle, "Ошибка теста! Ожидалось - " + expected + ". Получено - " + resultTitle);
		}
	}
}

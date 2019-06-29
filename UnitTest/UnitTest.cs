using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UnitTest
{
	[TestClass]
	public class UnitTest
	{
		public IWebDriver Driver { get; set; }

		[TestInitialize]
		public void SetupTest()
		{
			Driver = new ChromeDriver();
		}

		[TestCleanup]
		public void TeardownTest()
		{
			this.Driver.Quit();
		}

		[TestMethod]
		[System.Obsolete]
		public void TestCase1()
		{
			YandexMarketPage yandexMarketPage = new YandexMarketPage(Driver);
			string section = "Телевизоры";
			string startingPrice = "20000";
			string[] manufacturersList =
			{
				"Samsung",
				"LG"
			};

			yandexMarketPage.Navigate();
			yandexMarketPage.Paragraph1();
			yandexMarketPage.Paragraph2(section, startingPrice, manufacturersList);

			string resultTitle = yandexMarketPage.Paragraph3();

			yandexMarketPage.VolidateResults(resultTitle);
		}

		[TestMethod]
		[System.Obsolete]
		public void TestCase2()
		{
			YandexMarketPage yandexMarketPage = new YandexMarketPage(Driver);
			string section = "Наушники";
			string startingPrice = "5000";
			string[] manufacturersList =
			{
				"Beats"
			};

			yandexMarketPage.Navigate();
			yandexMarketPage.Paragraph1();
			yandexMarketPage.Paragraph2(section, startingPrice, manufacturersList);

			string resultTitle = yandexMarketPage.Paragraph3();

			yandexMarketPage.VolidateResults(resultTitle);
		}
	}
}
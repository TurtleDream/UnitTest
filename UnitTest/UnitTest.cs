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
			Driver.Quit();
		}

		[TestMethod]
		[System.Obsolete]
		public void TestCase1()
		{
			YandexPage yandexPage = new YandexPage(Driver);
			YandexMarketPage yandexMarketPage = new YandexMarketPage(Driver);
			ElectronicsPage electronicsPage = new ElectronicsPage(Driver);
			CategoryPage categoryPage = new CategoryPage(Driver);
			FiltersPage filtersPage = new FiltersPage(Driver);
			ProductPage productPage = new ProductPage(Driver);
			string startingPrice = "20000";
			string[] manufacturersList =
			{
				"Samsung",
				"LG"
			};

			yandexPage.Navigate();
			yandexPage.GoToMarket();

			yandexMarketPage.GoToElectronics();

			electronicsPage.GoToTVs();

			categoryPage.GoToFilters();

			filtersPage.SettingTheInitialPrice(startingPrice);
			filtersPage.SettingTheManufacturers(manufacturersList);
			filtersPage.ApplyFilters();

			categoryPage.CheckListbox();
			categoryPage.CheckRadioButton();
			string result = categoryPage.GetTitle();
			productPage.VolidateResults(result);
		}

		[TestMethod]
		[System.Obsolete]
		public void TestCase2()
		{
			YandexPage yandexPage = new YandexPage(Driver);
			YandexMarketPage yandexMarketPage = new YandexMarketPage(Driver);
			ElectronicsPage electronicsPage = new ElectronicsPage(Driver);
			CategoryPage categoryPage = new CategoryPage(Driver);
			FiltersPage filtersPage = new FiltersPage(Driver);
			ProductPage productPage = new ProductPage(Driver);
			string startingPrice = "5000";
			string[] manufacturersList =
			{
				"Beats"
			};

			yandexPage.Navigate();
			yandexPage.GoToMarket();

			yandexMarketPage.GoToElectronics();

			electronicsPage.GoToHeadphones();

			categoryPage.GoToFilters();

			filtersPage.SettingTheInitialPrice(startingPrice);
			filtersPage.SettingTheManufacturers(manufacturersList);
			filtersPage.ApplyFilters();

			categoryPage.CheckListbox();
			categoryPage.CheckRadioButton();
			string result = categoryPage.GetTitle();
			productPage.VolidateResults(result);
		}
	}
}
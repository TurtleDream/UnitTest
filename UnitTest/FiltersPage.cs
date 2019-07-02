using MyLibrary;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace UnitTest
{
	class FiltersPage
	{
		private readonly IWebDriver driver;
		private readonly Logger logger = LogManager.GetCurrentClassLogger();
		WebDriverWait wait;
		MyClass myClass;

		public FiltersPage(IWebDriver driver)
		{
			this.driver = driver;
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			PageFactory.InitElements(driver, this);
			myClass = new MyClass(driver, wait, logger);
		}

		/// <summary>
		/// Поле ввода начальной цены
		/// </summary>
		[FindsBy(How = How.Id, Using = "glf-pricefrom-var")]
		public IWebElement InputButton { get; set; }

		/// <summary>
		/// Кнопка применения фильтров
		/// </summary>
		[FindsBy(How = How.LinkText, Using = "Показать подходящие")]
		public IWebElement ApplyButton { get; set; }

		public void SettingTheInitialPrice(string startingPrice)
		{
			myClass.Sender(InputButton, startingPrice);
		}

		public void SettingTheManufacturers(string[] manufacturersList)
		{
			foreach (string manufacturer in manufacturersList)
			{
				myClass.Clicker(driver.FindElement(By.LinkText(manufacturer)));
				logger.Info("Выбор производителя " + manufacturer);
			}
		}

		public void ApplyFilters()
		{
			myClass.Clicker(ApplyButton);
		}
	}
}

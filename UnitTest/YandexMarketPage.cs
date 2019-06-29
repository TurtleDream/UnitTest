using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace UnitTest
{
	public class YandexMarketPage
	{
		private readonly IWebDriver driver;
		private readonly string url = "https://yandex.ru/";
		private readonly Logger logger = LogManager.GetCurrentClassLogger();
		WebDriverWait wait;

		[System.Obsolete]
		public YandexMarketPage(IWebDriver driver)
		{
			this.driver = driver;
			this.driver.Manage().Window.Maximize();
			this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			PageFactory.InitElements(driver, this);
			logger.Info("Браузер запущен");
		}

		/// <summary>
		/// Кнопка перехода в яндекс маркет
		/// </summary>
		[FindsBy(How = How.PartialLinkText, Using = "Маркет")]
		public IWebElement MarketButton { get; set; }

		/// <summary>
		/// Кнопка перехода в раздел Электроника
		/// </summary>
		[FindsBy(How = How.PartialLinkText, Using = "Электроника")]
		public IWebElement ElectronicsButton { get; set; }

		/// <summary>
		/// Кнопка перехода в Расширеный фильтр
		/// </summary>
		[FindsBy(How = How.CssSelector, Using = "[class='_28j8Lq95ZZ']")]
		public IWebElement FiltersButton { get; set; }

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

		/// <summary>
		/// Кнопка показа 12-ти элементов
		/// </summary>
		[FindsBy(How = How.ClassName, Using = "select__item")]
		public IWebElement ShowBy12Button { get; set; }

		/// <summary>
		/// Поисковая строка
		/// </summary>
		[FindsBy(How = How.Id, Using = "header-search")]
		public IWebElement SearchString { get; set; }

		/// <summary>
		/// Название товара, найденого с помощью поисковой строки
		/// </summary>
		[FindsBy(How = How.CssSelector, Using = "[class='n-title__text']")]
		public IWebElement FinalTitle { get; set; }

		public void Navigate()
		{
			driver.Navigate().GoToUrl(url);
			logger.Info("Переход по ссылке " + url);
		}

		public void Paragraph1()
		{
			Clicker(MarketButton);
			Clicker(ElectronicsButton);
		}

		public void Paragraph2(string section, string startingPrice, string[] manufacturersList)
		{
			Clicker(driver.FindElement(By.PartialLinkText(section)));
			Clicker(FiltersButton);
			Sender(InputButton, startingPrice);

			foreach (string manufacturer in manufacturersList)
			{
				Clicker(driver.FindElement(By.LinkText(manufacturer)));
				logger.Info("Выбор производителя " + manufacturer);
			}

			Clicker(ApplyButton);
		}

		[Obsolete]
		public string Paragraph3()
		{
			if (check(By.CssSelector("[role='listbox']")))
			{
				if (!driver.FindElement(By.CssSelector("[role='listbox']")).GetAttribute("textContent").Contains("12"))
				{
					Clicker(driver.FindElement(By.CssSelector("[role='listbox']")));
					Clicker(ShowBy12Button);
				}
			}

			if(check(By.CssSelector("[class='radio-button__radio radio-button__radio_side_right']")))
			{
				Clicker(driver.FindElement(By.CssSelector("[class='radio-button__radio radio-button__radio_side_right']")));
			}

			wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("n-snippet-card2__title")));
			string firstElementTitle = driver.FindElements(By.ClassName("n-snippet-card2__title")).ElementAt(0).GetAttribute("textContent");
			firstElementTitle = firstElementTitle.Substring(firstElementTitle.IndexOf(' ') + 1);
			logger.Info("Получен первый элемент списка - " + firstElementTitle);

			Sender(SearchString, firstElementTitle);
			wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText(firstElementTitle.ToLower())));
			Clicker(driver.FindElement(By.LinkText(firstElementTitle.ToLower())));

			return firstElementTitle;
		}

		public void VolidateResults(string expected)
		{
			string resultTitle = FinalTitle.GetAttribute("textContent");
			resultTitle = resultTitle.Substring(resultTitle.IndexOf(' ') + 1);
			logger.Info("Получено имя найденного продукта - " + resultTitle);

			Assert.AreEqual(expected, resultTitle, "Ошибка теста! Ожидалось - " + expected + ". Получено - " + resultTitle);
		}

		private void Clicker(IWebElement webElement)
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
			}
			
		}

		private void Sender(IWebElement webElement, string startingPrice)
		{
			try
			{
				webElement.SendKeys(startingPrice);
				logger.Info("Ввод в поле " + webElement.Text + " значения " + startingPrice);
			}
			catch (Exception exception)
			{
				logger.Error("Ошибка " + exception);
			}
		}

		private bool check(By by)
		{
			try
			{
				driver.FindElement( by );
				return true;
			}
			catch (NoSuchElementException)
			{
				return false;
			}
		}
	}
}
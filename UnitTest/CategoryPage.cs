using MyLibrary;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace UnitTest
{
	class CategoryPage
	{
		private readonly IWebDriver driver;
		private readonly Logger logger = LogManager.GetCurrentClassLogger();
		WebDriverWait wait;
		MyClass myClass;

		public CategoryPage(IWebDriver driver)
		{
			this.driver = driver;
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			PageFactory.InitElements(driver, this);
			myClass = new MyClass(driver, wait, logger);
		}

		/// <summary>
		/// Кнопка перехода в Расширеный фильтр
		/// </summary>
		[FindsBy(How = How.CssSelector, Using = "[class='_28j8Lq95ZZ']")]
		public IWebElement FiltersButton { get; set; }

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

		public void GoToFilters()
		{
			myClass.Clicker(FiltersButton);
		}

		public void CheckListbox()
		{
			if (myClass.check(By.CssSelector("[role='listbox']")))
			{
				if (!driver.FindElement(By.CssSelector("[role='listbox']")).GetAttribute("textContent").Contains("12"))
				{
					myClass.Clicker(driver.FindElement(By.CssSelector("[role='listbox']")));
					myClass.Clicker(ShowBy12Button);
				}
			}
		}

		public void CheckRadioButton()
		{
			if (myClass.check(By.CssSelector("[class='radio-button__radio radio-button__radio_side_right']")))
			{
				myClass.Clicker(driver.FindElement(By.CssSelector("[class='radio-button__radio radio-button__radio_side_right']")));
			}
		}

		[Obsolete]
		public string GetTitle()
		{
			wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("n-snippet-card2__title")));
			string firstElementTitle = driver.FindElements(By.ClassName("n-snippet-card2__title")).ElementAt(0).GetAttribute("textContent");
			firstElementTitle = firstElementTitle.Substring(firstElementTitle.IndexOf(' ') + 1);
			logger.Info("Получен первый элемент списка - " + firstElementTitle);

			myClass.Sender(SearchString, firstElementTitle);
			wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText(firstElementTitle.ToLower())));
			myClass.Clicker(driver.FindElement(By.LinkText(firstElementTitle.ToLower())));

			return firstElementTitle;
		}
	}
}

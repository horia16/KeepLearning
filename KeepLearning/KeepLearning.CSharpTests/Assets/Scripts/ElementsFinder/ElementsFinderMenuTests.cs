using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using UnityEngine;

namespace Tests
{
    [TestClass()]
    public class ElementsFinderMenuTests
    {
        [TestMethod()]
        public void CanChooseTest()
        {
            CategoriesInfoContainer container = CategoryInfo.LoadCategoriesInfo(Path.Combine(Environment.CurrentDirectory, "CategoryInfoTest.xml"));
            container.GetSubcategoriesElements();
            Assert.IsTrue(ElementsFinderMenu.CanChooseCategory(container.Categories[0].Subcategories[0]));
        }
    }
}
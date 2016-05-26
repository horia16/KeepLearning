using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KeepLearning;
using System.Xml.Serialization;
using System.IO;

namespace KeepLearningTests
{
    [TestClass]
    public class CategoryInfoTest
    {
        [TestMethod]
        public void TestClass()
        {
            CategoriesInfoContainer container = CategoryInfo.LoadCategoriesInfo(Path.Combine(Environment.CurrentDirectory, "CategoryInfoTest.xml"));

            Assert.IsTrue(container.Categories[0].Subcategories.Count == 2);
            Assert.IsTrue(container.Categories[0].Subcategories[0].Subcategories.Count == 2);
        }
        
        [TestMethod]
        public void SetBaseCategories()
        {
            CategoriesInfoContainer container = CategoryInfo.LoadCategoriesInfo(Path.Combine(Environment.CurrentDirectory, "CategoryInfoTest.xml"));
            container.SetBaseCategories();

            Assert.AreSame(container.Categories[0].Subcategories[0].BaseCategory, container.Categories[0]);
        }
    }
}

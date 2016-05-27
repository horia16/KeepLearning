using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeepLearning.Tests
{
    [TestClass]
    public class CategoryTests
    {
        [TestMethod()]
        public void GetSubcategoriesElementsTest1()
        {
            Category tester = new Category();

            Category subcategory1 = new Category();
            subcategory1.Words = new List<string>() { "paa", "daa" };

            Category subcategory2 = new Category();
            subcategory2.Words = new List<string>() { "subred", "tatar" };
            subcategory2.Subcategories = new List<Category>() { subcategory1 };

            tester.Subcategories = new List<Category>() { subcategory1, subcategory2 };
            tester.GetSubcategoriesElements();

            Assert.AreEqual(tester.Words.Count, 6);
        }

        [TestMethod()]
        public void GetSubcategoriesElementsTest2()
        {
            CategoriesContainer container = LoadCategoryTestXml();

            foreach (Category category in container.Categories)
                category.GetSubcategoriesElements();

            Assert.IsTrue(container.Categories[0].Words.Count == 12);
            Assert.IsTrue(container.Categories[0].Subcategories[0].Words.Count == 6);

            Assert.IsTrue(container.Categories[0].Images.Count == 6);
            Assert.IsTrue(container.Categories[0].Subcategories[0].Images.Count == 4);
        }

        private CategoriesContainer LoadCategoryTestXml()
        {
            var serializer = new XmlSerializer(typeof(CategoriesContainer));
            var stream = new FileStream(Path.Combine(Environment.CurrentDirectory, "CategoryTest.xml"), FileMode.Open);

            CategoriesContainer container = (CategoriesContainer)serializer.Deserialize(stream);
            stream.Close();

            return container;
        }

        [TestMethod()]
        public void GetCategoryTest()
        {
            CategoryInfo info = new CategoryInfo();
            info.Name = "Mate";
            string path = Path.Combine(Environment.CurrentDirectory, "CategoryTest.xml");
            Category category = Category.GetCategory(info, path);
            if (category == null)
                Assert.Fail();
            if (category.Name != info.Name)
                Assert.Fail();
        }


    }
}
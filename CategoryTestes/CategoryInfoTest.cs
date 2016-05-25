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
            CategoriesInfoContainer container = LoadCategoryInfoTestXml();

            Assert.IsTrue(container.Categories[0].Subcategories.Count == 2);
            Assert.IsTrue(container.Categories[0].Subcategories[0].Subcategories.Count == 2);
        }

        private CategoriesInfoContainer LoadCategoryInfoTestXml()
        {
            var serializer = new XmlSerializer(typeof(CategoriesInfoContainer));
            var stream = new FileStream(Path.Combine(Environment.CurrentDirectory, "CategoryInfoTest.xml"), FileMode.Open);

            CategoriesInfoContainer container = (CategoriesInfoContainer)serializer.Deserialize(stream);
            stream.Close();

            return container;
        }
    }
}

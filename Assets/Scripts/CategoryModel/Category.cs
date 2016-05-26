using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace KeepLearning
{
    public class Category
    {
        [XmlAttribute("name")]
        public string Name;

        [XmlArray("Subcategories")]
        [XmlArrayItem("Category")]
        public List<Category> Subcategories = new List<Category>();

        [XmlArray("Words")]
        [XmlArrayItem("Word")]
        public List<string> Words = new List<string>();

        [XmlArray("Images")]
        [XmlArrayItem("Image")]
        public List<string> Images = new List<string>();
        
        public void GetSubcategoriesElements()
        {
            if (Subcategories == null)
                return;

            foreach (Category subcategory in Subcategories)
            {
                subcategory.GetSubcategoriesElements();

                Words.AddRange(subcategory.Words);
                Images.AddRange(subcategory.Images);
            }
        }

        public void DestroyUnusedSubcategories()
        {
            foreach (Category subcategory in Subcategories)
            {
                subcategory.Subcategories.Clear();
            }
        }

        public static Category GetCategory(CategoryInfo info, string path)
        {
            XDocument root = XDocument.Load(path);
            foreach (XElement element in root.Descendants("Category"))
                if (element.Attribute("name").Value == info.Name)
                    return GetCategory(element);
            return null;
        }

        public static Category GetCategory(XElement element)
        {
            var serializer = new XmlSerializer(typeof(Category));
            return (Category)serializer.Deserialize(element.CreateReader());
        }
    }
}

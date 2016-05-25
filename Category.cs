using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KeepLearning
{
    public class Category
    {
        public Category()
        {

        }

        public void GetSubcategoriesElements()
        {
            foreach(Category subcategory in Subcategories)
            {
                subcategory.GetSubcategoriesElements();

                Words.AddRange(subcategory.Words);
                ImagePaths.AddRange(ImagePaths);
            }
        }

        public void DestroyUnusedSubcategories()
        {
            foreach (Category subcategory in Subcategories)
            {
                subcategory.Subcategories.Clear();
            }
        }

        [XmlAttribute("name")]
        public string Name;

        [XmlArray("Subcategories")]
        [XmlArrayItem("Category")]
        IList<Category> Subcategories;

        [XmlArray("Words")]
        public List<string> Words;

        [XmlArray("ImagePaths")]
        public List<string> ImagePaths;
    }
}

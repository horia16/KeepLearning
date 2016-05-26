using System.Collections.Generic;
using System.Xml.Serialization;

namespace KeepLearning
{
    [XmlRoot("CategoriesInfoContainer")]
    public class CategoriesInfoContainer
    {
        [XmlArray("Categories")]
        [XmlArrayItem("Category")]
        public List<CategoryInfo> Categories = new List<CategoryInfo>();

        public void SetBaseCategories()
        {
            foreach(CategoryInfo category in Categories)
            {
                category.SetBaseCategories();
            }
        }
    }
}

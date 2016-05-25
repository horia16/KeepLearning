using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KeepLearning
{
    [XmlRoot("CategoriesContainer")]
    public class CategoriesContainer
    {
        [XmlArray("Categories")]
        [XmlArrayItem("Category")]
        public List<Category> Categories = new List<Category>();
    }
}

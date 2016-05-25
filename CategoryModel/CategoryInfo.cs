using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KeepLearning
{
    public class CategoryInfo
    {
        [XmlAttribute("name")]
        public string Name;

        [XmlArray("Subcategories")]
        [XmlArrayItem("Category")]
        public List<CategoryInfo> Subcategories = new List<CategoryInfo>();
    }
}

using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("CategoriesContainer")]
public class CategoriesContainer
{
    [XmlArray("Categories")]
    [XmlArrayItem("Category")]
    public List<Category> Categories = new List<Category>();
}
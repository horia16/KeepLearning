using System.Collections.Generic;
using System.Xml.Serialization;

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

    public void GetSubcategoriesElements()
    {
        foreach (CategoryInfo category in Categories)
            category.GetSubcategoriesElements();
    }
}
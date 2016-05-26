using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

public class CategoryInfo
{
    [XmlAttribute("name")]
    public string Name;

    [XmlArray("Subcategories")]
    [XmlArrayItem("Category")]
    public List<CategoryInfo> Subcategories = new List<CategoryInfo>();

    public CategoryInfo BaseCategory;

    public void SetBaseCategories()
    {
        foreach(CategoryInfo category in Subcategories)
        {
            category.BaseCategory = this;
            category.SetBaseCategories();
        }
    }
}

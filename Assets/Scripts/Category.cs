using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

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
}

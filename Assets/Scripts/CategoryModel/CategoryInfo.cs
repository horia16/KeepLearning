using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class CategoryInfo
{
    [XmlAttribute("name")]
    public string Name;

    [XmlArray("Subcategories")]
    [XmlArrayItem("Category")]
    public List<CategoryInfo> Subcategories = new List<CategoryInfo>();

    public int WordsCount;
    public int ImagesCount;

    public CategoryInfo BaseCategory;

    public void SetBaseCategories()
    {
        foreach(CategoryInfo category in Subcategories)
        {
            category.BaseCategory = this;
            category.SetBaseCategories();
        }
    }

    public void GetSubcategoriesElements()
    {
        if (Subcategories == null)
            return;

        foreach (CategoryInfo subcategory in Subcategories)
        {
            subcategory.GetSubcategoriesElements();

            WordsCount += subcategory.WordsCount;
            ImagesCount += subcategory.ImagesCount;
        }
    }

    public static CategoriesInfoContainer LoadCategoriesInfo(string path)
    {
        var serializer = new XmlSerializer(typeof(CategoriesInfoContainer));
        var stream = new FileStream(path, FileMode.Open);

        CategoriesInfoContainer container = (CategoriesInfoContainer)serializer.Deserialize(stream);
        stream.Close();

        return container;
    }
}

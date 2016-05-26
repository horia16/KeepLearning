using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;


public class MenuManager : MonoBehaviour
{

	public void CloseMenu(GameObject Object)
	{
	   	Object.SetActive(false);
	}

	public void OpenMenu(GameObject Object)
	{
		Object.SetActive (true);
	}

    public static CategoriesInfoContainer CategoriesInfoContainer;

    public void Awake()
    {
        CategoriesInfoContainer = LoadCategoriesInfo();
    }

    public void ChangeMenu(GameObject oldMenu, GameObject newMenu)
    {
        oldMenu.SetActive(false);
        newMenu.SetActive(true);
    }

    public static CategoriesInfoContainer LoadCategoriesInfo()
    {
        return LoadCategoriesInfo(Path.Combine(Environment.CurrentDirectory, "CategoriesInfo.xml"));
    }

    public static CategoriesInfoContainer LoadCategoriesInfo(string path)
    {
        var serializer = new XmlSerializer(typeof(CategoriesInfoContainer));
        var stream = new FileStream(path, FileMode.Open);

        CategoriesInfoContainer container = (CategoriesInfoContainer)serializer.Deserialize(stream);
        stream.Close();

        return container;
    }

	public static CategoriesContainer LoadCategories(string path)
	{
		var serializer = new XmlSerializer(typeof(CategoriesContainer));
		var stream = new FileStream(path, FileMode.Open);

		CategoriesContainer container = (CategoriesContainer)serializer.Deserialize(stream);
		stream.Close();

		return container;
	}
}
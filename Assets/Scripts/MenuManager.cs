using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace KeepLearning
{
    public class MenuManager : MonoBehaviour
    {
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
    }
}

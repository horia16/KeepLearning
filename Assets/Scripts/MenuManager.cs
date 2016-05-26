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
            return CategoryInfo.LoadCategoriesInfo(Path.Combine(Environment.CurrentDirectory, "CategoriesInfo.xml"));
        }
    }
}

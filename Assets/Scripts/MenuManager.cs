using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace KeepLearning
{
    public class MenuManager : MonoBehaviour
    {
		public GameObject elementRemoverCanvas;

        public static CategoriesInfoContainer CategoriesInfoContainer;

        public GameObject SelectGameMenu;
        public ElementsRemoverController ElementsRemoverController;

        string CategoryInfoXmlPath = Path.Combine(Environment.CurrentDirectory, "CategoryInfo.xml");
        string CategoryPathXmlPath = Path.Combine(Environment.CurrentDirectory, "Category.xml");

        public void Awake()
        {
            CategoriesInfoContainer = CategoryInfo.LoadCategoriesInfo(CategoryInfoXmlPath);
        }

        public void CloseMenu(GameObject Object)
        {
            Object.SetActive(false);
        }

        public void OpenMenu(GameObject Object)
        {
            Object.SetActive(true);
        }

        public void StartElementsRemoverSelecter(GameObject SelectorMenu)
        {
            OpenMenu(SelectorMenu);
            CategoriesInfoMenu menu = Instantiate(Resources.Load<GameObject>("Prefabs/CategoriesInfoMenu/CategoryInfoMenu")).GetComponent<CategoriesInfoMenu>();
            menu.transform.SetParent(SelectorMenu.transform);
            menu.SetContainer(CategoriesInfoContainer);
            menu.OnCategoryChoosed += (CategoryInfo info) => 
			{
				elementRemoverCanvas.gameObject.SetActive(true);
				ElementsRemoverController.StartGame(Category.GetCategory(info, CategoryPathXmlPath)); 
			};
            menu.OnBack += () => { SelectGameMenu.SetActive(true); };
        }
    }
}
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

        public GameObject CategorySelectorMenu;
        public GameObject GameSpace;
        public GameObject GameSelectorMenu;

        string CategoryInfoXmlPath = Path.Combine(Environment.CurrentDirectory, "CategoryInfo.xml");
        string CategoryPathXmlPath = Path.Combine(Environment.CurrentDirectory, "Category.xml");

        public void Awake()
        {
            CategoriesInfoContainer = CategoryInfo.LoadCategoriesInfo(CategoryInfoXmlPath);
            CategoriesInfoContainer.GetSubcategoriesElements();
            CategoriesInfoContainer.SetBaseCategories();

            ElementsFinderGame = Resources.Load<GameObject>("Prefabs/ElementsFinder/Game");
            ElementsFinderMenu = Resources.Load<GameObject>("Prefabs/ElementsFinder/Menu");
        }

        public void CloseMenu(GameObject Object)
        {
            Object.SetActive(false);
        }

        public void OpenMenu(GameObject Object)
        {
            Object.SetActive(true);
        }

        GameObject ElementsFinderGame;
        GameObject ElementsFinderMenu;

        public void ElementsFinderGameSelected()
        {
            OpenCategoryInfoMenu(ElementsFinderMenu, ElementsFinderGame);
        }

        public void OpenCategoryInfoMenu(GameObject menuPrefab, GameObject miniGamePrefab)
        {
            CloseMenu(GameSelectorMenu);
            OpenMenu(CategorySelectorMenu);
            CategoriesInfoMenu menu = Instantiate(menuPrefab).GetComponent<CategoriesInfoMenu>();

            menu.transform.SetParent(CategorySelectorMenu.transform, false);
            menu.SetContainer(CategoriesInfoContainer);

            menu.OnCategoryChoosed += (CategoryInfo info) => 
			{
                CloseMenu(CategorySelectorMenu);
                MiniGame miniGame = Instantiate(miniGamePrefab).GetComponent<MiniGame>();
				miniGame.StartGame(Category.GetCategory(info, CategoryPathXmlPath), GameSpace); 
			};

            menu.OnBack += () => { GameSelectorMenu.SetActive(true); };
            menu = null;
        }
    }
}
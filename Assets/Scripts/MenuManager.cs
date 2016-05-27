using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace KeepLearning
{
    public class MenuManager : MonoBehaviour
    {
		public GameObject elementRemoverCanvas;

        public static CategoriesInfoContainer CategoriesInfoContainer;

        public GameObject CategorySelectorMenu;
        public GameObject GameSpace;
        public GameObject GameSelectorMenu;
        public GameObject GamePanel;

        public MiniGame currentGame;

        string CategoryInfoXmlPath = Path.Combine(Environment.CurrentDirectory, "CategoryInfo.xml");
        string CategoryPathXmlPath = Path.Combine(Environment.CurrentDirectory, "Category.xml");

		int points;
		public List<Text> textPoints;

        public void Awake()
        {
			points = PlayerPrefs.GetInt("points");
			RefreshPoints ();
			
            CategoriesInfoContainer = CategoryInfo.LoadCategoriesInfo(CategoryInfoXmlPath);
            CategoriesInfoContainer.GetSubcategoriesElements();
            CategoriesInfoContainer.SetBaseCategories();

            ElementsFinderGame = Resources.Load<GameObject>("Prefabs/ElementsFinder/Game");
            ElementsFinderMenu = Resources.Load<GameObject>("Prefabs/ElementsFinder/Menu");

            ElementsRemoverGame = Resources.Load<GameObject>("Prefabs/ElementsRemover/Game");
            ElementsRemoverMenu = Resources.Load<GameObject>("Prefabs/ElementsRemover/Menu");

            SpeedFindingGame = Resources.Load<GameObject>("Prefabs/SpeedFinding/Game");
            SpeedFindingMenu = Resources.Load<GameObject>("Prefabs/SpeedFinding/Menu");
        }

		void RefreshPoints()
		{
			for (int i = 0; i < textPoints.Count; i++) {
				textPoints [i].text = "Score: " + points.ToString ();
			}
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

		GameObject ElementsRemoverGame;
		GameObject ElementsRemoverMenu;

		public void ElementsRemoverGameSelected()
		{
			OpenCategoryInfoMenu(ElementsRemoverMenu, ElementsRemoverGame);
		}
        
        GameObject SpeedFindingGame;
        GameObject SpeedFindingMenu;

        public void SpeedFindingGameSelected()
        {
            OpenCategoryInfoMenu(SpeedFindingMenu, SpeedFindingGame);
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
                GamePanel.SetActive(true);
                CloseMenu(CategorySelectorMenu);
                currentGame = Instantiate(miniGamePrefab).GetComponent<MiniGame>();
                currentGame.OnGameFinished += (int points) => { this.points+=points; PlayerPrefs.SetInt("points",points); RefreshPoints(); PlayerPrefs.Save();};
                currentGame.StartGame(Category.GetCategory(info, CategoryPathXmlPath), GameSpace); 
			};

            menu.OnBack += () => { GameSelectorMenu.SetActive(true); };
            menu = null;
        }
        

    }
}
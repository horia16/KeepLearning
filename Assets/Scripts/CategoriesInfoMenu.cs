using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace KeepLearning
{
    public partial class CategoriesInfoMenu : MonoBehaviour
    {
        CategoriesInfoContainer categoriesInfoContainer;
        CategoryInfo ActualCategory;

        public event Action<CategoryInfo> OnCategoryChoosed;
        public event Action OnBack;

        public virtual void SetContainer(CategoriesInfoContainer categoriesInfoContainer)
        {
            this.categoriesInfoContainer = categoriesInfoContainer;
            RefreshView();
        }

        public virtual void ChangeCategory(CategoryInfo newCategory)
        {
            ActualCategory = newCategory;
            RefreshView();
        }

        public virtual void CategoryChoosed()
        {
            if(OnCategoryChoosed != null)
                OnCategoryChoosed(ActualCategory);

            Destroy(gameObject);
        }

        public virtual void BackToBaseCategory()
        {
            if (ActualCategory == null)
            {
                if (OnBack != null)
                    OnBack();

                Destroy(gameObject);
            }
            else
                ChangeCategory(ActualCategory.BaseCategory);
        }

        public virtual bool CanChoose(CategoryInfo category)
        {
            return true;
        }
    }

    public partial class CategoriesInfoMenu : MonoBehaviour
    {
        // View
        public GameObject ButtonPrefab;
        public GameObject ContentPanel;
        public Text BackButtonText;
        public Button SelectButton;

        void Awake()
        {
            ContentPanel = transform.FindChild("ContentPanel").gameObject;
            ButtonPrefab = Resources.Load<GameObject>("Prefabs/CategoriesInfoMenu/Button");
            BackButtonText = transform.FindChild("BackButton").GetComponentInChildren<Text>();
            SelectButton = transform.FindChild("SelectButton").GetComponent<Button>();
        }

        public void RefreshView()
        {
            ClearContentPanel();
            AddNewButtons();
            ResetBackButton();
            ResetSelectButton();
        }

        internal virtual void ClearContentPanel()
        {
            foreach(Button button in ContentPanel.GetComponentsInChildren<Button>())
            {
                Destroy(button.gameObject);
            }
        }

        internal void AddNewButtons()
        {
            if (ActualCategory == null)
                AddNewButtons(categoriesInfoContainer.Categories);
            else
                AddNewButtons(ActualCategory.Subcategories);
        }
        
        internal void AddNewButtons(IList<CategoryInfo> categories)
        {
            foreach (CategoryInfo category in categories)
                if (CanChoose(category))
                    AddNewButton(category);
        }

        internal void AddNewButton(CategoryInfo category)
        {
            GameObject newButton = Instantiate(ButtonPrefab);
            newButton.GetComponentInChildren<Text>().text = category.Name;
            newButton.transform.SetParent(ContentPanel.transform);
            newButton.GetComponent<Button>().onClick.AddListener(delegate () { ChangeCategory(category); });
        }

        internal virtual void ResetBackButton()
        {
            if (ActualCategory == null)
                BackButtonText.text = "GamesMenu";
            else
                if (ActualCategory.BaseCategory == null)
                BackButtonText.text = "Principal Categories";
                else
                    BackButtonText.text = ActualCategory.BaseCategory.Name;
        }

        internal virtual void ResetSelectButton()
        {
            if (ActualCategory == null)
                SelectButton.enabled = false;
            else
                SelectButton.enabled = true;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ElementsFinderController
{
    public class ElementsFinderController : MiniGame
    {
        static int NumberOfCorectWords;
        static int NumberOfCorectImages;
        static int NumberOfWrongImages;
        static int NumberOfWrongWords;
        
        public Category CorectCategory;

        public GameObject ItemPrefab;
        public Transform GameSpace;

        public int CorectItemsCount;
        public int WrongItemsCount;

        public override void StartGame(Category domain)
        {
            Domain = domain;

            Domain.GetSubcategoriesElements();
            Domain.DestroyUnusedSubcategories();
            Initialize();
        }

        void Awake()
        {
            GameSpace = transform.FindChild("Game space");
            ItemPrefab = Resources.Load<GameObject>("Prefabs/ElementsFinder/GameItem");
        }

        void Initialize()
        {
            CorectCategory = CategoryRandomer.ChooseRandomSubcategory(Domain, CanBeCorectCategory);
            AddCorectItems();
            AddIncorectItems();
        }

        void AddCorectItems()
        {
            if (CorectCategory == null)
                throw new System.Exception("No category match!");

            AddImages(CategoryRandomer.ChooseItems(CorectCategory.Images, NumberOfCorectImages), true);
            AddWords(CategoryRandomer.ChooseItems(CorectCategory.Words, NumberOfCorectWords), true);

            GameItem.GameItemChoosed += ItemChoosed;
        }

        void AddIncorectItems()
        {
            AddWords(CategoryRandomer.GetRandomWords(Domain, NumberOfWrongWords,
                delegate (Category category)
                {
                    return category != CorectCategory;
                }), false);
            
            AddImages(CategoryRandomer.GetRandomImages(Domain, NumberOfWrongImages,
                delegate (Category category)
                {
                    return category != CorectCategory;
                }), false);
        }

        public static bool CanBeCorectCategory(Category category)
        {
            if (category.Words.Count >= NumberOfCorectWords && category.Images.Count >= NumberOfCorectImages)
                return true;
            else
                return false;
        }

        void AddWords(IList<string> words, bool isCorectWord)
        {
            foreach(string word in words)
            {
                GameItem item = Instantiate(ItemPrefab).GetComponent<GameItem>();
                item.IsCorectItem = isCorectWord;
                item.transform.SetParent(GameSpace);
                item.gameObject.GetComponentInChildren<Text>().text = word;
            }
        }

        void AddImages(IList<string> images, bool isCorectImage)
        {
            foreach (string image in images)
            {
                GameItem item = Instantiate(ItemPrefab).GetComponent<GameItem>();
                item.IsCorectItem = isCorectImage;
                item.transform.SetParent(GameSpace);
                item.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(image);
                item.gameObject.GetComponentInChildren<Text>().text = "";
            }
        }

        void ItemChoosed(GameItem item)
        {
            if(item.IsCorectItem)
            {
                CorectItemsCount++;
                if (CorectItemsCount == NumberOfCorectImages + NumberOfCorectWords)
                    GameFinished();
            }
            else
            {
                WrongItemsCount++;
                if (WrongItemsCount > 3)
                    GameFinished();
            }

            Destroy(item);
        }
        
        internal override void Refresh()
        {
            base.Refresh();
            foreach (GameItem item in GameSpace.gameObject.GetComponentsInChildren<GameItem>())
                Destroy(item);
        }

        internal override int GetPoints()
        {
            if (CorectItemsCount < NumberOfCorectImages + NumberOfCorectWords)
                return 0;
            else
                return CorectItemsCount;
        }
    }
}
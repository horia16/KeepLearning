using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ElementsFinderController
{
    public class ElementsFinderController : MonoBehaviour
    {
        static int NumberOfCorectWords;
        static int NumberOfCorectImages;
        static int NumberOfWrongImages;
        static int NumberOfWrongWords;

        public Category Domain;
        public Category CorectCategory;

        public GameObject WordPrefab, ImagePrefab;

        public void StartGame()
        {
            if (Domain == null)
                throw new System.Exception("Cannot start a game with no category!");

            Domain.GetSubcategoriesElements();
            Domain.DestroyUnusedSubcategories();
            Initialize();
        }

        void Initialize()
        {
            CorectCategory = CategoryRandomer.ChooseSubcategory(Domain, CanBeCorectCategory);
            AddCorectItems();
            AddIncorectItems();
        }

        void AddCorectItems()
        {
            if (CorectCategory == null)
                throw new System.Exception("No category match!");

            AddImages(CategoryRandomer.ChooseItems(CorectCategory.Images, NumberOfCorectImages), true);
            AddWords(CategoryRandomer.ChooseItems(CorectCategory.Words, NumberOfCorectWords), true);
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
                GameItem item = Instantiate(WordPrefab).GetComponent<GameItem>();
                item.IsCorectItem = isCorectWord;
                item.gameObject.GetComponent<Text>().text = word;
            }
        }

        void AddImages(IList<string> images, bool isCorectImage)
        {
            foreach (string image in images)
            {
                GameItem item = Instantiate(ImagePrefab).GetComponent<GameItem>();
                item.IsCorectItem = isCorectImage;
                item.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(image);
            }
        }
    }
}
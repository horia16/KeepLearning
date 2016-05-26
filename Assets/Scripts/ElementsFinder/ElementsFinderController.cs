using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementsFinderController : MiniGame
{
    public static int NumberOfCorectWords = 2;
    public static int NumberOfCorectImages = 1;
    public static int NumberOfWrongImages = 2;
    public static int NumberOfWrongWords = 2;
        
    public Category CorectCategory;

    public GameObject ItemPrefab;
    public Transform GameSpace;

    public int CorectItemsCount;
    public int WrongItemsCount;

    public override void StartGame(Category domain, GameObject canvas)
    {
        base.StartGame(domain, canvas);

        Domain.GetSubcategoriesElements();
        Domain.DestroyUnusedSubcategories();
        Initialize();
    }

    void Awake()
    {
        GameSpace = transform.FindChild("Game space");
        ItemPrefab = Resources.Load<GameObject>("Prefabs/ElementsFinder/Item");
    }

    void Initialize()
    {
        CorectCategory = CategoryRandomer.ChooseSubcategory(Domain, CanBeCorectCategory);
        transform.FindChild("CategoryName").GetComponent<Text>().text = CorectCategory.Name;
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

    public static bool CanBeCorectCategory(CategoryInfo category)
    {
        if (category.WordsCount >= NumberOfCorectWords && category.ImagesCount >= NumberOfCorectImages)
            return true;
        else
            return false;
    }

    void AddWords(IList<string> words, bool isCorectWord)
    {
        foreach(string word in words)
        {
            ElementsFinderItem item = Instantiate(ItemPrefab).GetComponent<ElementsFinderItem>();
            item.IsCorectItem = isCorectWord;
            item.transform.SetParent(GameSpace, false);
            item.gameObject.GetComponentInChildren<Text>().text = word;
            item.SetRandom();
        }
    }

    void AddImages(IList<string> images, bool isCorectImage)
    {
        foreach (string image in images)
        {
            ElementsFinderItem item = Instantiate(ItemPrefab).GetComponent<ElementsFinderItem>();
            item.IsCorectItem = isCorectImage;
            item.transform.SetParent(GameSpace, false);
            item.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(image);
            item.gameObject.GetComponentInChildren<Text>().text = "";
            item.SetRandom();
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

    internal override int GetPoints()
    {
        if (CorectItemsCount < NumberOfCorectImages + NumberOfCorectWords)
            return 0;
        else
            return CorectItemsCount;
    }
}
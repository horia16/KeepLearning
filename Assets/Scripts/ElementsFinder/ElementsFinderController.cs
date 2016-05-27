using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementsFinderController : MiniGame
{
    public static int NumberOfCorectItems = 10;
    public static int NumberOfWrongItems = 20;

    public GameObject ItemPrefab;
    public Transform GameSpace;

    public int CorectItemsCount;
    public int WrongItemsCount;

    public override void StartGame(Category domain, GameObject canvas)
    {
        Domain = domain;

        canvas.SetActive(false);

        Domain.GetSubcategoriesElements();
        Domain.DestroyUnusedSubcategories();
        base.StartGame(domain, gameObject);
        Initialize();
    }

    void Awake()
    {
        GameSpace = transform.FindChild("GameSpace");

        ItemPrefab = Resources.Load<GameObject>("Prefabs/ElementsFinder/Item");

        Debug.Log(Screen.currentResolution.width);
        Debug.Log(Screen.width);

        transform.localScale = new Vector3(transform.localScale.x * Screen.currentResolution.width / 1280, transform.localScale.y * Screen.currentResolution.height / 800, 1);
    }

    void Initialize()
    {
        Category CorectCategory = CategoryRandomer.ChooseSubcategory(Domain, CanBeCorectCategory);
        base.SetCategory(CorectCategory);
        AddCorectItems(CorectCategory);
        AddIncorectItems(CorectCategory);

        RefreshScore();
        CorectCategory = null;
        Domain = null;
    }

    void FixedUpdate()
    {
        Physics2D.gravity = 9.8f * Input.acceleration.normalized;
    }

    void AddCorectItems(Category CorectCategory)
    {
        if (CorectCategory == null)
            throw new System.Exception("No category match!");

        AddImages(CategoryRandomer.ChooseItems(CorectCategory.Images, Mathf.Min(NumberOfCorectItems, CorectCategory.Images.Count)), true);
        if(CorectCategory.Images.Count < NumberOfCorectItems)
            AddWords(CategoryRandomer.ChooseItems(CorectCategory.Words, 
                Mathf.Min(NumberOfCorectItems - CorectCategory.Images.Count, CorectCategory.Words.Count)), true);

        GameItem.GameItemChoosed += ItemChoosed;
    }

    void AddIncorectItems(Category CorectCategory)
    {
        int imagesNumber = Domain.Images.Count - CorectCategory.Images.Count;
        AddImages(CategoryRandomer.GetRandomImages(Domain, Mathf.Min(NumberOfWrongItems, imagesNumber),
            delegate (Category category)
            {
                return category != CorectCategory;
            }), false);

        if(imagesNumber < NumberOfWrongItems)
            AddWords(CategoryRandomer.GetRandomWords(Domain, 
                Mathf.Min(NumberOfWrongItems - imagesNumber, Domain.Words.Count - CorectCategory.Words.Count),
                delegate (Category category)
                {
                    return category != CorectCategory;
                }), false);
    }

    public static bool CanBeCorectCategory(Category category)
    {
        if (category.Words.Count + category.Images.Count >= NumberOfCorectItems)
            return true;
        else
            return false;
    }

    public static bool CanBeCorectCategory(CategoryInfo category)
    {
        if (category.WordsCount + category.ImagesCount >= NumberOfCorectItems)
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
            Sprite s = Resources.Load<Sprite>(image);
            if (s == null)
                s = null;
            item.GetComponent<Image>().sprite = s;
            item.SetRandom();
        }
    }

    void ItemChoosed(GameItem item)
    {
        if(item.IsCorectItem)
        {
            CorectItemsCount++;
            if (CorectItemsCount == NumberOfCorectItems)
                GameFinished();
        }
        else
        {
            WrongItemsCount++;
            if (WrongItemsCount > 3)
                GameFinished();
        }
        RefreshScore();
        Destroy(item.gameObject);
    }

    void RefreshScore()
    {
        Score.text = "Corect: " + CorectItemsCount + " Wrong: " + WrongItemsCount;
    }

    internal override int GetPoints()
    {
        if (CorectItemsCount < NumberOfCorectItems)
            return 0;
        else
            return CorectItemsCount;
    }

    internal override void Refresh()
    {
        GameItem.Refresh();
    }
}
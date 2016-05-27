using KeepLearning;

public class ElementsFinderMenu : CategoriesInfoMenu
{
    public static bool CanChooseCategory(CategoryInfo category)
    {
        if (category.Subcategories.Count < 2)
            return false;

        CategoryInfo okCategory = null;

        foreach(CategoryInfo info in category.Subcategories)
        {
            if (ElementsFinderController.CanBeCorectCategory(info) && (okCategory == null ||
                                    okCategory.WordsCount + okCategory.ImagesCount > info.WordsCount + info.ImagesCount))
                okCategory = info;
        }

        return okCategory != null && category.WordsCount + category.ImagesCount -
                            okCategory.WordsCount - okCategory.ImagesCount >=
                            ElementsFinderController.NumberOfWrongItems;
    }

    public override bool CanChoose(CategoryInfo category)
    {
        return CanChooseCategory(category);
    }
}
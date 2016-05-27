using UnityEngine;
using System.Collections;
using KeepLearning;
public class ElementsRemoverMenu : CategoriesInfoMenu {

    public override bool CanChoose(CategoryInfo category)
    {
        if (category.Subcategories.Count <= 1)
            return false;
        int numberOfOk=0;
        for (int i = 0; i < category.Subcategories.Count; i++)
        {
            if (category.Subcategories[i].ImagesCount != 0 || category.Subcategories[i].WordsCount != 0)
                numberOfOk++;
            if (numberOfOk>1)
                return true;
        }
        return false;
    }
}

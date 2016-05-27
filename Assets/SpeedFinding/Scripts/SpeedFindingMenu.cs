using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class SpeedFindingMenu : KeepLearning.CategoriesInfoMenu
{
    public override bool CanChoose(CategoryInfo category)
    {
        if (category.WordsCount >= 20 && category.Subcategories.Count >= 2)
            for (int i = 0; i < category.Subcategories.Count; i++)
                if (category.Subcategories[i].WordsCount >= 5 && category.WordsCount - category.Subcategories[i].WordsCount >= 15)
                    return true;
        return false;
    }

}


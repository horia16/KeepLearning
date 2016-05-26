using System;
using System.Collections.Generic;
using UnityEngine;

public static class CategoryRandomer
{
    
    public static Category ChooseSubcategory(Category category)
    {
        throw new NotImplementedException();
    }

    public static IList<T> ChooseItems<T>(List<T> list, int number)
    {
        throw new NotImplementedException();
    }

    public static Category ChooseRandomSubcategory(Category domain, Func<Category, bool> test)
    {
        throw new NotImplementedException();
    }

    internal static IList<string> GetRandomWords(Category domain, int number, Func<Category, bool> p)
    {
        throw new NotImplementedException();
    }

    internal static IList<string> GetRandomImages(Category domain, int numberOfWrongImages, Func<Category, bool> p)
    {
        throw new NotImplementedException();
    }
}
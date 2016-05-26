using System.Collections.Generic;
using UnityEngine;

public static class CategoryRandomer
{

    public static Category ChooseSubcategory(Category domain, System.Func<Category, bool> CanBeCorectCategory)
    {
		if (domain.Subcategories.Count == 0)
			throw new System.Exception ("No subcategory!");

        IList<Category> okCategories = new List<Category>();
        foreach(Category category in domain.Subcategories)
        {
            if (CanBeCorectCategory(category))
                okCategories.Add(category);
        }

		return okCategories[Random.Range (0, okCategories.Count - 1)];
    }

    public static IList<T> ChooseItems<T>(List<T> list, int number)
    {
		if (number > list.Count)
			throw new System.Exception ("Insuficient Items!");
    
		List<T> clone = new List<T>();
		IList<T> res = new List<T>();
		clone.AddRange (list);

		for (int i = 1; i <= number; i++)
		{
			int index = Random.Range (0, list.Count);
			res.Add (list [index]);
			list.RemoveAt (index);
		}
		return res;
	}

    internal static IList<string> GetRandomWords(Category domain, int number, System.Func<Category, bool> IsCorectCategory)
    {
		IList<string> res = new List<string>();

		for (int i = 1; i <= number; i++)
		{
			int index = Random.Range (0, domain.Subcategories.Count);
			while (IsCorectCategory (domain.Subcategories [index]) == false)
				index = Random.Range (0, domain.Subcategories.Count);
			res.Add (domain.Subcategories [index].Words[Random.Range(0,domain.Subcategories [index].Words.Count)]);
		}
    
		return res;
	}

    internal static IList<string> GetRandomImages(Category domain, int numberOfWrongImages, System.Func<Category, bool> p)
    {
		IList<string> res = new List<string>();

		for (int i = 1; i <= numberOfWrongImages; i++) 
		{
			int index = Random.Range (0, domain.Subcategories.Count);
			while (p (domain.Subcategories [index]) == false)
				index = Random.Range (0, domain.Subcategories.Count);
			res.Add (domain.Subcategories [index].Images[Random.Range(0,domain.Subcategories [index].Images.Count)]);
		}
		return res;
    }
}
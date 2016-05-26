using System.Collections.Generic;
using UnityEngine;

public static class CategoryRandomer
{
    
    public static Category ChooseSubcategory(Category category)
    {
		if (category.Subcategories.Count == 0)
			throw new System.Exception ("No subcategory!");


		category = category.Subcategories [Random.Range (0, category.Subcategories.Count)];
		while (category.Subcategories.Count > 0)
		{
			if (Random.Range (0, 2) > 0)
				return category;
			category = category.Subcategories [Random.Range (0, category.Subcategories.Count)];
		}
		return category;
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

    internal static IList<string> GetRandomWords(Category domain, int number, System.Func<Category, bool> p)
    {
		IList<string> res = new List<string>();

		for (int i = 1; i <= number; i++)
		{
			int index = Random.Range (0, domain.Subcategories.Count);
			while (p (domain.Subcategories [index]) == false)
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
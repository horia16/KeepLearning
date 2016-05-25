using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ElementsRemoverController : MonoBehaviour {

	public GameObject workSpace;
	public Text categoryName;
	private Category category;
	private IList<Category> wrongCategories;
	private List<string> rightWords,rightImages;
	private List<string> wrongWords,wrongImages;
	private bool isStarted;
	private float time;
	private int numberOfWrong;


	void Start()
	{
		isStarted = false;
	}

	public void StartGame(Category category)
	{
		isStarted = true;
		this.category = category;
		Category aux = GetRandomSubcategory (category);
		ExtractElements (category,aux,rightWords,rightImages,wrongWords,wrongImages);
	}

	public void StopGame()
	{
		isStarted = false;
	}

	void Update()
	{
		if (isStarted)
		{
			time += Time.deltaTime;
			if(time>=2)
			{
				time = 0;
				int index;
				if (rightWords.Count == 0 && rightImages.Count == 0)
					isStarted = false;

				if (numberOfWrong == 0)
				{
	 				if (rightWords.Count == 0)
						index = 1;
					else if (rightImages.Count == 0)
						index = 0;
					else
						index = Random.Range (0, 2);

					if (index == 0)
					{
						GameObject textGO = new GameObject();
						Text text = textGO.AddComponent <Text>();
						int aux = Random.Range (0, rightWords.Count);
						text.text = rightWords [aux];
						rightWords.RemoveAt (aux);
						LoadElement (text.gameObject);
					}
					else
					{
						Image image;
						int aux = Random.Range (0, rightImages.Count);
						image = Resources.Load <Image>(rightImages [aux]);
						rightImages.RemoveAt (aux);
						LoadElement (image.gameObject);
					}
					numberOfWrong = Random.Range(2,6);
				}
				else
				{
					if (wrongWords.Count == 0)
						index = 1;
					else if (wrongImages.Count == 0)
						index = 0;
					else
						index = Random.Range(0,2);

					if (index == 0)
					{
						GameObject textGO = new GameObject();
						Text text = textGO.AddComponent <Text>();

						int aux = Random.Range (0, wrongWords.Count);
						text.text = wrongWords [aux];
						LoadElement (text.gameObject);
					}
					else
					{
						Image image;
						int aux = Random.Range (0, wrongImages.Count);
						image = Resources.Load <Image>(wrongImages [aux]);
						LoadElement (image.gameObject);
					}
					numberOfWrong--;
				}
			}
		}
	}

	void LoadElement(GameObject element)
	{
		GameObject.Instantiate (element);
		element.AddComponent <ElementMover>();
	}

	Category GetRandomSubcategory(Category category)
	{
		int index = Random.Range (0, category.Subcategories.Count);		
		category = category.Subcategories [index];

		while (category.Subcategories.Count != 0)
		{
			if (Random.Range (0, 2) == 0)
			{
				index = Random.Range (0, category.Subcategories.Count);		
				category = category.Subcategories [index];
			}
			else
			{
				return category;
			}
		}

		return category;
	}

	void ExtractElements(Category allCategory, Category wrongCategory, List<string> rightWords, List<string> rightImagePath, List<string> wrongWords, List<string> wrongImagePath)
	{
		rightWords.AddRange (wrongCategory.Words);
		rightImagePath.AddRange (wrongCategory.Images);
		categoryName.text = wrongCategory.Name;
		wrongCategory.Words.Clear ();
		wrongCategory.Images.Clear ();
		wrongCategory.Subcategories.Clear ();

		ExtractWrongElements (allCategory, wrongWords, wrongImagePath);
	}

	void ExtractWrongElements(Category rightCategory, List<string> wrongWords, List<string> wrongImagePath)
	{
		if (rightCategory.Subcategories.Count == 0)
		{
			wrongWords.AddRange (rightCategory.Words);
			wrongImagePath.AddRange (rightCategory.Images);
		}
		else
		{
			foreach (Category i in rightCategory.Subcategories)
				ExtractWrongElements (i, wrongWords, wrongImagePath);
		}
	}
}

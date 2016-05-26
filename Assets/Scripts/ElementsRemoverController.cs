using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


namespace KeepLearning
{
	public class ElementsRemoverController : MonoBehaviour
	{
		public Text categoryName;
		public Canvas gameCanvas;
		public Button categoryButton;
		public GameObject startPointUp;
		public GameObject startPointDown;
		public GameObject prefabText;
		public bool isStarted;
		public GameObject startCanvas;
		private Category category;
		private IList<Category> wrongCategories;
		private List<string> rightWords = new List<string>();
		private List<string> rightImages = new List<string>();
		private List<string> wrongWords = new List<string>();
		private List<string> wrongImages = new List<string>();
		private float time;
		private int numberOfWrong;


		void Start()
		{
			isStarted = false;
		}

		void resizeCollider()
		{
			Vector2 size = new Vector2 (((RectTransform)gameCanvas.transform).sizeDelta.x,((RectTransform)gameCanvas.transform).sizeDelta.y);
			Debug.Log (size);
			size = new Vector2 (((RectTransform)gameCanvas.transform).rect.width,((RectTransform)gameCanvas.transform).rect.height);
			Debug.Log (size);

			BoxCollider2D collider = gameCanvas.GetComponent<BoxCollider2D> ();
			collider.size = size;
			collider.offset = new Vector2 (0, 0);
		}

		// to do resize collider
		public void StartGame(Category category)
		{
			isStarted = true;
			category.GetSubcategoriesElements ();
			//resizeCollider ();
			this.category = category;
			Category aux = GetRandomSubcategory (category);
			ExtractElements (category,aux,rightWords,rightImages,wrongWords,wrongImages);
		}

		public void StopGame(bool win)
		{
			isStarted = false;

			startCanvas.gameObject.SetActive (true);
			gameCanvas.gameObject.SetActive (false);

		}

		public void ButtonClick(GameObject button)
		{
			if (button.GameItem.isOk == true)
			{
				Destroy (button);
			}
			else
			{
				StopGame (false);
			}
		}

		void Update()
		{
			if (isStarted)
			{
				time += Time.deltaTime;
				if(time>=4)
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
							
							int aux = Random.Range (0, rightWords.Count);

							prefabText.GetComponentInChildren<Text>().text = rightWords [aux];
							rightWords.RemoveAt (aux);
							LoadElement (prefabText, true);
						}
						else
						{
							Image image;
							int aux = Random.Range (0, rightImages.Count);
							image = Resources.Load <Image>(rightImages [aux]);
							rightImages.RemoveAt (aux);

							if (image!=null)
								LoadElement (image.gameObject, true);

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
							int aux = Random.Range (0, wrongWords.Count);
							prefabText.GetComponentInChildren<Text>().text = wrongWords [aux];
							LoadElement (prefabText, false);
						}
						else
						{
							Image image;
							int aux = Random.Range (0, wrongImages.Count);
							image = Resources.Load <Image>(wrongImages [aux]);

							if (image!=null)
							LoadElement (image.gameObject, false);

						}
						numberOfWrong--;
					}
				}
			}
		}

		void LoadElement(GameObject element2, bool isOk)
		{
			GameObject element = GameObject.Instantiate (element2);
			element.transform.SetParent (gameCanvas.gameObject.transform,false);
			element.transform.position = new Vector3 (startPointDown.transform.position.x, Random.Range (startPointDown.transform.position.y, startPointUp.transform.position.y), 0f);
			element.AddComponent <ElementMover>();
			element.GetComponent <ElementMover> ().mother = this;
			element.GetComponent <GameItem> ().isOk = isOk;
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
}
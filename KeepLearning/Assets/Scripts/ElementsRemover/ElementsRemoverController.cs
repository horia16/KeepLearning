using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace KeepLearning
{
	public class ElementsRemoverController : MiniGame
	{
		public Text categoryName;
		Canvas gameCanvas;
		GameObject startPointUp;
		GameObject startPointDown;
		GameObject prefabButton;
		Text scoreText;
		public bool isStarted;
		Category category;
		IList<Category> wrongCategories;
		List<string> rightWords = new List<string>();
		List<string> rightImages = new List<string>();
		List<string> wrongWords = new List<string>();
		List<string> wrongImages = new List<string>();
		float time;
		int numberOfWrong;
		int point;


		void Awake()
		{
			isStarted = false;
			startPointUp = this.gameObject.transform.FindChild ("StartPointUp").gameObject;
			startPointDown = this.gameObject.transform.FindChild ("StartPointDown").gameObject;
			prefabButton = Resources.Load <GameObject> ("Prefabs/ElementsRemover/ButtonPrefab");
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

		void ScoreUpdate()
		{
			scoreText.text = "Score: " + point.ToString ();
		}

		public void Back()
		{
			GameFinished ();
		}

		// to do resize collider
		public override void StartGame(Category domain, GameObject canvas)
		{
			point = 0;
			time = 0;
			isStarted = true;
			domain.GetSubcategoriesElements ();
            //resizeCollider ();
            transform.SetParent(canvas.transform, false);
			this.category = domain;
			gameCanvas = canvas.GetComponent<Canvas>();
			Category aux = GetRandomSubcategory (category);

            base.StartGame(domain,canvas);

            ExtractElements(category, aux, rightWords, rightImages, wrongWords, wrongImages);

            scoreText = Score;
            ScoreUpdate();
            timer.SetActive(false);
        }

		internal override int GetPoints()
		{
			return point;
		}


		public override void GameFinished()
		{
			isStarted = false;
			base.GameFinished ();
		}

		public void ButtonClick(GameObject button)
		{
			if (button.GetComponent<GameItem>().IsCorectItem == true)
			{
				point++;
				ScoreUpdate ();
				Destroy (button);
			}
			else
			{
				Destroy (button);
				GameFinished ();
			}
		}

		void Update()
		{
			if (isStarted)
			{
				time += Time.deltaTime;
				if(time>=1)
				{
					time = 0;
					int index;
					if (rightWords.Count == 0 && rightImages.Count == 0)
					{
						GameFinished ();
					}

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

							prefabButton.GetComponentInChildren<Text>().text = rightWords [aux];
							rightWords.RemoveAt (aux);
							LoadElement (prefabButton, true);
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
							prefabButton.GetComponentInChildren<Text>().text = wrongWords [aux];
							LoadElement (prefabButton, false);
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

		void LoadElement(GameObject element2, bool IsCorectItem)
		{
			GameObject element = GameObject.Instantiate (element2);
			element.transform.SetParent (this.transform,false);
			element.transform.position = new Vector3 (startPointDown.transform.position.x, Random.Range (startPointDown.transform.position.y, startPointUp.transform.position.y), 0f);
			element.AddComponent <ElementMover>();
			element.AddComponent <GameItem> ();
			element.GetComponent <Button>().onClick.AddListener (() => ButtonClick(element));
			element.GetComponent <GameItem> ().IsCorectItem = IsCorectItem;
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
			SetCategory( wrongCategory);
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

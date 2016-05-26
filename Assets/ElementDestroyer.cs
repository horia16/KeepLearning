using UnityEngine;
using System.Collections;


namespace KeepLearning
{
	public class ElementDestroyer : MonoBehaviour {

		void OnTriggerExit2D(Collider2D element)
		{
			if (element.GetComponent <GameItem> ().IsCorectItem == false)
				GameObject.Destroy (element.gameObject);
			else
				GameObject.Find ("GameController").GetComponent<ElementsRemoverController> ().StopGame (false);
		}

	}
}

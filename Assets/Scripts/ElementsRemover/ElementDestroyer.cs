using UnityEngine;
using System.Collections;


namespace KeepLearning
{
	public class ElementDestroyer : MonoBehaviour
	{
		public ElementsRemoverController elementRemoverController;

        void Start()
        {
            elementRemoverController = this.gameObject.GetComponent<ElementsRemoverController>();
        }
		void OnTriggerExit2D(Collider2D element)
		{
			if (element.GetComponent <GameItem> ().IsCorectItem == false)
				GameObject.Destroy (element.gameObject);
			else
				elementRemoverController.GameFinished ();
		}

	}
}

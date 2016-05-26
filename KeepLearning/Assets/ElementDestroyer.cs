using UnityEngine;
using System.Collections;

public class ElementDestroyer : MonoBehaviour {

	void OnTriggerExit2D(Collider2D element)
	{
		GameObject.Destroy (element.gameObject);
	}

}

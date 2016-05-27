using UnityEngine;
using System.Collections;

namespace KeepLearning
{
	public class ElementMover : MonoBehaviour {

	    public int speed = 100;
	    Vector3 direction;

		void Start ()
		{
			RectTransform canvas = this.GetComponentInParent<RectTransform> ();
			int x = Random.Range (0, 4);
			x = 0;
			if (x==0)
			{
				Vector3 position = new Vector3();
				position = ((RectTransform)this.transform).localPosition;
				Debug.Log (position);

				float min = -0.5f;
				float max = 0.5f;

				if (position.y < 0)
					min=0;
				else
					max=0;

	            direction = new Vector3(1, Random.Range(min, max), 0);
	        }
		}
	    void Update()
	    {
	        this.transform.Translate(direction * speed * Time.deltaTime);
	    }

	}
}
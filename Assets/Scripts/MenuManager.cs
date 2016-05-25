using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{


	public void CloseMenu(GameObject Object)
    {
       	Object.SetActive(false);
	}

	public void OpenMenu(GameObject Object)
	{
		Object.SetActive (true);
	}


}
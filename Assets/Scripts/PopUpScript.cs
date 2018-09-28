using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PopUpScript : MonoBehaviour {

	void OnEnable()
	{
		transform.SetAsLastSibling ();
	}
}

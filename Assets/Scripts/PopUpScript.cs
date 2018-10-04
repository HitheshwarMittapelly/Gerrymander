using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Set as the last sibling when enabled
public class PopUpScript : MonoBehaviour {

	void OnEnable()
	{
		transform.SetAsLastSibling ();
	}
}

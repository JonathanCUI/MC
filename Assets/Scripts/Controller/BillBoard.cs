using UnityEngine;
using System.Collections;

public class BillBoard : MonoBehaviour {
	
	// Update is called once per frame
	void Update () 
    {
	    this.transform.LookAt(Camera.main.transform);
	}
}

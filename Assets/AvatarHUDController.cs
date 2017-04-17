using UnityEngine;
using System.Collections;

public class AvatarHUDController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.transform.parent.GetComponent<Canvas>().worldCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () 
    {
        this.transform.position = this.transform.parent.parent.position + new Vector3(0, 3.5f, 0);
	}
}

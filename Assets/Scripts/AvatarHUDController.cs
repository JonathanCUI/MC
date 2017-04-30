using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AvatarHUDController : MonoBehaviour {

    //共有界面索引，方便快速的改变界面
    public Image HPBarImage;

	// Use this for initialization
	void Start () {
        this.transform.parent.GetComponent<Canvas>().worldCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () 
    {
        this.transform.position = this.transform.parent.parent.position + new Vector3(0, 3.5f, 0);
	}

    public void SetData(HeroClass pHeroClass, Camp pCamp)
    {
        HPBarImage.color = HeroColor.ColorList[(int)pHeroClass];
    }
}

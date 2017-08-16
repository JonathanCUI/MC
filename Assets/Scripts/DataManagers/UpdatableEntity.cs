using UnityEngine;
using System.Collections;

public abstract class UpdateableEntity : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private Vector2 _logicSpeed;
    private Vector3 _renderSpeed;

    public Vector2 LogicSpeed
    {
        get { return _logicSpeed; }
        set {
            _logicSpeed = value;
            _renderSpeed = Const.LogicToRenderVector(_logicSpeed);
        }
    }

    public Vector2 _logicPosition;
    public Vector2 LogicPosition
    {
        get { return _logicPosition; }
        set {
            _logicPosition = value;            
        }
    }

    public virtual void UpdateLogic()
    {
        //更新位置
        _logicPosition += Const.UpdateGapTime * _logicSpeed;
        this.transform.position = Const.LogicToRenderVector(_logicPosition);

        //更新旋转
        if (_renderSpeed != Vector3.zero)
        {
         //   this.transform.rotation = Quaternion.LookRotation(_renderSpeed);
        }
    }

    public virtual void UpdatePosition(float pDeltaTime)
    {
        //Debug.Log(pDeltaTime.ToString());
        //更新位置
        this.transform.position += pDeltaTime * _renderSpeed;

        //更新旋转
        if (_renderSpeed != Vector3.zero)
        {
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_renderSpeed), pDeltaTime * 10f);
        }
    }


}

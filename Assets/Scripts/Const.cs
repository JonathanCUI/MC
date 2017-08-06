using UnityEngine;
using System.Collections;

public class Const {

    //所有常数定义
    public static float UpdateGapTime = 1f / 30f;
    public static float LTRScale = 1f; //logic to render scale
    public static float RTLScale = 1 / LTRScale; //render to logic scale
    public static float BattleFieldLength = 100f; //-100 ~ 100
    //常用转换函数
    public static Vector3 LogicToRenderVector(Vector2 pLogicPosition)
    {
        return new Vector3(pLogicPosition.x * LTRScale, 0, pLogicPosition.y * LTRScale);
    }

    public static Vector2 RenderToLogicVector(Vector3 pRenderPosition)
    {
        return new Vector2(pRenderPosition.x * RTLScale, pRenderPosition.z * RTLScale);
    }
}

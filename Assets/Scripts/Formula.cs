using UnityEngine;
using System.Collections;

public static class Formula  {

    private static float _meleeResistConst = 0.06f;
    private static float _rangeResistConst = 0.06f;
    private static float _magicResistConst = 0.06f;

    public static float CalculateDamage(AttackType pAttackType, float pAttackValue, float pDefenseValue)
    {
        if(pDefenseValue >= 0f)
        {
            switch(pAttackType)
            {
                case AttackType.Melee:
                    return pAttackValue / (1f + _meleeResistConst * pDefenseValue);
                case AttackType.Range:
                    return pAttackValue / (1f + _rangeResistConst * pDefenseValue);
                case AttackType.Magic:
                    return pAttackValue / (1f + _magicResistConst * pDefenseValue);
                default:
                    Debug.LogError("Invalid attack type");
                    return 0f;
            }
        }
        else
        {
            switch(pAttackType)
            {
                case AttackType.Melee:
                    return pAttackValue * (2f - Mathf.Pow(1f - _meleeResistConst, -pDefenseValue));
                case AttackType.Range:
                    return pAttackValue * (2f - Mathf.Pow(1f - _rangeResistConst, -pDefenseValue));
                case AttackType.Magic:
                    return pAttackValue * (2f - Mathf.Pow(1f - _magicResistConst, -pDefenseValue));
                default:
                    Debug.LogError("Invalid attack type");
                    return 0f;
            }
        }
    }

}

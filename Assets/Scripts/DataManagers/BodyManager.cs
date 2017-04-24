using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;

public class BodyManager {
    //data members
    private HeroClass _heroClass;
    private float _baseHP;
    private float _baseMP;
    private float _baseWalkSpeed;
    private float _baseRunSpeed;
    private float _baseMeleeAttack;
    private float _baseRangeAttack;
    private float _baseMagicAttack;
    private float _baseMeleeDefense;
    private float _baseRangeDefense;
    private float _baseMagicDefense;
    private AttackType _attackType;
    private float _baseAttackRange;
    private float _baseAttackGap;
    private float _baseHPRecoverSpeed;
    private float _baseMPRecoverSpeed;


    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//    private int myVar;

    public float RunSpeed
    {
        //这里以后要考虑buff对
        get { return _baseRunSpeed; }
    }



    //获得英雄的基本属性信息
    private void SetUpBattleProperties(HeroClass pHeroClass)
    {
        _heroClass = pHeroClass;
        SqliteDataReader reader = DBManager.ExecuteQuery("SELECT * FROM Hero WHERE id = " + ((int)pHeroClass).ToString());
        if (!reader.Read())
        {
            Debug.LogError("Incorrect Hero Class of" + pHeroClass.ToString());
        }
        _baseHP = reader.GetFloat(reader.GetOrdinal("hp"));
        _baseMP = reader.GetFloat(reader.GetOrdinal("mp"));
        _baseWalkSpeed = reader.GetFloat(reader.GetOrdinal("walk_spd"));
        _baseRunSpeed = reader.GetFloat(reader.GetOrdinal("run_spd"));
        _baseMeleeAttack = reader.GetFloat(reader.GetOrdinal("melee_atk"));
        _baseRangeAttack = reader.GetFloat(reader.GetOrdinal("range_atk"));
        _baseMagicAttack = reader.GetFloat(reader.GetOrdinal("magic_atk"));
        _baseMeleeDefense = reader.GetFloat(reader.GetOrdinal("melee_def"));
        _baseRangeDefense = reader.GetFloat(reader.GetOrdinal("range_def"));
        _baseMagicDefense = reader.GetFloat(reader.GetOrdinal("magic_def"));
        _attackType = (AttackType)reader.GetInt32(reader.GetOrdinal("atk_type"));
        _baseAttackRange = reader.GetFloat(reader.GetOrdinal("atk_range"));
        _baseAttackGap = reader.GetFloat(reader.GetOrdinal("atk_gap"));
        _baseHPRecoverSpeed = reader.GetFloat(reader.GetOrdinal("hp_recover_spd"));
        _baseMPRecoverSpeed = reader.GetFloat(reader.GetOrdinal("mp_recover_spd"));
    }



}

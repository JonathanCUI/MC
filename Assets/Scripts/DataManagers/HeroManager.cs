using UnityEngine;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public enum HeroAIStatus{
    Patrol,         //nothing to do
    Run,            //run to target
    Attack,         //attack an enemy
    Defense,        //fear, heal or others
}

public class HeroManager : MonoBehaviour {

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


    private float _currentHP;
    private float _currentMP;
    private float _currentSpeed;
    private HeroAIStatus _currentAIStatus;
    //AI relative
    private List<float> _heroLikeProrityList;

    //patrol const
    private const float _patrolNextTargetTime = 3f;
    private float _patrolNextTargetAccumulate = 3f;
    private Vector3 _patrolDirection = Vector3.zero;

    //move to target
    private Transform _rewardTransform;


	// Use this for initialization
	void Start () {
	    _currentAIStatus = HeroAIStatus.Patrol;
        _currentHP       = _baseHP;
        _currentMP       = _baseMP;


	}
	
	// Update is called once per frame
	void Update () 
    {
        //update battle properties
        //Mathf.Clamp()
        _currentHP = Mathf.Min(_currentHP + _baseHPRecoverSpeed * Time.deltaTime, _baseHP);
        _currentMP = Mathf.Min(_currentMP + _baseMPRecoverSpeed * Time.deltaTime, _baseMP);


	    //update normal attack
        switch(_currentAIStatus)
        {
            case HeroAIStatus.Patrol:
                UpdatePatrolLogic();
                break;
            case HeroAIStatus.Attack:
                break;
            case HeroAIStatus.Defense:
                break;
            default:
                break;
        }
	}

    private void UpdatePatrolLogic()
    {
        return;
        _patrolNextTargetAccumulate += Time.deltaTime;
        transform.position += _patrolDirection  * _baseWalkSpeed * Time.deltaTime;
        if(_patrolNextTargetAccumulate > _patrolNextTargetTime)
        {
            Vector2 v2 = Random.insideUnitCircle * 5f;
            _patrolDirection = new Vector3(v2.x, 0f, v2.y);
            _patrolNextTargetAccumulate = 0f;
        }
    }

    private void UpdateAttackLogic()
    {

    }

    private void UpdateDefenseLogic()
    {

    }

    private void UpdateMoveLogic()
    {
        int i = 0;
        i++;
    }

    public bool SetReward()
    {
        return false;
    }

    public void SetData(HeroClass pHeroClass)
    {
        SetUpBattleProperties(pHeroClass);
        SetUpAIProperties(pHeroClass);
    }

    private void SetUpBattleProperties(HeroClass pHeroClass)
    {
        _heroClass = pHeroClass;
        SqliteDataReader reader = DBManager.ExecuteQuery("SELECT * FROM Hero WHERE id = " + ((int)pHeroClass).ToString());
        if(!reader.Read())
        {
            Debug.LogError("Incorrect Hero Class of" + pHeroClass.ToString());
        }
        _baseHP             = reader.GetFloat(reader.GetOrdinal("hp"));
        _baseMP             = reader.GetFloat(reader.GetOrdinal("mp"));
        _baseWalkSpeed      = reader.GetFloat(reader.GetOrdinal("walk_spd"));
        _baseRunSpeed       = reader.GetFloat(reader.GetOrdinal("run_spd"));
        _baseMeleeAttack    = reader.GetFloat(reader.GetOrdinal("melee_atk"));
        _baseRangeAttack    = reader.GetFloat(reader.GetOrdinal("range_atk"));
        _baseMagicAttack    = reader.GetFloat(reader.GetOrdinal("magic_atk"));
        _baseMeleeDefense   = reader.GetFloat(reader.GetOrdinal("melee_def"));
        _baseRangeDefense   = reader.GetFloat(reader.GetOrdinal("range_def"));
        _baseMagicDefense   = reader.GetFloat(reader.GetOrdinal("magic_def"));
        _attackType         = (AttackType)reader.GetInt32(reader.GetOrdinal("atk_type"));
        _baseAttackRange    = reader.GetFloat(reader.GetOrdinal("atk_range"));
        _baseAttackGap      = reader.GetFloat(reader.GetOrdinal("atk_gap"));
        _baseHPRecoverSpeed = reader.GetFloat(reader.GetOrdinal("hp_recover_spd"));
        _baseMPRecoverSpeed = reader.GetFloat(reader.GetOrdinal("mp_recover_spd"));
    }

    private void SetUpAIProperties(HeroClass pHeroClass)
    {
        SqliteDataReader reader = DBManager.ExecuteQuery("SELECT * FROM HeroLike WHERE id = " + ((int)pHeroClass).ToString());
        if(!reader.Read())
        {
            Debug.LogError("Incorrect Hero Class of" + pHeroClass.ToString());
        }
        _heroLikeProrityList = new List<float>();
        _heroLikeProrityList.Add(reader.GetFloat(reader.GetOrdinal("glory")));
        _heroLikeProrityList.Add(reader.GetFloat(reader.GetOrdinal("gold")));
        _heroLikeProrityList.Add(reader.GetFloat(reader.GetOrdinal("knowledge")));
        _heroLikeProrityList.Add(reader.GetFloat(reader.GetOrdinal("belief")));
        _heroLikeProrityList.Add(reader.GetFloat(reader.GetOrdinal("power")));
    }
}



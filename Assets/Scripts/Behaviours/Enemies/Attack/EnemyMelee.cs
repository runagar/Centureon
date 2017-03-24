using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour {

    bool isChargingAttack;

	// Use this for initialization
	void Start () {
        isChargingAttack = false;
	}
	
	public void ChargeAttack()
    {

    }

    public void ConcludeAttack()
    {

    }

    public bool GetAttackStatus()
    {
        return isChargingAttack;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBasic : MonoBehaviour {

    bool isChargingAttack;

	// Use this for initialization
	void Start () {
        isChargingAttack = false;
	}
	
	public void Attack()
    {
        if (isChargingAttack)
        {
            isChargingAttack = false;
            Debug.Log("BASH!");
        }
        else
        {
            isChargingAttack = true;
            Debug.Log("HIYYY...");
        }
    }

    public bool GetAttackStatus()
    {
        return isChargingAttack;
    }
}

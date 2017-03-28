using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour {
    UnitStats stats;

    bool isChargingAttack;

    Vector3 attackDirection;
    GameObject player;

    // Use this for initialization
    void Start () {
        isChargingAttack = false;

        stats = this.GetComponent<UnitStats>();

        player = GameObject.FindGameObjectWithTag("PLAYER");
    }
	
	public void ChargeAttack(Vector3 attackDir)
    {
        attackDirection = attackDir;
        isChargingAttack = true;
    }

    public void ConcludeAttack()
    {
        if (player != null) {
            if (player.transform.position == this.transform.position + attackDirection) {
                Destroy(player.gameObject);
            }
        }


        isChargingAttack = false;
    }

    public bool GetAttackStatus()
    {
        return isChargingAttack;
    }
}

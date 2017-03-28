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

            Vector2 p = new Vector2(player.transform.position.x, player.transform.position.z);
            Vector2 a = new Vector2(this.transform.position.x + attackDirection.x, this.transform.position.z + attackDirection.z);

            if (p == a) {
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

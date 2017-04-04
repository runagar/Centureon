using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour {
    UnitStats stats;

    bool isChargingAttack;

    Vector3 attackDirection;
    GameObject player;

    EnemyTelegraphs telegraphScript;
    List<Vector2> targetTiles;

    // Use this for initialization
    void Start () {
        isChargingAttack = false;

        stats = this.GetComponent<UnitStats>();

        player = GameObject.FindGameObjectWithTag("PLAYER");
        telegraphScript = this.GetComponent<EnemyTelegraphs>();
        targetTiles = new List<Vector2>();
    }
	
	public void ChargeAttack(Vector3 attackDir)
    {
        attackDirection = attackDir;
        isChargingAttack = true;

        targetTiles.Clear();

        int x = (int)transform.position.x;
        int z = (int)transform.position.z;

        for (int i = 0; i < stats.range; i++)
        {
            if (attackDirection.x == 0 && Mathf.Sign(attackDirection.z) == -1) targetTiles.Add(new Vector2(x, -(i + 1) + z));
            if (attackDirection.x == 0 && Mathf.Sign(attackDirection.z) == 1) targetTiles.Add(new Vector2(x, i + 1 + z));
            if (attackDirection.z == 0 && Mathf.Sign(attackDirection.x) == -1) targetTiles.Add(new Vector2(-(i + 1) + x, z));
            if (attackDirection.z == 0 && Mathf.Sign(attackDirection.x) == 1) targetTiles.Add(new Vector2(i + 1 + x, z));
        }

        telegraphScript.startTelegraph(targetTiles);
    }

    public void ConcludeAttack()
    {
        if (player != null && stats.isKill == "no") {

            Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
            Vector2 attackPos = new Vector2(this.transform.position.x + attackDirection.x, this.transform.position.z + attackDirection.z);

            if (playerPos == attackPos && this.gameObject != null) {
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

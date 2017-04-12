using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour {
    UnitStats stats;
	private AudioSource audio;
	public AudioClip death;
	bool check = false;

    bool isChargingAttack;

    public Vector3 attackDirection;
    GameObject player;

    EnemyTelegraphs telegraphScript;
    public List<Vector2> targetTiles;

    // Use this for initialization
    void Start () {
		audio = gameObject.GetComponent<AudioSource>();
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

            foreach(Vector2 v in targetTiles)
            {
                if (v.x == playerPos.x && v.y == playerPos.y)
                {
					audio.Play();
						Destroy(player.gameObject);
					if (audio.isPlaying)
					break;
                }  
            }
            
        }
        isChargingAttack = false;
    }

    public bool GetAttackStatus()
    {
        return isChargingAttack;
    }
}

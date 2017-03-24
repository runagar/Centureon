using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour {

    UnitStats stats;

    bool isChargingAttack;

    ProjectileTracker projectileTracker;
    public GameObject arrowPrefab;
    Vector3 attackDirection;

	// Use this for initialization
	void Start () {
        isChargingAttack = false;

        projectileTracker = GameObject.Find("ProjectileTracker").GetComponent<ProjectileTracker>();

        stats = this.GetComponent<UnitStats>();
	}

    public void ChargeAttack(Vector3 direction)
    {
        attackDirection = direction;
        isChargingAttack = true;
    }

    public void ConcludeAttack()
    {
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.LookRotation(attackDirection), projectileTracker.transform);
        Projectile proj = arrow.GetComponent<Projectile>();
        proj.speed = stats.projectileSpeed;
        proj.range = stats.range;
        proj.vectorHeading = (attackDirection).normalized;
        projectileTracker.projectiles.Add(arrow);

        isChargingAttack = false;
    }

    public bool GetAttackStatus()
    {
        return isChargingAttack;
    }
}

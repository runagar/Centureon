using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public int speed;
    public int range;
    public Vector3 vectorHeading;

    public int distanceTravelled = 0;

    ProjectileTracker tracker;

    private void Start()
    {
        tracker = GameObject.Find("ProjectileTracker").GetComponent<ProjectileTracker>();
    }

    private void Update()
    {
        if (distanceTravelled >= range)
        {
            DestroyProjectile();
        }
    }

    public void DestroyProjectile()
    {
        tracker.projectiles.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
}

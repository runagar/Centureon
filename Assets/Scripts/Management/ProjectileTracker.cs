using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTracker : MonoBehaviour {

    public List<GameObject> projectiles;
    PlayerStats player;

	// Use this for initialization
	void Start () {
        //player = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<PlayerStats>();
    }

    public void MoveProjectiles()
    {
        Vector3 direction = new Vector3(0, 0, 0);

        foreach (GameObject o in projectiles)
        {
            Projectile projectileScript = o.GetComponent<Projectile>();

            direction = projectileScript.vectorHeading;

            for(int i = 0; i < projectileScript.speed; i++)
            {
                o.transform.position += direction;
                projectileScript.distanceTravelled++;
                if (o.transform.position == player.transform.position)
                {
                    player.playerIsHit();
                    projectileScript.DestroyProjectile();
                    break;
                }
            }
        }
    }
}

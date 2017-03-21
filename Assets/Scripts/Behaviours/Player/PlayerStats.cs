using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    int movementSpeed;
    public int attackRange;

    public void playerIsHit()
    {
        Destroy(this.gameObject);

        //TODO: Loss sequence.
    }
}

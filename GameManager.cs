using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        zombie zombie = new zombie();
        player player = new player();

        player.TakeDamage(zombie.Attack());
        player.TakeDamage(zombie.AcidPuke());

        player.Die();

        zombie[] zombies = new zombie[100];

        for (int x; x < zombies.Length; x++) {
            zombies[x] = new zombie();
            print("Create Zombie #: " + x);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

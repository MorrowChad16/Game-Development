using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class variables : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string name = "Thanos";
        string equipedWeapon = "Infinity Gauntlet";
        string favoriteFurniture = "Throne";

        string favoritePlanet;
        favoritePlanet = "Earth";

        Debug.Log(name);
        Debug.Log(equipedWeapon);
        Debug.Log(favoriteFurniture);

        Debug.Log(equipedWeapon.ToUpper());

        double hp = 100.0;
        double shieldPower = 76.5;
        double laserDamage = 30;
        double acutalDamagePercent = .05;

        double actualDamage = (laserDamage * acutalDamagePercent);

        hp -= actualDamage;
        shieldPower -= (laserDamage - actualDamage);

        Debug.Log("HP: " + hp);
        Debug.Log("Shield Power: " + shieldPower);

        int slices = 10 / 5;

        print(slices);

        int newDamage = 10 / 3;

        print(newDamage);

        int newDamageRemainder = 10 % 3;

        print("10 divided by 3 equals " + newDamage + " with a remainder of " + newDamageRemainder);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

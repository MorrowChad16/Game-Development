using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conditionals : MonoBehaviour {

    int playerOneTowersRemaining = 2;
    int playerTwoTowersRemaining = 2;

    bool playerOneMainTowerDestroyed = false;
    bool playerTwoMainTowerDestroyed = false;

    double timer = 200;

	// Use this for initialization
	void Start () {
        if (playerOneMainTowerDestroyed) { 
            print("Player Two has won!");
        } else if (playerTwoMainTowerDestroyed) {
            print("Player One has won!");
        } else if (timer <= 0) {
            if (playerOneTowersRemaining < playerOneTowersRemaining) {
                print("Player two wins!!");
            } else if (playerOneTowersRemaining > playerOneTowersRemaining) {
                print("Player one wins!!");
            } else {
                print("THe game was a draw!");
            }
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

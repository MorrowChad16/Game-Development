using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UptownFunctionYouUp : MonoBehaviour {

    int health = 100;
    int attackPower = 25;
    bool shieldOn = true;
    int shieldAmt = 15;

    private void Start() {
        print("Health at start: " + health);
    }
    public void Attack() {
        int damageToInflic = GetAttackDamage(shieldOn, shieldAmt, attackPower);
        health -= attackPower;
        print("Health After Attack: " + health);
    }

    public void Heal() {
        int healAmount = GetRandomNumber();
        health += healAmount;
        print("Received " + healAmount + " health");
        print("You now have " + health + " health");
    }

    private int GetAttackDamage(bool isShieldOn, int theShieldAmmt, int theAttackPower) {
        int damage = 0;

        if (isShieldOn) {
            damage = theAttackPower - (int)((float)theShieldAmmt * .10f);
        } else {
            damage = theAttackPower;
        }
        return damage;
    }

    private int GetRandomNumber() {
        return Random.Range(2, 10);
    }
}

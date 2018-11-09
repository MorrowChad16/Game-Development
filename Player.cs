using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : Humanoid {

    int spinAttackDamage = 10;

    public override int Attack() {
        return attackDamage + spinAttackDamage;
    }
}

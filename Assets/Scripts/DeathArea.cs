using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour {

    public Transform hitbox;

    // Update is called once per frame
    void Update() {
        if (GameManager.singleton.isGameActive) {
            if (Player.singleton != null && !Player.singleton.isInProtectionArea) {
                if (GameManager.collides(Player.singleton.hitBox, hitbox)) {
                    Player.singleton.die();
                }
            }
        }
    }
}

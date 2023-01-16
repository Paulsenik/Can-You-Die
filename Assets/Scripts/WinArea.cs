using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinArea : MonoBehaviour {

    public Transform hitbox;

    // Update is called once per frame
    void Update() {
        if (GameManager.singleton.isGameActive) {
            if (Player.singleton != null) {
                if (GameManager.collides(Player.singleton.hitBox, hitbox)) {
                    GameManager.singleton.startNextLevel();
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProtectionArea : MonoBehaviour {

    public Transform circleHitBox;
    public LayerMask collisionWithPlayer;

    void Update() {
        if (GameManager.singleton.isGameActive) {
            if (Player.singleton != null) {
                Player.singleton.isInProtectionArea = Physics2D.OverlapCircle(circleHitBox.position, circleHitBox.localScale.x / 2, collisionWithPlayer, 0);
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(circleHitBox.position, circleHitBox.localScale.x / 2);
    }
}

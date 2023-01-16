using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float maxDist, speedMultiplier;
    public Transform target;

    void FixedUpdate() {
        if (GameManager.singleton.isGameActive) {
            if (target != null) {
                Vector2 moveDir = target.position - transform.position;
                if (moveDir.magnitude > maxDist) {
                    Vector2 camMove = moveDir - moveDir.normalized * maxDist;
                    transform.Translate(camMove * speedMultiplier * Time.deltaTime);
                }
            }
        }
    }

    public void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, maxDist);
    }
}

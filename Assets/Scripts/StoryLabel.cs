using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLabel : MonoBehaviour {

    public Transform trigger;
    public LayerMask playerMask;

    private Animator anim;
    private bool hasBeenActivated = false;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (GameManager.singleton.isGameActive && !hasBeenActivated) {
            if (Physics2D.OverlapBox(trigger.position, trigger.localScale, 0, playerMask)) {
                Debug.Log("show story");
                anim.SetBool("Shown", true);
                hasBeenActivated = true;
            }
        }
    }

    public void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(trigger.position, trigger.localScale);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour {

    public Transform activationArea;
    public LayerMask colliderMask;
    public UnityEvent stateChangedEvent;

    private bool isPressed = false;

    void Update() {
        if (Physics2D.OverlapBox(activationArea.position,activationArea.localScale,0,colliderMask)) {
            if (!isPressed) {
                isPressed = true;
                stateChangedEvent.Invoke();
            }
        }else if (isPressed) {
            isPressed = false;
            stateChangedEvent.Invoke();
        }
    }
}

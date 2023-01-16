using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Text deathCount;

    void Update() {
        if (GameManager.singleton.isGameActive) {
            deathCount.text = "" + GameManager.singleton.deaths;
        }
    }
}

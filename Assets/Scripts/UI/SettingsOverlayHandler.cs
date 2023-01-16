using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsOverlayHandler : MonoBehaviour {

    public static SettingsOverlayHandler singleton;

    public Slider soundSlider;
    public Slider musicSlider;

    private void Awake() {

        if (singleton == null) {
            singleton = this;
            gameObject.SetActive(false);
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void toggleSettingsOverlay(bool b) {
        Debug.Log(b + " Settings");
        gameObject.SetActive(b);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIController : MonoBehaviour {

    public Canvas aboutOverlay, levelSelectionOverlay;

    public void toggleAboutOverlay(bool b) {
        Debug.Log(b + " About");
        aboutOverlay.gameObject.SetActive(b);
    }

    public void toggleSettingsOverlay(bool b) {
        Debug.Log(b + " Settings");
        SettingsOverlayHandler.singleton.gameObject.SetActive(b);
    }

    public void toggleLevelSelectionOverlay(bool b) {
        Debug.Log(b + " levelSelection");
        levelSelectionOverlay.gameObject.SetActive(b);
    }

}

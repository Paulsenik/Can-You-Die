using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {
    public void playSound(string s) {
        AudioManager.instance.play(s);
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager singleton;

    [Header("Menu")]
    public string mainMenu;
    public bool returnToMenuAfterLastLevel = false;

    [Header("Audio")]
    public SettingsOverlayHandler settings;
    public AudioMixer mixer;

    [Header("Attributes")]
    public List<string> scenesInLoadingOrder = new List<string>();
    public KeyCode settingsKey = KeyCode.Escape;
    [HideInInspector]
    public int deaths = 0;
    public bool isGameActive { get { return GAMEACTIVE; } }
    private bool GAMEACTIVE = true;

    public void resetDeaths() {
        deaths = 0;
    }

    public void Awake() {
        if (singleton == null)
            singleton = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void Update() {
        if (Input.GetKeyDown(settingsKey)) {
            toggleSettingsVisible();
        }
        GAMEACTIVE = !SettingsOverlayHandler.singleton.gameObject.active;
    }

    public void playSound(string s) {
        AudioManager.instance.play(s);
    }

    public void setSoundLvl(float volume) {
        mixer.SetFloat("SoundVol", volume);
    }
    public void setMusicLvl(float volume) {
        mixer.SetFloat("MusicVol", volume);
    }

    public void loadMenu() {
        MenuUIController mui = GameObject.FindObjectOfType<MenuUIController>();
        if (mui == null) {
            deaths = 0;
            setSettingsVisible(false);
            SceneManager.LoadScene(mainMenu);
        } else {
            mui.toggleAboutOverlay(false);
            mui.toggleSettingsOverlay(false);
            mui.toggleLevelSelectionOverlay(false);
        }
    }

    public void resetLevelAndRestart() {
        MenuUIController mui = GameObject.FindObjectOfType<MenuUIController>();
        if (mui == null) {
            Destroy(DeathShower.singleton.gameObject);
            playSound("LevelCompleted");
            restartLevel();
            deaths = 0;
        } else {
            loadMenu();
        }
    }

    public void setGameActive() {
        GAMEACTIVE = true;
        Debug.Log("active");
    }

    public void toggleSettingsVisible() {
        setSettingsVisible(isGameActive);
    }

    private void setSettingsVisible(bool visible) {
        GAMEACTIVE = !visible; // Updates
        if (Player.singleton != null)
            if (visible) {
                Player.singleton.rb.Sleep();
            } else {
                Player.singleton.rb.WakeUp();
            }
        SettingsOverlayHandler.singleton.gameObject.SetActive(visible);
    }

    // is only called by Player
    public void restartLevel() {
        deaths++;
        Invoke("reloadLevel", 1.5f);
    }

    // restarts
    private void reloadLevel() {
        setSettingsVisible(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void startLevel(int index) {
        setSettingsVisible(false);
        if (index >= scenesInLoadingOrder.Count) {
            Debug.Log("GameManager :: No next Scene -> Return to start");
            if (returnToMenuAfterLastLevel) {
                loadMenu();
                return;
            }
            SceneManager.LoadScene(scenesInLoadingOrder[0]);
        } else {
            Debug.Log("GameManager :: Next Scene -> " + scenesInLoadingOrder[index]);
            SceneManager.LoadScene(scenesInLoadingOrder[index]);
        }
    }

    public void startNextLevel() {
        Debug.Log("GameManager :: load next scene");
        Destroy(DeathShower.singleton.gameObject);
        deaths = 0;
        playSound("LevelCompleted");

        int index = getIndexOfScene(SceneManager.GetActiveScene().name);
        startLevel(index + 1);
    }

    private int getIndexOfScene(string name) {
        for (int i = 0; i < scenesInLoadingOrder.Count; i++) {
            if (scenesInLoadingOrder[i].Equals(name))
                return i;
        }
        return -1;
    }
    public static bool collides(Transform a, Transform b) {
        return new Bounds(a.position, a.localScale).Intersects(new Bounds(b.position, b.localScale));
    }

}

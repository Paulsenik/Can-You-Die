using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;

    public AudioMixer mixer;

    public Sound[] sounds;

    void Awake() {

        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioMixerGroup;
        }

    }

    private void Start() {
        play("Theme");
    }

    public void play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null && s.source != null)
            s.source.Play();
        else {
            Debug.Log("AudioManager :: couldn't play audio " + s );
        }
    }

    [Serializable]
    public class Sound {

        public String name;

        public AudioClip clip;
        public AudioMixerGroup audioMixerGroup;

        [Range(0f, 1f)]
        public float volume;
        [Range(0.1f, 3f)]
        public float pitch;

        public bool loop;

        [HideInInspector]
        public AudioSource source;
    }

}

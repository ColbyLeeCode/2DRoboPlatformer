using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public AudioMixerGroup audioMixerGroup;
    private AudioSource source;

    public string clipName;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0f, 2f)]
    public float pitch;

    public bool loop = false;
    public bool playOnAwake = false;

    public void setSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.volume = volume;
        source.playOnAwake = playOnAwake;
        source.loop = loop;
        source.pitch = pitch;
        source.outputAudioMixerGroup = audioMixerGroup;
    }

    public void Play()
    {
        source.Play();
    }

   
}

public class AudioManager : MonoBehaviour
{

    public AudioMixer audioMixer;

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", level);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", level);
    }

    public void SetSFXVolume(float level)
    {
        audioMixer.SetFloat("SFXVolume", level);
    }

    public static AudioManager instance;

    [SerializeField]
    Sound[] sound;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        for(int i = 0; i < sound.Length; i++)
        {
            GameObject go = new GameObject("Sound_" + i + "_" + sound[i].clipName);
            go.transform.SetParent(this.transform);
            sound[i].setSource(go.AddComponent<AudioSource>());
        }

        PlaySound("background");
    }

    public void PlaySound(string name)
    {
        for(int i = 0; i < sound.Length; i++)
        {
            if(sound[i].clipName == name)
            {
                sound[i].Play();
                return;
            }
        }
    }

    public Sound GetSound(int id)
    {
        return sound[id];
    }

}

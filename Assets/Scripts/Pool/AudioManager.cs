using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
     [SerializeField] private List<Clips> _audioClipsses;

    [SerializeField] private List<Source> _audioSourcees;

    public static AudioManager Instance = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }

        Instance = this;
    }

    public AudioClip GetSound(string key)
    {
        foreach (var clipss in _audioClipsses)
        {
            if (clipss.Key == key)
            {
                return clipss.AudioClip;
            }
        }
        
        Debug.LogError("No sound");
        return null;
    }

    public void GetSound(string key, Vector3 position)
    {
        foreach (var audioSourcee in _audioSourcees)
        {
            if (audioSourcee.Key == key)
            {
                var soundEffect = Instantiate(audioSourcee.AudioSource);
                soundEffect.transform.position = position;

                soundEffect.clip = GetSound(key);
                soundEffect.Play();

                Destroy(soundEffect.gameObject, 2f);
                return;
            }
        }
        
    }
}

[Serializable]
public struct Clips
{
    public string Key;
    public AudioClip AudioClip;
}

[Serializable]
public struct Source
{
    public string Key;
    public AudioSource AudioSource;
}

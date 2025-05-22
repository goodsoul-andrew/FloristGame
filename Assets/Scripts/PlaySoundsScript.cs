using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlaySoundsScript : MonoBehaviour
{
    [SerializeField] private SoundsGroup[] SoundsGroups;
    private Dictionary<string, int> namesToIndexes = new Dictionary<string, int>();
    private AudioSource audioSource;

    private AudioClip currentLoopedSound;
    private string currentLoopedGroupName;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        for (var i = 0; i < SoundsGroups.Length; i++)
        {
            namesToIndexes[SoundsGroups[i].Name] = i;
        }
    }

    public void PlaySound(int numberOfGroup)
    {
        StopLoopedSound();
        audioSource.PlayOneShot(SoundsGroups[numberOfGroup].Sounds[Random.Range(0, SoundsGroups[numberOfGroup].Sounds.Length)]);
    }

    public void PlaySound(string nameOfGroup)
    {
        StopLoopedSound();
        var ind = namesToIndexes[nameOfGroup];
        audioSource.PlayOneShot(SoundsGroups[ind].Sounds[Random.Range(0, SoundsGroups[ind].Sounds.Length)]);
    }

    public void PlayLoopedSound(int numberOfGroup)
    {
        StopLoopedSound();

        currentLoopedSound = SoundsGroups[numberOfGroup].Sounds[Random.Range(0, SoundsGroups[numberOfGroup].Sounds.Length)];
        currentLoopedGroupName = SoundsGroups[numberOfGroup].Name;

        audioSource.clip = currentLoopedSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayLoopedSound(string nameOfGroup)
    {
        StopLoopedSound();

        var ind = namesToIndexes[nameOfGroup];
        currentLoopedSound = SoundsGroups[ind].Sounds[Random.Range(0, SoundsGroups[ind].Sounds.Length)];
        currentLoopedGroupName = SoundsGroups[ind].Name;

        audioSource.clip = currentLoopedSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopLoopedSound()
    {
        if (audioSource.isPlaying && audioSource.loop)
        {
            audioSource.Stop();
        }
        audioSource.clip = null;
        audioSource.loop = false;
        currentLoopedSound = null;
        currentLoopedGroupName = null;

    }

    public void StopSound()
    {
        StopLoopedSound();
        audioSource.Stop();
    }
}

[System.Serializable]
public class SoundsGroup
{
    public string Name;
    public AudioClip[] Sounds;
}
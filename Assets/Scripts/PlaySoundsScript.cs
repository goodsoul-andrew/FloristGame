using UnityEngine;
using UnityEngine.Audio;

public class PlaySoundsScript : MonoBehaviour
{
    [SerializeField] private SoundsGroup[] SoundsGroups;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int numberOfGroup)
    { 
        audioSource.PlayOneShot(SoundsGroups[numberOfGroup].Sounds[Random.Range(0,SoundsGroups[numberOfGroup].Sounds.Length)]);
    }
}

[System.Serializable]
public class SoundsGroup
{
    public string Name;
    public AudioClip[] Sounds;

}

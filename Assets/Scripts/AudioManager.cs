using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip music1;
    [SerializeField] AudioClip music2;

    AudioClip[] clips = new AudioClip[2];
    AudioSource source;

    private void Start()
    {
        clips[0] = music1;
        clips[1] = music2;
        source = GetComponent<AudioSource>();
    }

    int currSong = 0;

    private void Update()
    {
        Invoke("WaitAndSwitchSong", source.clip.length);
    }

    public void WaitAndSwitchSong()
    {
        if (currSong++ >= 2)
        {
            currSong = 0;
        }
        source.clip = clips[currSong];
    }

    public IEnumerator PlayNext()
    {
        yield return new WaitForSeconds(60);
    }
}

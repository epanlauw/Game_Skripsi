using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioClip[] music;
    public float volume, pitch;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySong(int index)
    {
        AudioManager.instance.PlaySong(music[index]);
    }

    public void PlaySFX(int index)
    {
        AudioManager.instance.PlaySFX(clips[index]);
    }
}

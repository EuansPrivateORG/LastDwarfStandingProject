using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    public List<AudioClip> audioClips = new List<AudioClip>();


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponentInParent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private AudioClip GetAudioClip(string audioClipName)
    {
        if(audioClips.Count > 0)
        {
            foreach(AudioClip clip in audioClips)
            {
                if (clip.name == audioClipName) return clip;
                
            }
        } 
        return null;    
    }
    public void PlayAudioClip(string audioClipName)
    {

        AudioClip clip = GetAudioClip(audioClipName);
        if(clip != null)
        {
        _audioSource.clip = clip;
        _audioSource.Play();
        }
        else
        {
            Debug.Log("Audio Clip " + audioClipName + " not Found");
        }

    }
}

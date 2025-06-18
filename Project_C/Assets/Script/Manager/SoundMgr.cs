using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    [SerializeField]
    AudioClip[] Clip;

    AudioSource AudioSource;
    private void Awake()
    {
        Shared.SoundMgr = this;
    }

    public void PlayEffect(AudioClip _audioClip)
    {
        AudioSource.PlayOneShot(_audioClip);
    }
}

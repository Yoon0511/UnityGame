using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMgr : MonoBehaviour
{
    public AudioMixer Mixer;
    public AudioSource SfxSource;
    public List<AudioClip> SfxClips;
    private Dictionary<string, AudioClip> Dicsfx;

    private void Awake()
    {
        Shared.SoundMgr = this;
        DontDestroyOnLoad(this);
        InitializeSfxDict();
    }

    private void InitializeSfxDict()
    {
        Dicsfx = new Dictionary<string, AudioClip>();
        foreach (var clip in SfxClips)
        {
            if (clip != null)
                Dicsfx[clip.name] = clip;
        }
    }
    public void PlaySFX(string clipName)
    {
        if (Dicsfx.TryGetValue(clipName, out AudioClip clip))
        {
            SfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"효과음 '{clipName}'을 찾을 수 없습니다.");
        }
    }

    private float SafeVolume(float volume)
    {
        return Mathf.Clamp(volume, 0.0001f, 1f);
    }

    public void SetMasterVolume(float value)
    {
        Mixer.SetFloat("MasterVolume", Mathf.Log10(SafeVolume(value)) * 20);
    }

    public void SetSfxVolume(float value)
    {
        Mixer.SetFloat("SFXVolume", Mathf.Log10(SafeVolume(value)) * 20);
    }

    public void SetBGMVolume(float value)
    {
        Mixer.SetFloat("BGMVolume", Mathf.Log10(SafeVolume(value)) * 20);
    }
}

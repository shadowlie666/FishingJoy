using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public AudioSource bgmAudioSourece;
    public AudioClip seaWaveClip;
    public AudioClip goldClip;
    public AudioClip rewardClip;
    public AudioClip fireClip;
    public AudioClip changeClip;
    public AudioClip lvUpClip;
    private bool isMute = false;

    public bool IsMute
    {
        get
        {
            return isMute;
        }
    }

    private void Awake()
    {
        _instance = this;
        isMute = (PlayerPrefs.GetInt("mute", 0) == 0)?false:true;
        DoMute();
    }

    public void SwitchMuteState(bool isOn)
    {
        isMute = !isOn;
        DoMute();
    }

    void DoMute()
    {
        if (isMute)
        {
            bgmAudioSourece.Pause();
        }
        else
        {
            bgmAudioSourece.Play();
        }
    }

    public void PlayEffectSound(AudioClip clip)
    {
        if(!isMute)
        {
            AudioSource.PlayClipAtPoint(clip,Vector3.zero);
        }
        
    }
}

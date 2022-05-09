using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AudioManager : Singleton<AudioManager>
{
    //音效播放器
    public AudioSource SoundPlayer;
    public AudioSource SoundSword;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    //播放亚索音效
    public void PlayYaSuoSound(string name)
    {
        if(SoundPlayer != null)
        {
            AudioClip clip = Resources.Load<AudioClip>(name);
            SoundPlayer.clip = clip;
            SoundPlayer.PlayOneShot(clip);
        }
    }

    public void PlayYaSuoSound(AudioClip clip)
    {
        if(SoundPlayer != null)
        {
            SoundPlayer.clip = clip;
            SoundPlayer.PlayOneShot(clip);
        }
    }

    public void PlayRandomYaSuoSound(List<AudioClip> list)
    {
        if (list != null && list.Count > 0)
        {
            int range = Random.Range(0,list.Count);
            SoundPlayer.clip = list[range];
            SoundPlayer.PlayOneShot(list[range]);
        }
    }

    //播放剑音效
    public void PlaySwordSound(string name)
    {
        if(SoundSword != null)
        {
            AudioClip clip = Resources.Load<AudioClip>(name);
            SoundSword.clip = clip;
            SoundSword.PlayOneShot(clip);
        }
    }

    public void PlaySwordSound(AudioClip clip)
    {
        if(SoundSword != null)
        {
            SoundSword.clip = clip;
            SoundSword.PlayOneShot(clip);
        }
    }

    public void PlayRandomSwordSound(List<AudioClip> list)
    {
        if (list != null && list.Count > 0)
        {
            int range = Random.Range(0,list.Count);
            SoundSword.clip = list[range];
            SoundSword.PlayOneShot(list[range]);
        }
    }
}

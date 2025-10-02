using System.Collections;
using System.Collections.Generic;
using RysCorp.Core.Singleton;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    #region VARIAVEIS
    public List<MusicSetup> musicSetups;
    public List<SFXSetup> sfxSetups;

    public AudioSource audioSource;
    #endregion


    #region METODOS
    public void PlayMusicByType(MusicType musicType)
    {
        var music = GetMusicByType(musicType);

        audioSource.clip = music.audio;

        audioSource.Play();
    }

    public MusicSetup GetMusicByType(MusicType musicType)
    {
        return musicSetups.Find(i => i.musicType == musicType);
    }
    
    public void PlaySFXByType(SFXType sfxType)
    {
        var music = GetSFXByType(sfxType);

        audioSource.clip = music.audio;

        audioSource.Play();
    }

    public SFXSetup GetSFXByType(SFXType sfxType)
    {
        return sfxSetups.Find(i => i.sfxType == sfxType);
    }
    #endregion


    #region UNITY-METODOS

    #endregion
}

public enum MusicType
{
    CASUAL,
    BOSS
}

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audio;
}

public enum SFXType
{
    NONE,
    SHOOT,
    HIT,
    KILL,
    COLLECT
}

[System.Serializable]
public class SFXSetup
{
    public SFXType sfxType;
    public AudioClip audio;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    #region VARIAVEIS
    public MusicType musicType;
    public AudioSource audioSource;

    private MusicSetup _currMusicSetup;
    #endregion


    #region METODOS
    public void Play()
    {
        _currMusicSetup = SoundManager.Instance.GetMusicByType(musicType);

        audioSource.clip = _currMusicSetup.audio;
        audioSource.Play();
    }
    #endregion


    #region UNITY-METODOS
    private void Start()
    {
        Play();
    }
    #endregion
}

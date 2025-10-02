using System.Collections;
using System.Collections.Generic;
using RysCorp.Core.Singleton;
using UnityEngine;

public class SFXPool : Singleton<SFXPool>
{
    #region VARIAVEIS
    public int poolSize = 10;

    private List<AudioSource> _audioSourceList;

    private int _index = 0;
    #endregion


    #region METODOS
    private void CreatePool()
    {
        _audioSourceList = new List<AudioSource>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateAudioSourceItem();
        }
    }

    private void CreateAudioSourceItem()
    {
        GameObject go = new GameObject("SFX_Pool");
        go.transform.SetParent(gameObject.transform);
        _audioSourceList.Add(go.AddComponent<AudioSource>());
    }

    public void Play(SFXType sfxType)
    {
        if (sfxType == SFXType.NONE) return;
        var sfx = SoundManager.Instance.GetSFXByType(sfxType);

        _audioSourceList[_index].clip = sfx.audio;
        _audioSourceList[_index].Play();

        if (_index >= _audioSourceList.Count) _index = 0;
        else _index++;
    }
    #endregion


    #region UNITY-METODOS
    protected override void Awake()
    {
        base.Awake();
        CreatePool();
    }
    #endregion
}

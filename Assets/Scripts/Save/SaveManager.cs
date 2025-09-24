using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Items;
using RysCorp.Core.Singleton;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    #region VARIAVEIS
    [SerializeField] private SaveSetup _saveSetup;

    public List<string> Files;

    public int lastLevel;

    //public Action<SaveSetup, int> FileLoaded;
    public Action<List<string>> FileLoaded;

    public int currentSave = 0;
    #endregion


    #region METODOS

    #region SAVE
    [NaughtyAttributes.Button]
    private void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJson);

        SaveFile(setupToJson);
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItems();
        Save();
    }

    public void SaveName(string name)
    {
        _saveSetup.playerName = name;
        Save();
    }

    public void SaveItems()
    {
        _saveSetup.coins = Items.ItemsManager.Instance.GetItemByType(ItemType.COIN).soInt.count;
        _saveSetup.health = Items.ItemsManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt.count;
        _saveSetup.munition = Items.ItemsManager.Instance.GetItemByType(ItemType.MUNITION).soInt.count;
        _saveSetup.kills = Items.ItemsManager.Instance.GetItemByType(ItemType.KILLS).soInt.count;
    }
    #endregion

    private void SaveFile(string json)
    {
        for (var i = 0; i < Files.Count; i++)
        {
            if (i == _saveSetup.save)
            {
                Files[i] = Application.dataPath + $"/Saves/save{i}.txt";

                File.WriteAllText(Files[i], json);

                Debug.Log(Files[i]);
            }
        }
    }

    [NaughtyAttributes.Button]
    public void LoadFile()
    {
        string fileLoaded = "";

        var _path = Files[_saveSetup.save];

        if (File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);

            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);

            lastLevel = _saveSetup.lastLevel;
        }
        else
        {
            CreateNewSave();
            Save();
        }
        
        FileLoaded?.Invoke(Files);
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.coins = 0;
        _saveSetup.health = 0;
        _saveSetup.munition = 0;
        _saveSetup.kills = 0;
        _saveSetup.playerName = "Chrys";
    }

    public void CallLoadFile()
    {
        Invoke(nameof(LoadFile), .1f);
    }
    #endregion


    #region UNITY-METODOS
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        CallLoadFile();
    }

    private void Update()
    {
        _saveSetup.save = currentSave;
    }
    #endregion
}

[System.Serializable]
public class SaveSetup
{
    public int save;
    public int lastLevel;
    public int coins;
    public int health;
    public int munition;
    public int kills;

    public string playerName;
}
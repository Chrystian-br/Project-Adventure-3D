using System.Collections;
using System.Collections.Generic;
using System.IO;
using Items;
using TMPro;
using UnityEngine;

public class PlayLevel : MonoBehaviour
{
    #region VARIAVEIS
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI lifePackText;
    public TextMeshProUGUI munitionText;
    public TextMeshProUGUI killText;
    public TextMeshProUGUI saveText;

    public int save = 0;
    #endregion


    #region METODOS
    public void OnLoad(List<string> files)
    {
        foreach (var i in files)
        {
            var file = JsonUtility.FromJson<SaveSetup>(File.ReadAllText(i));

            if (file.save == save)
            {
                var setup = file;

                saveText.text = "Save " + save;

                levelText.text = "" + (setup.lastLevel + 1);
                coinText.text = "" + setup.coins;
                lifePackText.text = "" + setup.health;
                munitionText.text = "" + setup.munition;
                killText.text = "" + setup.kills;
            }
        }
        
    }

    public void OnPlay()
    {   
        SaveManager.Instance.currentSave = save;
    }
    #endregion


    #region UNITY-METODOS
    private void Start()
    {
        SaveManager.Instance.FileLoaded += OnLoad;
    }
    
    private void OnDestroy()
    {
        SaveManager.Instance.FileLoaded -= OnLoad;
    }
    #endregion
}

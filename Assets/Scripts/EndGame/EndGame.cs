using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    #region VARIAVEIS
    public List<GameObject> endGameObjects;

    public float duration = .2f;
    public Ease ease = Ease.OutBack;
    public int currentLevel = 1;

    private bool _endGame = false;
    #endregion


    #region METODOS
    private void ShowEndGame()
    {
        _endGame = true;

        foreach (var i in endGameObjects)
        {
            i.SetActive(true);
            i.transform.DOScale(0, duration).SetEase(ease).From();
        }

        SaveManager.Instance.SaveLastLevel(currentLevel);
    }
    #endregion


    #region UNITY-METODOS
    private void Awake()
    {
        endGameObjects.ForEach(i => i.SetActive(false));
    }

    private void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if (!_endGame && p != null)
        {
            ShowEndGame();
        }
    }
    #endregion
}

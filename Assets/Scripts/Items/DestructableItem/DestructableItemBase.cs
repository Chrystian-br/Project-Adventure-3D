using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestructableItemBase : MonoBehaviour
{
    #region VARIAVEIS
    public HealthBase healthBase;

    public float shakeDuration = .1f;
    public int shakeForce = 1;

    public int dropCoinsAmount = 2;
    public GameObject coinPrefab;
    public GameObject dropPosition;
    #endregion


    #region METODOS
    private void OnDamage(HealthBase h)
    {
        transform.DOShakeScale(shakeDuration, Vector3.up / 2, shakeForce);
        DropGroupOfCoins();
    }

    private void DropCoins()
    {
        var i = Instantiate(coinPrefab);
        i.transform.position = dropPosition.transform.position;
        i.transform.DOScale(0, 1f).SetEase(Ease.OutBack).From();
    }

    private void DropGroupOfCoins()
    {
        for (var i = 0; i < dropCoinsAmount; i++)
        {
            DropCoins();
        }
    }
    #endregion


    #region UNITY-METODOS
    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();
        healthBase.OnDamage += OnDamage;
    }
    #endregion
}

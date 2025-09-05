using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    #region VARIAVEIS
    public float startLife = 100f;
    public float _currentLife;

    public bool destroyOnKill = false;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    public List<UIFillUpdater> uIHealthUpdater;
    #endregion


    #region METODOS
    public void ResetLife()
    {
        _currentLife = startLife;
    }

    public void RecoverLife(int value)
    {
        _currentLife += value;
    }

    protected virtual void Kill()
    {
        if (destroyOnKill) Destroy(gameObject, 3f);

        OnKill?.Invoke(this);
    }

    public void Damage(float f)
    {
        _currentLife -= f;

        if (_currentLife <= 0)
        {
            Kill();
        }
        UpdateUI();
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    public void UpdateUI()
    {
        if (uIHealthUpdater != null)
        {
            uIHealthUpdater.ForEach(i => i.UpdateValue((float)_currentLife / startLife));
        }
    }
    #endregion


    #region UNITY-METODOS
    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }
    #endregion
}

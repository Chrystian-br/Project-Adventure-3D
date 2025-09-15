using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cloth;

public class HealthBase : MonoBehaviour, IDamageable
{
    #region VARIAVEIS
    public float startLife = 100f;
    public float _currentLife;

    public bool destroyOnKill = false;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    public List<UIFillUpdater> uIHealthUpdater;
    
    public float damageMultiplier = 1f;
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
        _currentLife -= f * damageMultiplier;

        if (_currentLife <= 0)
        {
            Kill();
        }
        UpdateUI();
        OnDamage?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void DebugDamage()
    {
        Damage(5);
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

    
    public void ChangeDamageMultiplier(float damageMultiplier, float duration)
    {
        StartCoroutine(ChangeDamageMultiplierCoroutine(damageMultiplier, duration));
    }

    IEnumerator ChangeDamageMultiplierCoroutine(float damageMultiplier, float duration)
    {
        this.damageMultiplier = damageMultiplier;

        yield return new WaitForSeconds(duration);

        this.damageMultiplier = 1;
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

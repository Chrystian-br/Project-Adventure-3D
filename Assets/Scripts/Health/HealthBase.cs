using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    #region VARIAVEIS
    public float startLife = 100f;
    [SerializeField] private float _currentLife;

    public bool destroyOnKill = false;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;
    #endregion


    #region METODOS
    protected void ResetLife()
    {
        _currentLife = startLife;
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

        OnDamage?.Invoke(this);
    }
    #endregion 
     
     
    #region UNITY-METODOS
     
    #endregion
}

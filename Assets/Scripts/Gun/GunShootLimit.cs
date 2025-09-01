using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    #region VARIAVEIS
    public List<UIFillUpdater> uIGunUpdaters;

    public float maxShoot = 5f;
    public float timeToRecharge = 1f;

    private float _currentShoots;
    private bool _recharging = false;
    #endregion


    #region METODOS
    protected override IEnumerator ShootCoroutine()
    {
        if(_recharging) yield break;

        while(true)
        {
            if(_currentShoots < maxShoot)
            {
                Shoot();
                _currentShoots++;
                CheckRecharge();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
        }
    }

    private void CheckRecharge()
    {
        if(_currentShoots >= maxShoot)
        {
            StopShoot();
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;

        while(time < timeToRecharge)
        {
            time += Time.deltaTime;
            Debug.Log("Recharging:" + time + "s");
            uIGunUpdaters.ForEach(i => i.UpdateValue(time/timeToRecharge));
            yield return new WaitForEndOfFrame();
        }

        _currentShoots = 0;
        _recharging = false;
    }

    private void UpdateUI()
    {
        uIGunUpdaters.ForEach(i => i.UpdateValue(maxShoot, _currentShoots));
    }
        

    private void GetAllUIs()
    {
        var allUIFillUpdaters = GameObject.FindObjectsOfType<UIFillUpdater>().ToList();

        for (var i = 0; i < allUIFillUpdaters.Count; i++)
        {
            if (allUIFillUpdaters[i].name == "BulletBar")
            {
                uIGunUpdaters.Add(allUIFillUpdaters[i]);
            }
        }
    }
    #endregion


    #region UNITY-METODOS
    public void Awake()
    {
        GetAllUIs();
    }
    #endregion
}

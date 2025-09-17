using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    #region VARIAVEIS
    public GunBase gunBase;
    public Transform gunPosition;

    public List<GunBase> listGunBase;
    public FlashColor flashColor;

    private GunBase _currentGun;
    #endregion


    #region METODOS
    protected override void Init()
    {
        base.Init();

        CreateGun();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();

        inputs.Gameplay.ChangeGun1.performed += ctx => ChangeGun(0);
        inputs.Gameplay.ChangeGun2.performed += ctx => ChangeGun(1);
        inputs.Gameplay.ChangeGun3.performed += ctx => ChangeGun(2);
    }

    private void CreateGun()
    {
        _currentGun = Instantiate(gunBase, gunPosition);
        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
    }

    private void StartShoot()
    {
        _currentGun.StartShoot();
        ShakeCamera.Instance.Shake();
        flashColor?.Flash();
    }

    private void CancelShoot()
    {
        _currentGun.StopShoot();
    }


    private void ChangeGun(int n)
    {
        if (_currentGun != null)
        {
            Destroy(_currentGun.gameObject);

            _currentGun = Instantiate(listGunBase[n], gunPosition);
            _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
        }
    }
    #endregion


    #region UNITY-METODOS
    #endregion
}

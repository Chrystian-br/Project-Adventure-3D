using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChestBase : MonoBehaviour
{
    #region VARIAVEIS
    public Animator anim;

    public string TriggerOpen = "Open";
    public string TriggerClose = "Close";
    public KeyCode openChestKeyCode = KeyCode.O;

    [Header("Notification")]
    public GameObject notification;
    public float tweenDuration = .2f;
    public Ease tweenEase = Ease.OutBack;
    
    [Space]
    public List<ChestItemBase> chestItems;

    private float _startScale;
    private bool _isOpened = false;
    #endregion


    #region METODOS
    [NaughtyAttributes.Button]
    public void OpenChest()
    {
        anim.SetTrigger(TriggerOpen);
        
    }

    [NaughtyAttributes.Button]
    public void CloseChest()
    {
        anim.SetTrigger(TriggerClose);
    }

    public void ShowNotification()
    {
        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(_startScale, tweenDuration);
    }

    public void HideNotification()
    {

        notification.SetActive(false);
    }

    private void ShowItem()
    {
        foreach (var i in chestItems)
        {
            i.ShowItem();
        }
    }

    private void CollectItem()
    {
        foreach (var i in chestItems)
        {
            i.Collect();
        }
    }
    #endregion


    #region UNITY-METODOS
    public void Start()
    {
        _startScale = notification.transform.localScale.x;

        ShowItem();
    }

    public void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if (p != null)
        {
            ShowNotification();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if (p != null)
        {
            notification.transform.DOScale(0, tweenDuration).From(_startScale);
            Invoke(nameof(HideNotification), tweenDuration);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(openChestKeyCode) && notification.activeSelf)
        {
            if (!_isOpened)
            {
                OpenChest();
                Invoke(nameof(CollectItem), 1.5f);
                _isOpened = true;
            }
            else
            {
                CloseChest();
                _isOpened = false;
            } 
        }
    }
    #endregion
}

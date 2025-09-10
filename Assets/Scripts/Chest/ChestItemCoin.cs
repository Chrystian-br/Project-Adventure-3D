using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Items;

public class ChestItemCoin : ChestItemBase
{
    #region VARIAVEIS
    public int coinAmount = 5;
    public GameObject coinObject;

    private List<GameObject> _items = new List<GameObject>();

    public Vector2 randomRange = new Vector2(-.5f, .5f);

    public float tweenEndTime = .5f;
    #endregion


    #region METODOS

    public override void ShowItem()
    {
        base.ShowItem();
        CreateItems();
    }

    private void CreateItems()
    {
        for (int i = 0; i < coinAmount; i++)
        {
            var item = Instantiate(coinObject);

            item.transform.position = transform.position + (Vector3.forward * Random.Range(randomRange.x, randomRange.y)) + (Vector3.right * Random.Range(randomRange.x, randomRange.y));
            item.transform.DOScale(0, .2f).SetEase(Ease.OutBack).From();
            _items.Add(item);
        }
    }

    [NaughtyAttributes.Button]
    public override void Collect()
    {
        base.Collect();
        if (_items != null)
        {
            foreach (var i in _items)
            {
                i.transform.DOMoveY(2f, tweenEndTime).SetRelative();
                i.transform.DOScale(0, tweenEndTime / 2).SetDelay(tweenEndTime / 2);
                ItemsManager.Instance.AddItemByType(ItemType.COIN);

                StartCoroutine(DestroyItems(i));
            }
        }
    }

    IEnumerator DestroyItems(GameObject item)
    {
        yield return new WaitForSeconds(tweenEndTime + .2f);

        Destroy(item);
        _items = null;
    }
    #endregion


    #region UNITY-METODOS

    #endregion
}

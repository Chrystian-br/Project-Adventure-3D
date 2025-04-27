using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIGunUpdater : MonoBehaviour
{
    #region VARIAVEIS
        public Image uiImage;

        [Header("Animation")]
        public float duration = .1f;
        public Ease ease = Ease.Linear;

        private Tween _currTween;
    #endregion
     
     
    #region METODOS
        public void UpdateValue(float f)
        {
            uiImage.fillAmount = f;
        }

        public void UpdateValue(float max, float current)
        {
            if(_currTween != null) _currTween.Kill();
            uiImage.DOFillAmount(1 - (current/max), duration).SetEase(ease);
        }
    #endregion


    #region UNITY-METODOS
        public void OnValidate()
        {
            if(uiImage == null) uiImage = GetComponent<Image>();
        }
    #endregion
}

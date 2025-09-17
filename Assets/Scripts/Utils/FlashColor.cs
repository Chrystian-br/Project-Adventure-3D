using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FlashColor : MonoBehaviour
{
    #region VARIAVEIS
    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    public Color color = Color.red;
    public float duration = .1f;

    public string colorParameter = "_EmissionColor";

    private Color _defaultColor;
    private Tween _currTween;
    #endregion


    #region METODOS
    [NaughtyAttributes.Button]
    public void Flash(Color c = default)
    {
        if (c == default) c = color;

        if (meshRenderer != null && !_currTween.IsActive())
        {
            _currTween = meshRenderer.material.DOColor(c, colorParameter, duration).SetLoops(2, LoopType.Yoyo);
        }

        if (skinnedMeshRenderer != null && !_currTween.IsActive())
        {
            _currTween = skinnedMeshRenderer.material.DOColor(c, colorParameter, duration).SetLoops(2, LoopType.Yoyo);
        }
    }
    #endregion


    #region UNITY-METODOS
    public void OnValidate()
    {
        if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null) skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }
    #endregion
}

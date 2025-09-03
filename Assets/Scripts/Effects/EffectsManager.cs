using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using RysCorp.Core.Singleton;

public class EffectsManager : Singleton<EffectsManager>
{
    #region VARIAVEIS
    public PostProcessVolume processVolume;
    public float duration = 1;

    [SerializeField] private Vignette _vignette;
    #endregion


    #region METODOS
    [NaughtyAttributes.Button]
    public void ChangeVignetteColor()
    {
        StartCoroutine(FlashColorVignette());
    }

    IEnumerator FlashColorVignette()
    {
        Vignette tmp;

        if (processVolume.profile.TryGetSettings<Vignette>(out tmp))
        {
            _vignette = tmp;
        }

        ColorParameter c = new ColorParameter();

        float time = 0;

        while (time < duration)
        {
            c.value = Color.Lerp(Color.black, Color.red, time / duration);
            time += Time.deltaTime;
            _vignette.color.Override(c);

            yield return new WaitForEndOfFrame();
        }
        time = 0;
        while (time < duration)
        {
            c.value = Color.Lerp(Color.red, Color.black, time / duration);
            time += Time.deltaTime;
            _vignette.color.Override(c);

            yield return new WaitForEndOfFrame();
        }
    }

    [NaughtyAttributes.Button]
    public void ChangeVignetteIntensity(FloatParameter intense)
    {
        _vignette.intensity.Override(intense);
    }
    #endregion


    #region UNITY-METODOS

    #endregion
}

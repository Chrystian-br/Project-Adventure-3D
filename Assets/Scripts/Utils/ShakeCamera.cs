using System.Collections;
using System.Collections.Generic;
using RysCorp.Core.Singleton;
using Cinemachine;
using UnityEngine;

public class ShakeCamera : Singleton<ShakeCamera>
{
    #region VARIAVEIS
    public CinemachineVirtualCamera virtualCamera;

    public float shakeTime;

    [Header("Shake Values")]
    public float amplitude = 3f;
    public float frequency = 3f;
    public float time = .2f;
    #endregion


    #region METODOS
    [NaughtyAttributes.Button]
    public void Shake()
    {
        Shake(amplitude, frequency, time);
    }

    public void Shake(float amplitude, float frequency, float time)
    {
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;

        shakeTime = time;
    }
    #endregion


    #region UNITY-METODOS
    private void Update()
    {
        if(shakeTime > 0){
            shakeTime -= Time.deltaTime;
        } else {
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
        }
    }
    #endregion
}

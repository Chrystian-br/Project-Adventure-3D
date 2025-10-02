using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArea : MonoBehaviour
{
    #region VARIAVEIS
    public GameObject boss;
    public bool lookAtPlayer = false;

    public GameObject bossCamera;
    
    public Color gizmoColor = Color.yellow;
    public GameObject bossHealthBar;

    public MusicPlayer music;
    #endregion


    #region METODOS
    private void Awake()
    {
        TurnCameraOff();
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            boss.SetActive(true);
            lookAtPlayer = true;

            TurnCameraOn();

            bossHealthBar.SetActive(true);

            music.musicType = MusicType.BOSS;
            music.Play();
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            lookAtPlayer = false;
            TurnCameraOff();

            music.musicType = MusicType.CASUAL;
            music.Play();
        }
    }

    private void TurnCameraOn()
    {
        bossCamera.SetActive(true);

        Invoke(nameof(TurnCameraOff), 2f);
    }

    private void TurnCameraOff()
    {
        bossCamera.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, transform.localScale.y);
    }
    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RysCorp.StateMachine;
using NaughtyAttributes;
using UnityEngine.Rendering.PostProcessing;

public class Player : MonoBehaviour//, IDamageable
{
    #region VARIAVEIS
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Animator animator;
    public List<Collider> colliders;

    public CharacterController characterController;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    public float jumpSpeed = 15f;

    private float vSpeed = 0f;

    public KeyCode jumpKeyCode = KeyCode.Space;

    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    [Header("Life")]
    public HealthBase healthBase;

    private bool _isAlive = true;

    private FloatParameter _intense = new FloatParameter();
    #endregion


    #region METODOS


    public void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    #region LIFE
    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignetteColor();

        if (h._currentLife < (h.startLife / 1.5))
        {
            _intense.value = 0.35f;

            EffectsManager.Instance.ChangeVignetteIntensity(_intense);
        }
    }

    private void OnKill(HealthBase h)
    {
        if (_isAlive)
        {
            _isAlive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);

            StartCoroutine(timeToRespawn());
        }
    }
    #endregion

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if (CheckPointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckPointManager.Instance.GetPositionFromLastCheckPoint();

            _isAlive = true;
            animator.SetTrigger("Idle");

            healthBase.ResetLife();

            _intense.value = 0.3f;
            EffectsManager.Instance.ChangeVignetteIntensity(_intense);
    
            Imortal();       
        }
    }

    IEnumerator timeToRespawn()
    {
        yield return new WaitForSeconds(2);
        Respawn();
    }

    public void Imortal()
    {
        colliders.ForEach(i => i.enabled = false);
        skinnedMeshRenderer.material.SetColor("_EmissionColor", Color.yellow);

        StartCoroutine(TimeToBecomeMortal());
    }

    IEnumerator TimeToBecomeMortal()
    {
        yield return new WaitForSeconds(5);

        colliders.ForEach(i => i.enabled = true);
        skinnedMeshRenderer.material.SetColor("_EmissionColor", Color.white);
    }
    #endregion


    #region UNITY-METODOS
    public void Awake()
    {
        OnValidate();
        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }


    public void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        if (characterController.isGrounded)
        {
            vSpeed = 0;
            if (Input.GetKeyDown(jumpKeyCode))
            {
                vSpeed = jumpSpeed;
            }
        }


        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        var isWalking = inputAxisVertical != 0;
        if (isWalking)
        {
            if (Input.GetKey(keyRun))
            {
                speedVector *= speedRun;
                animator.speed = speedRun;
            }
            else
            {
                animator.speed = 1;
            }
        }

        characterController.Move(speedVector * Time.deltaTime);

        animator.SetBool("IsWalking", isWalking);
    }
    #endregion
}

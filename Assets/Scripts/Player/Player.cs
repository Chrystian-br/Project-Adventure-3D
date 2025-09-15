using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RysCorp.StateMachine;
using NaughtyAttributes;
using UnityEngine.Rendering.PostProcessing;
using RysCorp.Core.Singleton;
using Cloth;

public class Player : Singleton<Player>//, IDamageable
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
    public Color damageColor = Color.red;

    [Header("Life")]
    public HealthBase healthBase;

    [Space]
    [SerializeField] private ClothChanger _clothChanger;

    private bool _isAlive = true;

    private FloatParameter _intense = new FloatParameter();

    private float _imortalDuration = 5f;
    #endregion


    #region METODOS


    public void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    #region LIFE
    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash(damageColor));
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
        healthBase.ChangeDamageMultiplier(0, _imortalDuration);
        colliders.ForEach(i => i.enabled = true);
        skinnedMeshRenderer.material.SetColor("_EmissionColor", Color.yellow);

        StartCoroutine(TimeToBecomeMortal());
    }

    IEnumerator TimeToBecomeMortal()
    {
        yield return new WaitForSeconds(_imortalDuration);

        skinnedMeshRenderer.material.SetColor("_EmissionColor", Color.white);
    }

    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }

    IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration)
    {
        var defaultSpeed = speed;
        speed *= localSpeed;

        yield return new WaitForSeconds(duration);

        speed = defaultSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        _clothChanger.ChangeTexture(setup);

        yield return new WaitForSeconds(duration);

        _clothChanger.ResetTexture();
    }
    #endregion


    #region UNITY-METODOS
    protected override void Awake()
    {
        base.Awake();
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

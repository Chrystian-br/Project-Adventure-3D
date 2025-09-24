using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;
using Items;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        #region VARIAVEIS
        public Collider coll;

        public float startLife = 10f;
        public bool lookAtPlayer = false;

        public ParticleSystem pSystem;
        public FlashColor flashColor;

        private float _currentLife;
        private Player _player;

        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        [Header("Events")]
        public UnityEvent OnKillEvent;

        #endregion


        #region METODOS
        protected virtual void Init()
        {
            ResetLife();
            if (startWithBornAnimation) BornAnimation();
        }

        protected virtual void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Kill()
        {
            OnKill();
            ItemsManager.Instance.AddItemByType(ItemType.KILLS, 1);
        }

        protected virtual void OnKill()
        {
            if (coll != null) coll.enabled = false;
            PlayAnimationByTrigger(AnimationType.DEATH);
            Destroy(gameObject, 3f);
            OnKillEvent?.Invoke();
        }

        public void OnDamage(float f)
        {
            if (flashColor != null) flashColor.Flash();
            if (pSystem != null) pSystem.Emit(15);

            _currentLife -= f;

            if (_currentLife <= 0)
            {
                Kill();
            }
        }

        public void Damage(float damage)
        {
            OnDamage(damage);
        }

        public void Damage(float damage, Vector3 dir)
        {
            OnDamage(damage);
            transform.DOMove(transform.position - dir, .1f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Player p = collision.transform.GetComponent<Player>();

            if (p != null)
            {
                p.healthBase.Damage(1);
            }
        }

        #region ANIMAÇÕES
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }
        #endregion
        #endregion


        #region UNITY-METODOS
        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            _player = GameObject.FindObjectOfType<Player>();   
        }

        public virtual void Update()
        {
            if (lookAtPlayer)
            {
                transform.LookAt(_player.transform.position);
            }
        }
        #endregion
    }
}


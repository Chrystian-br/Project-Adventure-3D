using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        #region VARIAVEIS
            public Collider coll;

            public float startLife = 10f;

            public ParticleSystem pSystem;
            public FlashColor flashColor;

            private float _currentLife;

            [SerializeField] private AnimationBase _animationBase;

            [Header("Start Animation")]
            public float startAnimationDuration = .2f;
            public Ease startAnimationEase = Ease.OutBack;
            public bool startWithBornAnimation = true;
        #endregion
         
         
        #region METODOS
            protected virtual void Init()
            {
                ResetLife();
                if(startWithBornAnimation) BornAnimation();
            }

            protected virtual void ResetLife()
            {
                _currentLife = startLife;
            }

            protected virtual void Kill()
            {
                
                OnKill();
            }

            protected virtual void OnKill()
            {
                if(coll != null) coll.enabled = false;
                PlayAnimationByTrigger(AnimationType.DEATH);
                Destroy(gameObject, 3f);
            }

            public void OnDamage(float f)
            {
                if(flashColor != null) flashColor.Flash();
                if (pSystem != null) pSystem.Emit(15);

                _currentLife -= f;

                if(_currentLife <= 0)
                {
                    Kill();
                }
            }

            public void Damage(float damage)
            {
                OnDamage(damage);
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
        #endregion
    }
}


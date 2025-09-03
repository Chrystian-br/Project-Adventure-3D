using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RysCorp.StateMachine;
using DG.Tweening;


namespace Boss {
    public enum BossAction
    {
        INIT,
        IDLE,
        ATTACK,
        WALK,
        DEATH
    }

    public class BossBase : MonoBehaviour
    {
        #region VARIAVEIS
        public Transform player;
        public BossArea bossArea;
        public float speed = 5f;
        public List<Transform> waypoints;

        public HealthBase healthBase;
        private StateMachine<BossAction> stateMachine;

        [Header("Animation")]
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBetweenAttacks = .5f;

        #endregion


        #region METODOS
        private void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());
        }

        #region ANIMATION
        public void StartInitAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }
        #endregion

        #region WALK
        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
        {
            while (Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);

                yield return new WaitForEndOfFrame();
            }
            onArrive?.Invoke();
        }
        #endregion

        #region ATTACK
        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(AttackCoroutine(endCallback));
        }

        IEnumerator AttackCoroutine(Action endCallback)
        {
            int attacks = 0;
            while (attacks < attackAmount)
            {
                attacks++;
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBetweenAttacks);
            }
            endCallback?.Invoke();
        }
        #endregion

        private void OnBossKill(HealthBase h)
        {
            stateMachine.SwitchState(BossAction.DEATH);
        }

        public void OnValidate()
        {
            if (healthBase == null) healthBase = GetComponent<HealthBase>();            
        }
        #endregion


        #region UNITY-METODOS
        private void Awake()
        {
            Init();
            OnValidate();
            healthBase.OnKill += OnBossKill;
        }

        private void Start()
        {
            SwitchState(BossAction.WALK);   
        }

        private void Update()
        {
            if(bossArea.lookAtPlayer) transform.LookAt(player);
        }
        #endregion

        #region STATE MACHINE
        public void SwitchState(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }
        #endregion






        #region DEBUG
        [NaughtyAttributes.Button]
        private void SwitchInit()
        {
            SwitchState(BossAction.INIT);
        }

        [NaughtyAttributes.Button]
        private void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }

        [NaughtyAttributes.Button]
        private void SwitchAttack()
        {
            SwitchState(BossAction.ATTACK);
        }
        #endregion
    }
    
}

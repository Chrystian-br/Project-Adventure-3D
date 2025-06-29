using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RysCorp.StateMachine;

public class Player : MonoBehaviour, IDamageable
{
    #region VARIAVEIS
        public Animator animator;

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
    #endregion


    #region METODOS
        #region LIFE
        public void Damage(float damage)
        {
            flashColors.ForEach(i => i.Flash());
        }

        public void Damage(float damage, Vector3 dir)
        {
            Damage(damage);
        }
        #endregion
    #endregion


    #region UNITY-METODOS
        public void Update()
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

            var inputAxisVertical = Input.GetAxis("Vertical");
            var speedVector = transform.forward * inputAxisVertical * speed;

            if(characterController.isGrounded)
            {
                vSpeed = 0;
                if(Input.GetKeyDown(jumpKeyCode))
                {
                    vSpeed = jumpSpeed;
                }
            }
            

            vSpeed -= gravity * Time.deltaTime;
            speedVector.y = vSpeed;

            var isWalking = inputAxisVertical != 0;
            if(isWalking)
            {
                if(Input.GetKey(keyRun))
                {
                    speedVector *= speedRun;
                    animator.speed = speedRun;
                } else 
                {
                    animator.speed = 1;
                }
            }

            characterController.Move(speedVector * Time.deltaTime);

            animator.SetBool("IsWalking", isWalking);
        }
    #endregion
}

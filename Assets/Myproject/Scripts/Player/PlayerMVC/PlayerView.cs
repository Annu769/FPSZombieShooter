using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace FPSZombie.player
{
    public class PlayerView : MonoBehaviour,PlayeryIDamagable
    {
        PlayerController playerController;
        private float horizontalInput;
        private float verticalInput;
        private Vector3 velocity;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundmask;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Animator animator;
        [SerializeField] private Camera mycamera;
        private void Start()
        {
            characterController = GetComponent<CharacterController>();
           
        }
        private void Update()
        {
            if (animator.GetBool("IsShooting"))
            {
                animator.SetBool("IsShooting", false);
            }
            if(animator.GetBool("IsRunning"))
            {
                animator.SetBool("IsRunning",false);
            }
            HandleInput();
            
            if (horizontalInput != 0 || verticalInput != 0)
            {
                animator.SetBool("IsRunning", true);
                playerController.PlayerMovement(horizontalInput, verticalInput);
                
            }
            playerController.PlayerGravity();
        }
        public void SetController(PlayerController _playerController)
        {
            playerController = _playerController;
        }
        private void HandleInput()
        {  
            // player movement input

            horizontalInput = Input.GetAxis("Horizontal");

            verticalInput = Input.GetAxis("Vertical");

            if (Input.GetButtonDown("Jump") && playerController.GetIGrounded())
            {
                playerController.PlayerJump();
            }
        }
        public void TakeDamage(int _damage)
        {
            playerController.TakeDamage(_damage);
        }
        public void Heal(int healamount)
        {
            playerController.Heal(healamount);
        }
        public void AddAmmo(int ammoAmount)
        {
            playerController.AddAmmo(ammoAmount);
        }
        public CharacterController GetCharacterController()
        {
            return characterController;
        }
        public Transform GetGroundCheck()
        {
            return groundCheck;
        }
        public LayerMask GetLayerMask()
        {
            return groundmask;
        }
        public Vector3 GetVelocity()
        {
            return velocity;
        }
        public Transform GetTransform()
        {
            return this.transform;
        }
        public Animator GetAnimator()
        {
            return animator;
        }
        public Camera GetCamera()
        {
            return mycamera;
        }  
    }
}
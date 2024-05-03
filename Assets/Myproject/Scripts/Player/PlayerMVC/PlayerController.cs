using UnityEngine;
using UnityEngine.UI;
using FPSZombie.Zombie;
using FPSZombie.ScriptableObjects;
using FPSZombie.Event;
using System;

namespace FPSZombie.player
{
    public class PlayerController
    {
        private PlayerModel playerModel;
        private PlayerView playerView;
        private CharacterController characterController;
        private Animator animator;
        private Camera mycamera;
        private Transform groundCheck;
        private Slider healthBar;
        private LayerMask groundmask;
        private Vector3 velocity;
        private float speed;  
        private float gravity = -9.81f;
        private bool isGrounded ;
        private float groundDistance = 0.4f;
        private float jumphight = 2f;
        private int bulletDamage;
        private int health;
        private int magSize;
        private PlayerSO playerSO1;
        public PlayerController(PlayerSO playerSO, Transform transform)
        {
            playerView = GameObject.Instantiate<PlayerView>(playerSO.playerView, transform.position, Quaternion.identity);
            playerModel = new PlayerModel(playerSO);  
            characterController = playerView.GetCharacterController();
            groundCheck = playerView.GetGroundCheck();
            animator = playerView.GetAnimator();            
            mycamera = playerView.GetCamera();
            groundmask = playerView.GetLayerMask();
            health = playerModel.Health;
            speed = playerModel.speed;
            playerSO1 = playerSO;
            magSize = playerSO1.magSize;
            playerView.SetController(this);
            playerModel.SetController(this);
           
            velocity = playerView.GetVelocity();
            EventService.Instance.InvokeSetMaxHealthBar(health);
            EventService.Instance.InvokeSetPlayerHealthBar(health);
        }
        public void TakeDamage(int _damage)
        {
            health -= _damage;
            EventService.Instance.InvokeSetPlayerHealthBar(health);
           
            if(health < 0)
            {
                
                PlayerDeath();
                
            }
        }
        public void PlayerMovement(float horizontalInput, float Vertical)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2.0f;
            }
            Vector3 move = playerView.transform.right * horizontalInput + playerView.transform.forward * Vertical;
            characterController.Move(move * speed * Time.deltaTime);
        }

        public void AddAmmo(int ammoAmount)
        {
            if (playerSO1.magSize < 500) // Check if ammo is less than the maximum capacity
            {
                int spaceLeft = 100 - playerSO1.magSize; // Calculate remaining space in the mag
                int addedAmmo = Mathf.Min(ammoAmount, spaceLeft); // Add only as much ammo as there is space left
                playerSO1.magSize += ammoAmount; // Add the full ammo amount
                Debug.Log("Ammo Added: " + addedAmmo);
                Debug.Log("Total Ammo in Magazine: " + playerSO1.magSize);
            }
            else
            {
                Debug.Log("Magazine is full");
            }
        }


        public void Heal(int healamount)
        {
                if(health > 0)
                {
                    Debug.Log("Heal Add " + healamount);
                    health += healamount;
                EventService.Instance.InvokeSetPlayerHealthBar(health);
                if (health > 100)
                   {
                    health = playerModel.Health;
                    Debug.Log("Health is After Healing " + health);
                   }
                }      
                else
                {
                   Debug.Log("health is less than 0");
                }   
        }
        public void PlayerGravity()
        {
            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }
        public void PlayerJump()
        {
            Debug.Log(isGrounded);
            velocity.y = Mathf.Sqrt(jumphight * -2f * gravity);
        }
        public bool GetIGrounded()
        {
            return isGrounded;
        }
        public Transform GetTransform()
        {        
            return playerView.transform;
        }
        public Camera GetCamera()
        {
            return playerView.GetCamera();
        }
        public void PlayerDeath()
        {
            EventService.Instance.InvokeGameOver();
            playerView.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
       
    }
}
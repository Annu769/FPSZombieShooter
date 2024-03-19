using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FPSZombie.ScriptableObjects;
namespace FPSZombie.Player
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
        private bool isGrounded;
        private float groundDistance = 0.4f;
        private float jumphight = 2f;
        private int bulletDamage;
        private int health;
        private int magSize;
        private PlayerSO playerSO1;
        public PlayerController(PlayerSO playerSO)
        {
            playerView = GameObject.Instantiate<PlayerView>(playerSO.playerView);
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
            healthBar = playerView.GetSlider();
            velocity = playerView.GetVelocity();
            playerView.SetController(this);
            playerModel.SetController(this);

        }
        public void TakeDamage(int _damage)
        {
            health -= _damage;
            healthBar.value = health;
            if (health < 0)
            {

                Debug.Log("player is Death");
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
            if (playerSO1.magSize >= 0)
            {
                Debug.Log("Ammo Add " + ammoAmount);
                playerSO1.magSize += ammoAmount;

                if (playerSO1.magSize > magSize)
                {
                    playerSO1.magSize = magSize;
                    Debug.Log("Ammo is After Adding " + playerSO1.magSize);
                }
            }
            else
            {
                Debug.Log("Mag is full");
            }
        }

        public void Heal(int healamount)
        {
            if (health > 0)
            {
                Debug.Log("Heal Add " + healamount);
                health += healamount;
                healthBar.value = health;
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
            Debug.Log(playerView.transform);
            return playerView.transform;

        }
        public Camera GetCamera()
        {
            return playerView.GetCamera();
        }
    }

}


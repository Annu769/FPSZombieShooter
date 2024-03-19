using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FPSZombie.ScriptableObjects;
using FPSZombie.Zombie;
namespace FPSZombie.player
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private PlayerSO gundata;
        [SerializeField] private Transform muzzle;
        [SerializeField] ParticleSystem muzzleFlash;
        [SerializeField] private GameObject bulletImpactEffect;
        [SerializeField] private GameObject bloodImpactEffect;
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] private Animator animator;
        private float timeSinceLastShot;
        private int _ammoToReload = 30;
        private int fullMagZine = 200;
        private void Start()
        {
            gundata.currentAmmo = _ammoToReload;
            gundata.magSize = fullMagZine;
            EventListner.shootInput += Shoot;
            EventListner.reloadInpt += StartReload;
        }
        public void StartReload()
        {
            if (!gundata.reloading)
            {
                StartCoroutine(Reload());
            }
        }
        public IEnumerator Reload()
        {
            gundata.reloading = true;
            yield return new WaitForSeconds(gundata.reloadTime);
            if (gundata.magSize > 0)
            {
                gundata.magSize = gundata.magSize - _ammoToReload + gundata.currentAmmo;
                gundata.currentAmmo = _ammoToReload;
            }
            if (gundata.magSize < 0)
            {
                gundata.magSize = 0;
            }
            gundata.reloading = false;
        }






        private bool canShoot() => !gundata.reloading && timeSinceLastShot > 1f / (gundata.fireRate / 60f);
        public void Shoot()
        {
            if (gundata.currentAmmo > 0)
            {
                if (canShoot())
                {
                    animator.SetBool("IsShooting", true);
                    muzzleFlash.Play();
                    if (Physics.Raycast(muzzle.position, transform.forward, out RaycastHit raycastHit, gundata.maxDistance))
                    {
                        if (raycastHit.collider.gameObject.GetComponent<ZombieView>())
                        {
                            EnemyIDamagable Idamage = raycastHit.collider.gameObject.GetComponent<EnemyIDamagable>();
                            Idamage.TakeDamage(gundata.damage);
                            GameObject bloodparticle = Instantiate(bloodImpactEffect, raycastHit.point, Quaternion.LookRotation(raycastHit.normal));
                        }
                        else
                        {
                            GameObject particle = Instantiate(bulletImpactEffect, raycastHit.point, Quaternion.LookRotation(raycastHit.normal));
                        }
                    }
                    gundata.currentAmmo--;
                    timeSinceLastShot = 0;
                }
            }
            else if (gundata.currentAmmo == 0)
            {
                StartReload();
            }
        }
        private void Update()
        {
            timeSinceLastShot += Time.deltaTime;
            text.SetText(gundata.currentAmmo + " / " + gundata.magSize);
        }

    }
}

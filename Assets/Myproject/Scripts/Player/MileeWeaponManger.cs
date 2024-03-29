using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MileeWeaponManger : MonoBehaviour
{
    [SerializeField] private float attackrange;
    [SerializeField] private LayerMask attackableLayers;
    [SerializeField] private int damage;
    [SerializeField] private GameObject bloodImpactEffect;
    [SerializeField] private Animator animator;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            PerformAttack();
        }
    }

    public void PerformAttack()
    {
        animator.SetTrigger("Attack");

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, attackrange,attackableLayers ))
        {
            EnemyIDamagable damageble = hit.collider.gameObject.GetComponent<EnemyIDamagable>();
            if(damageble != null)
            {
                damageble.TakeDamage(damage);
                GameObject bloodParticle = Instantiate(bloodImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}

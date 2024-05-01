using UnityEngine;

public class AxeWeapon : MonoBehaviour
{
    [SerializeField] private float attackRange = 2f; // Range of the axe swing
    [SerializeField] private LayerMask attackableLayers; // Layers that can be damaged by the axe
    [SerializeField] private int damage = 8; // Damage dealt on successful hit
    [SerializeField] private GameObject bloodImpacteffect;
    [SerializeField] private Animator animator; // Reference to the animator component (optional)
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            PerformAttack();
        }
    }
    public void PerformAttack()
    {
        animator.SetTrigger("Attack"); // Trigger attack animation (if applicable)
        // Perform raycast to check for enemies within range
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, attackableLayers))
        {
            // Deal damage to the hit object
            EnemyIDamagable damageable = hit.collider.gameObject.GetComponent<EnemyIDamagable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                GameObject bloodparticle = Instantiate(bloodImpacteffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}

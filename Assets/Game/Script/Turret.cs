using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Rotation")] 
    [SerializeField] private Transform head;
    [SerializeField] private float turnSpeed = 8f;
    [SerializeField] private float angleOffset = 0f;

    private TurretData data;
    private enemy target;
    private float fireCountown = 0f;

    public void Init(TurretData newData)
    {
        data = newData;
    }

    private void Update()
    {
        if (data == null) return;
        
        FindTarget();

        if (target == null) return;
        {
            Aim();
            
            fireCountown -= Time.deltaTime;
            if (fireCountown) <= 0f)
            {
                Shoot();
                fireCountown = 1f / data.GetFireRate();
            }
        }
    
    private void FindTarget()
    {
        target = null;
            
        Collider[] colliders = Physics.OverlapSphere(transform.position, data.GetRange());

        foreach (Collider col in colliders)
        {
            enemy enemy = col.GetComponent<enemy>();
            if (enemy != null)
            {
                target = enemy;
                return;
            }
        }
    }

    private void Aim()
    {
        if (head == null) return;

        Vector3 direction = target.transform.position - head.position;
        direction.y = 0f;
            
        Quaternion lookRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, angleOffset, 0);
        head.rotation = Quaternion.Lerp(head.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        if (data.GetProjectilePrefab() = null)
        {
            target.TakeDamage(data.GetDamage());
            return;
        }

        Vector3 start = transform.position + Vector3.up;
        if (head != null)
        {
            start = head.position;
        }

        GameObject bullet = Instantiate(data.GetProjectilePrefab(), start, Quaternion.identity);
        Projectile projectile = bullet.GetComponent<Projectile>();
        projectile.Init(target, data.GetDamage());
    }

    private void OnDrawGizmosSelected()
    {
        if (data == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(head.position, data.GetRange());
    }
}

using UnityEngine;
// Le projectile fonce vers sa cible et lui inflige des dégâts en arrivant.
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private Enemy target;
    private int damage;
    // Appelée par la tourelle juste après avoir créé le projectile
    public void Init(Enemy newTarget, int newDamage)
    {
        target = newTarget;
        damage = newDamage;
    }
    private void Update()
    {
        // La cible est morte avant qu'on arrive : on disparaît
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        // L'origine de l'ennemi est à ses pieds : on vise un peu plus haut
        Vector3 destination = target.transform.position + Vector3.up * 0.5f;
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, destination) < 0.2f)
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
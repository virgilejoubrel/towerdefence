using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int health = 10;
    [SerializeField] private int reward = 10;   // argent gagné quand on le tue
    [SerializeField] private int damage = 1;    // vies perdues s'il arrive au bout

    private GameManager gameManager;
    private WaveManager waveManager;
    private Waypoints waypoints;

    private int waypointIndex = 0;              // le point du chemin vers lequel on se dirige
    private bool isDead = false;

// Appelée par le WaveManager juste après avoir créé l'ennemi
    public void Init(GameManager newGameManager, WaveManager newWaveManager, Waypoints newWaypoints)
    {
        gameManager = newGameManager;
        waveManager = newWaveManager;
        waypoints = newWaypoints;
    }

    private void Update()
    {
        Transform target = waypoints.GetPoint(waypointIndex);

        // On avance vers le point
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.LookAt(target.position);

        // Arrivé au point ? On passe au suivant
        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            waypointIndex++;

            if (waypointIndex >= waypoints.GetCount())
            {
                ReachEnd();
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        gameManager.AddMoney(reward);
        waveManager.EnemiesRemoved();
        Destroy(gameObject);
    }

    private void ReachEnd()
    {
        isDead = true;
        gameManager.LoseLife(damage);
        waveManager.EnemiesRemoved();
        Destroy(gameObject);
    }

}

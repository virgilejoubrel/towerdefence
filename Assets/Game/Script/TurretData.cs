using UnityEngine;


    [CreateAssetMenu(fileName = "TurretData", menuName = "Tower Defense/Turret Data")]
    public class TurretData : ScriptableObject
    {
        [Header("Boutique")]
        [SerializeField] private string turretName = "Tourelle";
        [SerializeField] private int cost = 50;
        [SerializeField] private Color buttonColor = new Color(0.2f, 0.45f, 0.75f);
        [SerializeField] private GameObject prefab;               // le modèle à poser (il porte le script Turret)
        
        [Header("Tir")]
        [SerializeField] private float range = 5f;
        [SerializeField] private float fireRate = 2f;             // tirs par seconde
        [SerializeField] private int damage = 2;
        [SerializeField] private GameObject projectilePrefab;     // vide = dégâts directs, sans projectile
        
        public string GetTurretName()
        {
            return turretName;
        }
        public int GetCost()
        {
            return cost;
        }
        
        public Color GetButtonColor()
        {
            return buttonColor;
        }
        public GameObject GetPrefab()
        {
            return prefab;
        }
        public float GetRange()
        {
            return range;
        }
        public float GetFireRate()
        {
            return fireRate;
        }
        public int GetDamage()
        {
            return damage;
        }
        public GameObject GetProjectilePrefab()
        {
            return projectilePrefab;
        }
    }



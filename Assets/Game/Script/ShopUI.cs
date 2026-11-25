using UnityEngine;
// Construit la boutique tout seul : un bouton par tourelle de la liste.
// Ajouter une tourelle au jeu = ajouter son asset dans "turrets". Aucun bouton à placer à la main.
public class ShopUI : MonoBehaviour
{
    [SerializeField] private TurretData[] turrets;          // les tourelles en vente, dans l'ordre
    [SerializeField] private ShopButton buttonPrefab;       // le modèle de bouton
    [SerializeField] private Transform buttonParent;        // où ranger les boutons (le panneau Shop)
    [Header("Managers")]
    [SerializeField] private BuildManager buildManager;
    [SerializeField] private GameManager gameManager;
    private void Start()
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            // Instantiate avec un parent : le bouton devient un enfant du panneau
            ShopButton button = Instantiate(buttonPrefab, buttonParent);
            button.Setup(turrets[i], buildManager, gameManager);
            // Le bouton n°i se range en position i : les tourelles passent avant
            // les boutons déjà présents dans le panneau (vague, vitesse)
            button.transform.SetSiblingIndex(i);
        }
    }
}
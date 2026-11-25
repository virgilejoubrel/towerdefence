using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// Gère le choix de la tourelle dans la boutique et la pose sur les supports avec la souris.
public class BuildManager : MonoBehaviour
{
    // Références à glisser dans l'Inspector
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;
    
    private TurretData selectedTurret;   // la tourelle choisie dans la boutique
    private TurretSupport hoveredSupport;
    
    private void Update()
    {
        if (Mouse.current == null) return;
        
        // La souris est sur un bouton de l'UI : on ne touche pas aux supports
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            SetHoveredSupport(null);
            return;
        }
        
        // On lance un rayon depuis la caméra, à travers la position de la souris
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        TurretSupport support = null;
        
        if (Physics.Raycast(ray, out RaycastHit hit, 500f))
        {
            support = hit.collider.GetComponent<TurretSupport>();
        }
        
        SetHoveredSupport(support);
        
        if (hoveredSupport != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Build(hoveredSupport);
        }
    }
    
    private void SetHoveredSupport(TurretSupport newSupport)
    {
        if (newSupport == hoveredSupport) return;
        
        if (hoveredSupport != null)
        {
            hoveredSupport.StopHover();
        }
        
        hoveredSupport = newSupport;
        
        if (hoveredSupport != null)
        {
            hoveredSupport.Hover();
        }
    }
    
    // Appelée par les boutons de la boutique (ShopButton)
    public void SelectTurret(TurretData data)
    {
        selectedTurret = data;
        uiManager.ShowMessage(data.GetTurretName() + " sélectionnée (" + data.GetCost() + " $)");
    }
    
    private void Build(TurretSupport support)
    {
        if (selectedTurret == null)
        {
            uiManager.ShowMessage("Choisis d'abord une tourelle !");
            return;
        }
        
        if (support.HasTurret())
        {
            uiManager.ShowMessage("Il y a déjà une tourelle ici !");
            return;
        }
        
        if (gameManager.Spend(selectedTurret.GetCost()) == false)
        {
            uiManager.ShowMessage("Pas assez d'argent !");
            return;
        }
        support.BuildTurret(selectedTurret);
        support.Hover(); // met à jour la couleur (case occupée)
    }
}

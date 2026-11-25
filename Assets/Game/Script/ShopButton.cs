using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Un bouton de la boutique. Il n'est pas placé à la main : c'est ShopUI qui le crée
// à partir d'un prefab, une fois par tourelle, et lui donne ses données avec Setup().
public class ShopButton : MonoBehaviour
{
    // Références vers les enfants du prefab du bouton
    [SerializeField] private Button button;
    [SerializeField] private Image background;
    [SerializeField] private TMP_Text label;

    private TurretData data;
    private BuildManager buildManager;
    private GameManager gameManager;


    public void Setup(TurretData newData, BuildManager newBuildManager, GameManager newGameManager)
    {
        data = newData;
        buildManager = newBuildManager;
        gameManager = newGameManager;


        label.text = data.GetTurretName() + "\n<size=24>" + data.GetCost() + "</size>";
        background.color = data.GetButtonColor();


        button.onClick.AddListener(OnButtonClick);
    }

    private void OnClick()
    {
        buildManager.SelectTurret(data);
    }

    private void Update()
    {

        button.interactable = gameManager.GetMoney() >= data.GetCost();
    }
}
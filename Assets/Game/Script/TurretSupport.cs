using UnityEngine;

public class turretsupport : MonoBehaviour
{
    [SerializeField] private Color hoverColor = Color.yellow;
    [SerializeField] private Color pressedColor = Color.red;

    private GameObject turret;
    private Renderer rend;
    private Color baseColor;

    private void Start()

    {
        rend = GetComponent<Renderer>();
        baseColor = rend.material.color;
    }

    public bool HasTurret()
    {
        return turret != null;
    }

    public void Hover()
    {
        if (turret == null)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = baseColor;
        }
    }

    public void StopHover()
    {
        rend.material.color = baseColor;
    }

    public void BuildTurret(TurretData data)
    {
        Vector3 position = transform.position + Vector3.up * (transform.localScale.y / 2f);
        turret = Instantiate(data.GetPrefab(), position, Quaternion.identity);
        
        turret.GetComponent<Turret>().Init(data);
    }
}

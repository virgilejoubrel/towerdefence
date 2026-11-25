using UnityEngine;
using UnityEngine.UIElements;

public class Waypoints : MonoBehaviour
{
    private Transform[] points;

    private void Awake()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }

    public Transform GetPoint(int index)
    {
        return points[index];
    }

    public int GetCount()
    {
        return points.Length;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        for (int i = 0; i < transform.childCount - 1 ; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
            Gizmos.DrawSphere(transform.GetChild(i).position, 0.2f);
            
        }
    }
}

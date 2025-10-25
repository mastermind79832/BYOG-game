using UnityEngine;

public class GuideSystem : MonoBehaviour
{
    public float Offset = 5f;
    public float LineStartX = -15f;

    private Vector3 GridLineTop = Vector3.zero;
    private Vector3 GridLineBottom = Vector3.zero;
    public float GridLineTopY = 15f;
    public float GridLineBottomY = -15f;


    void OnDrawGizmos()
    {
        GridLineTop.y = GridLineTopY;
        GridLineBottom.y = GridLineBottomY;

        for (int i = 0; i < 7; i++)
        {
            GridLineTop.x = LineStartX + i * Offset;
            GridLineBottom.x = LineStartX + i * Offset;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(GridLineTop, GridLineBottom);
        }
    }
}

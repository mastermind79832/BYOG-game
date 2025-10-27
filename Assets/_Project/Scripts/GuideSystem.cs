using UnityEngine;

public class GuideSystem : MonoBehaviour
{
    public float Offset = 5f;
    public float LineStartX = -15f;

    private Vector3 GridLineTop = Vector3.zero;
    private Vector3 GridLineBottom = Vector3.zero;
    public float GridLineTopY = 15f;
    public float GridLineBottomY = -15f;

    public float JumpHeight = 10f;

    public float JumpStartY = 0f;
    public float JumpLineLeftX = -15f;
    public float JumpLineRightX = 15f;


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

        GridLineTop.x = JumpLineLeftX;
        GridLineBottom.x = JumpLineRightX;

        for (int i = 0; i < 3; i++)
        {
            GridLineTop.y = GridLineBottom.y = JumpStartY + i * JumpHeight;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(GridLineTop, GridLineBottom);
        }
    }
}

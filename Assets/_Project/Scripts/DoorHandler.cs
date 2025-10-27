using JetBrains.Annotations;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{

    public Color CloseColor;
    public Color OpenColor;
    public SpriteRenderer Lights;

    public float Offset = 5f;
    public Transform DoorLeft;
    public Transform DoorRight;

    public void OpenDoor()
    {
        Lights.color = OpenColor;

        DoorRight.transform.position += Vector3.right * Offset; 
        DoorLeft.transform.position -= Vector3.right * Offset;
    }

}

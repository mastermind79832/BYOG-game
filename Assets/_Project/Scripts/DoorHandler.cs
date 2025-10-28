using DG.Tweening;
using JetBrains.Annotations;
using Unity.VisualScripting;
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

        Lights.DOColor(OpenColor, 0.5f).SetEase(Ease.OutQuad);

        // DoorRight.transform.position += Vector3.right * Offset;
        // DoorLeft.transform.position -= Vector3.right * Offset;
        
        DoorRight.transform.DOMoveX(DoorRight.transform.position.x + Offset, 2f).SetEase(Ease.OutExpo);
        DoorLeft.transform.DOMoveX(DoorLeft.transform.position.x - Offset, 2f).SetEase(Ease.OutExpo);
    }

}

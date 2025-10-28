using DG.Tweening;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{

    public float Offset = 5f; // Adjust this offset as needed
    public float duration;
    private float timer;
    private bool IsMovingUp;


    void Start()
    {
        IsMovingUp = true;
        timer = duration;
    }

    public void Interact()
    {
        GameManager.Instance.KeyCollected();
        gameObject.SetActive(false);
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if(timer > duration)
        {
            transform.DOLocalMoveY(transform.position.y + (Offset * (IsMovingUp? 1 : -1)), duration).SetEase(Ease.Linear); // Move the key up by the offset amount over 0.5 seconds
            timer = 0; // Reset the timer after moving the key up
            IsMovingUp = !IsMovingUp;
        }
    }


}

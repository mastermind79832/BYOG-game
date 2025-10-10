using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameManager.Instance.KeyCollected();
        gameObject.SetActive(false);
    }
}

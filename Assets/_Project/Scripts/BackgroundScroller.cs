using JetBrains.Annotations;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float scrollSpeed;
    private Material spriteMat;
    private Vector2 direction;


    private void Start()
    {
        spriteMat = spriteRenderer.material;
        direction = (Vector2.up * Random.value) + (Vector2. right * Random.value);
    }


    void Update()
    {
        spriteMat.mainTextureOffset =  direction.normalized * Time.time * scrollSpeed;
    }

}

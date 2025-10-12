using UnityEngine;

public class FloorMovement : MonoBehaviour
{
    public float speed = 2f;              // Speed of movement
    public float moveDistance = 3f;       // How far up and down the floor moves

    private Vector3 startPos;
    private bool movingUp = true;

    void Start()
    {
        startPos = transform.position;    // Remember starting position
    }

    void Update()
    {
        MoveFloor();
    }

    void MoveFloor()
    {
        // Calculate current movement limit
        float upperLimit = startPos.y + moveDistance;
        float lowerLimit = startPos.y - moveDistance;

        // Move up or down
        if (movingUp)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            if (transform.position.y >= upperLimit)
                movingUp = false;
        }
        else
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
            if (transform.position.y <= lowerLimit)
                movingUp = true;
        }
    }
}

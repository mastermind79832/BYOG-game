using UnityEngine;

public class FloorMovementLvl4 : MonoBehaviour
{
    public float speed = 2f;            // How fast the floor moves
    public float moveDistance = 3f;     // How far left and right it moves

    private Vector3 startPos;
    private bool movingRight = true;

    void Start()
    {
        startPos = transform.position;  // Save the starting position
    }

    void Update()
    {
        MoveFloor();
    }

    void MoveFloor()
    {
        // Calculate limits
        float rightLimit = startPos.x + moveDistance;
        float leftLimit = startPos.x - moveDistance;

        // Move left or right
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= rightLimit)
                movingRight = false;
        }
        else
        {
            transform.position -= Vector3.right * speed * Time.deltaTime;
            if (transform.position.x <= leftLimit)
                movingRight = true;
        }
    }
}

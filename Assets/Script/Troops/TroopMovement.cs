using UnityEngine;

public class TroopMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Speed of the troop in units oer second")]
    public float movespeed = 2f;

    [Tooltip("set rue to move in +x direction, false to move in -x direction.")]
    public bool moveRight = true;

    private Vector2 moveDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Determine movement direction based on public variable
        moveDirection = moveRight ? Vector2.right : Vector2.left;
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
    }

    void MoveForward()
    {
        //move the troop each frame based on movespeed and direction
        Vector2 movement =  movespeed * Time.deltaTime * moveDirection;
        //Apply movement to the troop's position
        transform.Translate(movement);
    }
}

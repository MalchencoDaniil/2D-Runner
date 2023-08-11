using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Player))]

public class PlayerMove : MonoBehaviour
{
    private Player playerData;

    [SerializeField] private float movementSpeed = 4f;

    private void Awake()
    {
        playerData = GetComponent<Player>();
    }

    private void Update() 
    {
        Vector3 movementDirection = transform.right * movementSpeed * Time.deltaTime;

        Move(movementDirection);
    }

    private void Move(Vector3 movementDirection)
    {
        if (!playerData.canShoot && !GameManager.instance.loose && !GameManager.instance.won)
        {
            playerData.playerState = Player.PlayerState.Run;

            transform.position += movementDirection;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Won"))
        {
            playerData.playerState = Player.PlayerState.Idle;
            GameManager.instance.Won();
        }
    }
}
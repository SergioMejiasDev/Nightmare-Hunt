using UnityEngine;

/// <summary>
/// Class that is in charge of the player's movement.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Range(1.0f, 6.0f)] [SerializeField] float speed = 2.0f;
    Vector3 playerMovement;
    Rigidbody playerRigidbody;
    Animator playerAnimator;
    int floorMask;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        floorMask = LayerMask.GetMask("Floor");
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            GameManager.gameManager.PauseGame();
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);

        Turn();

        Animation(h, v);
    }

    void Move(float h, float v)
    {
        playerMovement.Set(h, 0.0f, v);
        playerMovement = playerMovement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + playerMovement);
    }

    void Turn()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(cameraRay, out floorHit, 1000.0f, floorMask))
        {
            Vector3 playerToHit = floorHit.point - transform.position;
            playerToHit.y = 0.0f;
            Quaternion rotation = Quaternion.LookRotation(playerToHit);
            playerRigidbody.MoveRotation(rotation);
        }
    }

    void Animation(float h, float v)
    {
        playerAnimator.SetBool("IsMoving", ((h != 0) || (v != 0)));
    }
}

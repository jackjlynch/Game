using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float gravity = -0.5f;
    public float terminalVelocity = -2f;
	public float runSpeed = 1f;
    public float jumpVelocity = 10f;

    private Vector3 velocity;
    private Vector3 scaleFacingLeft;
    private Vector3 scaleFacingRight;
    private float height;
    private float width;

	// Use this for initialization
	void Start () {
        velocity = new Vector3(0, 0, 0);
        scaleFacingLeft = new Vector3(-1, 1, 1);
        scaleFacingRight = new Vector3(1, 1, 1);
        height = ((BoxCollider2D) gameObject.GetComponent(typeof(BoxCollider2D))).bounds.size.y;
        width = ((BoxCollider2D)gameObject.GetComponent(typeof(BoxCollider2D))).bounds.size.x;
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 position = new Vector3(transform.position.x, transform.position.y);

        RaycastHit2D groundedRayCastHit = Physics2D.Raycast(position, Vector2.down, -terminalVelocity + height / 2, LayerMask.GetMask("Ground"));
        bool grounded = groundedRayCastHit.collider != null && groundedRayCastHit.distance <= height / 2;
 
        if (!grounded)
        {
            velocity.y = Mathf.Max(velocity.y + gravity, terminalVelocity);
        }
        else
        {
            velocity.y = Input.GetButtonDown("Jump") ? jumpVelocity : 0;
        }

        velocity.x = Input.GetAxis("Horizontal") * runSpeed;

        Vector3 movement = velocity * Time.deltaTime;
        if(groundedRayCastHit.collider != null)
        {
            movement.y = Mathf.Max(movement.y, -groundedRayCastHit.distance + height / 2);
        }
        
        if(movement.x != 0)
        {
            RaycastHit2D horizontalRayCast = Physics2D.Raycast(position, movement.x > 0 ? Vector2.right : Vector2.left, Mathf.Abs(movement.x) + width / 2, LayerMask.GetMask("Ground"));

            if (horizontalRayCast.collider != null)
            {
                movement.x = movement.x > 0 ? Mathf.Min(movement.x, horizontalRayCast.distance - width / 2) : Mathf.Max(movement.x, -horizontalRayCast.distance + width / 2);
            }

        }

        if (movement.y > 0)
        {
            RaycastHit2D upwardRaycastHit = Physics2D.Raycast(position, Vector2.up, movement.y + height / 2, LayerMask.GetMask("Ground"));

            if (upwardRaycastHit.collider != null)
            {
                movement.y = Mathf.Min(movement.y, upwardRaycastHit.distance - height / 2);
            }
        }

        gameObject.transform.position += movement;


        if (velocity.x > 0)
        {
            transform.localScale = scaleFacingRight;
        }
        else if (velocity.x < 0)
        {
            transform.localScale = scaleFacingLeft;
        }
    }
}

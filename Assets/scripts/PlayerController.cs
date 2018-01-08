using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float gravity = -0.5f;
    public float terminalVelocity = -2f;
	public float runSpeed = 8f;
    public float jumpHeight = 3f;

    private Vector3 velocity;

	// Use this for initialization
	void Start () {
        velocity = new Vector3(0, 0, 0);
        // gameObject.GetComponent(typeof(BoxCollider2D)).
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 position = transform.position;

        RaycastHit2D raycastHit = Physics2D.Raycast(new Vector2(position.x, position.y), Vector2.down, -terminalVelocity + 0.25f, LayerMask.GetMask("Ground"));
        bool grounded = raycastHit.collider != null && raycastHit.distance <= 0.25f;

        if (!grounded)
        {
            velocity.y = Mathf.Max(velocity.y + gravity, terminalVelocity);
        }
        else
        {
            velocity.y = 0;
        }

        Vector3 movement = velocity * Time.deltaTime;
        if(raycastHit.collider != null)
        {
            movement.y = Mathf.Max(movement.y, -raycastHit.distance);
        }
        

        gameObject.transform.position += movement;
    }
}

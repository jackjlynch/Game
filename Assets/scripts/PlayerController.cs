using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private float gravity = 1;
	public float runSpeed = 8f;
    public float jumpHeight = 3f;
    public float groundAcceleration = 0.1f;
    public float airAcceleration = 0.2f;
    public float smoothVelocity = 0f;
    
    private Transform transform;

	// Use this for initialization
	void Start () {
        transform = gameObject.transform;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 position = transform.position;
        gameObject.transform.position = new Vector3(position.x, position.y - gravity * Time.deltaTime, position.z);
	}
}

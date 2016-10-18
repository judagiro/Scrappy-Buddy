using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private float _move;

    public float Velocity = 10f;
    public Camera camera;

	// Use this for initialization
	void Start ()
    {
        this._initialize();
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        this._move = Input.GetAxis("Horizontal");
        if (this._move > 0f)
        {
            this._move = 1;

        }
        else if (this._move < 0f)
        {
            this._move = -1;

        }
        else
        {
            this._move = 0f;
        }
        

        this._rigidbody.AddForce(new Vector2(this._move * this.Velocity, 0f), ForceMode2D.Force);
        this.camera.transform.position = new Vector3(this.transform.position.x*0.8f, this.transform.position.y*0.8f, -10f);
	
	}

    private void _initialize()
    {
        this._transform = GetComponent<Transform>();
        this._rigidbody = GetComponent<Rigidbody2D>();
        this._move = 0f;
    }
}

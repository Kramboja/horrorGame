using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float walkSpeed			= 5.0f;
	public float runSpeed			= 11.0f;
	public float jumpForce			= 20000f;	

	private bool jumping			= false;
	private float speed 			= 5.0f;
	private float rotationspeed 	= 100f;
	private float mouseSensivity 	= 10f;
	private float verticalRotation 	= 0;
	private float upDownRange 		= 60f;

	void Awake()
	{
		Screen.lockCursor = true;
	}
	// Update is called once per frame
	void Update () 
	{
		//if (Input.anyKey)
			buttonPressed ();

		float rotHorizontal = Input.GetAxis ("Mouse X") * mouseSensivity;
		transform.Rotate (0, rotHorizontal, 0);

		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensivity;
		verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);

		Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);
	}

	private void buttonPressed()
	{	
		if (Input.GetKey(KeyCode.Escape) && Screen.lockCursor == true)
		{
			if(Screen.lockCursor == true)
			{
				Screen.lockCursor = false;
			}
			else
				Screen.lockCursor = true;
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(!jumping)
			{
				GetComponent<Rigidbody>().AddForce(new Vector2(0,jumpForce));
				jumping = true;
			}
		}

		if(Input.GetKeyDown(KeyCode.C))
		{
			transform.localScale = new Vector3(1f,1.5f,1f);
			transform.Translate(0,.75f,0);

		}

		if(Input.GetKeyUp(KeyCode.C))
		{
			transform.localScale = new Vector3(1f,3f,1f);
		}

		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			speed = runSpeed;
		}

		if(Input.GetKeyUp(KeyCode.LeftShift))
		{
			speed = walkSpeed;
		}

		float translationx = Input.GetAxis ("Vertical") 	* speed;
		float translationy	= Input.GetAxis ("Horizontal") 	* speed;

		translationx 	*= Time.deltaTime;
		translationy 	*= Time.deltaTime;

		transform.Translate (translationy, 0, translationx);
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Floor")
		{
			jumping = false;
		}
	}
}
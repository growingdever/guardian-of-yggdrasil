using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour
{
    public float _moveSpeed;
    public float _rotatePower;

    public GameObject _model;
    private Vector3 smoothedDirection = Vector3.zero;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 forward = transform.forward;
        
        forward.Normalize();
		smoothedDirection = Vector3.Slerp (smoothedDirection, forward, Time.deltaTime * 3.0f);	
		Vector3 deltaVelocity = (smoothedDirection * _moveSpeed) - rigidbody.velocity;		

        if (Input.GetKey (KeyCode.W)) {
//			rigidbody.AddRelativeForce (deltaVelocity, ForceMode.Force);
			transform.Translate( Vector3.forward * _moveSpeed );
		}
        if (Input.GetKey (KeyCode.S)) {
//			rigidbody.AddRelativeForce (deltaVelocity * -1, ForceMode.Force);
			transform.Translate( -Vector3.forward * _moveSpeed );
		}
        if (Input.GetKey (KeyCode.A)) {
//			rigidbody.AddRelativeTorque(Vector3.up * -_rotatePower);
			transform.Rotate( 0, -_rotatePower, 0 );
		}
        if (Input.GetKey (KeyCode.D)) {
//			rigidbody.AddRelativeTorque(Vector3.up * _rotatePower);
			transform.Rotate( 0, _rotatePower, 0 );
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
//			rigidbody.AddRelativeTorque(Vector3.up * -_rotatePower);
			transform.Rotate( 0, -_rotatePower, 0 );
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
//			rigidbody.AddRelativeTorque(Vector3.up * _rotatePower);
			transform.Rotate( 0, _rotatePower, 0 );
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
//			rigidbody.AddRelativeTorque(Vector3.left * _rotatePower);
			transform.Rotate( -_rotatePower, 0, 0 );
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
//			rigidbody.AddRelativeTorque(Vector3.left * -_rotatePower);
			transform.Rotate( _rotatePower, 0, 0 );
		}

		// go forward
//		this.transform.localPosition += this.transform.forward * _moveSpeed;
    }
}

using UnityEngine;
using System.Collections;

public class BulletMoving : MonoBehaviour {

	public float Speed;
	
	void Update () {
		// this is very special case...
		this.transform.position += this.transform.up * Speed;
	}

	void OnCollisionEnter(Collision collision) {
		print (collision);
	}

}

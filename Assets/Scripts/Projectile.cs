using UnityEngine;
using System.Collections;
using System;


public class Projectile : MonoBehaviour {

	public float Speed;
	public int Damage;

	public event EventHandler<EventArgsGameObject> OnCollidedCallbacks;

	protected virtual void OnCollisionEnter(Collision collision) {
		var e = new EventArgsGameObject {gameObject = collision.gameObject};
		OnCollidedCallbacks (this, e);
	}
	
}

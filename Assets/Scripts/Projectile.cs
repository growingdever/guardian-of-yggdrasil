using UnityEngine;
using System.Collections;
using System;


public abstract class Projectile : MonoBehaviour {

	public float Speed;
	public int Damage;
	public float LifeTime;

	public event EventHandler<EventArgsGameObject> OnCollidedCallbacks;

	protected virtual void OnCollisionEnter(Collision collision) {
		var e = new EventArgsGameObject {gameObject = collision.gameObject};
		OnCollidedCallbacks (this, e);
	}

	protected virtual void Update() {
		Move ();

		LifeTime -= Time.deltaTime;
		if( LifeTime < 0 ) {
			Destroy(this.gameObject);
		}
	}

	protected abstract void Move();
	
}

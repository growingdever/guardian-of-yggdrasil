using UnityEngine;
using System.Collections;
using System;


public abstract class Projectile : MonoBehaviour {

	public float Speed;
	public int Damage;
	public float LifeTime;

	public GameObject Shooter;
	public event EventHandler<EventArgsGameObject> OnCollidedCallbacks;

	protected virtual void OnCollisionEnter(Collision collision) {
		if( collision.gameObject == Shooter || collision.gameObject.GetComponent<Projectile>() != null ) {
			return;
		}

		var e = new EventArgsGameObject {gameObject = collision.gameObject};
		OnCollidedCallbacks (this, e);

		// every projectile will be exploded when collided with something
		Explode();
	}

	protected virtual void Update() {
		Move ();

		LifeTime -= Time.deltaTime;
		if( LifeTime < 0 ) {
			Destroy(this.gameObject);
		}
	}

	protected abstract void Move();

	protected abstract void Explode();

}

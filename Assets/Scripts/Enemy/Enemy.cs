using UnityEngine;
using System.Collections;
using System;


public class EventEnemyLifeCycle : EventArgs
{
	public GameObject gameObject;
}

public abstract class Enemy : MonoBehaviour {

	public GameObject PrefabExplosionEffect;

	int _hp;
	public int HP {
		get {
			return _hp;
		}
		set {
			_hp = value;
			if( _hp <= 0 ) {
				OnDead();
			}
		}
	}

	public int DamageCollidedYggdrasil {
		get;
		protected set;
	}

	public int DamageCollidedPlayer {
		get;
		protected set;
	}
	
	public event EventHandler<EventEnemyLifeCycle> OnDeadCallbacks;


	protected void OnDead() {
		var e = new EventEnemyLifeCycle {gameObject = this.gameObject};
		OnDeadCallbacks(this, e);

		Instantiate(PrefabExplosionEffect, this.transform.position, Quaternion.identity);

		this.transform.parent = null;
		Destroy(this.gameObject);
	}

	protected void OnCollisionEnter(Collision collision) {
		Yggdrasil yggdrasil = collision.gameObject.GetComponent<Yggdrasil> ();
		Player player = collision.gameObject.GetComponent<Player> ();

		if (yggdrasil != null) {
			OnCollidedWithYggdrasil( yggdrasil );
		}
		if (player != null) {
			OnCollidedWithPlayer( player );
		}

		OnDead ();
	}

	abstract protected void OnCollidedWithYggdrasil (Yggdrasil yggdrasil);

	abstract protected void OnCollidedWithPlayer (Player player);

	abstract protected void OnRoundChanged (int round);

}

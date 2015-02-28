using UnityEngine;
using System.Collections;
using System;


public class EnemyBigEye : Enemy {

	public readonly int[] ROUND_TABLE_DAMAGE_COLLIDED_YGGDRASIL = { 5, };
	public readonly int[] ROUND_TABLE_DAMAGE_COLLIDED_PLAYER = { 5, };

	public Transform MissileSpawnPoint;
	public GameObject PrefabMissile;
	public float AttackRange;
	public float AttackSpeed;

	EnemyMoving _moving;
	bool _attackable;
	float _attackDelay;


	protected override void Awake() {
		base.Awake ();
		_moving = this.GetComponent<EnemyMoving> ();
	}

	void Update() {
		if (!((EnemyMovingLinear)_moving).TargetPoint) {
			return;
		}

		Vector3 dest = ((EnemyMovingLinear)_moving).TargetPoint.transform.position;
		float dist = (this.transform.position - dest).magnitude;
		_moving.enabled = dist >= AttackRange;
		_attackable = dist < AttackRange;


		_attackDelay -= Time.deltaTime;
		if (_attackable) {
			if( _attackDelay < 0 ) {
				_attackDelay = AttackSpeed;
				Fire();
			}
		}
	}

	void Fire() {
		GameObject clone = Instantiate (PrefabMissile, MissileSpawnPoint.position, MissileSpawnPoint.rotation) as GameObject;
		Vector3 scale = clone.transform.localScale;
		clone.transform.parent = this.transform;
		clone.transform.localScale = scale;
		clone.transform.forward = this.transform.forward;
		
		EnemyBigEyeMissile comp = clone.GetComponent<EnemyBigEyeMissile> ();
		comp.Damage = 10;
		comp.Speed = 10;
		comp.OnCollidedCallbacks += OnMissileCollisionEnter;
	}

	void OnMissileCollisionEnter(object sender, EventArgsGameObject e) {
		Projectile projectile = sender as Projectile;
		GameObject collided = e.gameObject;

		Yggdrasil yggdrasil = collided.GetComponent<Yggdrasil> ();
		if (yggdrasil != null) {
			yggdrasil.HP -= projectile.Damage;
		}

		Player player = collided.GetComponent<Player> ();
		if (player != null) {
			player.HP -= projectile.Damage;
		}
	}

	protected override void OnRoundChanged (object sender, EventRoundChange e)
	{
		int round = e.round;

		int i = round < ROUND_TABLE_DAMAGE_COLLIDED_YGGDRASIL.Length ? round : ROUND_TABLE_DAMAGE_COLLIDED_YGGDRASIL.Length - 1;
		DamageCollidedYggdrasil = ROUND_TABLE_DAMAGE_COLLIDED_YGGDRASIL [i];

		i = round < ROUND_TABLE_DAMAGE_COLLIDED_PLAYER.Length ? round : ROUND_TABLE_DAMAGE_COLLIDED_PLAYER.Length - 1;
		DamageCollidedPlayer = ROUND_TABLE_DAMAGE_COLLIDED_PLAYER [i];
	}
	
	protected override void OnCollidedWithYggdrasil (Yggdrasil yggdrasil)
	{
		yggdrasil.HP -= DamageCollidedYggdrasil;
	}
	
	protected override void OnCollidedWithPlayer (Player player)
	{
		player.HP -= DamageCollidedPlayer;
	}

}

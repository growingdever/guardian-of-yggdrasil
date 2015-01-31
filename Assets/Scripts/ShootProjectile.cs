using UnityEngine;
using System.Collections;

public class ShootProjectile : MonoBehaviour {

	public GameObject BulletContainer;
	public Transform SpawnPoint;
	public GameObject PrefabBullet;
	public GameObject PrefabMissile;

	// below two speed is not unified....
	public float BulletSpeed = 3.0f;
	public int BulletDamage;
	public float BulletDelay;
	public float MissileSpeed = 500.0f;
	public int MissileDamage;
	public int MissileDelay;
	public float ForwardOffsetFactor = 1000.0f;
	public float ColliderRadius = 5.0f;

	public enum WeaponType {
		Bullet,
		Missile,
	}

	public WeaponType CurrentWeapon {
		get;
		private set;
	}

	Camera _playerCamera;
	float _bulletDelay;
	float _missileDelay;

	
	void Awake() {
		_playerCamera = this.GetComponentInChildren<Camera>();
	}

	void Start () {

	}

	void Update () {
		_bulletDelay += Time.deltaTime;
		_missileDelay += Time.deltaTime;
		if (Input.GetButton("Fire1")) {
			if( CurrentWeapon == WeaponType.Bullet && _bulletDelay > BulletDelay ) {
				_bulletDelay = 0;
				SpawnBullet(SpawnPoint);
			} else if( CurrentWeapon == WeaponType.Missile && _missileDelay > MissileDelay ) {
				_missileDelay = 0;
				SpawnMissile(SpawnPoint);
			}
		}

		if (Input.GetKeyDown("1")) {
			CurrentWeapon = WeaponType.Bullet;
		}
		if (Input.GetKeyDown("2")) {
			CurrentWeapon = WeaponType.Missile;
		}
	}

	void SpawnBullet(Transform spawnPoint) {
		GameObject clone = Instantiate (PrefabBullet, spawnPoint.position, spawnPoint.rotation) as GameObject;
		clone.transform.parent = BulletContainer.transform;
		clone.transform.Rotate(90, 0, 0);

		BulletMachineGun comp = clone.GetComponent<BulletMachineGun>();
		comp.Damage = BulletDamage;
		comp.Speed = BulletSpeed;
		comp.OnCollidedCallbacks += OnBulletCollisionEnter;

		StartCoroutine (DestroyProjectile (clone));
	}


	void SpawnMissile(Transform spawnPoint) {
		GameObject clone = Instantiate (PrefabMissile, spawnPoint.position, spawnPoint.rotation) as GameObject;
		clone.transform.parent = BulletContainer.transform;

		EffectSettings setting = clone.GetComponent<EffectSettings>();
		setting.MoveSpeed = MissileSpeed;
		setting.MoveDistance = 1000.0f;
		setting.IsHomingMove = true;
		setting.ColliderRadius = ColliderRadius;
		setting.CollisionEnter += this.OnMissileCollisionEnter;

		var go = new GameObject();
		go.transform.position = spawnPoint.position + _playerCamera.transform.forward * ForwardOffsetFactor;
		go.transform.parent = setting.gameObject.transform;
		setting.Target = go;

		StartCoroutine (DestroyProjectile (clone));
	}
	
	IEnumerator DestroyProjectile( GameObject obj ) {
		yield return new WaitForSeconds( 10 );
		if (obj != null) {
			Destroy( obj );
		}
	}

	void OnBulletCollisionEnter(object sender, EventArgsGameObject e) {
		GameObject collided = e.gameObject;
		Enemy enemy = collided.GetComponent<Enemy> ();
		if (enemy != null) {
			Projectile projectile = sender as Projectile;
			enemy.HP -= projectile.Damage;
			Destroy( collided );
		}
	}

	void OnMissileCollisionEnter(object sender, CollisionInfo e) {
		Enemy enemy = e.Hit.collider.GetComponent<Enemy>();
		if( enemy != null ) {
			// this collider is enemy!
			GameObject goEnemy = enemy.gameObject;
			enemy.HP -= MissileDamage;
		}
	}

}

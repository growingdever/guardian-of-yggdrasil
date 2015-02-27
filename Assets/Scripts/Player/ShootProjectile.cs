using UnityEngine;
using System.Collections;

public class ShootProjectile : MonoBehaviour {

	public enum WeaponType {
		Bullet,
		Missile,
	}

	public readonly int[] UPGRADE_TABLE_BULLET_DAMAGE = { 2, 3, 4, 5, 6, 7, 8, 9, 10 };
	public readonly int[] UPGRADE_TABLE_MISSILE_DAMAGE = { 10, 14, 18, 22, 26, 30 };
	public readonly float[] UPGRADE_TABLE_MISSILE_DELAY = { 5.0f, 4.0f, 4.0f, 3.0f, 3.0f, 2.0f, 2.0f, 1.0f };

	public Transform ProjectileContainer;
	public Transform SpawnPoint;
	public GameObject PrefabBullet;
	public GameObject PrefabMissile;

	// UI
	public UISprite SpriteWeaponBullet;
	public UISprite SpriteWeaponMissile;

	// below two speed is not unified....
	public float BulletSpeed = 3.0f;
	public float MissileSpeed = 500.0f;

	public float ForwardOffsetFactor = 1000.0f;
	public float ColliderRadius = 5.0f;

	// attack 
	public WeaponType CurrentWeapon { get; private set; }
	public int BulletDamageLevel { get; private set; }
	public int MissileDamageLevel { get; private set; }
	public int MissileDelayLevel { get; private set; }
	public int BulletDamage { get; private set; }
	public float BulletDelay { get; private set; }
	public int MissileDamage { get; private set; }
	public float MissileDelay { get; private set; }

	Camera _playerCamera;
	float _bulletDelay;
	float _missileDelay;

	
	void Awake() {
		_playerCamera = this.GetComponentInChildren<Camera>();
	}

	void Start () {
		UpdateWeaponUIByState();

		// for initializing property about attack
		BulletDamageLevel = MissileDamageLevel = MissileDelayLevel = -1;
		UpgradeBulletDamage ();
		UpgradeMissileDamage ();
		UpgradeMissileDelay ();
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
			UpdateWeaponUIByState();
		}
		if (Input.GetKeyDown("2")) {
			CurrentWeapon = WeaponType.Missile;
			UpdateWeaponUIByState();
		}
	}

	void UpdateWeaponUIByState() {
		Color colorInactive = new Color(0.4f, 0.4f, 0.4f);
		Color colorActive = new Color(1.0f, 1.0f, 1.0f);

		if( CurrentWeapon == WeaponType.Bullet ) {
			SpriteWeaponBullet.color = colorActive;
			SpriteWeaponMissile.color = colorInactive;
		}
		else if( CurrentWeapon == WeaponType.Missile ) {
			SpriteWeaponBullet.color = colorInactive;
			SpriteWeaponMissile.color = colorActive;
		}
	}

	void SpawnBullet(Transform spawnPoint) {
		GameObject clone = Instantiate (PrefabBullet, spawnPoint.position, spawnPoint.rotation) as GameObject;
		clone.transform.parent = ProjectileContainer;
		clone.transform.LookAt (_playerCamera.transform.position + _playerCamera.transform.forward * 10000);
		clone.transform.Rotate (90, 0, 0);

		BulletMachineGun comp = clone.GetComponent<BulletMachineGun>();
		comp.Damage = BulletDamage;
		comp.Speed = BulletSpeed;
		comp.OnCollidedCallbacks += OnBulletCollisionEnter;

		StartCoroutine (DestroyProjectile (clone));
	}


	void SpawnMissile(Transform spawnPoint) {
		GameObject clone = Instantiate (PrefabMissile, spawnPoint.position, spawnPoint.rotation) as GameObject;
		clone.transform.parent = ProjectileContainer;
		clone.transform.LookAt (_playerCamera.transform.position + _playerCamera.transform.forward * 10000);

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
			enemy.HP -= MissileDamage;
		}
	}

	public void UpgradeBulletDamage() {
		BulletDamageLevel++;
		BulletDamage = Util.UpdateValueByTable (UPGRADE_TABLE_BULLET_DAMAGE, BulletDamageLevel);
	}

	public void UpgradeMissileDamage() {
		MissileDamageLevel++;
		MissileDamage = Util.UpdateValueByTable (UPGRADE_TABLE_MISSILE_DAMAGE, MissileDamageLevel);
	}

	public void UpgradeMissileDelay() {
		MissileDelayLevel++;
		MissileDelay = Util.UpdateValueByTable (UPGRADE_TABLE_MISSILE_DELAY, MissileDelayLevel);
	}

}

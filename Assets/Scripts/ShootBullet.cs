using UnityEngine;
using System.Collections;

public class ShootBullet : MonoBehaviour {

	public GameObject BulletContainer;
	public Transform SpawnPoint;
	public GameObject PrefabBullet;
	public GameObject PrefabMissile;

	// below two speed is not unified....
	public float BulletSpeed = 3.0f;
	public float MissileSpeed = 500.0f;
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

	
	void Awake() {
		_playerCamera = this.GetComponentInChildren<Camera>();
	}

	void Start () {

	}

	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			if( CurrentWeapon == WeaponType.Bullet ) {
				SpawnBullet(SpawnPoint);
			} else if( CurrentWeapon == WeaponType.Missile ) {
				SpawnMissile(SpawnPoint);
			}
		}

		if (Input.GetKeyDown(KeyCode.Keypad1)) {
			CurrentWeapon = WeaponType.Bullet;
		}
		if (Input.GetKeyDown(KeyCode.Keypad2)) {
			CurrentWeapon = WeaponType.Missile;
		}
	}

	void SpawnBullet(Transform spawnPoint) {
		GameObject clone = Instantiate (PrefabBullet, spawnPoint.position, spawnPoint.rotation) as GameObject;
		clone.transform.parent = BulletContainer.transform;
		clone.transform.Rotate(90, 0, 0);

		BulletMoving comp = clone.GetComponent<BulletMoving>();
		comp.Speed = BulletSpeed;

		StartCoroutine (DestroyBullet (clone));
	}


	void SpawnMissile(Transform spawnPoint) {
		GameObject clone = Instantiate (PrefabBullet, spawnPoint.position, spawnPoint.rotation) as GameObject;
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

		StartCoroutine (DestroyBullet (clone));
	}
	
	IEnumerator DestroyBullet( GameObject obj ) {
		yield return new WaitForSeconds( 10 );
		Destroy( obj );
	}

	void OnMissileCollisionEnter(object sender, CollisionInfo e) {
		Enemy enemy = e.Hit.collider.GetComponent<Enemy>();
		if( enemy != null ) {
			// this collider is enemy!
			GameObject goEnemy = enemy.gameObject;
			enemy.HP -= 10;
		}
	}

}

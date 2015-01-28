using UnityEngine;
using System.Collections;

public class ShootBullet : MonoBehaviour {

	public GameObject BulletContainer;
	public GameObject PrefabBullet;
	public Transform SpawnPoint;

	public float MissileSpeed = 500.0f;
	public float ForwardOffsetFactor = 1000.0f;

	Camera _playerCamera;

	
	void Awake() {
		_playerCamera = this.GetComponentInChildren<Camera>();
	}

	void Start () {

	}

	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			SpawnBullet(SpawnPoint);
		}
	}

	void SpawnBullet(Transform spawnPoint) {
		GameObject clone = Instantiate (PrefabBullet, spawnPoint.position, spawnPoint.rotation) as GameObject;
		clone.transform.parent = BulletContainer.transform;

		EffectSettings setting = clone.GetComponent<EffectSettings>();
		setting.MoveSpeed = MissileSpeed;
		setting.MoveDistance = 1000.0f;
		setting.IsHomingMove = true;

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
}

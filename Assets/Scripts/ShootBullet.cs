using UnityEngine;
using System.Collections;

public class ShootBullet : MonoBehaviour {
	
	public GameObject PrefabBullet;
	public GameObject SpawnPoint1;
	public GameObject SpawnPoint2;
	

	void Start () {

	}

	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			SpawnBullet(SpawnPoint1.transform);
			SpawnBullet(SpawnPoint2.transform);
		}
	}

	void SpawnBullet(Transform spawnPoint) {
		GameObject clone = Instantiate (PrefabBullet, spawnPoint.position, spawnPoint.rotation) as GameObject;

		EffectSettings setting = clone.GetComponent<EffectSettings>();
		setting.MoveSpeed = 20.0f;
		setting.MoveDistance = 100.0f;

		var go = new GameObject();
		go.transform.position = spawnPoint.forward + spawnPoint.position;
		go.transform.parent = setting.gameObject.transform;
		setting.Target = go;

		StartCoroutine (DestroyBullet (clone));
	}
	
	IEnumerator DestroyBullet( GameObject obj ) {
		yield return new WaitForSeconds( 10 );
		Destroy( obj );
	}
}

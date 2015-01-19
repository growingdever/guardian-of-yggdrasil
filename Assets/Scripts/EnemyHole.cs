using UnityEngine;
using System.Collections;

public class EnemyHole : MonoBehaviour {

	public Transform[] LocationPoints;
	public GameObject EnemyContainer;
	public GameObject[] PrefabEnemies;
	public int CurrentLevel;
	public Transform SpawnPoint;
	public Transform TargetPoint;
	public float EnemyMoveSpeed;

	// Use this for initialization
	void Start () {
		StartCoroutine(CreateEnemy());
	}

	IEnumerator CreateEnemy() {
		while(true) {
			GameObject go = Instantiate(PrefabEnemies[CurrentLevel], SpawnPoint.position, Quaternion.identity) as GameObject;
			go.transform.parent = EnemyContainer.transform;
			StartCoroutine( AppearanceEnemy(go) );

			yield return new WaitForSeconds( 5.0f );

			// change location
			this.transform.parent = LocationPoints[ Random.Range(0, LocationPoints.Length) ];
			this.transform.localPosition = new Vector3();
			this.transform.rotation = Quaternion.identity;

			Vector3 dest = TargetPoint.position;
			dest.y = 0;
			Vector3 curr = this.transform.position;
			curr.y = 0;
			Vector3 dir = dest - curr;
			this.transform.forward = dir;
		}
	}

	IEnumerator AppearanceEnemy(GameObject go) {
		// appearance!!
		{
			Hashtable ht = new Hashtable();
			ht["z"] = 10.0f;
			ht["time"] = 3.0f;
			ht["easetype"] = "linear";
			iTween.MoveBy(go, ht);
		}

		yield return new WaitForSeconds(3.5f);

		EnemyMoving comp = go.GetComponent<EnemyMoving>();
		comp.TargetPoint = TargetPoint;
		comp.MoveSpeed = EnemyMoveSpeed;
		comp.enabled = true;
	}

}

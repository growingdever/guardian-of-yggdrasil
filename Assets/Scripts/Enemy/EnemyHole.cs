using UnityEngine;
using System.Collections;

public class EnemyHole : MonoBehaviour {

	public float Range;
	public GameObject EnemyContainer;
	public GameObject[] PrefabEnemies;
	public int CurrentLevel;
	public Transform SpawnPoint;
	public Transform TargetPoint;
	public float EnemyMoveSpeed;

	EnemyMarkCreator _markCreator;


	void Awake() {
		_markCreator = this.GetComponent<EnemyMarkCreator> ();
	}
	
	void Start () {
		StartCoroutine(CreateEnemy());
	}

	IEnumerator CreateEnemy() {
		while(true) {
			GameObject go = Instantiate(PrefabEnemies[Random.Range(0, Mathf.Max(CurrentLevel, PrefabEnemies.Length - 1))], 
			    SpawnPoint.position, 
			    Quaternion.identity) as GameObject;
			go.transform.parent = EnemyContainer.transform;
			StartCoroutine( AppearanceEnemy(go) );

			yield return new WaitForSeconds( 5.0f );

			float radian = Random.Range(0.0f, Mathf.PI * 2);
			Vector3 new_pos = TargetPoint.position;
			new_pos.x = new_pos.x + Range * Mathf.Cos(radian);
			new_pos.y = this.transform.position.y;
			new_pos.z = new_pos.z + Range * Mathf.Sin(radian);

			this.transform.position = new_pos;
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

		_markCreator.CreateMark (go);
		go.GetComponent<Enemy>().OnDeadCallbacks += _markCreator.RemoveMark;

		EnemyMovingLinear comp = go.GetComponent<EnemyMovingLinear>();
		comp.TargetPoint = TargetPoint;
		comp.enabled = true;
	}

}

using UnityEngine;
using System.Collections;

public class EnemyHole : MonoBehaviour {

	public float GenerateInterval;
	public float Range;
	public GameObject EnemyContainer;
	public GameObject[] PrefabEnemies;
	public int CurrentLevel;
	public Transform SpawnPoint;
	public Transform TargetPoint;

	EnemyFollowingUIManager _enemyFollowingUIManager;


	void Awake() {
		_enemyFollowingUIManager = this.GetComponent<EnemyFollowingUIManager> ();
	}
	
	void Start () {
		StartCoroutine(CreateEnemy());
	}

	IEnumerator CreateEnemy() {
		while(true) {
			GameObject selected = PrefabEnemies[Random.Range(0, Mathf.Max(CurrentLevel, PrefabEnemies.Length - 1))];
			GameObject go = Instantiate(selected, 
			    SpawnPoint.position, 
			    Quaternion.identity) as GameObject;
			go.transform.parent = EnemyContainer.transform;
			StartCoroutine( AppearanceEnemy(go) );

			yield return new WaitForSeconds( GenerateInterval );

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

		_enemyFollowingUIManager.CreateUI (go);
		go.GetComponent<Enemy>().OnDeadCallbacks += _enemyFollowingUIManager.RemoveUI;

		Enemy enemy = go.GetComponent<Enemy> ();
		enemy.CurrentState = Enemy.State.Active;
	}

}

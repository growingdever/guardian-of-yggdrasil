using UnityEngine;
using System.Collections;

public class EnemyMarkCreator : MonoBehaviour {

	public Camera PlayerCamera;
	public UICamera UICamera;
	public EnemyMark PrefabEnemyMark;


	public void CreateMark(GameObject enemy) {
		GameObject goMark = Instantiate (PrefabEnemyMark.gameObject, new Vector3 (), Quaternion.identity) as GameObject;
		goMark.transform.parent = UICamera.transform;
		goMark.transform.localScale = new Vector3 (1, 1, 1);
		EnemyMark enemyMark = goMark.GetComponent<EnemyMark> ();
		enemyMark.PlayerCamera = PlayerCamera;
		enemyMark.Enemy = enemy;
	}

	public void RemoveMark(EnemyMark go) {
		//TODO
	}

}

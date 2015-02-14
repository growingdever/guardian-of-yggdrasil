using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMarkCreator : MonoBehaviour {

	public Camera PlayerCamera;
	public UICamera UICamera;
	public EnemyMark PrefabEnemyMark;

	Dictionary<Enemy, EnemyMark> _enemyMarkMap = new Dictionary<Enemy, EnemyMark>();


	public void CreateMark(GameObject goEnemy) {
		GameObject goMark = Instantiate (PrefabEnemyMark.gameObject, new Vector3 (), Quaternion.identity) as GameObject;
		goMark.transform.parent = UICamera.transform;
		goMark.transform.localScale = new Vector3 (1, 1, 1);
		EnemyMark enemyMark = goMark.GetComponent<EnemyMark> ();
		enemyMark.PlayerCamera = PlayerCamera;
		enemyMark.Enemy = goEnemy;

		Enemy enemy = goEnemy.GetComponent<Enemy>();
		_enemyMarkMap[enemy] = enemyMark;
	}

	public void RemoveMark(object sender, EventEnemyLifeCycle e) {
		Enemy enemy = e.gameObject.GetComponent<Enemy>();
		EnemyMark enemyMark = _enemyMarkMap[enemy];
		enemyMark.transform.parent = null;
		Destroy(enemyMark.gameObject);
	}

}

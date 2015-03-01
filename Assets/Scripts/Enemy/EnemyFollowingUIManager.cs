using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFollowingUIManager : MonoBehaviour {

	public UIRoot UIRoot;
	public Camera PlayerCamera;
	public UICamera UICamera;
	public Transform Container;
	public EnemyFollowingUI PrefabEnemyFollowingUI;

	Dictionary<Enemy, EnemyFollowingUI> _followingUIMap = new Dictionary<Enemy, EnemyFollowingUI>();


	public void CreateUI(GameObject goEnemy) {
		GameObject go = Instantiate (PrefabEnemyFollowingUI.gameObject, new Vector3 (), Quaternion.identity) as GameObject;
		go.transform.parent = Container;
		go.transform.localScale = new Vector3 (1, 1, 1);
		go.transform.localRotation = Quaternion.identity;

		EnemyFollowingUI ui = go.GetComponent<EnemyFollowingUI> ();
		ui.UIRoot = UIRoot;
		ui.PlayerCamera = PlayerCamera;
		ui.Owner = goEnemy.GetComponent<Enemy>();

		Enemy enemy = goEnemy.GetComponent<Enemy>();
		_followingUIMap[enemy] = ui;
	}

	public void RemoveUI(object sender, EventEnemyLifeCycle e) {
		Enemy enemy = e.gameObject.GetComponent<Enemy>();
		EnemyFollowingUI ui = _followingUIMap[enemy];
		ui.transform.parent = null;
		Destroy(ui.gameObject);
	}

}

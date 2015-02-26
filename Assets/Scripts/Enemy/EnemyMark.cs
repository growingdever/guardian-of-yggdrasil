using UnityEngine;
using System.Collections;

public class EnemyMark : MonoBehaviour {

	public Camera PlayerCamera;
	public GameObject Enemy;

	UIRoot UIRoot;
	UISprite SpriteMark;


	void Start () {
		UIRoot = GameObject.Find ("UI").GetComponent<UIRoot> ();
		SpriteMark = this.GetComponent<UISprite> ();
	}
	
	void Update () {
		float ratio = (float)UIRoot.activeHeight / Screen.height;
		float uiWidth = Mathf.Ceil (Screen.width * ratio);
		float uiHeight = Mathf.Ceil (Screen.height * ratio);

		Vector3 worldPos = Enemy.transform.position;
		Vector3 viewportPos = PlayerCamera.WorldToViewportPoint (worldPos);
		SpriteMark.enabled = viewportPos.z >= 0;

		Vector3 uiPos = new Vector3 ();
		uiPos.x = (viewportPos.x - 0.5f) * uiWidth;
		uiPos.y = (viewportPos.y - 0.5f) * uiHeight;
		uiPos.z = 0;

		this.transform.localPosition = uiPos;
	}
}

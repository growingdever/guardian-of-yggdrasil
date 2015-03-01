using UnityEngine;
using System.Collections;

public class EnemyFollowingUI : MonoBehaviour {

	public UIRoot UIRoot;
	public Camera PlayerCamera;
	public Enemy Owner;
	
	public UISprite SpriteMark;
	public UISlider HPBar;


	void Start () {

	}
	
	void Update () {
		float ratio = (float)UIRoot.activeHeight / Screen.height;
		float uiWidth = Mathf.Ceil (Screen.width * ratio);
		float uiHeight = Mathf.Ceil (Screen.height * ratio);

		Vector3 worldPos = Owner.transform.position;
		Vector3 viewportPos = PlayerCamera.WorldToViewportPoint (worldPos);
		SpriteMark.gameObject.SetActive(viewportPos.z >= 0);
		HPBar.gameObject.SetActive (viewportPos.z >= 0);
		if (viewportPos.z < 0) {
			return;
		}

		Vector3 uiPos = new Vector3 ();
		uiPos.x = (viewportPos.x - 0.5f) * uiWidth;
		uiPos.y = (viewportPos.y - 0.5f) * uiHeight;
		uiPos.z = 0;
		this.transform.localPosition = uiPos;

		if( Owner.IsHPMax ) {
			HPBar.gameObject.SetActive(false);
		} else {
			HPBar.gameObject.SetActive(true);
			HPBar.value = 1.0f * Owner.HP / Owner.MaxHP;
		}
	}
}

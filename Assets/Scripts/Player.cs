using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public UISlider SliderHP;
	
	
	public int MaxHP {
		get;
		set;
	}
	
	int _hp;
	public int HP {
		get {
			return _hp;
		}
		set {
			_hp = value;
			if( _hp <= 0 ) {
				SliderHP.value = 1.0f * HP / MaxHP;
			}
		}
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

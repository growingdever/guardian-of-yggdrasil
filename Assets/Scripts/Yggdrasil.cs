using UnityEngine;
using System.Collections;

public class Yggdrasil : MonoBehaviour {

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
			if( _hp >= 0 ) {
				SliderHP.value = 1.0f * _hp / MaxHP;
			}
		}
	}


	void Awake () {
		MaxHP = 100;
		HP = MaxHP;
	}

	void Start () {
	
	}

	void Update () {
	
	}
}

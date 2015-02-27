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
			if( _hp >= 0 ) {
				SliderHP.value = 1.0f * _hp / MaxHP;
			}
		}
	}

	StateManager StateManager;


	void Awake () {
		MaxHP = 100;
		HP = MaxHP;

		RoundManager roundManager = GameObject.Find ("RoundManager").GetComponent<RoundManager> ();
		roundManager.OnRoundChangeCallbacks += this.OnRoundChanged;

		StateManager = GameObject.Find("UI").GetComponent<StateManager>();
	}

	void Start () {
	
	}

	void Update () {
	
	}

	void OnRoundChanged (object sender, EventRoundChange e) {
		StateManager.CurrentState = StateManager.EState.Upgrade;
	}

	public void UpgradeSpeed() {
		FlightController flightController = this.GetComponent<FlightController> ();
		flightController.UpgradeSpeed ();
	}

	public void UpgradeDamageOfBullet() {
		ShootProjectile comp = this.GetComponent<ShootProjectile> ();
		comp.UpgradeBulletDamage ();
	}

	public void UpgradeDamageOfMissile() {
		ShootProjectile comp = this.GetComponent<ShootProjectile> ();
		comp.UpgradeMissileDamage ();
	}

	public void UpgradeCoolDownOfMissile() {
		ShootProjectile comp = this.GetComponent<ShootProjectile> ();
		comp.UpgradeMissileDelay ();
	}
}

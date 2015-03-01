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

	ShootProjectile _shootProjectile;
	FlightController _flightController;

	public int SpeedLevel {
		get {
			return _flightController.CurrentUpgradeSpeedLevel + 1;
		}
	}
	public bool IsSpeedMaxLevel {
		get {
			return SpeedLevel >= _flightController.UPGRADE_TABLE_SPEED_NORMAL.Length;
		}
	}

	public int MachineGunPowerLevel {
		get {
			return _shootProjectile.BulletDamageLevel + 1;
		}
	}
	public bool IsMachineGunPowerMaxLevel {
		get {
			return MachineGunPowerLevel >= _shootProjectile.UPGRADE_TABLE_BULLET_DAMAGE.Length;
		}
	}

	public int MissilePowerLevel {
		get {
			return _shootProjectile.MissileDamageLevel + 1;
		}
	}
	public bool IsMissilePowerMaxLevel {
		get {
			return MissilePowerLevel >= _shootProjectile.UPGRADE_TABLE_MISSILE_DAMAGE.Length;
		}
	}

	public int MissileDelayLevel {
		get {
			return _shootProjectile.MissileDelayLevel + 1;
		}
	}
	public bool IsMissileDelayMaxLevel {
		get {
			return MissileDelayLevel >= _shootProjectile.UPGRADE_TABLE_MISSILE_DELAY.Length;
		}
	}


	void Awake () {
		MaxHP = 100;
		HP = MaxHP;

		RoundManager roundManager = GameObject.Find ("RoundManager").GetComponent<RoundManager> ();
		roundManager.OnRoundChangeCallbacks += this.OnRoundChanged;

		StateManager = GameObject.Find("UI").GetComponent<StateManager>();

		_shootProjectile = this.GetComponent<ShootProjectile>();
		_flightController = this.GetComponent<FlightController>();
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

		int level = flightController.CurrentUpgradeSpeedLevel;
		int max = flightController.UPGRADE_TABLE_SPEED_NORMAL.Length;
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

using UnityEngine;
using System.Collections;

public class FlightController : MonoBehaviour
{
	public enum FlightState {
		Normal,
		Booster,
		Decelerating,
	}

	public readonly int[] UPGRADE_TABLE_SPEED_NORMAL = { 0, 200, 220, 230, 240 };
	public readonly int[] UPGRADE_TABLE_SPEED_BOOSTER = { 200, 220, 240, 250, 260 };
	public readonly int[] UPGRADE_TABLE_SPEED_DECELERATING = { 160, 180, 200, 210, 220 };

	public float RotationPowerRoll = 0.3f;
	public float RotationPowerYaw = 0.3f;
	public float RotationPowerPitch = 0.3f;
	public float RollDampingFactor = 1.0f;

	public KeyCode KeyBooster;
	public KeyCode KeyDecelerating;

	FlightState _currentFlightState;
	public FlightState CurrentFlightState {
		get {
			return _currentFlightState;
		}
		set {
			_currentFlightState = value;
			switch( CurrentFlightState ) {
			case FlightState.Normal:
				_targetSpeed = SpeedOnNormal;
				break;
			case FlightState.Booster:
				_targetSpeed = SpeedOnBooster;
				break;
			case FlightState.Decelerating:
				_targetSpeed = SpeedOnDecelerating;
				break;
			}
		}
	}
	// kilometer per hour
	public float CurrentSpeed {
		get;
		private set;
	}
	public float SpeedOnNormal {
		get;
		private set;
	}
	public float SpeedOnBooster {
		get;
		private set;
	}
	public float SpeedOnDecelerating {
		get;
		private set;
	}
	public int CurrentUpgradeSpeedLevel {
		get;
		private set;
	}

	float _targetSpeed;


    void Start() {
		UpdateSpeedByUpgradeLevel ();
		CurrentFlightState = FlightState.Normal;
		CurrentSpeed = SpeedOnNormal;
    }

	void Update () {
		if( Input.GetKeyDown(KeyCode.LeftShift) ) {
			CurrentFlightState = FlightState.Decelerating;
		}
		if( Input.GetKeyUp(KeyCode.LeftShift) ) {
			CurrentFlightState = FlightState.Normal;
		}


		//
		// rotating
		//
		Vector3 dir = new Vector3(-Input.GetAxis("Vertical") * RotationPowerRoll, Input.GetAxis("Horizontal") * RotationPowerYaw);
		this.transform.Rotate(dir);

		Vector3 euler = this.transform.eulerAngles;
		float rollDamping;
		if( euler.z > 180 ) {
			rollDamping = 360 - euler.z;
		} else {
			rollDamping = -euler.z;
		}
		this.transform.Rotate(0, 0, rollDamping * Time.deltaTime * RollDampingFactor);


		//
		// moving
		//
		float dtSpeed = Mathf.Lerp (CurrentSpeed, _targetSpeed, Time.deltaTime * 2);
		CurrentSpeed = dtSpeed;
		float speedFactor = UnitCalculator.ToUnitFactorFromVelocity(CurrentSpeed);
		this.transform.position += this.transform.forward * speedFactor;
	}

	public void UpgradeSpeed() {
		CurrentUpgradeSpeedLevel++;
		UpdateSpeedByUpgradeLevel ();
	}

	void UpdateSpeedByUpgradeLevel() {
		SpeedOnNormal = Util.UpdateValueByTable (UPGRADE_TABLE_SPEED_NORMAL, CurrentUpgradeSpeedLevel);
		SpeedOnBooster = Util.UpdateValueByTable (UPGRADE_TABLE_SPEED_BOOSTER, CurrentUpgradeSpeedLevel);
		SpeedOnDecelerating = Util.UpdateValueByTable (UPGRADE_TABLE_SPEED_DECELERATING, CurrentUpgradeSpeedLevel);

		CurrentFlightState = CurrentFlightState;
	}
}

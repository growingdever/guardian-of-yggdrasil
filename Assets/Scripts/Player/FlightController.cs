using UnityEngine;
using System.Collections;

public class FlightController : MonoBehaviour
{
	public enum FlightState {
		Normal,
		Booster,
		Decelerating,
	}

	public readonly int[] UPGRADE_TABLE_SPEED_NORMAL = { 180, 200, 220, 230, 240 };
	public readonly int[] UPGRADE_TABLE_SPEED_BOOSTER = { 200, 220, 240, 250, 260 };
	public readonly int[] UPGRADE_TABLE_SPEED_DECELERATING = { 160, 180, 200, 210, 220 };

	public GameObject Model;
	public float AirResistanceRoll = 1.0f;
	public float AirResistanceYaw = 1.0f;
	public float AirResistancePitch = 1.0f;
	public float ModelRotationLimitRoll = 15;
	public float ModelRotationLimitYaw = 20;
	public float ModelRotationLimitPitch = 35;
	public float RotationPowerRoll = 15.0f;
	public float RotationPowerYaw = 15.0f;
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
		// air resistance make rotating model
		//
		{
			Model.transform.Rotate( new Vector3(-1 * Mathf.Abs(Input.GetAxis("Horizontal")) * AirResistanceRoll, 
			                                    Input.GetAxis("Horizontal") * AirResistanceYaw, 
			                                    -Input.GetAxis("Horizontal") * AirResistancePitch) );
			Vector3 euler = Model.transform.localEulerAngles;
			if( euler.x < 180 ) {
				euler.x = Mathf.Min( euler.x, ModelRotationLimitRoll );
			} else {
				euler.x = Mathf.Max( euler.x, 360 - ModelRotationLimitRoll );
			}
			if( euler.y < 180 ) {
				euler.y = Mathf.Min( euler.y, ModelRotationLimitYaw );
			} else {
				euler.y = Mathf.Max( euler.y, 360 - ModelRotationLimitYaw );
			}
			if( euler.z < 180 ) {
				euler.z = Mathf.Min( euler.z, ModelRotationLimitPitch );
			} else {
				euler.z = Mathf.Max( euler.z, 360 - ModelRotationLimitPitch );
			}
			Model.transform.localEulerAngles = euler;

			// damping rotation of model
			float rollDampingX = euler.x > 180 ? 360 - euler.x : -euler.x;
			float rollDampingY = euler.y > 180 ? 360 - euler.y : -euler.y;
			float rollDampingZ = euler.z > 180 ? 360 - euler.z : -euler.z;
			Model.transform.Rotate(rollDampingX * Time.deltaTime * RollDampingFactor, 
			                       rollDampingY * Time.deltaTime * RollDampingFactor,
			                       rollDampingZ * Time.deltaTime * RollDampingFactor);
		}


		//
		// rotating
		//
		{
			Vector3 dir = new Vector3(-Input.GetAxis("Vertical") * RotationPowerRoll, Input.GetAxis("Horizontal") * RotationPowerYaw);
			this.transform.Rotate(dir * Time.deltaTime);
			
			Vector3 euler = this.transform.eulerAngles;
			float rollDamping;
			if( euler.z > 180 ) {
				rollDamping = 360 - euler.z;
			} else {
				rollDamping = -euler.z;
			}
			this.transform.Rotate(0, 0, rollDamping * Time.deltaTime * RollDampingFactor);
		}


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

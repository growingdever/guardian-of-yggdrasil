using UnityEngine;
using System.Collections;

public class FlightController : MonoBehaviour
{
	public float RotationPowerRoll = 0.3f;
	public float RotationPowerYaw = 0.3f;
	public float RotationPowerPitch = 0.3f;
	public float RollDampingFactor = 1.0f;
	
    public float KilometerPerHour = 180.0f;


    void Start() {

    }

	void Update () {
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

		float speedFactor = UnitCalculator.ToUnitFactorFromVelocity(KilometerPerHour);
		this.transform.position += this.transform.forward * speedFactor;
	}
}

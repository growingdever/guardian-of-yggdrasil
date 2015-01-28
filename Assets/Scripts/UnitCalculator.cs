using UnityEngine;
using System.Collections;

public class UnitCalculator {



	public static float ToUnitFactorFromVelocity(float kmPerHour, float unitPerMeter = 1.0f, int meterPerKM = 1000, int secondsPerHour = 3600) {
		float meterPerSecond = kmPerHour * meterPerKM / secondsPerHour;
		float unitPerSecond = meterPerSecond / unitPerMeter;
		float fps = 1.0f / Time.deltaTime;
		float speedFactor = unitPerSecond / fps;
		return speedFactor;
	}
	
}

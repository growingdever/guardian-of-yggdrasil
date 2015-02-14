using UnityEngine;
using System.Collections;

public class EnemyMovingLinear : EnemyMoving {

	public Transform TargetPoint;

	protected override void Move() {
		Vector3 dir = TargetPoint.transform.position - this.transform.position;
		Vector3 norm = dir.normalized;
		float speedFactor = UnitCalculator.ToUnitFactorFromVelocity(KilometerPerHour);
		
		this.transform.position += norm * speedFactor;
	}
}

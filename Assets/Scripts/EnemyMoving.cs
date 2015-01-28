using UnityEngine;
using System.Collections;

public class EnemyMoving : MonoBehaviour {

	public Transform TargetPoint;
	public float KilometerPerHour = 120.0f;

	float _aliveTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		_aliveTime += Time.deltaTime;

		Vector3 dir = TargetPoint.transform.position - this.transform.position;
		Vector3 norm = dir.normalized;

		float speedFactor = UnitCalculator.ToUnitFactorFromVelocity(KilometerPerHour);

		Vector3 movement = norm * speedFactor;
		movement.y += Mathf.Abs( Mathf.Sin(_aliveTime) ) * 0.2f;

		this.transform.position += movement;
	}
}

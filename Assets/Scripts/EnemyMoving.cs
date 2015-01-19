using UnityEngine;
using System.Collections;

public class EnemyMoving : MonoBehaviour {

	public Transform TargetPoint;
	public float MoveSpeed;

	float _aliveTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		_aliveTime += Time.deltaTime;

		Vector3 dir = TargetPoint.transform.position - this.transform.position;
		dir.Normalize();

		Vector3 movement = dir * MoveSpeed;
		movement.y += Mathf.Abs( Mathf.Sin(_aliveTime) ) * 0.2f;

		this.transform.position += movement;
//		this.transform.forward = (TargetPoint.transform.position - this.transform.position).normalized;
	}
}

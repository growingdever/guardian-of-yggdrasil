using UnityEngine;
using System.Collections;

public abstract class EnemyMoving : MonoBehaviour {

	public float KilometerPerHour = 80;

	public virtual void Start () {
	
	}

	public virtual void Update () {
		Move ();
	}

	protected abstract void Move();
}

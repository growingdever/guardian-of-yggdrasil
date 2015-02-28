using UnityEngine;
using System.Collections;

public class PlayerMissile : Projectile {

	void Awake() {
		EffectSettings setting = this.GetComponent<EffectSettings>();
		setting.Target = this.transform.FindChild ("Target").gameObject; // cheating
		setting.DeactivateTimeDelay = LifeTime;
		setting.IsHomingMove = false;
		setting.MoveSpeed = 0;
	}

	protected override void Move ()
	{
		this.transform.position += this.transform.forward * this.Speed;
	}

}

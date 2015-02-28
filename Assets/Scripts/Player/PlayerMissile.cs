using UnityEngine;
using System.Collections;

public class PlayerMissile : Projectile {

	void Awake() {
		EffectSettings setting = this.GetComponent<EffectSettings>();
		setting.Target = this.transform.FindChild ("Target").gameObject; // cheating
		setting.DeactivateTimeDelay = LifeTime;
	}

	protected override void Move ()
	{
		this.transform.position += this.transform.forward * this.Speed;
	}

}

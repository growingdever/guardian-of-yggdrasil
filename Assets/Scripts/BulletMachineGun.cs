using UnityEngine;
using System.Collections;

public class BulletMachineGun : Projectile {

	protected override void Move ()
	{
		// this is very special case...
		this.transform.position += this.transform.up * this.Speed;
	}

}

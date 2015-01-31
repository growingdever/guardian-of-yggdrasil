using UnityEngine;
using System.Collections;

public class BulletMachineGun : Projectile {

	void Update () {
		// this is very special case...
		this.transform.position += this.transform.up * this.Speed;
	}

}

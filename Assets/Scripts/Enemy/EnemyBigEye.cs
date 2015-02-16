using UnityEngine;
using System.Collections;
using System;


public class EnemyBigEye : Enemy {

	public readonly int[] ROUND_TABLE_DAMAGE_COLLIDED_YGGDRASIL = { 5, };
	public readonly int[] ROUND_TABLE_DAMAGE_COLLIDED_PLAYER = { 5, };
	
	protected override void OnRoundChanged (object sender, EventRoundChange e)
	{
		int round = e.round;

		int i = round < ROUND_TABLE_DAMAGE_COLLIDED_YGGDRASIL.Length ? round : ROUND_TABLE_DAMAGE_COLLIDED_YGGDRASIL.Length - 1;
		DamageCollidedYggdrasil = ROUND_TABLE_DAMAGE_COLLIDED_YGGDRASIL [i];

		i = round < ROUND_TABLE_DAMAGE_COLLIDED_PLAYER.Length ? round : ROUND_TABLE_DAMAGE_COLLIDED_PLAYER.Length - 1;
		DamageCollidedPlayer = ROUND_TABLE_DAMAGE_COLLIDED_PLAYER [i];
	}
	
	protected override void OnCollidedWithYggdrasil (Yggdrasil yggdrasil)
	{
		yggdrasil.HP -= DamageCollidedYggdrasil;
	}
	
	protected override void OnCollidedWithPlayer (Player player)
	{
		player.HP -= DamageCollidedPlayer;
	}

}

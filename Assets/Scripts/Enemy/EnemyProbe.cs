using UnityEngine;
using System.Collections;
using System;


public class EnemyProbe : Enemy {

	public readonly int[] ROUND_TABLE_MAX_HP = { 10, };
	public readonly int[] ROUND_TABLE_DAMAGE_COLLIDED_YGGDRASIL = { 5, };
	public readonly int[] ROUND_TABLE_DAMAGE_COLLIDED_PLAYER = { 5, };


	void Start() {
		MaxHP = HP = Util.UpdateValueByTable (ROUND_TABLE_MAX_HP, RoundManager.CurrentRound);
	}

	protected override void OnRoundChanged (object sender, EventRoundChange e)
	{
		int round = e.round;

		DamageCollidedYggdrasil = Util.UpdateValueByTable (ROUND_TABLE_DAMAGE_COLLIDED_YGGDRASIL, round);
		DamageCollidedPlayer = Util.UpdateValueByTable (ROUND_TABLE_DAMAGE_COLLIDED_PLAYER, round);
	}

	protected override void OnCollidedWithYggdrasil (Yggdrasil yggdrasil)
	{
		yggdrasil.HP -= DamageCollidedYggdrasil;
		print (DamageCollidedYggdrasil + " " + yggdrasil.HP);
		this.HP = 0;
	}
	
	protected override void OnCollidedWithPlayer (Player player)
	{
		player.HP -= DamageCollidedPlayer;
		this.HP = 0;
	}

}

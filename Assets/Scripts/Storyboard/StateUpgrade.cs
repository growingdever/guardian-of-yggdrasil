using UnityEngine;
using System.Collections;

public class StateUpgrade : State {

	public Player player;

	protected override void Awake ()
	{
		base.Awake ();

		StateSymbol = StateManager.EState.Upgrade;
	}

	override protected void Start () {
	
	}
	
	override protected void Update () {
	
	}

	public override void OnEnter ()
	{
		base.OnEnter ();

		Time.timeScale = 0.0f;
	}

	public override void OnExit ()
	{
		base.OnExit ();

		Time.timeScale = 1.0f;
	}

	public void OnClickSpeedUp()
	{
		player.UpgradeSpeed ();
		StartNextRound ();
	}

	public void OnClickBulletDamageUp()
	{
		player.UpgradeDamageOfBullet ();
		StartNextRound ();
	}

	public void OnClickMissileDamageUp()
	{
		player.UpgradeDamageOfMissile ();
		StartNextRound ();
	}

	public void OnClickMissileCoolDown()
	{
		player.UpgradeCoolDownOfMissile ();
		StartNextRound ();
	}

	void StartNextRound()
	{
		StateManager.CurrentState = StateManager.EState.Game;
	}
}

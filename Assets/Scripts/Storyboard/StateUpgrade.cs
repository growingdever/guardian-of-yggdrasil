using UnityEngine;
using System.Collections;

public class StateUpgrade : State {

	public Player player;
	public UILabel LabelFlightSpeedLevel;
	public UIButton ButtonFlightSpeed;
	public UILabel LabelBulletDamageLevel;
	public UIButton ButtonBulletDamage;
	public UILabel LabelMissileDamageLevel;
	public UIButton ButtonMissileDamage;
	public UILabel LabelMissileDelayLevel;
	public UIButton ButtonMissileDelay;


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

		PauseManager.Instance.Pause = true;
	}

	public override void OnExit ()
	{
		base.OnExit ();

		PauseManager.Instance.Pause = false;
	}

	public void OnClickSpeedUp()
	{
		player.UpgradeSpeed ();
		UpdateByStatus();
	}

	public void OnClickBulletDamageUp()
	{
		player.UpgradeDamageOfBullet ();
		UpdateByStatus();
	}

	public void OnClickMissileDamageUp()
	{
		player.UpgradeDamageOfMissile ();
		UpdateByStatus();
	}

	public void OnClickMissileCoolDown()
	{
		player.UpgradeCoolDownOfMissile ();
		UpdateByStatus();
	}

	public void StartNextRound()
	{
		StateManager.CurrentState = StateManager.EState.Game;
	}

	void UpdateByStatus() {
		LabelFlightSpeedLevel.text = string.Format("Level : {0}", player.SpeedLevel);
		LabelBulletDamageLevel.text = string.Format("Level : {0}", player.MachineGunPowerLevel);
		LabelMissileDamageLevel.text = string.Format("Level : {0}", player.MissilePowerLevel);
		LabelMissileDelayLevel.text = string.Format("Level : {0}", player.MissileDelayLevel);

		ButtonFlightSpeed.isEnabled = ! player.IsSpeedMaxLevel;
		ButtonBulletDamage.isEnabled = ! player.IsMachineGunPowerMaxLevel;
		ButtonMissileDamage.isEnabled = ! player.IsMissilePowerMaxLevel;
		ButtonMissileDelay.isEnabled = ! player.IsMissileDelayMaxLevel;
	}
}

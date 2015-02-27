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
		FlightController flightController = player.GetComponent<FlightController> ();
		flightController.KilometerPerHour += 10.0f;

		StartNextRound ();
	}

	public void OnClickBulletDamageUp()
	{
		StartNextRound ();
	}

	public void OnClickMissileDamageUp()
	{
		StartNextRound ();
	}

	public void OnClickMissileCoolDown()
	{
		StartNextRound ();
	}

	void StartNextRound()
	{
		StateManager.CurrentState = StateManager.EState.Game;
	}
}

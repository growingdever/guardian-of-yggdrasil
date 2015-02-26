using UnityEngine;
using System.Collections;

public class StateUpgrade : State {

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
		StateManager.CurrentState = StateManager.EState.Game;
	}

	public void OnClickBulletDamageUp()
	{
		
	}

	public void OnClickMissileDamageUp()
	{
		
	}

	public void OnClickMissileCoolDown()
	{
		
	}
}

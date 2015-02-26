using UnityEngine;
using System.Collections;

public class StateGame : State {

	protected override void Awake ()
	{
		base.Awake ();
		
		StateSymbol = StateManager.EState.Game;
	}

	override protected void Start () {
	
	}
	
	override protected void Update () {
	
	}
}

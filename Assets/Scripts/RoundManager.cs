using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class EventRoundChange : EventArgs
{
	public int round;
}

public class RoundManager : MonoBehaviour {

	public UILabel LabelNextRound;
	public UILabel LabelCurrentRound;
	public float SecondsPerRound;

	public event EventHandler<EventRoundChange> OnRoundChangeCallbacks;

	int _currentRound;
	public int CurrentRound {
		get {
			return _currentRound;
		}
		private set {
			_currentRound = value;

			var e = new EventRoundChange {round = this.CurrentRound};
			OnRoundChangeCallbacks( this, e );
		}
	}

	
	void Start () {
		StartCoroutine( "IncreaseRound" );

		CurrentRound = 1;
		LabelNextRound.text = SecondsPerRound + "";
		LabelCurrentRound.text = CurrentRound + "";
	}

	IEnumerator IncreaseRound() {
		// temporary :)
		for( int i = 0; i < 999; i ++ ) {
			yield return StartCoroutine(CountDown());
			CurrentRound++;
			LabelCurrentRound.text = CurrentRound + "";
		}

		yield break;
	}

	IEnumerator CountDown() {
		for( int i = (int)SecondsPerRound; i >= 0; i -- ) {
			LabelNextRound.text = i + "";
			yield return new WaitForSeconds(1.0f);
		}
	}
	
}

using UnityEngine;
using System.Collections;

public class RoundManager : MonoBehaviour {

	public UILabel LabelNextRound;
	public UILabel LabelCurrentRound;
	public float SecondsPerRound;
	
	public int CurrentRound {
		get;
		private set;
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

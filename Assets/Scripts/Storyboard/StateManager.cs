using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {

	public enum EState {
		Game,
		Upgrade,
	}

	public State[] States;
	State _currState;

	EState _currStateEnum;
	public EState CurrentState {
		get {
			return _currStateEnum;
		}

		set {
			_currStateEnum = value;
			UpdateByState(value);
		}
	}


	void Start () {
		CurrentState = EState.Game;
	}
	
	void UpdateByState(EState nextEnum) {
		State next = null;
		foreach(State state in States) {
			if( state.StateSymbol == nextEnum ) {
				next = state;
				break;
			}
		}

		if( _currState ) {
			_currState.OnExit();
		}

		_currState = next;

		if( _currState ) {
			_currState.OnEnter();
		}
	}
}

using UnityEngine;
using System.Collections;

public class State : MonoBehaviour {

	protected StateManager StateManager;
	public StateManager.EState StateSymbol;

	public bool Visible {
		get {
			return this.GetComponent<Camera>().enabled;
		}
		set {
			this.GetComponent<Camera>().enabled = value;
			this.GetComponent<UICamera>().enabled = value;
		}
	}


	virtual protected void Awake() {
		StateManager = GameObject.Find("UI").GetComponent<StateManager>();
	}

	virtual protected void Start () {
	
	}
	
	virtual protected void Update () {
	
	}

	virtual public void OnEnter() {
		Visible = true;
	}

	virtual public void OnExit() {
		Visible = false;
	}
}

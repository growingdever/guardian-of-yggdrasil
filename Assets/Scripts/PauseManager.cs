using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour
{
	bool _pause;
	public bool Pause {
		get {
			return _pause;
		}
		set {
			_pause = value;
			if( _pause ) {
				Time.timeScale = 0.0f;
			} else {
				Time.timeScale = 1.0f;
			}
		}
	}


	private static PauseManager _instance;
	public static PauseManager Instance {  
		get {  
			if (!_instance) {  
				_instance = GameObject.FindObjectOfType (typeof(PauseManager)) as PauseManager;
				if (!_instance) {
					GameObject container = new GameObject ();  
					container.name = "PauseManager";  
					_instance = container.AddComponent (typeof(PauseManager)) as PauseManager;  
				}
			}  
			
			return _instance;  
		}  
	}  
}

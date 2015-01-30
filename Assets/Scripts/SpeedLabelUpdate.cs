using UnityEngine;
using System.Collections;

public class SpeedLabelUpdate : MonoBehaviour {

	public FlightController Flight;

	UILabel Label;

	void Start () {
		Label = this.GetComponent<UILabel>();
	}

	void Update () {
		Label.text = Flight.KilometerPerHour + " km/h";
	}
}

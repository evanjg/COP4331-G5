using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.MeshGeneration;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using AssemblyCSharp; 
using Newtonsoft.Json;
using UnityEngine.UI;

public class GeoPoints : MonoBehaviour {
	
	public TextAsset geoPointsText;
	public Text t;

	[HideInInspector]
	private List<GeoPoint> points;


	GameObject player;

	void Start () {


		string json = geoPointsText.text;

		points = JsonConvert.DeserializeObject<List<GeoPoint>>(json);

		player = GameObject.FindWithTag ("Player");
	}


	void Update () {
		
		if (MapController.ReferenceTileRect == null)
		{
			return;
		}
		// If player is within a distance of 10 away from the point, change player color to blue.
		foreach (GeoPoint geoPoint in points) {
			Vector2d point = new Vector2d (geoPoint.latitude, geoPoint.longitude);
			Vector3 pointOnMap = Conversions.GeoToWorldPosition(point, MapController.ReferenceTileRect.Center, MapController.WorldScaleFactor).ToVector3xz();

			Vector2d playerPosition = new Vector2d (player.transform.position.x, player.transform.position.z);

			if (Mapbox.Utils.Vector2d.Distance (new Vector2d (pointOnMap.x, pointOnMap.z), playerPosition) < 10) {
				player.GetComponent<Renderer> ().material.color = Color.blue;
				t.text = geoPoint.description.ToString ();
				break;
			} else {
				player.GetComponent<Renderer> ().material.color = Color.green;
				t.text = "";
			}

		}

	}
}

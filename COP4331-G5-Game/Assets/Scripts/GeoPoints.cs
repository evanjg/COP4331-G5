using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.MeshGeneration;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;

public class GeoPoints : MonoBehaviour {
	
	public TextAsset geoPoints;

	[HideInInspector]
	private List<Vector2d> points;

	GameObject player;

	void Start () {
		points = new List<Vector2d> ();

		string tempString = geoPoints.text;

		List<string> line = new List<string> ();
		line.AddRange (tempString.Split ("\n" [0]));

		List<string> temp = new List<string> ();
		foreach (string geo in line) {
			temp.AddRange (geo.Split ("," [0]));
			points.Add (new Vector2d(float.Parse (temp [0]), float.Parse (temp [1])));
			temp.Clear ();
		}
		player = GameObject.FindWithTag ("Player");
	}


	void Update () {

		// If player is within a distance of 10 away from the point, change player color to blue.
		foreach (Vector2d point in points) {
			Vector3 pointOnMap = Conversions.GeoToWorldPosition(point, MapController.ReferenceTileRect.Center, MapController.WorldScaleFactor).ToVector3xz();

			Vector2d playerPosition = new Vector2d (player.transform.position.x, player.transform.position.z);

			if (Mapbox.Utils.Vector2d.Distance(new Vector2d(pointOnMap.x,pointOnMap.z), playerPosition) < 10) {
				player.GetComponent<Renderer>().material.color = Color.blue;
				break;
			}
			else
				player.GetComponent<Renderer> ().material.color = Color.green;

		}
	}
}

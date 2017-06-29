using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Map;
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
	    CheckPlayerPostion();

	}

    private void CheckPlayerPostion()
    {
        Vector2d playerLatLongPosition = player.transform.GetGeoPosition(MapController.ReferenceTileRect.Center, MapController.WorldScaleFactor);

        // If player is within a distance of 50 feet away from the point, change player color to blue.
        foreach (GeoPoint geoPoint in points)
        {

            if (geoPoint.BoundingCircle(playerLatLongPosition.x,playerLatLongPosition.y,50))
            {
                player.GetComponent<Renderer>().material.color = Color.blue;
                t.text = geoPoint.description.ToString();
                break;
            }
            else
            {
                player.GetComponent<Renderer>().material.color = Color.green;
                //t.text = "";
            }

        }
    }
}

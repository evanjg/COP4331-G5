using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Mapbox.Map;
using UnityEngine;
using Mapbox.Unity.MeshGeneration;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using AssemblyCSharp; 
using Newtonsoft.Json;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GeoPoints : MonoBehaviour {
	
	public TextAsset geoPointsText;
	public Text t;

	[HideInInspector]
	private List<GeoPoint> points;

	GameObject player;
    public GameObject prefab;

    private int minutesSinceEpoch;

	void Start () {
		player = GameObject.FindWithTag ("Player");

        readJsonFile();
        Debug.Log(Application.persistentDataPath + @"/POI.json");
	}

    void readJsonFile()
    {
        Debug.Log("Read From File");
        try
        {
            using (StreamReader file = File.OpenText(Application.persistentDataPath + @"/POI.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                points = (List<GeoPoint>)serializer.Deserialize(file, typeof(List<GeoPoint>));
            }
        }
        catch (Exception)
        {
            Debug.Log("here");
            string json = geoPointsText.text;

            points = JsonConvert.DeserializeObject<List<GeoPoint>>(json);
        }
       
    }

    void writeJsonFile()
    {
        Debug.Log("Write To File");
        using (FileStream fs = File.Open(Application.persistentDataPath + @"/POI.json", FileMode.Create))

        using (StreamWriter sw = new StreamWriter(fs))

        using (JsonWriter jw = new JsonTextWriter(sw))
        {
            jw.Formatting = Formatting.Indented;

            JsonSerializer serializer = new JsonSerializer();

            serializer.Serialize(jw, points);

        }
    }

	void Update () {

	    if (MapController.ReferenceTileRect == null)
	    {
	        return;
	    }

        
        minutesSinceEpoch = getMinutes();

	    CheckPlayerPostion();

	}

    private int getMinutes()
    {
        TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
        return (int)t.TotalMinutes;
    }


    private void CheckPlayerPostion()
    {
        Vector2d playerLatLongPosition = player.transform.GetGeoPosition(MapController.ReferenceTileRect.Center, MapController.WorldScaleFactor);

        // If player is within a distance of 50 feet away from the point, change player color to blue.
        foreach (GeoPoint geoPoint in points)
        {
            if (geoPoint.BoundingCircle(playerLatLongPosition.x,playerLatLongPosition.y,50) && (geoPoint.timeSinceVisited <= minutesSinceEpoch))
            {
                
                Vector3 cratePosition =
                    Conversions.GeoToWorldPosition(geoPoint.latitude, geoPoint.longitude, MapController.ReferenceTileRect.Center,MapController.WorldScaleFactor)
                        .ToVector3xz();
                Instantiate(prefab, cratePosition + new Vector3(0, 10, 0), Quaternion.identity);
                geoPoint.timeSinceVisited = getMinutes() + 5;
                writeJsonFile();
                break;
            }
        }
    }
}

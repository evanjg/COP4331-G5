using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Configuration;
using Mapbox.Map;
using UnityEngine;
using Mapbox.Unity.MeshGeneration;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
//using AssemblyCSharp; 
using Newtonsoft.Json;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// This script no longer used, replaced by PawstopManager

public class GeoPoints : MonoBehaviour {
	
	public TextAsset geoPointsText;
	public Text t;

    public int timeSpanInSeconds;
	[HideInInspector]
	private List<GeoPoint> points;

	GameObject player;
    public GameObject prefab;

    private int secondsSinceEpoch;


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

    /*void CheckCrates()
    {
        foreach (GeoPoint geoPoint in points)
        {
            if (geoPoint.getCrate() == null && geoPoint.getScaled())
            {
                geoPoint.setScaled(false);
                geoPoint.timeSinceVisited = getSeconds() + timeSpanInSeconds;
                writeJsonFile();
            }
            if (geoPoint.getCrate() == null && (geoPoint.timeSinceVisited <= secondsSinceEpoch))
            {
                Vector3 cratePosition =
                    Conversions.GeoToWorldPosition(geoPoint.latitude, geoPoint.longitude,
                        MapController.ReferenceTileRect.Center, MapController.WorldScaleFactor)
                        .ToVector3xz();
                GameObject crate = Instantiate(prefab, cratePosition + new Vector3(0, 10, 0), Quaternion.identity);
                geoPoint.setCrate(crate);
            }
            
        }
    }*/


	void Update () {

	    if (MapController.ReferenceTileRect == null)
	    {
	        return;
	    }
	   
        
        secondsSinceEpoch = getSeconds();

        //CheckCrates();
	    //CheckPlayerPostion();

	}

    private int getSeconds()
    {
        TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
        return (int)t.TotalSeconds;
    }


    private void CheckPlayerPostion()
    {
        Vector2d playerLatLongPosition = player.transform.GetGeoPosition(MapController.ReferenceTileRect.Center, MapController.WorldScaleFactor);

        // If player is within a distance of 50 feet away from the point, scale the player up.
        foreach (GeoPoint geoPoint in points)
        {
            // Check if player is in the bounding circle.
            bool isInBoundingCircle = geoPoint.BoundingCircle(playerLatLongPosition.x, playerLatLongPosition.y, 50);

            /*
            // If the player is in the circle, scale the crate up.
            if (!geoPoint.getScaled() && isInBoundingCircle && (geoPoint.timeSinceVisited <= secondsSinceEpoch))
            {
                geoPoint.setScaled(true);

                StartCoroutine(ScaleCrateUp(geoPoint.getCrate()));
                break;
            }
            // If the crate is scaled and the player left the bounding circle, scale the crate down.
            if (geoPoint.getScaled() && !isInBoundingCircle && geoPoint.getCrate() != null)
            {
                geoPoint.setScaled(false);
                StartCoroutine(ScaleCrateDown(geoPoint.getCrate()));
            }*/
        }
    }
    /*
    IEnumerator ScaleCrateUp(GameObject crate)
    {
        while (crate.transform.localScale.x <= 2)
        {
            crate.transform.localScale += new Vector3(2 * Time.deltaTime, 2 * Time.deltaTime, 2 * Time.deltaTime);
            crate.transform.position += new Vector3(0, 1 * Time.deltaTime, 0);
            yield return null;
        }
    }
    IEnumerator ScaleCrateDown(GameObject crate)
    {
        
        while (crate.transform.localScale.x >= 1)
        {
            crate.transform.localScale -= new Vector3(2 * Time.deltaTime, 2 * Time.deltaTime, 2 * Time.deltaTime);
            crate.transform.position -= new Vector3(0, 1 * Time.deltaTime, 0);
            yield return null;
        }
    }*/
}

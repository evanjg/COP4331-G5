using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mapbox.Utils;
using Mapbox.Unity.Utilities;

using Newtonsoft.Json;

// Most of this code from GeoPoints.cs

public class PawstopManager : MonoBehaviour {
	public TextAsset geoPointsText;
	public Pawstop pawstopPrefab;

	public List<GeoPoint> points;
	public List<Pawstop> pawstops = new List<Pawstop>();
	private Player player;
	public GameObject avatar;

	public BoxGiver boxGiver = new BoxGiver();

	public float timeUntilSave = 10.0f;

	public static int GetTimestamp() {
        return (int) (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
    }

	void Awake() {
		player = Player.instance;
	}

	// Use this for initialization
	void Start () {
		Invoke("LoadPawstops", .5f);
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePawstops();
	}

	private void LoadPawstops() {
		/*try {
			string fileName = Application.persistentDataPath + "/POI.json";
			using (StreamReader file = File.OpenText(fileName)) {
				JsonSerializer serializer = new JsonSerializer();
				points = (List<GeoPoint>) serializer.Deserialize(file, typeof(List<GeoPoint>));
			}
			Debug.Log("Loaded from " + fileName);
		} catch (Exception) {*/
			string json = geoPointsText.text;
			points = JsonConvert.DeserializeObject<List<GeoPoint>>(json);
		//}
		//Debug.Log("Loaded " + points.Count + " points");
		// Instantiate prefabs at proper position
		foreach (GeoPoint geoPoint in points) {
            //Debug.Log(geoPoint.description);
			Pawstop pawstop = Instantiate<Pawstop>(pawstopPrefab);
			//pawstop.transform.SetParent(transform);
			pawstop.name = "Pawstop " + geoPoint.id + " (" + geoPoint.name + ")";
			pawstop.transform.position = Conversions.GeoToWorldPosition(
				geoPoint.latitude, geoPoint.longitude,
				MapController.ReferenceTileRect.Center, MapController.WorldScaleFactor
			).ToVector3xz();
            Debug.Log(geoPoint.description + ": " + pawstop.transform.position);
			pawstop.Setup(geoPoint);
			pawstop.onBoxCollected.AddListener( () => boxGiver.GiveABox());
			pawstops.Add(pawstop);
		}
	}

	private void WriteJsonFile() {
		using (FileStream fs = File.Open(Application.persistentDataPath + @"/POI.json", FileMode.Create))

        using (StreamWriter sw = new StreamWriter(fs))

        using (JsonWriter jw = new JsonTextWriter(sw))
        {
            jw.Formatting = Formatting.Indented;

            JsonSerializer serializer = new JsonSerializer();

            serializer.Serialize(jw, points);

        }
	}

	private void UpdatePawstops() {
		if (avatar != null) {
			Vector2d playerLatLongPosition = avatar.transform.GetGeoPosition(
				MapController.ReferenceTileRect.Center, MapController.WorldScaleFactor);

			if (pawstops != null) {
               
				foreach (Pawstop pawstop in pawstops) {
                    pawstop.SetIsActive(pawstop.geoPoint.BoundingCircle(playerLatLongPosition.x, playerLatLongPosition.y, pawstop.geoPoint.radius));
				}
			}
		}
	}
}

using UnityEngine;
using Mapbox.Unity.Location;
using Mapbox.Unity.MeshGeneration;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using System.Collections;
using UnityEngine.UI;

public class MapUpdateFOV : MonoBehaviour
{
	public GameObject player;

   	public MapController mapController;
    private Camera camera;

    [SerializeField]
    private int range = 1;

    Ray ray;
    float hitDistance;
    Plane yPlane;
    Vector3 cameraTarget;
    Vector2 cachedTile;
    Vector2 currentTile;
	int count = 0;

    void Start()
    {
        mapController = GetComponent<MapController>();
        camera = Camera.main;
        yPlane = new Plane(Vector3.up, Vector3.zero);
    }

    void Update()
    {
		ray = camera.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));
		if (yPlane.Raycast (ray, out hitDistance)) {
			cameraTarget = ray.GetPoint (hitDistance) / MapController.WorldScaleFactor;
			currentTile = Conversions.MetersToTile (new Vector2d (MapController.ReferenceTileRect.Center.x + cameraTarget.x, MapController.ReferenceTileRect.Center.y + cameraTarget.z), mapController.Zoom);
			if (currentTile != cachedTile) {
				for (int i = -range; i <= range; i++) {
					for (int j = -range; j <= range; j++) {
						mapController.Request (new Vector2 (currentTile.x + i, currentTile.y + j), mapController.Zoom);
					}
				}
				cachedTile = currentTile;
			}
		}
    }
}

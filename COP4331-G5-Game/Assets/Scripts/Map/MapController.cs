using UnityEngine;
using System.Collections.Generic;
using Mapbox.Unity.MeshGeneration.Data;
using Mapbox.Unity;
using Mapbox.Platform;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using Mapbox.Unity.MeshGeneration;
using UnityEngine.UI;
/// <summary>
/// MapController is just an helper class imitating the game/app logic controlling the map. It creates and passes the tiles requests to MapVisualization.
/// </summary>
public class MapController : MonoBehaviour
{
    public static RectD ReferenceTileRect { get; set; }
    public static float WorldScaleFactor { get; set; }

    public MapVisualization mapVisualization;
    public float TileSize = 100;

    //[SerializeField]
    //private bool snapYToZero = true;

	public Text countText;

    [HideInInspector]
	public double currentLat = 0;
    [HideInInspector]
	public double currentLong = 0;

    public int Zoom;
    public Vector4 Range;



    private GameObject root;
    private Dictionary<Vector2, UnityTile> tiles;

	public bool MapGen = false;
	public int MaxTile = 20;

	private int count = 0;


    /// <summary>
    /// Resets the map controller and initializes the map visualization
    /// </summary>
    public void Awake()
    {
        mapVisualization.Initialize(MapboxAccess.Instance);
        tiles = new Dictionary<Vector2, UnityTile>();
    }

    public void Start()
    {
		
    }

    public void Update()
    {
		
    }

    // Initial Map Setup
    public void Execute(double lat, double lng, int zoom, Vector4 frame)
    {
        GameObject r = GameObject.Find("worldRoot");

        if (r!=null)
        {
            root = r;
            return;
        }
        //frame goes left-top-right-bottom here
        if (root != null)
        {
            foreach (Transform t in root.transform)
            {
                Destroy(t.gameObject);
            }
        }

        root = new GameObject("worldRoot");
        
        DontDestroyOnLoad(root);

        var v2 = Conversions.GeoToWorldPosition(lat, lng, new Vector2d(0, 0));
        var tms = Conversions.MetersToTile(v2, zoom);
        ReferenceTileRect = Conversions.TileBounds(tms, zoom);
        WorldScaleFactor = (float)(TileSize / ReferenceTileRect.Size.x);
        root.transform.localScale = Vector3.one * WorldScaleFactor;

		pullRootDown();
    }

	/// Pulls the root world object to origin for ease of use/view
	private void pullRootDown()
	{
		var ray = new Ray(new Vector3(0, 1000, 0), Vector3.down);
		RaycastHit rayhit;
		if (Physics.Raycast (ray, out rayhit)) {
			root.transform.position = new Vector3 (0, -rayhit.point.y, 0);
		}
	}

    public void Execute(double lat, double lng, int zoom, Vector2 frame)
    {
        Execute(lat, lng, zoom, new Vector4(frame.x, frame.y, frame.x, frame.y));
    }

    public void Execute(double lat, double lng, int zoom, int range)
    {
        Execute(lat, lng, zoom, new Vector4(range, range, range, range));
    }

    /// <summary>
    /// Used for loading new tiles on the existing world. Unlike Execute function, doesn't destroy the existing ones.
    /// </summary>
    /// <param name="pos">Tile coordinates of the requested tile</param>
    /// <param name="zoom">Zoom/Detail level of the requested tile</param>
    public void Request(Vector2 pos, int zoom)
    {
		
		if (!tiles.ContainsKey(pos) && (count < MaxTile))
        {
            var tile = new GameObject("Tile - " + pos.x + " | " + pos.y).AddComponent<UnityTile>();
            tiles.Add(pos, tile);
            tile.transform.SetParent(root.transform, false);
            tile.Zoom = zoom;
            tile.TileCoordinate = new Vector2(pos.x, pos.y);
            tile.Rect = Conversions.TileBounds(tile.TileCoordinate, zoom);
            tile.RelativeScale = Conversions.GetTileScaleInMeters(0, Zoom) / Conversions.GetTileScaleInMeters((float)Conversions.MetersToLatLon(tile.Rect.Center).x, Zoom);
            tile.transform.localPosition = new Vector3((float)(tile.Rect.Center.x - ReferenceTileRect.Center.x), 0, (float)(tile.Rect.Center.y - ReferenceTileRect.Center.y));
            mapVisualization.ShowTile(tile);
			//Debug.Log ("Tile count: " + ++count);
        }
    }
}

using UnityEngine;
using Mapbox.Unity.MeshGeneration;
using Mapbox.Unity.Location;

/// <summary>
/// Override the map center (latitude, longitude) for a MapController, based on the DefaultLocationProvider.
/// This will enable you to generate a map for your current location, for example.
/// </summary>
public class BuildMapAtLocation : MonoBehaviour
{
    [SerializeField]
    MapController mapController;


	private bool isCorrectLocation = false;

    ILocationProvider _locationProvider;
    ILocationProvider LocationProvider
    {
        get
        {
            if (_locationProvider == null)
            {
                _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
            }

            return _locationProvider;
        }
    }

    void Start()
    {
        LocationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;
    }


    void LocationProvider_OnLocationUpdated(object sender, Mapbox.Unity.Location.LocationUpdatedEventArgs e)
    {
        LocationProvider.OnLocationUpdated -= LocationProvider_OnLocationUpdated;

		mapController.currentLat = e.Location.x;
		mapController.currentLong = e.Location.y;

		mapController.enabled = true;
		if (!(mapController.currentLat == 0 && mapController.currentLong == 0) && !isCorrectLocation) {
			mapController.Execute (mapController.currentLat, mapController.currentLong, mapController.Zoom, mapController.Range);
			isCorrectLocation = true;
		}
    }
}

using Mapbox.Unity.Location;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.MeshGeneration;
using UnityEngine;
using UnityEngine.UI;
public class PositionWithLocationProvider : MonoBehaviour
{
    /// <summary>
    /// The rate at which the transform's position tries catch up to the provided location.
    /// </summary>
	[SerializeField]
	float positionFollowFactor;

	public GameObject player;

    /// <summary>
    /// Use a mock <see cref="T:Mapbox.Unity.Location.TransformLocationProvider"/>,
    /// rather than a <see cref="T:Mapbox.Unity.Location.EditorLocationProvider"/>. 
    /// </summary>
    [SerializeField]
    bool useTransformLocationProvider;

	public bool firstTimePositionUpdate = false;
	public MapController mapController;
    /// <summary>
    /// The location provider.
    /// This is public so you change which concrete <see cref="T:Mapbox.Unity.Location.ILocationProvider"/> to use at runtime.
    /// </summary>
	ILocationProvider locationProvider;
	public ILocationProvider LocationProvider
	{
		private get
		{
			if (locationProvider == null)
			{
                locationProvider = useTransformLocationProvider ? 
                    LocationProviderFactory.Instance.TransformLocationProvider : LocationProviderFactory.Instance.DefaultLocationProvider;

			}


			return locationProvider;
		}
		set
		{
			if (locationProvider != null)
			{
				locationProvider.OnLocationUpdated -= LocationProvider_OnLocationUpdated;

			}
			locationProvider = value;
			locationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;
		}
	}

	Vector3 targetPosition;

	void Start()
	{
		LocationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;
	}

	void OnDestroy()
	{
		if (LocationProvider != null)
		{
			LocationProvider.OnLocationUpdated -= LocationProvider_OnLocationUpdated;
		}
	}

	void LocationProvider_OnLocationUpdated(object sender, LocationUpdatedEventArgs e)
	{
        if (MapController.ReferenceTileRect == null)
        {
            return;
        }
		//t.text = e.Location.ToString ();

        targetPosition = Conversions.GeoToWorldPosition(e.Location,
                                                         MapController.ReferenceTileRect.Center, 
                                                         MapController.WorldScaleFactor).ToVector3xz();
	}

	void Update()
	{
		//TODO: Needs more work
		// If it is the first time getting the device position, jump to location instead of 'gliding' to it.
		if (!firstTimePositionUpdate) {
			transform.position = targetPosition;
			firstTimePositionUpdate = true;
		} 
		else {
			transform.position = targetPosition;
			//transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * positionFollowFactor);
		}
		// Keep player on the ground
		transform.position = new Vector3(transform.position.x, 0, transform.position.z);
	}
}

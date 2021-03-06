﻿using System;
using System.Net.Configuration;
using Mapbox.Utils;
//using NUnit.Framework;
using UnityEngine;

public class GeoPoint
{

	public int id = -1;
	public double latitude = 0.0;
	public double longitude = 0.0;
	public string name = "";
	public string description = "";
	public int radius = 50;
    public int lastVisit = 0;

    public GameObject pawstop;

	public GeoPoint() {
		radius = 50;
	}

    public bool BoundingCircle(double targetLat, double targetLong, double CircleSize)
    {
        double distance = Distance(latitude, longitude, targetLat, targetLong);

        return Mathd.Round(distance) <= CircleSize;
    }

    public static double Distance(double lat1, double lon1, double lat2, double lon2)
    {
        // Haversine formula for calculating the distance between two points

        const double radiusOfEarthInMeters = 6371000;
        const double mTofeet = 3.28084; 
        double theta = Mathd.Deg2Rad * lat1;
        double phi = Mathd.Deg2Rad * lat2;
        double deltaLat = Mathd.Deg2Rad * (lat2 - lat1);
        double deltaLong = Mathd.Deg2Rad * (lon2 - lon1);

        double halfChordLength = Mathd.Sin(deltaLat / 2) * Mathd.Sin(deltaLat / 2) + Mathd.Cos(theta) * Mathd.Cos(phi) *
                                 Mathd.Sin(deltaLong / 2) * Mathd.Sin(deltaLong / 2);

        double angularDistance = 2 * Mathd.Atan2(Mathd.Sqrt(halfChordLength), Mathd.Sqrt(1 - halfChordLength));

        double distance = radiusOfEarthInMeters * angularDistance;

        // Conversion to feet
        distance = distance*mTofeet;

        return distance;
    }
}

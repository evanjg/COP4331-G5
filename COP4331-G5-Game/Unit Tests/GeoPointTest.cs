using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AssemblyCSharp;
using NUnit.Framework;
using NUnit.Framework.Constraints;


[TestFixture]
internal class GeoPointTest
{
    private List<double[]> latLongList = new List<double[]>();
    

    [SetUp]
    public void setUp()
    {
        
        // East
        double[] temp1 = { 28.601937, -81.200374, 57 };
        latLongList.Add(temp1);

        // South
        double[] temp2 = { 28.601827, -81.200565, 41 };
        latLongList.Add(temp2);

        // North
        double[] temp3 = { 28.602077, -81.200559, 50};
        latLongList.Add(temp3);

        // West
        double[] temp4 = { 28.601949, -81.200714, 52 };
        latLongList.Add(temp4);

        // North-East
        double[] temp5 = { 28.602009, -81.200473, 36 };
        latLongList.Add(temp5);

        // North-West
        double[] temp6 = { 28.602031, -81.200655, 47 };
        latLongList.Add(temp6);

        // South-West
        double[] temp7 = { 28.601862, -81.200674, 48 };
        latLongList.Add(temp7);

        // South-East
        double[] temp8 = { 28.60184, -81.200439, 51 };
        latLongList.Add(temp8);
    }

    [Test]
    public void BoundingCircleTest([Range(0,7,1)] int x, [Range(0, 100, 2)] int distance)
    {
        GeoPoint a = new GeoPoint();

        a.latitude = 28.601940;
        a.longitude = -81.200552;


        if(distance >= latLongList[x][2])
            Assert.That(a.BoundingCircle(latLongList[x][0], latLongList[x][1], distance), Is.True);
        else
            Assert.That(a.BoundingCircle(latLongList[x][0], latLongList[x][1], distance), Is.False);
    }

    [Test]
    public void DistanceTest([Range(0, 7, 1)] int x)
    {
        Assert.That(Math.Round(GeoPoint.Distance(28.601940, -81.200552, latLongList[x][0], latLongList[x][1])), Is.EqualTo(latLongList[x][2]));
        
    }

}



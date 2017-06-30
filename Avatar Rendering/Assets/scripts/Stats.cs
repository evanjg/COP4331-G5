// code brought to you by Nashaly
// sprint 2 
// 6/30/2013

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
     // seting up a class that will be applied to health,feels, strangth and level
    {
        public string name;
        public float max;
        public float min;
        public float increment;
        public float current; 

    public Stats(string n, float min, float max, float inc) // constructor
    {
        
        this.name = n;
        this.min = min;
        this.current = min;
        this.max = max;
        this.increment= inc;
    }
       
        

        

    public float increase() // increasing the stats/level
    {
        if (current >= max) 
        {
            current = max; 
            return current;
        }
        else if (current < min)
        {
            current = min;
            return current;
        }
        else
        {
            current = current + increment;
             return current; 
        }
     
    }



    public float decrease() // mainly for testing 
    {
        if (current >= max) // current is larger dont pass max
        {
            current = current - increment;
            return current;
        }
        else if (current <= min)
        {
            current = min;
            return current;

        }
        else
        {
            current = current - increment;
            return current;
        }

    }

}




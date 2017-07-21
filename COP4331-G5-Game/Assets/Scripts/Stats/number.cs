// code brought to you by Nashaly
// sprint 2 
// 6/30/2013

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class number : MonoBehaviour {


        
    public Text showTextnum; // setting the # on the screen to get values to show up

    public Text nameofStat; // nameing to call apon later when needed

    
    public float min; // start value

  
    public float max; //  end value

    public float increment; // amount wanted to increment

    public Stats stats; // class

    // Use this for initialization
    void Start()
    {
        stats = new Stats(nameofStat.ToString(),min,max,increment);
        showTextnum.text = stats.current.ToString();
    }

    
    // uncomment when test is not being used
    /* Update is called once per frame, get whats eneterd on the screen
    void Update()
   {
        if(stats== null)
        {
            stats = new Stats(nameofStat.ToString(),min,max,increment);
            showTextnum.text = stats.current.ToString();
        }       
       
    }
    */

 

}




// code brought to you by Nashaly
// sprint 3 (updated)
// 7/21/2017

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Testing : MonoBehaviour {

    // creating the test varibles 
    public Number feels_num;
    public Number heals_num;
    public Number strengths_num;
    public Number level_num;

    public bool running;

   
    // Use this for initialization
    void Start ()
    {
        running = false;
        StartCoroutine(testing());  
	}

    // increment the object indicated
    IEnumerator incThething(Number num, float  min,  float max, float inc)
    {
        running = true;

        //creating with whats given 
        num.stats = new Stats(num.nameofStat.ToString(), min, max, inc);
        num.showTextnum.text = num.stats.current.ToString(); 
        
        // test if num can go from min- max
        for (float i = min; i <= max; i += inc)
        {
            yield return new WaitForSeconds(0.05f); // 1 second wait time

            num.stats.increase();
            num.showTextnum.text = num.stats.current.ToString();

        }
        if (num.stats.current == max)
        {
            print(" passed");
            //return true;
        }

        // resetting current
        num.stats.current = min;
        num.showTextnum.text = num.stats.current.ToString();

       // StopAllCoroutines();
       running = false;
        StopCoroutine("incThething");
    }

    // the test code
    // for automatic testing 
    IEnumerator testing() // needed for StartCoroutine
    {
        int count = 0;// count number of passses 
               
        print("starting test: ");

        // test if all three stats can go from 0-100 at same time

        StartCoroutine(incThething(heals_num, 0, 100, 1));

        if (running == true) yield return new WaitForSeconds(8);

        StartCoroutine(incThething(strengths_num, 0, 100, 1));

        if (running == true) yield return new WaitForSeconds(8);
        StartCoroutine(incThething(feels_num, 0, 100, 1));

        if (running == true) yield return new WaitForSeconds(8);
        StartCoroutine(incThething(level_num, 1, 10, 1));

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //buffer to wait
        if (running == true) yield return new WaitForSeconds(8);

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        // test to see stats that have reached 100 can wait for other stats to level up 
        // letting level up get to its max level and staying the same while stats coninue to loop 0-100.

        while (level_num.stats.current != 11)
        {

            for (float i = 0; i <= 100f * level_num.stats.current; i += 1)
            {
                yield return new WaitForSeconds(0.0005f); //  second wait time

                heals_num.stats.increase();
                heals_num.showTextnum.text = heals_num.stats.current.ToString();
            }
            for (float i = 0; i <= 100f * level_num.stats.current; i += 1)
            {
                yield return new WaitForSeconds(0.0005f); //  second wait time

                strengths_num.stats.increase();
                strengths_num.showTextnum.text = strengths_num.stats.current.ToString();

            }
            for (float i = 0; i <= 100f * level_num.stats.current; i += 1)
            {
                yield return new WaitForSeconds(0.0005f); //  second wait time

                feels_num.stats.increase();
                feels_num.showTextnum.text = feels_num.stats.current.ToString();

            }

            if (((heals_num.stats.current) == 100f* level_num.stats.current) && ((strengths_num.stats.current == 100f * level_num.stats.current)) && ((feels_num.stats.current == 100f * level_num.stats.current)))
            {
                heals_num.stats.current = 0;
                heals_num.showTextnum.text = heals_num.stats.current.ToString();
                strengths_num.stats.current = 0;
                strengths_num.showTextnum.text = strengths_num.stats.current.ToString();
                feels_num.stats.current = 0;
                feels_num.showTextnum.text = feels_num.stats.current.ToString();

                

                if (level_num.stats.current == 10)
                { break;  }

                level_num.stats.increase();
                level_num.showTextnum.text = level_num.stats.current.ToString();

                // increaseing the max for each stat
                heals_num.stats.max = 100f * level_num.stats.current;
                strengths_num.stats.max = 100f * level_num.stats.current;
                feels_num.stats.max =  100f * level_num.stats.current;
              }
        }

        if (level_num.stats.current >= 10)
        {
            print("passed");
            count++;
        }
  

        heals_num.stats.current = 0;
        heals_num.showTextnum.text = heals_num.stats.current.ToString();
        strengths_num.stats.current = 0;
        strengths_num.showTextnum.text = strengths_num.stats.current.ToString();
        feels_num.stats.current = 0;
        feels_num.showTextnum.text = feels_num.stats.current.ToString();
        level_num.stats.current = 1;
        level_num.showTextnum.text = level_num.stats.current.ToString();
        
    }

        // for manual testing method with key pressing comment out test() and uncomment Update
        // key: a to increment heals
        // key z to decrement heals
        // key s to increment feels
        // key x to decrement feels
        // key d to increment strength
        // key c to decrement strength 
        // once all stats reach 100 level will increment and all stats will restart to min.
        // key q will decrement level
    // Update is called once per frame
    /*
    void Update ()
    {
        if (Input.GetKeyDown("a"))
        {
            heals_num.stats.increase();
            heals_num.showTextnum.text = heals_num.stats.current.ToString();
        }

        if(Input.GetKeyDown("s"))
        {
            feels_num.stats.increase();
            feels_num.showTextnum.text = feels_num.stats.current.ToString();
        }

        if (Input.GetKeyDown("d"))
        {
            strengths_num.stats.increase();
            strengths_num.showTextnum.text = strengths_num.stats.current.ToString();
        }

 //       if (Input.GetKeyDown("w"))
 //       {
 //           level_num.stats.increase();
 //           level_num.showTextnum.text = level_num.stats.current.ToString();
  //      }
            // will increment level when max is reached by all other stats then will reset stats to min.
        if ( ((heals_num.stats.current) == 100) && ((strengths_num.stats.current == 100)) && ((feels_num.stats.current == 100)))
        {
            heals_num.stats.current = 0;
            heals_num.showTextnum.text = heals_num.stats.current.ToString();
            strengths_num.stats.current = 0;
            strengths_num.showTextnum.text = strengths_num.stats.current.ToString();
            feels_num.stats.current = 0;
            feels_num.showTextnum.text = feels_num.stats.current.ToString();

            level_num.stats.increase();
            level_num.showTextnum.text = level_num.stats.current.ToString();
        }

        // decreasing the values ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        if (Input.GetKeyDown("z"))
        {
            heals_num.stats.decrease();
            heals_num.showTextnum.text = heals_num.stats.current.ToString();
        }

        if (Input.GetKeyDown("x"))
        {
            feels_num.stats.decrease();
            feels_num.showTextnum.text = feels_num.stats.current.ToString();
        }

        if (Input.GetKeyDown("c"))
        {
            strengths_num.stats.decrease();
            strengths_num.showTextnum.text = strengths_num.stats.current.ToString();
        }

        if (Input.GetKeyDown("q"))
        {
            level_num.stats.decrease();
            level_num.showTextnum.text = level_num.stats.current.ToString();
        }
    }
    */
}

// code brought to you by Nashaly
// sprint 2 
// 6/30/2013

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {

    public number feels_num;
    public number heals_num;
    public number strengths_num;
    public number level_num;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(testing()); // runs when start is done 
	}



    IEnumerator testing() // needed for StartCoroutine
    {
        int count = 0;// count number of passses 
        // for automatic testing 
        // for testing perposes min, max, increment valuse for all stats will be min=0, max=100 , increment=20;
        print("starting test: 6 passes are required");

        heals_num.stats = new Stats(heals_num.nameofStat.ToString(), 0, 100, 25);
        heals_num.showTextnum.text = heals_num.stats.current.ToString();


        // test if health can go from 0- 100 
        for (float i=0; i <= 100; i+= 25)
         {
            yield return new WaitForSeconds(0.5f); // 1 second wait time
            
            heals_num.stats.increase();
            heals_num.showTextnum.text = heals_num.stats.current.ToString();

        }
        if (heals_num.stats.current == 100)
        {
            print("1. passed");
            count++;
        }
        else
        {
            print("1. failed");
        }
        // resetting health
        heals_num.stats.current = 0;
        heals_num.showTextnum.text = heals_num.stats.current.ToString();

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        // test if strength can go from 0-100 

        strengths_num.stats = new Stats(strengths_num.nameofStat.ToString(), 0, 100, 25);
        strengths_num.showTextnum.text = strengths_num.stats.current.ToString();

        // test if health can go from 0- 100 
        for (float i = 0; i <= 100; i += 25)
        {
            yield return new WaitForSeconds(0.5f); // 1 second wait time

            strengths_num.stats.increase();
            strengths_num.showTextnum.text = strengths_num.stats.current.ToString();

        }
        if (strengths_num.stats.current == 100)
        {
            print("2. passed");
            count++;
        }
        else
        {
            print("2. failed");
        }
        // resetting  strengths
        strengths_num.stats.current = 0;
        strengths_num.showTextnum.text = heals_num.stats.current.ToString();
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        // test if feels can go from 0-100 

        feels_num.stats = new Stats(feels_num.nameofStat.ToString(), 0, 100, 25);
        feels_num.showTextnum.text = feels_num.stats.current.ToString();

  
        for (float i = 0; i <= 100; i += 25)
        {
            yield return new WaitForSeconds(0.5f); // 1 second wait time

            feels_num.stats.increase();
            feels_num.showTextnum.text = feels_num.stats.current.ToString();

        }
        if (feels_num.stats.current == 100)
        {
            print("3. passed");
            count++;
        }
        else
        {
            print("3. failed");
        }
        // resetting  strengths
        feels_num.stats.current = 0;
        feels_num.showTextnum.text = feels_num.stats.current.ToString();

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // test if level can go from 1-10 

        level_num.stats = new Stats(level_num.nameofStat.ToString(),1, 10, 1);
        level_num.showTextnum.text = level_num.stats.current.ToString();

        // test if level can go from 0- 10
        for (float i = 0; i <= 10; i += 1)
        {
            yield return new WaitForSeconds(0.5f); // 1 second wait time

            level_num.stats.increase();
            level_num.showTextnum.text = level_num.stats.current.ToString();

        }
        if (level_num.stats.current == 10)
        {
            print("4. passed");
            count++;
        }
        else
        {
            print("4. failed");
        }
        // resetting  strengths
        level_num.stats.current = 1;
        level_num.showTextnum.text = level_num.stats.current.ToString();
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        // test if all three stats can go from 0-100 at same time 
        // test if feels can go from 0-100 

        for (float i = 0; i <= 100; i += 25)
        {
            

            feels_num.stats.increase();
            feels_num.showTextnum.text = feels_num.stats.current.ToString();

            heals_num.stats.increase();
            heals_num.showTextnum.text = heals_num.stats.current.ToString();

            strengths_num.stats.increase();
            strengths_num.showTextnum.text = strengths_num.stats.current.ToString();
         
        yield return new WaitForSeconds(0.5f); // 1 second wait time
        }

        if (((heals_num.stats.current) == 100) && ((strengths_num.stats.current == 100)) && ((feels_num.stats.current == 100)))
        {
            print("5. passed");
            count++;
        }
        else
        {
            print("5. failed");
        }

        heals_num.stats.current = 0;
        heals_num.showTextnum.text = heals_num.stats.current.ToString();
        strengths_num.stats.current = 0;
        strengths_num.showTextnum.text = strengths_num.stats.current.ToString();
        feels_num.stats.current = 0;
        feels_num.showTextnum.text = feels_num.stats.current.ToString();


        // test to see stats that have reached 100 can wait for other stats to level up 
        // letting level up get to its max level and staying the same while stats coninue to loop 0-100.

        while (level_num.stats.current != 11)
        {

            for (float i = 0; i <= 100; i += 25)
            {
                yield return new WaitForSeconds(0.5f); // 1 second wait time

                heals_num.stats.increase();
                heals_num.showTextnum.text = heals_num.stats.current.ToString();
            }
            for (float i = 0; i <= 100; i += 25)
            {
                yield return new WaitForSeconds(0.5f); // 1 second wait time

                strengths_num.stats.increase();
                strengths_num.showTextnum.text = strengths_num.stats.current.ToString();

            }
            for (float i = 0; i <= 100; i += 25)
            {
                yield return new WaitForSeconds(0.5f); // 1 second wait time

                feels_num.stats.increase();
                feels_num.showTextnum.text = feels_num.stats.current.ToString();

            }

            if (((heals_num.stats.current) == 100) && ((strengths_num.stats.current == 100)) && ((feels_num.stats.current == 100)))
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

                
  
                

            }
        }

        if (level_num.stats.current >= 10)
        {
            print("6.passed");
            count++;
        }
        else print("FAIL");

        heals_num.stats.current = 0;
        heals_num.showTextnum.text = heals_num.stats.current.ToString();
        strengths_num.stats.current = 0;
        strengths_num.showTextnum.text = strengths_num.stats.current.ToString();
        feels_num.stats.current = 0;
        feels_num.showTextnum.text = feels_num.stats.current.ToString();
        level_num.stats.current = 1;
        level_num.showTextnum.text = level_num.stats.current.ToString();

        if(count==6)
        {
            print("all passed!!");
        }
        else
        {
            print("there was a problem");
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
    }


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

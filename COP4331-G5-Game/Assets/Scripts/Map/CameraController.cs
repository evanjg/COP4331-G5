using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;
    private float initialStart;
	// Use this for initialization
	void Start ()
	{
	    offset = player.transform.position - transform.position;
	    initialStart = transform.position.y;
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{

        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;


            if (deltaMagnitudeDiff >= 0 && transform.position.y < 100)
            {
                transform.position = new Vector3(transform.position.x, smoothZoom(3), transform.position.z);
            }
            else if (deltaMagnitudeDiff < 0 && transform.position.y > initialStart)
            {
                transform.position = new Vector3(transform.position.x, smoothZoom(-3), transform.position.z);
            }
        }
        else if (Input.touchCount == 1)
	    {
            float currentAngle = transform.eulerAngles.y;
            float desiredAngle = currentAngle;

            float dis = player.transform.position.y - transform.position.y;
            Vector2 deltaTouch = Input.GetTouch(0).deltaPosition;

            float vel = deltaTouch.magnitude / Time.deltaTime;
            


            transform.Rotate(0, deltaTouch.x * scale(-dis) * 10 * Time.deltaTime, 0);

            desiredAngle = transform.eulerAngles.y;


            float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * 1000);

            if (!(desiredAngle == currentAngle))
            {
                Quaternion rotation = Quaternion.Euler(0, angle, 0);

                transform.position = player.transform.position - (rotation * new Vector3(offset.x, dis, offset.z));
            }

	    }
	    
        
	    transform.LookAt(player.transform);
	}

    private float scale(float dis)
    {
        Debug.Log(dis);
        if (dis > 75)
        {
            return .2f;
        }
        if (dis > 25)
        {
            return .5f;
        }
       
        return 1f;
        
    }

    private float smoothZoom(int val)
    {
        return Mathf.Lerp(transform.position.y, transform.position.y + val, 20*Time.deltaTime);
    }
}
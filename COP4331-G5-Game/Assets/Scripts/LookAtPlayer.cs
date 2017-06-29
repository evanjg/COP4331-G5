using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LookAtPlayer : MonoBehaviour {

	public GameObject target;
	Vector3 offset;
	public Text t;

	public float TurnSpeed = .00001f;



	// Use this for initialization
	void Start () {
		offset = target.transform.position - transform.position;	
	}

	// Update is called once per frame
	void LateUpdate () {

        // If there are two touches on the device...
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
                transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
            }
            else if (deltaMagnitudeDiff < 0 && transform.position.y > 12)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);   
            }
        }
        else if (Input.touchCount == 1)
        {
            float currentAngle = transform.eulerAngles.y;
            float desiredAngle = currentAngle;

            float dis = target.transform.position.y - transform.position.y;
            Vector2 deltaTouch = Input.GetTouch(0).deltaPosition;

            float vel = deltaTouch.magnitude / Time.deltaTime;
            float scale = 1;

            if (dis < -25)
            {
                scale = .1f;
            }
            else
            {
                scale = 1f;
            }

            if (deltaTouch.x >= 0)
            {
                transform.Rotate(0, vel * scale * Time.deltaTime, 0);
            }
            if (deltaTouch.x < 0)
            {
                transform.Rotate(0, -1f * vel * scale * Time.deltaTime, 0);

            }
            
            desiredAngle = transform.eulerAngles.y;

            
            float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime*1000);

            if (!(desiredAngle == currentAngle))
            {
                Quaternion rotation = Quaternion.Euler(0, angle, 0);

                transform.position = target.transform.position - (rotation * new Vector3(offset.x,dis,offset.z));
            }
        }
       
        transform.LookAt(target.transform);
	}
}

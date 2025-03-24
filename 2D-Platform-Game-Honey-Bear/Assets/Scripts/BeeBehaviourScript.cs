using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBehaviourScript : MonoBehaviour
{
    public float speed;
    public float range;

    Animator myAnim;

    Vector3 wayPoint;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        setNewDestination();
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, wayPoint) < range)
        {
            setNewDestination();
        }
    }

  void setNewDestination()
{
    Vector3 currentPosition = transform.position; // Use Vector3

    float xrandom = Random.Range(-1, 20);
    float yrandom = Random.Range(-9,9);

    

    float xDifference = xrandom - currentPosition.x;
    float yDifference = yrandom - currentPosition.y;

    if (Mathf.Abs(xDifference) > Mathf.Abs(yDifference))
    {
        // The waypoint is more left or right than up or down
        if (xDifference > 0)
        {
            myAnim.SetInteger("speed", Mathf.Abs(4));
        }
        else
        {
            myAnim.SetInteger("speed", Mathf.Abs(3));
        }
    }
    else
    {
        // The waypoint is more up or down than left or right
        if (yDifference > 0)
        {
            myAnim.SetInteger("speed", Mathf.Abs(1));
        }
        else
        {
            myAnim.SetInteger("speed", Mathf.Abs(2));
        }
    }

    // Update the class-level variable 'wayPoint' with the new values
    wayPoint = new Vector3(xrandom, yrandom, currentPosition.z);
}
}

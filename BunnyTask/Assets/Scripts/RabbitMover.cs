using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMover : MonoBehaviour
{

    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;

    private bool isSwipe = false;
    private float minSwipeDist = 30.0f;
    private float maxSwipeTime = 10f;

    //public PlayerMovement playerMovementScript;

    bool canInvoke = true;

    float speed = 5f;
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speed);
    }

    void Update()
    {
        SwipeHorizontal();
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z - 2.2f) ;
    }

    public void invokeMovement()
    {

        canInvoke = true;

    }

    void moveTotheDirection(bool isRight)
    {
        if (isRight)
        {
            transform.position = new Vector3(transform.position.x + .02f > 0.9f ? 0.9f : transform.position.x + .02f, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - .02f < -0.9f ? -0.9f : transform.position.x - .02f, transform.position.y, transform.position.z);
        }
    }
    private void SwipeHorizontal()
    {
        if (Input.touchCount > 0 && Time.timeScale > 0.0f)
        {

            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    isSwipe = true;
                    fingerStartTime = Time.time;
                    fingerStartPos = touch.position;
                }

                if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                {


                    float gestureTime = Time.time - fingerStartTime;
                    float gestureDist = (touch.position - fingerStartPos).magnitude;

                    if (canInvoke && isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist)
                    {

                        canInvoke = false;
                        Invoke("invokeMovement", 0f);

                        Vector2 direction = touch.position - fingerStartPos;
                        int swipeType = -1;
                        if (Mathf.Sign(direction.x) > 0)
                        {

                            if (Mathf.Sign(direction.y) > 0)
                                swipeType = 4; // swipe diagonal up-right
                            else
                                swipeType = 5; // swipe diagonal down-right

                        }
                        else
                        {

                            if (Mathf.Sign(direction.y) > 0)
                                swipeType = 6; // swipe diagonal up-left
                            else
                                swipeType = 7; // swipe diagonal down-left

                        }

                        switch (swipeType)
                        {

                            case 0: //right
                                //Debug.Log("right");
                                moveTotheDirection(true);
                                break;

                            case 1: //left
                                //Debug.Log("left");
                                moveTotheDirection(false);
                                break;

                            case 2: //up
                                //Debug.Log("up");
                                break;

                            case 3: //down
                                //Debug.Log("down");
                                break;

                            case 4: //up right
                                //Debug.Log("upright");
                                moveTotheDirection(true);
                                break;
                            case 5: //down right
                                //Debug.Log("downright");
                                moveTotheDirection(true);
                                break;

                            case 6: //up left
                                //Debug.Log("upleft");
                                moveTotheDirection(false);
                                break;

                            case 7: //down left
                                //Debug.Log("downleft");
                                moveTotheDirection(false);
                                break;
                        }
                    }
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    canInvoke = true;
                }
            }
        }
    }
}

/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: 
 * Last Edited:
 * 
 * Description: Paddle controler on Horizontal Axis
****/

/*** Using Namespaces ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10; //speed of paddle


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetAxis("Horizontal")); //just testing what I should do.                                         
        //Less than 0 is Left, more than 0 is right.
        Vector3 pos = transform.position;

        //get the position
        pos.x = this.transform.position.x;
        pos.x += (Input.GetAxis("Horizontal") * speed * Time.deltaTime);

        //set the position
        this.transform.position = pos;
    }//end Update()
}

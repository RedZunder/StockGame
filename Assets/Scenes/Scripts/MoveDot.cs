using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveDot : MonoBehaviour
{
    [SerializeField] float pace, initialValue;
    [SerializeField] int diff;
    [SerializeField] GameObject sprite;
    [SerializeField] TMPro.TextMeshProUGUI textMP;
    [SerializeField] Camera cam;
    GameObject obj;
    float gains, aux = 0, value, perct, camAux= 88;
    Color color;

    void Start()
    {
        value = initialValue;

    }

    void Update()
    {
        //Randomly increase or decrease the value of stock: 48% chance of decreasing
        if (Random.Range(0, 1f) > 0.48f)
        {
            gains = Random.Range(1f, 8);
            color = Color.green;        //color for increase in value
        }
        else
        {
            gains = -Random.Range(1f, 8);
            color = Color.red;          //color for decrease in value

        }
        if (transform.position.x < 160)
        {
            transform.Translate(new Vector3(4 * pace * Time.deltaTime, 0));

            if (Mathf.Abs(transform.position.x - aux) >= diff)
            {
                aux = drawStonk(color, gains);      //we save in "aux" the position of the last drawn rectangle
                transform.position += new Vector3(0, gains);
                runNumbers(gains);
            }
            adjustCamera();
        }
        

    }
    
    //draw a rectangle according to value increase/decrease
    float drawStonk(Color colour, float gains)
    {
        sprite.transform.localScale = new Vector2(diff, gains);
        sprite.GetComponent<SpriteRenderer>().color = colour;
        sprite.GetComponent<SpriteRenderer>().sortingOrder = 1;
        obj = Instantiate(sprite, new GameObject().transform);

        obj.transform.position = new Vector3(transform.position.x, transform.position.y + gains / 2);

        killParent();
        return transform.position.x;        
    }

    //calculate updated stock value
    void runNumbers(float gains)
    {
        value += gains / 10;
        textMP.text = value.ToString("F3");
        perct =  -(1-(value / initialValue)) * 100;
        textMP.text += "      "+perct.ToString("F3")+"%";
    }

    void killParent()       //self-explanatory
    {
        obj = obj.transform.parent.gameObject;
        obj.transform.DetachChildren();
        Destroy(obj);
    }

    void adjustCamera()     //move the camera to fit the entire graph
    {
        if ((int)transform.position.y + 10 > camAux)
        {
            cam.orthographicSize = (int)transform.position.y + 10;
            camAux = cam.orthographicSize;
        }
    }

}

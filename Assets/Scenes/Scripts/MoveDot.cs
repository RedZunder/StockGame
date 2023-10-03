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
    float gainz, aux = 0, value, perct, camAux= 88;
    Color color;

    void Start()
    {
        value = initialValue;

    }
    void Update()
    {
        if (Random.Range(0, 1f) > 0.48f)
        {
            gainz = Random.Range(1f, 8);
            color = Color.green;
        }
        else
        {
            gainz = -Random.Range(1f, 8);
            color = Color.red;

        }
        if (transform.position.x < 160)
        {
            transform.Translate(new Vector3(4 * pace * Time.deltaTime, 0));

            if (Mathf.Abs(transform.position.x - aux) >= diff)
            {
                aux = drawStonk(color, gainz);
                transform.position += new Vector3(0, gainz);
                runNumbers(gainz);
            }
            adjustCamera();
        }
        

    }
    
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
    void runNumbers(float gains)
    {
        value += gains / 10;
        textMP.text = value.ToString("F3");
        perct =  -(1-(value / initialValue)) * 100;
        textMP.text += "      "+perct.ToString("F3")+"%";
    }

    void killParent()
    {
        obj = obj.transform.parent.gameObject;
        obj.transform.DetachChildren();
        Destroy(obj);
    }

    void adjustCamera()
    {
        if ((int)transform.position.y + 10 > camAux)
        {
            cam.orthographicSize = (int)transform.position.y + 10;
            camAux = cam.orthographicSize;
        }
    }

}

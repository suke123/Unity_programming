using UnityEngine;
using System.Collections;

public class Blinker : MonoBehaviour
{
    private float Timer;
    public float interval = 0.2f;   // 点滅周期
    //public Gun gunLife;             // 大砲のHP

    //private Renderer renderer;
    private Color materialColor;
    public bool redFlag;
    //int count = 0;

    // Use this for initialization
    void Start()
    {
        Timer = 0.0f;
        //renderer = GetComponent<MeshRenderer>();
        materialColor = GetComponent<Renderer>().material.color;
        redFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > interval)
        {
            //renderer.enabled = !renderer.enabled;
            redFlag = !redFlag;
            
            Timer = 0.0f; ;
        }

        if (redFlag)
        {
            GetComponent<Renderer>().material.color = new Color(200, 0, 0);
            /*if (count == 0)
            {
                gunLife.isDamaged();
                count = 1;
            }*/
        }
        else
        {
            GetComponent<Renderer>().material.color = materialColor;
            //count = 0;
        }
    }

    public void reDoMaterial()
    {
        redFlag = false;
        //count = 0;
        GetComponent<Renderer>().material.color = materialColor;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game : MonoBehaviour
{
    public float endTimer;
    void Start()
    {
        Generate();
    }

    private void FixedUpdate()
    {
        if (endTimer != 0)
        {
            if (endTimer == 0)
            {
                GameObject.Find("prisoner").GetComponent<player>().enabled = false;
                GameObject.Find("police").GetComponent<player>().enabled = false;
            }

            endTimer += 0.1f;
            if (endTimer >= 25)
            {
                Generate();
                GameObject.Find("prisoner").GetComponent<player>().enabled = true;
                GameObject.Find("police").GetComponent<player>().enabled = true;
            }
        }
    }

    public void Generate()
    {
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj.name == "point")
            {
                obj.tag = "point";
                obj.GetComponent<SpriteRenderer>().color = new Color(0.9433962f, 0.9433962f, 0.9433962f);
            }
        }
        GameObject.Find("prisoner").GetComponent<player>().enabled = true;
        GameObject.Find("police").GetComponent<player>().enabled = false;

        GameObject.Find("prisoner").transform.position = new Vector3(4.8f, 1.2f * Random.Range(-3, 4), -1);
        GameObject.Find("police").transform.position = new Vector3(-4.8f, 1.2f * Random.Range(-3, 4), -1);

        GameObject.Find("door").transform.position = new Vector3(-5.48f, 1.2f * Random.Range(-3, 4), -1);
    }
}

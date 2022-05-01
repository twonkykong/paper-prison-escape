using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public GameObject point, button;
    public Text placeWallButtonText;
    public GameObject[] arrows;
    public bool placingWall;

    private void Update()
    {
        //arrows
        RaycastHit2D hitUp = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y) + Vector2.up * 1.2f, Vector2.zero);
        RaycastHit2D hitDown = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y) - Vector2.up * 1.2f, Vector2.zero);
        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y) - Vector2.right * 1.2f, Vector2.zero);
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y) + Vector2.right * 1.2f, Vector2.zero);

        if (hitUp.collider != null && hitUp.collider.tag == "point") arrows[0].SetActive(true);
        else arrows[0].SetActive(false);

        if (hitDown.collider != null && hitDown.collider.tag == "point") arrows[1].SetActive(true);
        else arrows[1].SetActive(false);

        if (hitLeft.collider != null && hitLeft.collider.tag == "point") arrows[2].SetActive(true);
        else arrows[2].SetActive(false);

        if (hitRight.collider != null && hitRight.collider.tag == "point") arrows[3].SetActive(true);
        else arrows[3].SetActive(false);

        //moving
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "point")
            {
                Debug.Log(hit.collider.GetComponent<SpriteRenderer>().color);
                if (point != null && hit.collider.gameObject != point)
                {
                    point.GetComponent<SpriteRenderer>().color = new Color(0.9433962f, 0.9433962f, 0.9433962f);
                }
                point = hit.collider.gameObject;
                point.GetComponent<SpriteRenderer>().color = new Color(0.8301887f, 0.8301887f, 0.8301887f);
                if (Input.GetMouseButtonDown(0))
                {
                    foreach (Vector3 pos in new Vector3[4] { Vector3.up * 1.2f, -Vector3.up * 1.2f, Vector3.right * 1.2f, -Vector3.right * 1.2f })
                    {
                        if (hit.collider.transform.position == transform.position + pos + transform.forward)
                        {
                            if (!placingWall) transform.position = hit.collider.transform.position - transform.forward;
                            else
                            {
                                point = null;
                                hit.collider.tag = "Untagged";
                                hit.collider.GetComponent<SpriteRenderer>().color = new Color(0.3018868f, 0.3018868f, 0.3018868f);

                                PlaceWallButton();
                            }

                            foreach (GameObject arrow in arrows) arrow.SetActive(false);
                            if (name == "prisoner")
                            {
                                GameObject.Find("police").GetComponent<player>().enabled = true;
                                button.SetActive(false);
                            }
                            else
                            {
                                GameObject.Find("prisoner").GetComponent<player>().enabled = true;
                                button.SetActive(true);
                            }
                            GetComponent<player>().enabled = false;
                        }
                    }
                }
            }
        }
        else
        {
            if (point != null)
            {
                if (point.tag == "point") point.GetComponent<SpriteRenderer>().color = new Color(0.9433962f, 0.9433962f, 0.9433962f);
                point = null;
            }
        }
    }

    public void PlaceWallButton()
    {
        if (placingWall == false)
        {
            placingWall = true;
            placeWallButtonText.text = "cancel";
        }
        else
        {
            placingWall = false;
            placeWallButtonText.text = "place wall";
        }
    }
}

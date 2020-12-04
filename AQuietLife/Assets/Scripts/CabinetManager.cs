﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetManager : MonoBehaviour
{
    public ClockManager clock;

    public GameObject returnArrow;
    public GameObject cabinetButtons;
    public GameObject cabinetGeneral;
    public GameObject cabinet;
    public GameObject plate;
    public GameObject glass;

    public Animator door1Anim;
    public Animator door3Anim;
    public Animator glassAnim;
    public Animator plateAnim;

    private Animator anim;

    private bool isLocked;

    public bool hasTime;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isLocked = false;
        hasTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isLocked == false)
        {
            //Debug.Log("Mouse Clicked");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider.CompareTag("Nothing"))
            {
                cabinetGeneral.SetActive(true);
                cabinet.SetActive(false);
                returnArrow.SetActive(false);
                cabinetButtons.SetActive(false);
            }

            if (hit.collider.CompareTag("Cabinet"))
            {
                door1Anim.SetTrigger("Door1OpenFull");
                plate.SetActive(true);
                glass.SetActive(true);
                StartCoroutine(CabinetRewind());
            }
        }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            if (anim != null)
            {
                // play Bounce but start at a quarter of the way though
                anim.Play("Base Layer.cabinetOpen", 0, 0.25f);
            }
        }*/
    }

    IEnumerator CabinetRewind()
    {
        yield return new WaitForSeconds(2);
        glassAnim.SetTrigger("GlassTaken");

        yield return new WaitForSeconds(2);
        glass.SetActive(false);

        yield return new WaitForSeconds(2);
        door3Anim.SetTrigger("Door3OpenFull");

        yield return new WaitForSeconds(2);
        plateAnim.SetTrigger("PlateTaken");

        yield return new WaitForSeconds(2);
        plate.SetActive(false);
    }

    public void ButtonBehaviour(int i)
    {
        if (isLocked == false && hasTime == true)
        {
            switch (i)
            {
                case (0):
                default:
                    glass.SetActive(true);
                    door1Anim.SetTrigger("Door1Part1");
                    clock.Drain();
                    break;
                case (1):
                    glass.SetActive(true);
                    door1Anim.SetTrigger("Door1Part2");
                    glassAnim.SetTrigger("GlassTaken");
                    clock.Drain();
                    break;
                case (2):
                    glass.SetActive(false);
                    door1Anim.SetTrigger("Door1Part3");
                    clock.Drain();
                    break;
                case (3):
                    plate.SetActive(true);
                    door3Anim.SetTrigger("Door3Part1");
                    clock.Drain();
                    break;
                case (4):
                    plate.SetActive(true);
                    door3Anim.SetTrigger("Door3Part2");
                    plateAnim.SetTrigger("PlateTaken");
                    clock.Drain();
                    break;
                case (5):
                    plate.SetActive(false);
                    door3Anim.SetTrigger("Door3Part3");
                    clock.Drain();
                    break;
            }
        }
        
    }

    public void LockAndUnlock()
    {
        if (isLocked == false && hasTime == true)
        {
            returnArrow.SetActive(false);
            isLocked = true;
            StartCoroutine(Unlock());
        }        
    }

    public void NoMoreTime()
    {
        hasTime = false;
    }

    IEnumerator Unlock()
    {
        yield return new WaitForSeconds(2);
        isLocked = false;
        returnArrow.SetActive(true);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogControl : MonoBehaviour
{
    public GameObject introTextObj;
    public GameObject introCover;
    public GameObject returnArrow;

    public Animator fadeAnim;

    public SpriteRenderer[] dialog;

    private float delay = 0.1f;

    [SerializeField]
    private string introText;

    private string currentText = "";

    [SerializeField]
    private int sequenceNum;

    // Start is called before the first frame update
    void Start()
    {
        sequenceNum = -1;
        StartCoroutine(DialogStart());
    }

    // Update is called once per frame
    void Update()
    {
        switch (sequenceNum)
        {
            case 1:
                dialog[0].enabled = false;
                break;
            case 2:
                dialog[1].enabled = false;
                break;
            case 3:
                dialog[2].enabled = false;
                break;
            case 4:
                dialog[3].enabled = false;
                break;
            case 5:
                dialog[4].enabled = false;
                break;
            case 6:
                dialog[5].enabled = false;
                returnArrow.SetActive(false);
                StartCoroutine(TransitionToLevel());
                break;
        }
    }

    public void ButtonBehaviour()
    {
        sequenceNum++;       
    }

    IEnumerator DialogStart()
    {
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < introText.Length; i++)
        {
            currentText = introText.Substring(0, i);
            introTextObj.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(1.0f);
        //introCover.SetActive(false);
        fadeAnim.SetTrigger("FadeIn");
        sequenceNum++;
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < dialog.Length; i++)
            dialog[i].enabled = true;
        returnArrow.SetActive(true);
    }

    IEnumerator TransitionToLevel()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Kitchen");
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class DialogControl : MonoBehaviour
{
    public GameObject introTextObj;
    public GameObject introCover;
    public GameObject returnArrow;
    public GameObject skipText;
    public GameObject[] balloons;

    public Animator fadeAnim;
    public Animator explosiveAnim;

    public SpriteRenderer[] dialog;
    public SpriteRenderer explosiveChar;

    public AudioMixer musicMix;

    public GameObject[] hmm;

    private float delay = 0.1f;
    private float dialogDelay = 0.02f;

    [SerializeField]
    private string introText;

    private string currentText = "";

    [SerializeField]
    private int sequenceNum;

    public Animator[] balloon;

    public string text;

    public GameObject[] textObj;

    public bool isSpeaking;

    [SerializeField] private bool isSkipping;
    [SerializeField] private bool isLocked;

    // Start is called before the first frame update
    void Start()
    {
        sequenceNum = -1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(DialogStart());

    }

    // Update is called once per frame
    void Update()
    {
        /*switch (sequenceNum)
        {
            case 1:
                dialog[0].enabled = false;
                dialog[1].enabled = true;
                break;
            case 2:
                dialog[1].enabled = false;
                dialog[2].enabled = true;
                break;
            case 3:
                dialog[2].enabled = false;
                dialog[3].enabled = true;
                break;
            case 4:
                dialog[3].enabled = false;
                dialog[4].enabled = true;
                break;
            case 5:
                dialog[4].enabled = false;
                dialog[5].enabled = true;
                break;
            case 6:
                dialog[5].enabled = false;
                returnArrow.SetActive(false);
                StartCoroutine(TransitionToLevel());
                break;
        }*/
    }

    public void ButtonBehaviour()
    {
        if (isLocked == false)
        {
            sequenceNum++;
            isSkipping = true;

            switch (sequenceNum)
            {
                case 1:
                    HideThought(1);
                    StartCoroutine(NextText(0, 1, 3));
                    StartCoroutine(Unlock());
                    break;
                case 2:
                    HideThought(0);
                    StartCoroutine(NextText(1, 2, 1));
                    StartCoroutine(Unlock());
                    break;
                case 3:
                    HideThought(1);
                    StartCoroutine(NextText(0, 3, 3));
                    StartCoroutine(Unlock());
                    break;
                case 4:
                    HideThought(0);
                    StartCoroutine(NextText(1, 4, 2));
                    StartCoroutine(Unlock());
                    break;
                case 5:
                    HideThought(1);
                    StartCoroutine(NextText(0, 5, 3));
                    StartCoroutine(Unlock());
                    break;
                case 6:
                    HideThought(0);
                    StartCoroutine(Unlock());
                    skipText.GetComponent<Animator>().SetTrigger("Hide");
                    StartCoroutine(TransitionToLevel());
                    StartCoroutine(FadeMixerGroup.StartFade(musicMix, "BackMusic", 2, 0));
                    StartCoroutine(Unlock());
                    break;
            }
        }
    }

    IEnumerator NextText(int i, int b, int c)
    {
        yield return new WaitForSeconds(1.0f);
        KeepThought(i, c);
        switch (b)
        {
            case 1:
                text = "Thanks, it'll be good to share the expenses. And don't worry, I'm aware of the situation..";
                break;
            case 2:
                text = "Yeah, but you know... Since the accident 2 months ago, finding suitors has been impossible!.";
                break;
            case 3:
                text = "I can imagine! Luckily, it seems our abilities are a match made in heaven..";
                break;
            case 4:
                text = "Hope so. I just don't want to go through the same thing again....";
                break;
            case 5:
                text = "Don't worry! You'll see that we will get along very well....";
                break;
        }
    }

    IEnumerator DialogStart()
    {
        yield return new WaitForEndOfFrame();
        StartCoroutine(FadeMixerGroup.StartFade(musicMix, "BackMusic", 10, 1));
        yield return new WaitForSeconds(2.5f);
        for (int i = 0; i < introText.Length; i++)
        {
            currentText = introText.Substring(0, i);
            introTextObj.GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(1.5f);
        //introCover.SetActive(false);
        fadeAnim.SetTrigger("FadeIn");
        sequenceNum++;
        yield return new WaitForSeconds(1.0f);
        introTextObj.SetActive(false);
        explosiveChar.enabled = true;
        explosiveAnim.SetTrigger("FadeIn");
        FindObjectOfType<AudioCtrl>().Play("Scribble");
        yield return new WaitForSeconds(2.0f);
        KeepThought(1, 0);
        text = "Welcome! I'm so glad you showed interest in the room. It has been a though few weeks....";
        skipText.SetActive(true);
        skipText.GetComponent<Animator>().SetTrigger("Show");
        /*yield return new WaitForSeconds(5.0f);
        if (isSkipping == true)
            StartCoroutine(RemoveSkip());
        else
            HideThought(1);
        yield return new WaitForSeconds(1.0f);
        if (isSkipping == true)
            StartCoroutine(RemoveSkip());
        else
        {
            KeepThought(0);
            text = "Thanks, it'll be good to share the expenses. And don't worry, I'm aware of the situation..";
        }
        yield return new WaitForSeconds(4.0f);
        if (isSkipping == true)
            StartCoroutine(RemoveSkip());
        else
            HideThought(0);
        yield return new WaitForSeconds(1.0f);
        if (isSkipping == true)
            StartCoroutine(RemoveSkip());
        else
        {
            KeepThought(1);
            text = "Yeah, but you know... Since the accident 2 months ago, finding suitors has been impossible!.";
        }
        yield return new WaitForSeconds(5.0f);
        if (isSkipping == true)
            StartCoroutine(RemoveSkip());
        else
            HideThought(1);
        yield return new WaitForSeconds(1.0f);
        if (isSkipping == true)
            StartCoroutine(RemoveSkip());
        else
        {
            KeepThought(0);
            text = "I can imagine! Luckily, it seems our abilities are a match made in heaven..";
        }
        yield return new WaitForSeconds(3.0f);
        if (isSkipping == true)
            StartCoroutine(RemoveSkip());
        else
            HideThought(0);
        yield return new WaitForSeconds(1.0f);
        if (isSkipping == true)
            StartCoroutine(RemoveSkip());
        else
        {
            KeepThought(1);
            text = "Hope so. I just don't want to go through the same thing again....";
        }
        yield return new WaitForSeconds(3.0f);
        if (isSkipping == true)
            StartCoroutine(RemoveSkip());
        else
            HideThought(1);
        yield return new WaitForSeconds(1.0f);
        if (isSkipping == true)
            StartCoroutine(RemoveSkip());
        else
        {
            KeepThought(0);
            text = "Don't worry! You'll see that we will get along very well....";
        }
        yield return new WaitForSeconds(3.0f);
        if (isSkipping == true)
            StartCoroutine(RemoveSkip());
        else
            HideThought(0);
        yield return new WaitForSeconds(1.0f);
        if (isSkipping == true)
            StartCoroutine(RemoveSkip());
        else
        {
            skipText.GetComponent<Animator>().SetTrigger("Hide");
            StartCoroutine(TransitionToLevel());
            StartCoroutine(FadeMixerGroup.StartFade(musicMix, "BackMusic", 2, 0));
        }*/
    }

    IEnumerator TransitionToLevel()
    {
        yield return new WaitForSeconds(0.5f);
        fadeAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Kitchen");
    }

    public void KeepThought(int i, int b)
    {
        if (isSpeaking == false)
        {
            balloon[i].SetBool("Visible", true);
            isSpeaking = true;
            StartCoroutine(ShowText(i, b));
        }
    }

    public void HideThought(int i)
    {
        StartCoroutine(InstantDissappear(i));
    }

    IEnumerator InstantDissappear(int i)
    {
        yield return new WaitForSeconds(0.1f);
        balloon[i].SetBool("Visible", false);
        text = "";
        currentText = "";
        isSpeaking = false;
        yield return new WaitForSeconds(1.0f);
        textObj[i].GetComponent<TextMeshProUGUI>().text = currentText;
    }

    IEnumerator ShowText(int b, int c)
    {
        yield return new WaitForSecondsRealtime(0.7f);
        if (c <= 2)
            hmm[c].GetComponent<AudioSource>().Play();
        for (int i = 0; i < text.Length; i++)
        {
            currentText = text.Substring(0, i);
            textObj[b].GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSecondsRealtime(dialogDelay);
        }
    }

    IEnumerator RemoveSkip()
    {
        yield return new WaitForEndOfFrame();
        isSkipping = false;
    }

    IEnumerator Unlock()
    {
        yield return new WaitForEndOfFrame();
        isLocked = true;
        yield return new WaitForSeconds(2.0f);
        isLocked = false;
    }
}

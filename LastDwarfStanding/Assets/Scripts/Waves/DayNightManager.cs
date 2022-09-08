using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class DayNightManager : MonoBehaviour
{

    public bool isDayTime;
    public float lengthOfDay = 20f;
    private float currentTime = 0f;
    //public GameObject dayNightmother;
    public Image[] sceneImages;
    public GameObject player;

    //public SpriteRenderer[] sceneSprites;
    public List<SpriteRenderer> SpritesInScene;

    public Color dayColour = new Color(255, 255, 255, 255);
    public Color nightColour = new Color(119, 122, 154, 255);
    public Color targetColour;
    public Color currentColor = Color.white;
    public Color currentSpriteColor = Color.white;

    //public bool lerpColour;


    float t;
    public float dayRate = 1;
    public float nightRate = 1;
    private float _targetRate = 1;
    public float lerpVectorDistance = 1f;
    // public Image nightMaterialImage;

    public List<float> transitionDurations = new List<float>();
    private float durationTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        targetColour = dayColour;
        //  nightMaterialImage = nightMaterial.GetComponent<Image>();

        //sceneImages = FindObjectOfType<Image>();
        isDayTime = true;
        //  ChangeNightTimeMaterial();



    }

    // Update is called once per frame
    void Update()


    {

        //print(SpritesInScene);
        currentTime += Time.deltaTime;
        //print("CurrentTime = " + currentTime);

        if (currentTime > lengthOfDay)
        {
            currentTime = 0f;
            ToggleDayNight();
            //lerpColour = true;
            //Debug.Log("Day Night swiching");
        }

        /*  if (isDayTime)
          {
              LinearColourTransitionToDay();
          }
          else
          {
              LinearColourTransitionToNight();
          }*/


        t += Time.deltaTime / lengthOfDay;


        ColourLerp();
    }

    private void ToggleDayNight()
    {
        if (isDayTime)
        {
            isDayTime = false;
            //ChangeNightTimeColours();
            targetColour = nightColour;
            _targetRate = nightRate;



        }
        else
        {
            isDayTime = true;
            // ChangeDayTimeColours();
            targetColour = dayColour;
            _targetRate = dayRate;
        }
        //ChangeNightTimeMaterial();
    }



    private void ChangeNightTimeColours()
    {
        Debug.Log("Changing colour to night");
        foreach (Image image in sceneImages)
        {
            image.GetComponent<Image>().color = nightColour;
        }

    }

    private void ChangeDayTimeColours()
    {
        Debug.Log("Changing colour to day");
        foreach (Image image in sceneImages)
        {
            image.GetComponent<Image>().color = dayColour;
        }

    }

    public void ColourLerp()
    {
        currentColor = sceneImages[0].color;
        foreach (Image image in sceneImages)
        {

            if (image.color != targetColour)
            {
                //Debug.Log(image.name + " Lerping to target colour");
                image.color = Color.LerpUnclamped(currentColor, targetColour, _targetRate * Time.deltaTime);

                if (image == sceneImages[0]) durationTimer += Time.deltaTime;


                //Debug.Log("Colour Vector Distance is " + Vector3.Distance(ColorToVector(image.color), ColorToVector(targetColour)));
                if (Vector3.Distance(ColorToVector(image.color), ColorToVector(targetColour)) < lerpVectorDistance)
                {
                    //currentColor = targetColour;
                    image.color = targetColour;

                    if (image == sceneImages[0])
                    {
                        transitionDurations.Add(durationTimer);
                        durationTimer = 0;
                    }
                }
            }
        }
        currentSpriteColor = SpritesInScene[0].color;


        foreach (SpriteRenderer sprite in SpritesInScene)
        {

            if (sprite.color != targetColour)
            {
                //Debug.Log(image.name + " Lerping to target colour");
                sprite.color = Color.LerpUnclamped(currentColor, targetColour, _targetRate * Time.deltaTime);

                if (sprite == SpritesInScene[0]) durationTimer += Time.deltaTime;


                //Debug.Log("Colour Vector Distance is " + Vector3.Distance(ColorToVector(image.color), ColorToVector(targetColour)));
                if (Vector3.Distance(ColorToVector(sprite.color), ColorToVector(targetColour)) < lerpVectorDistance)
                {
                    //currentColor = targetColour;
                    sprite.color = targetColour;

                    if (sprite == SpritesInScene[0])
                    {
                        transitionDurations.Add(durationTimer);
                        durationTimer = 0;
                    }
                }
            }
        }

    }



    private Vector3 ColorToVector(Color color)
    {
        return new Vector3(color.r, color.g, color.b);
    }
}

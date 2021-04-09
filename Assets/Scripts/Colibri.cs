using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Colibri : MonoBehaviour
{
    public float scrollSpeed = 0.11F;
    public float speed = 0f;
    public Renderer rend;
    public bool adv = true;
    public float advanceI;
    public Slider mainSlider;
    public float newValue = 0f;
    public Dropdown myDropdown;
    public GameObject camera;
    public GameObject plano;
    public Light lightComp1;
    public Light lightComp2;
    public Light lightComp3;
    public GameObject col1=null;
    public GameObject col2 = null;
    public GameObject col3 = null;


    public float transitionDuration = 2.5f;
    public Transform target;

    IEnumerator Transition(Vector3 newPosition)
    {
        float t = 0.0f;
        Vector3 startingPos = camera.transform.position;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);


            camera.transform.position = Vector3.Lerp(startingPos, newPosition, t);
            yield return 0;
        }

    }


    //Invoked when a submit button is clicked.
    public void SubmitSliderSetting()
    {
        if (newValue != mainSlider.value)
        {
            newValue = mainSlider.value;
            Debug.Log(mainSlider.value);
            RenderSettings.skybox.SetFloat("_AtmosphereThickness", mainSlider.value);
            DynamicGI.UpdateEnvironment();
        }
    }
    public void ChangeTextureColibri()
    {
        if (speed > 0.10f && adv == true)
        {
            adv = false;

        }
        else if (speed < 0f && adv == false)
        {
            adv = true;

        }
        if (adv == true)
        {
            speed += 0.001f;

        }
        else
        {
            speed -= 0.001f;


        }
        rend.material.SetFloat("_Parallax", speed);
    }
    public void createLights()
    {
        //Luces de la flor
        GameObject lightGameObject = new GameObject("LightFlower1");
        Light lightComp = lightGameObject.AddComponent<Light>();

        // Set color and position
        lightComp.color = new Color(0.678f, 0.662f, 0.207f);
        lightComp.intensity = 18f;
        lightComp.range = 1f;
        lightComp.type = LightType.Point;
        lightComp.lightmapBakeType = LightmapBakeType.Baked;
        lightComp.transform.position = new Vector3(0.479999989f, 7.65999985f, 0.699999988f);

        GameObject lightGameObject2 = new GameObject("LightFlower2");
        Light lightComp2 = lightGameObject2.AddComponent<Light>();

        // Set color and position
        lightComp2.color = new Color(0.678f, 0.662f, 0.207f);
        lightComp2.intensity = 18f;
        lightComp2.range = 1f;
        lightComp2.type = LightType.Point;
        lightComp2.lightmapBakeType = LightmapBakeType.Baked;
        lightComp2.transform.position = new Vector3(0.479999989f, 8f, 0.699999988f);


        //Luz colibri

        GameObject lightGameObject3 = new GameObject("LightColi1");
        Light lightComp3 = lightGameObject3.AddComponent<Light>();

        // Set color and position
        lightComp3.color = Color.white;
        lightComp3.intensity = 1.5f;
        lightComp3.range = 1f;
        lightComp3.type = LightType.Point;
        lightComp3.lightmapBakeType = LightmapBakeType.Baked;
        lightComp3.transform.position = new Vector3(-0.0199999996f, 8.10000038f, 1.49000001f);

       


    }
    void Destroy()
    {
        myDropdown.onValueChanged.RemoveAllListeners();
    }

    private void myDropdownValueChangedHandler(Dropdown target)
    {
        Debug.Log("selected: " + target.value);
        if (target.value == 0)
        {
            StartCoroutine(Transition(new Vector3(33.75f, 8.01200008f, -5.40999985f)));

        }
        else if (target.value == 2)
        {
            StartCoroutine(Transition(new Vector3(0.870000005f, 8.01200008f, 1.22000003f)));

        }
        else
        {
            StartCoroutine(Transition(new Vector3(9.19999981f, 8.01200008f, -0.699999988f)));


        }
    }

    public void SetDropdownIndex(int index)
    {
        myDropdown.value = index;
    }
   
    public void createMaterialPlane()
    {
        Texture runtimeTexture = Resources.Load<Texture>("Textures/terrainpasto");
        Texture runtimeTextureNormal = Resources.Load<Texture>("Textures/terrainpastoNormalMap");
        Material runtimeMaterial = new Material(Shader.Find("Standard"));
        runtimeMaterial.SetTexture("_MainTex", runtimeTexture);
        runtimeMaterial.SetTexture("_BumpMap", runtimeTextureNormal);
        runtimeMaterial.SetTexture("_ParallaxMap", runtimeTexture);
        runtimeMaterial.SetFloat("_ParallaxScale", 1f);
        plano.GetComponent<Renderer>().material = runtimeMaterial;

    }
    public void createColibries()
    {
        /** if (obj == null)
         {
             obj = Resources.Load("Objects/colibriObj Variant") as GameObject;
             GameObject.Instantiate(obj, new Vector3(-2.0999999f, 1.35124993f, 118.099998f), Quaternion.identity);

         }**/
        col1 = GameObject.Find("col1");
        col2 = GameObject.Find("col2");
        col3 = GameObject.Find("col3");

        col1.transform.position = new Vector3(32.9000015f, 7.71999979f, -4.88000011f);
        col2.transform.position = new Vector3(30.0400009f, 6.53999996f, -6.55000019f);


    }
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        camera = GameObject.Find("Main Camera");
        mainSlider = GameObject.Find("Slider").GetComponent<Slider>();
        myDropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        myDropdown.onValueChanged.AddListener(delegate {
            myDropdownValueChangedHandler(myDropdown);
        });
        plano = GameObject.Find("plano");

        createLights();
        createMaterialPlane();
        createColibries();





    }

    void Update()
    {
        ChangeTextureColibri();
        SubmitSliderSetting();
    }
}

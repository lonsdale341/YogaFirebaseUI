using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class ControllerEvents : MonoBehaviour
{
   
    public LabellEntry[] Labels;
   public  Animator []animators;
    
    public Text textState;
    public GameObject Body;
    public GameObject Muscule;
    private int stateFade;
    public Material mater;
    public Renderer[] MusculeReners;
    public List<Material> MusculeMaterials;
    public bool IsFade;
    float prevSpeed;

    // Use this for initialization
    void Start()
    {;
        //animators = FindObjectsOfType<Animator>();
        animators = gameObject.GetComponentsInChildren<Animator>();
        foreach (LabellEntry label in Labels)
        {
            label.Labels.SetActive(false);
        }
        textState.text = StringConstants.BodyState;
        foreach (MaterialEntry entry in CommonData.prefabs.objectMaterials)
        {
            foreach (Material materialEntry in entry.Materials)
            {
                materialEntry.SetColor("_Color", new Vector4(1, 1f, 1f, 1f));
            }
        }
        IsFade = true;
        MusculeReners = Muscule.GetComponentsInChildren<Renderer>();
        //   foreach (Renderer musculeRender in MusculeReners)
        //   {
        //       foreach (Material m in musculeRender.materials)
        //       {
        //           MusculeMaterials.Add(m);
        //          //if (MusculeMaterials.Count(item=>item.name==m.name)==0)
        //          //{
        //          //    
        //          //}
        //           
        //       }
        //   }

        stateFade = 0;
        Body.SetActive(true);
        Muscule.SetActive(false);
        // Muscule_SetMaterialTransparent();
        // Body_SetMaterialOpaque();
        // iTween.FadeTo(Muscule, 0, 0.001f);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetFade()
    {
        if (IsFade)
        {
            // IsFade = false;
            switch (stateFade)
            {
                case 0:
                    foreach (ControllerEvents events in CommonData.controllers_Events)
                    {
                        events.SetMuscule();
                    }
                    break;
                case 1:
                    foreach (ControllerEvents events in CommonData.controllers_Events)
                    {
                        events.SetBody();
                    }
                    break;
            }
        }


    }

    public void SetBody()
    {
        foreach (Animator an in animators)
        {

            an.speed = prevSpeed;

        }

        textState.text = StringConstants.BodyState;
        stateFade = 0;
        CommonData.StateFade = 0;
        Body.SetActive(true);
        Muscule.SetActive(false);
        //  Muscule_SetMaterialTransparent();
        //  iTween.FadeTo(Muscule, 0, 1);
        //  iTween.FadeTo(Body, 1, 0.5f);
        //  Invoke("Body_SetMaterialOpaque", 0.5f);
        //  StartCoroutine(SetIsFade());
    }

    public void SetMuscule()
    {
        stateFade = 1;
        CommonData.StateFade = 1;
        Body.SetActive(false);
        Muscule.SetActive(true);
        textState.text = StringConstants.MusculeState;
        foreach (Animator an in animators)
        {
            prevSpeed = an.speed;
            an.speed = 0;

        }

        // Body_SetMaterialTransparent();
        // iTween.FadeTo(Body, 0, 1);
        // iTween.FadeTo(Muscule, 1, 0.5f);
        // Invoke("Muscule_SetMaterialOpaque", 0.5f);
        // StartCoroutine(SetIsFade());
    }
    IEnumerator SetIsFade()
    {
        yield return new WaitForSeconds(1.5f);
        IsFade = true;
    }
    public void GetAnimationEvent(string message)
    {

        Debug.Log(message);

    }

    public void GetAnimationEvent_Label(string message)
    {

        foreach (LabellEntry label in Labels)
        {
            label.Labels.SetActive(false);
        }
        if (!string.IsNullOrEmpty(message))
        {
            var items = Labels.Where(item => item.nameAnimation == message);
            foreach (LabellEntry entry in items)
            {
                entry.Labels.SetActive(true);
            }
        }
        
    }
    private void Body_SetMaterialTransparent()
    {

        foreach (Material m in Body.GetComponent<Renderer>().materials)
        {

            m.SetFloat("_Mode", 2);

            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

            m.SetInt("_ZWrite", 0);

            m.DisableKeyword("_ALPHATEST_ON");

            m.EnableKeyword("_ALPHABLEND_ON");

            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");

            m.renderQueue = 3000;

        }

    }
    private void Muscule_SetMaterialTransparent()
    {
        foreach (Material m in MusculeMaterials)
        {
            m.SetFloat("_Mode", 2);

            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

            m.SetInt("_ZWrite", 0);

            m.DisableKeyword("_ALPHATEST_ON");

            m.EnableKeyword("_ALPHABLEND_ON");

            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");

            m.renderQueue = 3000;
        }



    }
    private void Body_SetMaterialOpaque()
    {

        foreach (Material m in Body.GetComponent<Renderer>().materials)
        {

            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

            m.SetInt("_ZWrite", 1);

            m.DisableKeyword("_ALPHATEST_ON");

            m.DisableKeyword("_ALPHABLEND_ON");

            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");

            m.renderQueue = -1;

        }

    }
    private void Muscule_SetMaterialOpaque()
    {

        foreach (Material m in MusculeMaterials)
        {
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

            m.SetInt("_ZWrite", 1);

            m.DisableKeyword("_ALPHATEST_ON");

            m.DisableKeyword("_ALPHABLEND_ON");

            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");

            m.renderQueue = -1;
        }



    }
}

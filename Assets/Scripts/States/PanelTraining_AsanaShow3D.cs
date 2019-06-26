using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace States
{
    class PanelTraining_AsanaShow3D : BaseState
    {

        
        Animator animatorComponents;
        Menus.PanelTrainingAsanaShow3DGUI dialogComponent;
        GameObject person;
        public PanelTraining_AsanaShow3D()
        {
            
        }
        public override void Initialize()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            //person=GameObject.Instantiate(CommonData.prefabs.menuGameObject[StringConstants.ShowPerson_3D]);
             CommonData.prefabs.menuGameObject[StringConstants.ShowPerson_3D].SetActive(true);
             animatorComponents = CommonData.prefabs.menuGameObject[StringConstants.ShowPerson_3D].GetComponentInChildren<Animator>(true);
            //animatorComponents = person.GetComponentInChildren<Animator>(true);
            animatorComponents.SetTrigger(CommonData.currentAssana);
            dialogComponent = SpawnUI<Menus.PanelTrainingAsanaShow3DGUI>(StringConstants.PrefabsPanelTrainingAsanaShow3D);
        }

        public override void Suspend()
        {
            Screen.orientation = ScreenOrientation.Portrait;
            // GameObject.Destroy(person);
            //person.SetActive(false);
            CommonData.prefabs.menuGameObject[StringConstants.ShowPerson_3D].SetActive(false);
            HideUI();
        }

        public override void Resume(StateExitValue results)
        {
            ShowUI();
            // person.SetActive(true);
            //  person = GameObject.Instantiate(CommonData.prefabs.menuGameObject[StringConstants.ShowPerson_3D]);
            CommonData.prefabs.menuGameObject[StringConstants.ShowPerson_3D].SetActive(true);
            animatorComponents = CommonData.prefabs.menuGameObject[StringConstants.ShowPerson_3D].GetComponentInChildren<Animator>(true);
            animatorComponents.SetTrigger(CommonData.currentAssana);
             
           // animatorComponents.SetTrigger(assanaName);
        }

        public override StateExitValue Cleanup()
        {
            Screen.orientation = ScreenOrientation.Portrait;
           // GameObject.Destroy(person);
            CommonData.prefabs.menuGameObject[StringConstants.ShowPerson_3D].SetActive(false);
            DestroyUI();
            return new StateExitValue(typeof(PanelTraining_AsanaShow3D), null);
        }

        public override void HandleUIEvent(GameObject source, object eventData)
        {
            if (source == dialogComponent.Back.gameObject)
            {

                manager.PopState();
            }

            else if (source == dialogComponent.AR.gameObject)
            {
                manager.PushState(new PanelTraining_AsanaShow_AR());
            }


        }
    }


}

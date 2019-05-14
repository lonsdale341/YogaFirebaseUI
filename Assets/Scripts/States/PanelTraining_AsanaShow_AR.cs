using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace States
{
    class PanelTraining_AsanaShow_AR : BaseState
    {

        Animator[] animatorComponents;
        Menus.PanelTrainingAsanaShow_AR_GUI dialogComponent;
        public PanelTraining_AsanaShow_AR()
        {

        }
        public override void Initialize()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;

            animatorComponents = CommonData.prefabs.menuGameObject[StringConstants.ShowPerson_AR].GetComponentsInChildren<Animator>(true);
            foreach (var component in animatorComponents)
                component.SetTrigger(CommonData.currentAssana);
            //animatorComponents.SetTrigger(CommonData.currentAssana);
            dialogComponent = SpawnUI<Menus.PanelTrainingAsanaShow_AR_GUI>(StringConstants.PrefabsPanelTrainingAsanaShowAR);
        }

        public override void Suspend()
        {
            HideUI();
        }

        public override void Resume(StateExitValue results)
        {
            ShowUI();

        }

        public override StateExitValue Cleanup()
        {
            // Screen.orientation = ScreenOrientation.Portrait;
            // CommonData.prefabs.menuLookup[StringConstants.ShowPerson_3D].SetActive(false);
            DestroyUI();
            return new StateExitValue(typeof(PanelTraining_AsanaShow_AR), null);
        }

        public override void HandleUIEvent(GameObject source, object eventData)
        {
            if (source == dialogComponent.Back.gameObject)
            {

                manager.PopState();
            }

            else if (source == dialogComponent.D3.gameObject)
            {
                manager.PopState();
            }


        }
    }


}

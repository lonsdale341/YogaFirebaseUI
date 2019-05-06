using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace States
{
    class PanelProfile : BaseState
    {


        Menus.PanelProfileGUI dialogComponent;
        public PanelProfile()
        {



        }
        public override void Initialize()
        {

            dialogComponent = SpawnUI<Menus.PanelProfileGUI>(StringConstants.PrefabsPanelProfile);

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
            DestroyUI();
            return new StateExitValue(typeof(PanelProfile), null);
        }

        public override void HandleUIEvent(GameObject source, object eventData)
        {
            if (source == dialogComponent.Favourites.gameObject)
            {

                manager.PushState(new PanelProfile_Favourites());
            }

            else if (source == dialogComponent.Goals.gameObject)
            {

                manager.PushState(new PanelProfile_Goals());
            }
            else if (source == dialogComponent.PaymentInformation.gameObject)
            {

                manager.PushState(new PanelProfile_Payment());
            }
            else if (source == dialogComponent.PaymentInformation.gameObject)
            {

                //manager.PopState();
            }
            else if (source == dialogComponent.Training.gameObject)
            {

                manager.ClearStack(new PanelTrainingCatalogList());
            }
            else if (source == dialogComponent.Instructor.gameObject)
            {

                //manager.PopState();
            }
            else if (source == dialogComponent.Costomize.gameObject)
            {

                manager.ClearStack(new PanelCustomize());
            }
            else if (source == dialogComponent.Social.gameObject)
            {

                manager.ClearStack(new PanelSocialMain());
            }


        }
    }


}

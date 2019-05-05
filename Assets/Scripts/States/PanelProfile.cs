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

                //manager.PopState();
            }

            if (source == dialogComponent.Goals.gameObject)
            {

                //manager.PopState();
            }
            if (source == dialogComponent.PaymentInformation.gameObject)
            {

                //manager.PopState();
            }
            if (source == dialogComponent.Training.gameObject)
            {

                manager.SwapState(new PanelTrainingCatalogList());
            }
            if (source == dialogComponent.Instructor.gameObject)
            {

                //manager.PopState();
            }
            if (source == dialogComponent.Costomize.gameObject)
            {

                //manager.PopState();
            }
            if (source == dialogComponent.Social.gameObject)
            {

                //manager.PopState();
            }
            

        }
    }
   

}
 
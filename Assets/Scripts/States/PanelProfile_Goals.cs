using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace States
{
    class PanelProfile_Goals : BaseState
    {

       
        Menus.PanelProfile_GoalsGUI dialogComponent;
        public PanelProfile_Goals()
        {

           

        }
        public override void Initialize()
        {
           
            dialogComponent = SpawnUI<Menus.PanelProfile_GoalsGUI>(StringConstants.PrefabsPanelProfile_Goals);
            
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
            return new StateExitValue(typeof(PanelProfile_Goals), null);
        }
        
        public override void HandleUIEvent(GameObject source, object eventData)
        {
            if (source == dialogComponent.Back.gameObject)
            {

                manager.PopState();
            }
            else if(source == dialogComponent.Raiting.gameObject)
            {

                manager.PushState(new PanelProfile_Raiting());
            }
            else if(source == dialogComponent.Training.gameObject)
            {

                manager.ClearStack(new PanelTrainingCatalogList());
            }
            else if(source == dialogComponent.Instructor.gameObject)
            {

                //manager.PopState();
            }
            else if(source == dialogComponent.Costomize.gameObject)
            {

                //manager.PopState();
            }
            else if(source == dialogComponent.Social.gameObject)
            {

                //manager.PopState();
            }

        }
    }
   

}
 
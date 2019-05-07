﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace States
{
    class PanelProfile_Raiting : BaseState
    {

       
        Menus.PanelProfile_RaitingGUI dialogComponent;
        public PanelProfile_Raiting()
        {

           

        }
        public override void Initialize()
        {
           
            dialogComponent = SpawnUI<Menus.PanelProfile_RaitingGUI>(StringConstants.PrefabsPanelProfile_Raiting);
            
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
            return new StateExitValue(typeof(PanelProfile_Raiting), null);
        }
        
        public override void HandleUIEvent(GameObject source, object eventData)
        {
            if (source == dialogComponent.Back.gameObject)
            {

                manager.PopState();
            }

            else if (source == dialogComponent.Training.gameObject)
            {

                manager.ClearStack(new PanelTrainingCatalogList());
            }
            else if (source == dialogComponent.Instructor.gameObject)
            {

                manager.ClearStack(new PanelInstructor());
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
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace States
{
    class PanelAssanInfo : BaseState
    {

        string assanaName;
        int assanaNumber;
        Menus.PanelAssanInfoGUI dialogComponent;
        public PanelAssanInfo(int number)
        {

           
            assanaNumber = number;

        }
        public override void Initialize()
        {
           
            dialogComponent = SpawnUI<Menus.PanelAssanInfoGUI>(StringConstants.PrefabsPanelAssanInfo);
            dialogComponent.Asana_Name.text = "Asana "+ assanaNumber.ToString();
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
            return new StateExitValue(typeof(PanelAssanInfo), null);
        }
        
        public override void HandleUIEvent(GameObject source, object eventData)
        {
            if (source == dialogComponent.Back.gameObject)
            {

                manager.PopState();
            }
            if (source == dialogComponent.ShowMode.gameObject)
            {

                manager.PushState(new PanelTraining_AsanaShow3D());
            }
            if (source == dialogComponent.Profile.gameObject)
            {

                manager.ClearStack(new PanelProfile());
            }
            if (source == dialogComponent.Instructor.gameObject)
            {

                manager.ClearStack(new PanelInstructor());
            }
            if (source == dialogComponent.Costomize.gameObject)
            {

                manager.ClearStack(new PanelCustomize());
            }
            if (source == dialogComponent.Social.gameObject)
            {

                manager.ClearStack(new PanelSocialMain());
            }

        }
    }
   

}
 
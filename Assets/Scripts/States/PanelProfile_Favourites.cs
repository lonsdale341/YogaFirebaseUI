using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace States
{
    class PanelProfile_Favourites : BaseState
    {

       
        Menus.PanelProfile_FavouritesGUI dialogComponent;
        public PanelProfile_Favourites()
        {

           

        }
        public override void Initialize()
        {
           
            dialogComponent = SpawnUI<Menus.PanelProfile_FavouritesGUI>(StringConstants.PrefabsPanelProfile_Favourites);
            
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
            return new StateExitValue(typeof(PanelProfile_Favourites), null);
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
 
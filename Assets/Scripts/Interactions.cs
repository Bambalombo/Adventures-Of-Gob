using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    public GameObject objectToActivate;
    public void DoInteraction(Interactable interactable, Interactable.PickUpItem heldItem, Interactable.InteractItem itemToInteractWith)
    {
        //Cases
        if (heldItem == Interactable.PickUpItem.Acorn && itemToInteractWith == Interactable.InteractItem.DirtMould)
        {
            foreach (ChangeSprite changeSprite in interactable.changeSprites)
                changeSprite.Change();
            ResetHeldItem(interactable);
            PlaySoundSetNotInteractableAndActivateObjectNiceFunction(interactable);
            GameController._instance.plantedTree = true;
                    }
        if (heldItem == Interactable.PickUpItem.Ladder && itemToInteractWith == Interactable.InteractItem.WaterEdge)
        {
            ResetHeldItem(interactable);
            PlaySoundSetNotInteractableAndActivateObjectNiceFunction(interactable);
        }
        if (heldItem == Interactable.PickUpItem.solarpanel && itemToInteractWith == Interactable.InteractItem.roof)
        {
            ResetHeldItem(interactable);
            PlaySoundSetNotInteractableAndActivateObjectNiceFunction(interactable);
            GameController._instance.solarPanelPlanted = true;
        }
    }

    private void ResetHeldItem(Interactable interactable)
    {
        GameController._instance.heldItem = Interactable.PickUpItem.None;
        Destroy(GameController._instance.heldItemGameObject);
        GameController._instance.heldItemGameObject = null;
    }

    private void PlaySoundSetNotInteractableAndActivateObjectNiceFunction(Interactable interactable)
    {
        SoundController.instance.PlaySound(SoundController.instance.interactSuccesfull);
        interactable.SetNotInteractable();
        objectToActivate.SetActive(true);
    }
}
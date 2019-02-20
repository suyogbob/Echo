using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Pickups
{
    /// <summary>
    /// Tells you if the player can hold multiple instances of this pickup or only one. An example of a pickup that would return
    /// true would be new powers. An example of a pickup that would return false would be batteries or some consumable (TBD).
    /// </summary>
    /// <author>Chris Foley</author>
    /// <returns>Boolean value denoting whether this pickup should even be considered if the user has it.</returns>
    bool isUnique();
    /// <summary>
    /// This function should be run to handle any pickup level operations upon pickups. This function SHOULD NOT change 
    /// anything outside of the scope of the pickup. For example, if I run into a power pickup, it is not the power pickups job to 
    /// add it's new power to my power inventory!!! That is my inventories job!!!
    /// </summary>
    /// <author>Chris Foley</author>
    /// <return>Nothing</return>
    void onPickup();
    /// <summary>
    /// Just a general function used to differentiate between the different classes of pickups. Each pickup class should have a unique name. Names are FINAL.
    /// </summary>
    /// <author>Chris Foley</author>
    /// <returns>A name for the pickup class.</returns>
    string getName();
    string getText();
    void playAudio();

}
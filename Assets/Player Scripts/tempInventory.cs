using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class tempInventory {
    //As a static class all this does is hold values between levels
    public static HashSet<PowerPickups> powerPickup = new HashSet<PowerPickups>();
    public static HashSet<Pickups> inventoryPickup = new HashSet<Pickups>();
}

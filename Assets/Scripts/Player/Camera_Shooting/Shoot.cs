using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shoot : MonoBehaviour {

    public int itemIndex = 0;

    private Item[] attachedItems;
    private Rigidbody rigid;

    // Use this for initialization
    void Awake()
    {
        attachedItems = GetComponentsInChildren<Item>();
        rigid = GetComponent<Rigidbody>();
    }
    void Start()
    {
        SwitchWeapon(itemIndex);
    }
    void Update()
    {
        CheckFire();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CycleWeapon(-1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            CycleWeapon(1);
        }
    }
    void CheckFire()
    {
        // Set CurrentWepaon to attachedWeapon[weaponIndex]
        Item currentItem = attachedItems[itemIndex];
        //if space is down
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentItem.uses > 0)
            {
                //Fire currentWeapon
                currentItem.Use();
            }
            else
            {
                currentItem.Reload();
            }
        }
    }
    void CycleWeapon(int amount)
    {
        // Set desiredindex to weaponIndex + amount
        int desiredIndex = itemIndex + amount;
        // If desired index is >= weapons length
        if (desiredIndex >= attachedItems.Length)
        {
            // Set desiredindex to zero
            desiredIndex = 0;
        }
        //Else if desiredIndex < 0
        else if (desiredIndex < 0)
        {
            //Set desired index to weapons length - 1
            desiredIndex = attachedItems.Length - 1;
        }
        //Set weaponIndex to desiredIndex
        itemIndex = desiredIndex;
        //SwitchWeapon() and pass weaponIndex
        SwitchWeapon(itemIndex);
    }
    Item SwitchWeapon(int itemIndex)
    {
        // Check Bounds
        if (itemIndex < 0 || itemIndex > attachedItems.Length)
        {
            // Return null as Error
            return null;
        }
        // Loop through all attached weapons
        for (int i = 0; i < attachedItems.Length; i++)
        {
            //Set w to attachedWeapons[weaponIndex]
            Item item = attachedItems[i];
            //if i == weaponIndex
            if (i == itemIndex)
            {
                // Activate GameObject in w variable
                item.gameObject.SetActive(true);
            }
            else
            {
                // Deactivate gameObject in w variable
                item.gameObject.SetActive(false);
            }
        }
        return attachedItems[itemIndex];
    }
}

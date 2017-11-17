using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class Shoot : NetworkBehaviour
{
    //Set references for the Player and equipped item
    public Item item;
    private Player player;

    //SET references for a delay on use
    public float useRate = 1f;
    private float useFactor = 0f;

    //Set an item array we can cycle through
    public Item[] equippables;
    int itemIndex = 0;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Player>();
        EquipItem(item);
    }

    // Update is called once per frame
    void Update()
    {
        //If we are not the local client
        if (!isLocalPlayer)
        {
            //Do nothing and return
            return;
        }
        HandleInput();        
        #region Change items
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            CycleItem(-1);
        }
        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            CycleItem(1);
        }
        #endregion
    }
    #region Networking Commands
    [Command]
    void Cmd_Use()
    {
        Rpc_Use();
    }
    [ClientRpc]
    void Rpc_Use()
    {
        item.UseItem();
    }
    #endregion
    void EquipItem(Item currentItem)
    {
        item.player = player;
        item = currentItem;
    }
    #region Set new Item
    void CycleItem(int amount)
    {
        int desiredIndex = itemIndex + amount;
        //If this index is higher than the array amount
        if(desiredIndex >= equippables.Length)
        {
            //SET to zero
            desiredIndex = 0;
        }
        //If the index is lower than Zero
        else if (desiredIndex <0)
        {
            //SET to the array amount -1 (Loop around)
            desiredIndex = equippables.Length - 1;
        }
        //SET our itemIndex to this index and switch to the new Item
        itemIndex = desiredIndex;
        SetNewItem(itemIndex);
    }
    Item SetNewItem(int itemID)
    {
        //Set the bounds of the check in case there is an error in our cycle method
        if(itemID < 0 || itemID > equippables.Length)
        {
            //Force an error
            return null;
        }
        if (isLocalPlayer)
        {
            for (int i = 0; i < equippables.Length; i++)
            {
                Item equippedItem = equippables[i];
                if(i == itemIndex)
                {
                    equippedItem.gameObject.SetActive(true);
                }
                else
                {
                    equippedItem.gameObject.SetActive(false);
                }
            }
        }
        return equippables[itemID];
    }
    #endregion
    #region Input
    void HandleInput()
    {
        //SET fireFactor to fireFactor + Time.deltaTime
        useFactor = useFactor + Time.deltaTime;
        //SET fireInterval to 1/fireRate
        float useInterval = 1 / useRate;
        if (useFactor >= useInterval)
        { 
        //If we click the mouse button or the Right Trigger of an Xbox360 controller is pushed past an arbitrary dead zone
        if (Input.GetButton("Fire1") || Input.GetAxis("Fire2") > 0.1)
        {
            //Begin the Sequence of using an Item
            Cmd_Use();
        }
    }
    }
    #endregion
}

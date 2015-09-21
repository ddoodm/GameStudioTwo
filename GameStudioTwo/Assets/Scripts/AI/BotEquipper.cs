using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SocketEquipment))]
public class BotEquipper : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        // Jesse's code to add attachments to the bot
        AddAttachments();
    }

    public void AddAttachments()
    {
        Equipment[] botItems = new Equipment[5];
        for (int i = 0; i < 5; i++)
            botItems[i] = Equipment.EMPTY;

        int rand;
        for (int i = 0; i < 3; i++)
        {
            rand = Random.Range(0, 3);

            switch (rand)
            {
                case 0:
                    botItems[i] = Equipment.Item_Spike;
                    break;

                case 1:
                    botItems[i] = Equipment.Item_Flipper;
                    break;

                case 2:
                    if ((SocketLocation)i == SocketLocation.FRONT)
                        i--;
                    else
                        botItems[i] = Equipment.Item_Booster;
                    break;

                default:
                    break;
            }
        }

        //botItems[3] = Equipment.Item_Handle;
        botItems[(int)SocketLocation.BACK] = Equipment.Item_Booster;

        GetComponent<SocketEquipment>().SocketItems(botItems, true);
    }
}

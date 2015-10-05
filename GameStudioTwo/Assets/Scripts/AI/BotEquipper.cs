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
        for (int i = 0; i < 5; i++)
        {
            rand = Random.Range(0, 9);

            switch (rand)
            {
                case 0:
                    if ((SocketLocation)i != SocketLocation.BACK)
                        i--;
                    else
                        botItems[i] = Equipment.Item_Handle;
                    break;

                case 1:
                    if ((SocketLocation)i != SocketLocation.TOP)
                        i--;
                    else
                        botItems[i] = Equipment.Item_BasicEngine;
                    break;

                case 2:
                        botItems[i] = Equipment.Item_Spike;
                    break;

                case 3:
                    if ((SocketLocation)i == SocketLocation.BACK || (SocketLocation)i == SocketLocation.TOP)
                        i--;
                    else
                        botItems[i] = Equipment.Item_Flipper;
                    break;

                case 4:
                    if ((SocketLocation)i == SocketLocation.FRONT || (SocketLocation)i == SocketLocation.TOP)
                        i--;
                    else
                        botItems[i] = Equipment.Item_Booster;
                    break;

                case 5:
                    if ((SocketLocation)i == SocketLocation.BACK || (SocketLocation)i == SocketLocation.TOP)
                        i--;
                    else
                        botItems[i] = Equipment.Item_MetalShield;
                    break;

                case 6:
                    if ((SocketLocation)i != SocketLocation.TOP)
                        i--;
                    else
                        botItems[i] = Equipment.Item_PlasmaShield;
                    break;

                case 7:
                    if ((SocketLocation)i == SocketLocation.BACK || (SocketLocation)i == SocketLocation.TOP)
                        i--;
                    else
                        botItems[i] = Equipment.Item_CircularSaw;
                    break;

                case 8:
                    if ((SocketLocation)i == SocketLocation.BACK || (SocketLocation)i == SocketLocation.TOP)
                        i--;
                    else
                        botItems[i] = Equipment.Item_Hammer;
                    break;
                    

                default:
                    break;
            }
        }

        //botItems[3] = Equipment.Item_Handle;
        //botItems[(int)SocketLocation.BACK] = Equipment.Item_Booster;

        GetComponent<SocketEquipment>().SocketItems(botItems, true);
    }
}

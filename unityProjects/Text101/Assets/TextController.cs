using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextController : MonoBehaviour {

    public Text text;
    private enum States
    {
        inventory, start,
        cell, bed, mirror, mirror_break, door, door_unlocked, //scene 1
        hallway, exit, //scene 2
        securityRoom, guard, guard_murder, computer, //scene 3
        freedom
    }
    bool mirrorBroken = false, cellDoorOpen = false, exitOpen = false, guard_alive = true;
    List<string> bag = new List<string>();
    private States currentState;
    private States previousState;

    // Use this for initialization
    void Start() {
        currentState = States.start;
    }

    // Update is called once per frame
    void Update() {
        if (currentState == States.start)
        {
            state_cellStart();
        }
        else if (currentState == States.cell)
        {
            state_cell();
        }
        else if (currentState == States.inventory)
        {
            state_inventory();
        }
        else if (currentState == States.bed)
        {
            state_bed();
        }
        else if (currentState == States.mirror)
        {
            state_mirror();
        }
        else if (currentState == States.mirror_break)
        {
            state_mirrorBreak();
        }
        else if (currentState == States.door)
        {
            state_door();
        }
        else if (currentState == States.door_unlocked)
        {
            state_cellDoorUnlocked();
        }
        //SCENE 2
        else if (currentState == States.hallway)
        {
            state_hall();
        }
        else if (currentState == States.exit)
        {
            state_exit();
        }
        //SCENE 3
        else if (currentState == States.guard)
        {
            state_guard();
        }
        else if (currentState == States.computer)
        {
            state_computer();
        }
        else if (currentState == States.guard_murder)
        {
            state_guard_murder();
        }
        else if (currentState == States.freedom)
        {
            state_freedom();
        }
    }

    //SCENE 1
    void state_cellStart()
    {
        text.text = "You awaken in a dark room to a steady dripping noise and the vague scent of mildew. Groggily, you open your eyes and find yourself " +
                    "in what appears to be a dim, stone-walled prison cell, complete with a bucket and tiny slit in the wall through which light may pour. " +
                    "You're not quite sure how you arrived here, but you know you don't want to stay for long. The walls are completely bare aside from " +
                    "a mirror on the far wall. The door is locked from the outside." +
                    "\n\nPress B to examine the bed, M to examine the mirror, D to examine the door, or I to look in your inventory.";
        if (Input.GetKeyDown(KeyCode.B))
        {
            currentState = States.bed;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            currentState = States.mirror;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (cellDoorOpen)
            {
                currentState = States.door_unlocked;
            }
            else
            {
                currentState = States.door;
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            goToInventory();
        }
    }

    void state_cell()
    {
        text.text = "You stand in your cell, trying to plan your escape." +
                    "\n\nPress B to examine the bed, M to examine the mirror, D to examine the door, or I to look in your inventory.";
        if (Input.GetKeyDown(KeyCode.B))
        {
            currentState = States.bed;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            currentState = States.mirror;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (cellDoorOpen)
            {
                currentState = States.door_unlocked;
            }
            else
            {
                currentState = States.door;
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            goToInventory();
        }
    }
    
    void state_mirror()
    {
        if (mirrorBroken)
        {
            text.text = "You've broken the mirror. There's not much more you can do with it." +
                        "\n\nPress C to return to your cell, or I to examine your inventory.";
            if (Input.GetKeyDown(KeyCode.C))
            {
                currentState = States.cell;
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                goToInventory();
            }
        }
        else
        {
            text.text = "You walk over to the plain, rectangular mirror on the wall. You notice its screws are quite loose. With just " +
                        "a bit of force, you could rip the mirror off the wall and break it." +
                        "\n\nPress C to return to your cell, B to try to break the mirror, or I to examine your inventory.";
            if (Input.GetKeyDown(KeyCode.C))
            {
                currentState = States.cell;
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                inventoryAdd("Shard");
                mirrorBroken = true;
                currentState = States.mirror_break;
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                goToInventory();
            }
        }
    }

    void state_mirrorBreak()
    {
        text.text = "You break the mirror, and pocket one of the sharp shards of glass.\n\nPress C to return to your cell.";
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentState = States.cell;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            previousState = States.cell;
            currentState = States.inventory;
        }
    }

    void state_bed()
    {
        if (bag.Contains("Sheets"))
        {
            text.text = "The hard, lumpy bed is bare.\n\nPress C to return to your cell, or I to look in your inventory.";
            if (Input.GetKeyDown(KeyCode.C))
            {
                currentState = States.cell;
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                goToInventory();
            }
        }
        else
        {
            text.text = "You examine your bed. It's hard and lumpy, with nothing adorning it but sheets." +
                        "\n\nPress S to take the sheets. Press C to return to your cell, or I to look in your inventory.";
            if (Input.GetKeyDown(KeyCode.S))
            {
                inventoryAdd("Sheets");
                currentState = States.cell;
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                currentState = States.cell;
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                goToInventory();
            }
        }
    }

    void state_door()
    {
        if (cellDoorOpen)
        {
            text.text = "The cell's door is unlocked. You may move freely into the hall." +
                        "\n\nPress C to return to your cell, H to move into the hall, or I to look in your inventory.";
            if (Input.GetKeyDown(KeyCode.H))
            {
                currentState = States.hallway;
            }
        }
        else
        {
            text.text = "Your cell door is locked, but the lock looks flimsy. You might be able to force it with something sharp.";
            if (bag.Contains("Shard"))
            {
                text.text += "\n\nPress C to return to your cell, I to open your inventory, or F to try to use the shard you " +
                             "collected earlier to force the lock.";
                if (Input.GetKeyDown(KeyCode.F))
                {
                    cellDoorOpen = true;
                    inventoryRemove("Shard");
                    currentState = States.door_unlocked;
                }
            }
            else
            {
                text.text += "\n\nPress C to return to your cell or I to open your inventory.";
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentState = States.cell;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            goToInventory();
        }
    }

    void state_cellDoorUnlocked()
    {
        text.text = "You successfully break the lock despite your glass shard breaking, and can now move freely outside of your cell." +
                    "\n\nPress C to return to your cell, I to enter your inventory, or H to enter the hallway.";
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentState = States.cell;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            currentState = States.hallway;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            previousState = currentState;
            currentState = States.inventory;
        }
    }

    //SCENE 2
    void state_hall()
    {
        text.text = "You find yourself in the prison's hallway, dimly lit. The walls are lined with empty cells, and there are only " +
                    "two doors that look like they lead anywhere. One has a large \"EXIT\" sign above, while the other reads \"AUTHORIZED " +
                    "PERSONNEL ONLY;\" presumably, that is you'll find the guards." +
                    "\n\nPress E to approach the exit, G to approach the guard's door, or I to access your inventory.";
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentState = States.exit;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            currentState = States.guard;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            goToInventory();
        }
    }

    void state_exit()
    {
        if (exitOpen)
        {
            text.text = "The exit is open! You're free to leave the prison. " +
                        "\n\nPress F to complete your escape, H to return to hallway, or I to look at your inventory.";
            if (Input.GetKeyDown(KeyCode.F))
            {
                currentState = States.freedom;
            }
        }
        else
        {
            if (bag.Contains("Key"))
            {
                text.text = "The exit is locked, but you have the key." +
                            "\n\nPress U to unlock the door, H to return to the hallway, or I to view your inventory.";
                if (Input.GetKeyDown(KeyCode.U))
                {
                    exitOpen = true;
                }
            }
            else
            {
                text.text = "The exit is locked, and you'll need a key to open it." +
                            "\n\nPress H to return to the hallway, or I to look into your inventory.";
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            currentState = States.hallway;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            goToInventory();
        }
    }

    //SCENE 3
    void state_guard()
    {
        if (bag.Contains("Key") || exitOpen)
        {
            if (guard_alive)
            {
                text.text = "You've got what you need! What are you still doing here, the guard could wake up any minute!" +
                            "\n\nPress H to return to the hallway, I to open your inventory, or K to murder the guard for " +
                            "literally no reason.";
                if (Input.GetKeyDown(KeyCode.K))
                {
                    currentState = States.guard_murder;
                }
            }
            else if (!guard_alive)
            {
                text.text = "You're sick. In front of you lies the poor, innocent guard. He was just doing his job, and you murdered " +
                            "him in cold blood. He had a family too! No wonder you're in prison." +
                            "\n\nPress H to return to the hallway, or I to open your inventory.";
                if (!bag.Contains("Key"))
                {
                    text.text += "He's still got a key on him, though it'll be no use to you. Press K to take it.";
                    if (Input.GetKeyDown(KeyCode.K))
                    {
                        inventoryAdd("Key");
                    }
                }
            }
        }
        else
        {
            if (guard_alive)
            {
                text.text = "A security guard slumbers in a chair. You're not sure what you expected. His keys hang loosely from his fingertips " +
                            "and could open the exit door. There's also a computer sitting in front of the guard." +
                            "\n\nPress C to try and sneak around the guard and check out the computer, S to try and steal the key, K to murder the " +
                            "guard, H to return to the hallway, or I to check your inventory.";
                if (Input.GetKeyDown(KeyCode.S))
                {
                    inventoryAdd("Key");
                }
                if (Input.GetKeyDown(KeyCode.K))
                {
                    currentState = States.guard_murder;
                }
            }
            else
            {
                text.text = "Well, the security guard's dead. What are you waiting for?" +
                            "\n\nPress K to take the keys, C to check out the computer, H to return to the hallway, or I to check your inventory.";
                if (Input.GetKeyDown(KeyCode.K))
                {
                    inventoryAdd("Key");
                }
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                currentState = States.computer;
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            currentState = States.hallway;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            goToInventory();
        }
    }

    void state_computer()
    {

        text.text = "You get on the computer. The screen shows nothing but a bunch of security feeds and a massive, conveniently placed, " +
                    "\"MASTER UNLOCK\" button." +
                    "\n\n Press B to use the button to unlock the exit, G to get off the computer, or I to check your inventory.";
        if (Input.GetKeyDown(KeyCode.B))
        {
            exitOpen = true;
            currentState = States.guard;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            currentState = States.guard;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            goToInventory();
        }
    }

    void state_guard_murder()
    {
        guard_alive = false;
        if (bag.Contains("Sheets"))
        {
            if (!bag.Contains("Key"))
            {
                text.text = "Realizing you still have your bedsheets, you quietly sneak up behind him. Holding one end of the sheets " +
                            "in each hand, you quickly wrap the sheets around the mans throat and tighten them around his neck. He wakes " +
                            "up, gasping for air, and lets out a faint \"please...\" as his face turns purple and the life leaves his body. " +
                            "As his body slumps over, you look up to see a photograph of the man standing next to his wife and two daughters." +
                            "\n\nPress K to take the keys, H to return to the hallway, or I to examine your inventory.";
                if (Input.GetKeyDown(KeyCode.K))
                {
                    inventoryAdd("Key");
                    currentState = States.guard;
                }
            }
            else
            {
                text.text = "Realizing you still have your bedsheets, you quietly sneak up behind him. Holding one end of the sheets " +
                            "in each hand, you quickly wrap the sheets around the mans throat and tighten them around his neck. He wakes " +
                            "up, gasping for air, and lets out a faint \"please...\" as his face turns purple and the life leaves his body. " +
                            "As his body slumps over, you look up to see a photograph of the man standing next to his wife and two daughters." +
                            "\n\nPress H to return to the hallway, or I to examine your inventory.";
            }
        }
        else
        {
            if (!bag.Contains("Key"))
            {
                text.text = "With no weapon in sight, you realize you're going to have to get your hands dirty. You sneak up to the guard and " +
                            "catch him in a chokehold, slamming him backwards. The man wakes up and begins to resist, flailing his arms and " +
                            "reaching for his gun. However, you are the stronger one, and despite his desperate attempts, the guard manages " +
                            "no more than a few scratches on your face and a black eye. You knock his head against the hard concrete floor " +
                            "one last time to make sure the job is done. As you stand, you see on his desk a photograph of his wife and two " +
                            "daughters.\n\n Press K to take his keys, H to return to the hallway, or I to examine your inventory.";
                if (Input.GetKeyDown(KeyCode.K))
                {
                    inventoryAdd("Key");
                    currentState = States.guard;
                }
            }
            else
            {
                text.text = "With no weapon in sight, you realize you're going to have to get your hands dirty. You sneak up to the guard and " +
                            "catch him in a chokehold, slamming him backwards. The man wakes up and begins to resist, flailing his arms and " +
                            "reaching for his gun. However, you are the stronger one, and despite his desperate attempts, the guard manages " +
                            "no more than a few scratches on your face and a black eye. You knock his head against the hard concrete floor " +
                            "one last time to make sure the job is done. As you stand, you see on his desk a photograph of his wife and two " +
                            "daughters.\n\n Press H to return to the hallway, or I to examine your inventory.";
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            currentState = States.hallway;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            previousState = States.guard;
            currentState = States.inventory;
        }

    }

    void state_freedom()
    {
        text.text = "Congratulations! You successfully escaped prison.";
    }
    //MISC/OTHER METHODS
    void goToInventory()
    {
        previousState = currentState;
        currentState = States.inventory;
    }
    void inventoryAdd(string item)
    {
        bag.Add(item);
    }
    void inventoryRemove(string item)
    {
        if (bag.Contains(item))
        {
            bag.Remove(item);
        }
    }
    void state_inventory()
    {
        string textString = "Items you're holding: ";
        if (bag.Count == 0)
        {
            textString = "Your bag is empty!";
        }
        else
        {
            foreach (string i in bag)
            {
                textString += "\n\t" + i;
            }
        }
        text.text = textString + "\n\nPress space to return to game.";
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentState = previousState;
        }
    }



}
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Need to be change according to gameloop

public class CrystalUpdate : MonoBehaviour
{

    public bool MyManaCrystals;

    void OnEnable()
    {
        EventManager.StartListening(EventManager.ON_TURN_START, StartCrystal);
        EventManager.StartListening(EventManager.ON_MANA_USAGE, UpdateCrystal);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.ON_TURN_START, StartCrystal);
        EventManager.StopListening(EventManager.ON_MANA_USAGE, UpdateCrystal);
    }


    void StartCrystal()
    {
        if (GameLoop.isMyTurn && MyManaCrystals)
        {
            if (GameLoop.myMana > 0)
            {
                foreach (Transform t in transform)
                {
                    if (t.name == "Crystal1")
                    {

                    }
                        if (t.name == "Crystal1")
                    {
                        t.gameObject.SetActive(true);
                    }
                    if (GameLoop.myMana > 1)
                    {
                        if (t.name == "Crystal2")
                        {
                            t.gameObject.SetActive(true);
                        }
                        if (GameLoop.myMana > 2)
                        {
                            if (t.name == "Crystal3")
                            {
                                t.gameObject.SetActive(true);
                            }
                            if (GameLoop.myMana > 3)
                            {
                                if (t.name == "Crystal4")
                                {
                                    t.gameObject.SetActive(true);
                                }
                                if (GameLoop.myMana > 4)
                                {
                                    if (t.name == "Crystal5")
                                    {
                                        t.gameObject.SetActive(true);
                                    }
                                    if (GameLoop.myMana > 5)
                                    {
                                        if (t.name == "Crystal6")
                                        {
                                            t.gameObject.SetActive(true);
                                        }
                                        if (GameLoop.myMana > 6)
                                        {
                                            if (t.name == "Crystal7")
                                            {
                                                t.gameObject.SetActive(true);
                                            }
                                            if (GameLoop.myMana > 7)
                                            {
                                                if (t.name == "Crystal8")
                                                {
                                                    t.gameObject.SetActive(true);
                                                }
                                                if (GameLoop.myMana > 8)
                                                {
                                                    if (t.name == "Crystal9")
                                                    {
                                                        t.gameObject.SetActive(true);
                                                    }
                                                    if (GameLoop.myMana > 9)
                                                    {
                                                        if (t.name == "Crystal0")
                                                        {
                                                            t.gameObject.SetActive(true);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void UpdateCrystal()
    {
        if (GameLoop.isMyTurn && MyManaCrystals)
        {
            if (GameLoop.myAvailableMana < GameLoop.myMana)
            {
                foreach (Transform t in transform)
                {
                    if(GameLoop.myMana == 10)
                    {
                        if (t.name == "Crystal10")
                        {
                            t.gameObject.SetActive(false);
                        }
                        if (t.name == "UnCrystal10")
                        {
                            t.gameObject.SetActive(true);
                        }
                        if (GameLoop.myAvailableMana < 9)
                        {
                            if (t.name == "Crystal9")
                            {
                                t.gameObject.SetActive(false);
                            }
                            if (t.name == "UnCrystal9")
                            {
                                t.gameObject.SetActive(true);
                            }
                            if (GameLoop.myAvailableMana < 8)
                            {
                                if (t.name == "Crystal8")
                                {
                                    t.gameObject.SetActive(false);
                                }
                                if (t.name == "UnCrystal8")
                                {
                                    t.gameObject.SetActive(true);
                                }
                                if (GameLoop.myAvailableMana < 7)
                                {
                                    if (t.name == "Crystal7")
                                    {
                                        t.gameObject.SetActive(false);
                                    }
                                    if (t.name == "UnCrystal7")
                                    {
                                        t.gameObject.SetActive(true);
                                    }
                                    if (GameLoop.myAvailableMana < 6)
                                    {
                                        if (t.name == "Crystal6")
                                        {
                                            t.gameObject.SetActive(false);
                                        }
                                        if (t.name == "UnCrystal6")
                                        {
                                            t.gameObject.SetActive(true);
                                        }
                                        if (GameLoop.myAvailableMana < 5)
                                        {
                                            if (t.name == "Crystal5")
                                            {
                                                t.gameObject.SetActive(false);
                                            }
                                            if (t.name == "UnCrystal5")
                                            {
                                                t.gameObject.SetActive(true);
                                            }
                                            if (GameLoop.myAvailableMana < 4)
                                            {
                                                if (t.name == "Crystal4")
                                                {
                                                    t.gameObject.SetActive(false);
                                                }
                                                if (t.name == "UnCrystal4")
                                                {
                                                    t.gameObject.SetActive(true);
                                                }
                                                if (GameLoop.myAvailableMana < 3)
                                                {
                                                    if (t.name == "Crystal3")
                                                    {
                                                        t.gameObject.SetActive(false);
                                                    }
                                                    if (t.name == "UnCrystal3")
                                                    {
                                                        t.gameObject.SetActive(true);
                                                    }
                                                    if (GameLoop.myAvailableMana < 2)
                                                    {
                                                        if (t.name == "Crystal2")
                                                        {
                                                            t.gameObject.SetActive(false);
                                                        }
                                                        if (t.name == "UnCrystal2")
                                                        {
                                                            t.gameObject.SetActive(true);
                                                        }
                                                        if (GameLoop.myAvailableMana < 1)
                                                        {
                                                            if (t.name == "Crystal1")
                                                            {
                                                                t.gameObject.SetActive(false);
                                                            }
                                                            if (t.name == "UnCrystal1")
                                                            {
                                                                t.gameObject.SetActive(true);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    } else if (GameLoop.myMana == 9)
                    {
                        if (GameLoop.myAvailableMana < 9)
                        {
                            if (t.name == "Crystal9")
                            {
                                t.gameObject.SetActive(false);
                            }
                            if (t.name == "UnCrystal9")
                            {
                                t.gameObject.SetActive(true);
                            }
                            if (GameLoop.myAvailableMana < 8)
                            {
                                if (t.name == "Crystal8")
                                {
                                    t.gameObject.SetActive(false);
                                }
                                if (t.name == "UnCrystal8")
                                {
                                    t.gameObject.SetActive(true);
                                }
                                if (GameLoop.myAvailableMana < 7)
                                {
                                    if (t.name == "Crystal7")
                                    {
                                        t.gameObject.SetActive(false);
                                    }
                                    if (t.name == "UnCrystal7")
                                    {
                                        t.gameObject.SetActive(true);
                                    }
                                    if (GameLoop.myAvailableMana < 6)
                                    {
                                        if (t.name == "Crystal6")
                                        {
                                            t.gameObject.SetActive(false);
                                        }
                                        if (t.name == "UnCrystal6")
                                        {
                                            t.gameObject.SetActive(true);
                                        }
                                        if (GameLoop.myAvailableMana < 5)
                                        {
                                            if (t.name == "Crystal5")
                                            {
                                                t.gameObject.SetActive(false);
                                            }
                                            if (t.name == "UnCrystal5")
                                            {
                                                t.gameObject.SetActive(true);
                                            }
                                            if (GameLoop.myAvailableMana < 4)
                                            {
                                                if (t.name == "Crystal4")
                                                {
                                                    t.gameObject.SetActive(false);
                                                }
                                                if (t.name == "UnCrystal4")
                                                {
                                                    t.gameObject.SetActive(true);
                                                }
                                                if (GameLoop.myAvailableMana < 3)
                                                {
                                                    if (t.name == "Crystal3")
                                                    {
                                                        t.gameObject.SetActive(false);
                                                    }
                                                    if (t.name == "UnCrystal3")
                                                    {
                                                        t.gameObject.SetActive(true);
                                                    }
                                                    if (GameLoop.myAvailableMana < 2)
                                                    {
                                                        if (t.name == "Crystal2")
                                                        {
                                                            t.gameObject.SetActive(false);
                                                        }
                                                        if (t.name == "UnCrystal2")
                                                        {
                                                            t.gameObject.SetActive(true);
                                                        }
                                                        if (GameLoop.myAvailableMana < 1)
                                                        {
                                                            if (t.name == "Crystal1")
                                                            {
                                                                t.gameObject.SetActive(false);
                                                            }
                                                            if (t.name == "UnCrystal1")
                                                            {
                                                                t.gameObject.SetActive(true);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    } else if (GameLoop.myMana == 8)
                    {
                        if (GameLoop.myAvailableMana < 8)
                        {
                            if (t.name == "Crystal8")
                            {
                                t.gameObject.SetActive(false);
                            }
                            if (t.name == "UnCrystal8")
                            {
                                t.gameObject.SetActive(true);
                            }
                            if (GameLoop.myAvailableMana < 7)
                            {
                                if (t.name == "Crystal7")
                                {
                                    t.gameObject.SetActive(false);
                                }
                                if (t.name == "UnCrystal7")
                                {
                                    t.gameObject.SetActive(true);
                                }
                                if (GameLoop.myAvailableMana < 6)
                                {
                                    if (t.name == "Crystal6")
                                    {
                                        t.gameObject.SetActive(false);
                                    }
                                    if (t.name == "UnCrystal6")
                                    {
                                        t.gameObject.SetActive(true);
                                    }
                                    if (GameLoop.myAvailableMana < 5)
                                    {
                                        if (t.name == "Crystal5")
                                        {
                                            t.gameObject.SetActive(false);
                                        }
                                        if (t.name == "UnCrystal5")
                                        {
                                            t.gameObject.SetActive(true);
                                        }
                                        if (GameLoop.myAvailableMana < 4)
                                        {
                                            if (t.name == "Crystal4")
                                            {
                                                t.gameObject.SetActive(false);
                                            }
                                            if (t.name == "UnCrystal4")
                                            {
                                                t.gameObject.SetActive(true);
                                            }
                                            if (GameLoop.myAvailableMana < 3)
                                            {
                                                if (t.name == "Crystal3")
                                                {
                                                    t.gameObject.SetActive(false);
                                                }
                                                if (t.name == "UnCrystal3")
                                                {
                                                    t.gameObject.SetActive(true);
                                                }
                                                if (GameLoop.myAvailableMana < 2)
                                                {
                                                    if (t.name == "Crystal2")
                                                    {
                                                        t.gameObject.SetActive(false);
                                                    }
                                                    if (t.name == "UnCrystal2")
                                                    {
                                                        t.gameObject.SetActive(true);
                                                    }
                                                    if (GameLoop.myAvailableMana < 1)
                                                    {
                                                        if (t.name == "Crystal1")
                                                        {
                                                            t.gameObject.SetActive(false);
                                                        }
                                                        if (t.name == "UnCrystal1")
                                                        {
                                                            t.gameObject.SetActive(true);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    } else if (GameLoop.myMana == 7)
                    {
                        if (GameLoop.myAvailableMana < 7)
                        {
                            if (t.name == "Crystal7")
                            {
                                t.gameObject.SetActive(false);
                            }
                            if (t.name == "UnCrystal7")
                            {
                                t.gameObject.SetActive(true);
                            }
                            if (GameLoop.myAvailableMana < 6)
                            {
                                if (t.name == "Crystal6")
                                {
                                    t.gameObject.SetActive(false);
                                }
                                if (t.name == "UnCrystal6")
                                {
                                    t.gameObject.SetActive(true);
                                }
                                if (GameLoop.myAvailableMana < 5)
                                {
                                    if (t.name == "Crystal5")
                                    {
                                        t.gameObject.SetActive(false);
                                    }
                                    if (t.name == "UnCrystal5")
                                    {
                                        t.gameObject.SetActive(true);
                                    }
                                    if (GameLoop.myAvailableMana < 4)
                                    {
                                        if (t.name == "Crystal4")
                                        {
                                            t.gameObject.SetActive(false);
                                        }
                                        if (t.name == "UnCrystal4")
                                        {
                                            t.gameObject.SetActive(true);
                                        }
                                        if (GameLoop.myAvailableMana < 3)
                                        {
                                            if (t.name == "Crystal3")
                                            {
                                                t.gameObject.SetActive(false);
                                            }
                                            if (t.name == "UnCrystal3")
                                            {
                                                t.gameObject.SetActive(true);
                                            }
                                            if (GameLoop.myAvailableMana < 2)
                                            {
                                                if (t.name == "Crystal2")
                                                {
                                                    t.gameObject.SetActive(false);
                                                }
                                                if (t.name == "UnCrystal2")
                                                {
                                                    t.gameObject.SetActive(true);
                                                }
                                                if (GameLoop.myAvailableMana < 1)
                                                {
                                                    if (t.name == "Crystal1")
                                                    {
                                                        t.gameObject.SetActive(false);
                                                    }
                                                    if (t.name == "UnCrystal1")
                                                    {
                                                        t.gameObject.SetActive(true);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    } else if (GameLoop.myMana == 6)
                    {
                        if (GameLoop.myAvailableMana < 6)
                        {
                            if (t.name == "Crystal6")
                            {
                                t.gameObject.SetActive(false);
                            }
                            if (t.name == "UnCrystal6")
                            {
                                t.gameObject.SetActive(true);
                            }
                            if (GameLoop.myAvailableMana < 5)
                            {
                                if (t.name == "Crystal5")
                                {
                                    t.gameObject.SetActive(false);
                                }
                                if (t.name == "UnCrystal5")
                                {
                                    t.gameObject.SetActive(true);
                                }
                                if (GameLoop.myAvailableMana < 4)
                                {
                                    if (t.name == "Crystal4")
                                    {
                                        t.gameObject.SetActive(false);
                                    }
                                    if (t.name == "UnCrystal4")
                                    {
                                        t.gameObject.SetActive(true);
                                    }
                                    if (GameLoop.myAvailableMana < 3)
                                    {
                                        if (t.name == "Crystal3")
                                        {
                                            t.gameObject.SetActive(false);
                                        }
                                        if (t.name == "UnCrystal3")
                                        {
                                            t.gameObject.SetActive(true);
                                        }
                                        if (GameLoop.myAvailableMana < 2)
                                        {
                                            if (t.name == "Crystal2")
                                            {
                                                t.gameObject.SetActive(false);
                                            }
                                            if (t.name == "UnCrystal2")
                                            {
                                                t.gameObject.SetActive(true);
                                            }
                                            if (GameLoop.myAvailableMana < 1)
                                            {
                                                if (t.name == "Crystal1")
                                                {
                                                    t.gameObject.SetActive(false);
                                                }
                                                if (t.name == "UnCrystal1")
                                                {
                                                    t.gameObject.SetActive(true);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    } else if (GameLoop.myMana == 5)
                    {
                        if (GameLoop.myAvailableMana < 5)
                        {
                            if (t.name == "Crystal5")
                            {
                                t.gameObject.SetActive(false);
                            }
                            if (t.name == "UnCrystal5")
                            {
                                t.gameObject.SetActive(true);
                            }
                            if (GameLoop.myAvailableMana < 4)
                            {
                                if (t.name == "Crystal4")
                                {
                                    t.gameObject.SetActive(false);
                                }
                                if (t.name == "UnCrystal4")
                                {
                                    t.gameObject.SetActive(true);
                                }
                                if (GameLoop.myAvailableMana < 3)
                                {
                                    if (t.name == "Crystal3")
                                    {
                                        t.gameObject.SetActive(false);
                                    }
                                    if (t.name == "UnCrystal3")
                                    {
                                        t.gameObject.SetActive(true);
                                    }
                                    if (GameLoop.myAvailableMana < 2)
                                    {
                                        if (t.name == "Crystal2")
                                        {
                                            t.gameObject.SetActive(false);
                                        }
                                        if (t.name == "UnCrystal2")
                                        {
                                            t.gameObject.SetActive(true);
                                        }
                                        if (GameLoop.myAvailableMana < 1)
                                        {
                                            if (t.name == "Crystal1")
                                            {
                                                t.gameObject.SetActive(false);
                                            }
                                            if (t.name == "UnCrystal1")
                                            {
                                                t.gameObject.SetActive(true);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    } else if (GameLoop.myMana == 4)
                    {
                        if (GameLoop.myAvailableMana < 4)
                        {
                            if (t.name == "Crystal4")
                            {
                                t.gameObject.SetActive(false);
                            }
                            if (t.name == "UnCrystal4")
                            {
                                t.gameObject.SetActive(true);
                            }
                            if (GameLoop.myAvailableMana < 3)
                            {
                                if (t.name == "Crystal3")
                                {
                                    t.gameObject.SetActive(false);
                                }
                                if (t.name == "UnCrystal3")
                                {
                                    t.gameObject.SetActive(true);
                                }
                                if (GameLoop.myAvailableMana < 2)
                                {
                                    if (t.name == "Crystal2")
                                    {
                                        t.gameObject.SetActive(false);
                                    }
                                    if (t.name == "UnCrystal2")
                                    {
                                        t.gameObject.SetActive(true);
                                    }
                                    if (GameLoop.myAvailableMana < 1)
                                    {
                                        if (t.name == "Crystal1")
                                        {
                                            t.gameObject.SetActive(false);
                                        }
                                        if (t.name == "UnCrystal1")
                                        {
                                            t.gameObject.SetActive(true);
                                        }
                                    }
                                }
                            }
                        }
                    } else if (GameLoop.myMana == 3)
                    {
                        if (GameLoop.myAvailableMana < 3)
                        {
                            if (t.name == "Crystal3")
                            {
                                t.gameObject.SetActive(false);
                            }
                            if (t.name == "UnCrystal3")
                            {
                                t.gameObject.SetActive(true);
                            }
                            if (GameLoop.myAvailableMana < 2)
                            {
                                if (t.name == "Crystal2")
                                {
                                    t.gameObject.SetActive(false);
                                }
                                if (t.name == "UnCrystal2")
                                {
                                    t.gameObject.SetActive(true);
                                }
                                if (GameLoop.myAvailableMana < 1)
                                {
                                    if (t.name == "Crystal1")
                                    {
                                        t.gameObject.SetActive(false);
                                    }
                                    if (t.name == "UnCrystal1")
                                    {
                                        t.gameObject.SetActive(true);
                                    }
                                }
                            }
                        }
                    } else if (GameLoop.myMana == 2)
                    {
                        if (GameLoop.myAvailableMana < 2)
                        {
                            if (t.name == "Crystal2")
                            {
                                t.gameObject.SetActive(false);
                            }
                            if (t.name == "UnCrystal2")
                            {
                                t.gameObject.SetActive(true);
                            }
                            if (GameLoop.myAvailableMana < 1)
                            {
                                if (t.name == "Crystal1")
                                {
                                    t.gameObject.SetActive(false);
                                }
                                if (t.name == "UnCrystal1")
                                {
                                    t.gameObject.SetActive(true);
                                }
                            }
                        }
                    } else if (GameLoop.myMana == 1)
                    {
                        if (GameLoop.myAvailableMana < 1)
                        {
                            if (t.name == "Crystal1")
                            {
                                t.gameObject.SetActive(false);
                            }
                            if (t.name == "UnCrystal1")
                            {
                                t.gameObject.SetActive(true);
                            }
                        }
                    }
                }
            }
        }
    }
}

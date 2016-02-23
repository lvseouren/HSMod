using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Need to be change according to game loop

public class YourTurn : MonoBehaviour
{
    private int state;

    void OnEnable()
    {
        EventManager.StartListening(EventManager.ON_TURN_START, StartYourTurn);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.ON_TURN_START, StartYourTurn);
    }

    void StartYourTurn()
    {
        if (state == 0)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            state = 1;
            StartCoroutine(Delay());
        } else
        {
            state = 0;
        }
    }

    IEnumerator Delay()
    {

        yield return new WaitForSeconds(2f);

        this.transform.GetChild(0).gameObject.SetActive(false);
    }
}

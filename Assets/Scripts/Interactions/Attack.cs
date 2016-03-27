using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    public GameObject seta;
    public GameObject Ready;
    public LineRenderer linha;

    public bool canTargetAllies;
    public bool canTargetHero;
    public bool canTargetOponents;
    public bool isSleeping;
    public bool notYourTurn;        //Potential check for turns, not allowing player 1 to attack on enemy turns.
    public bool turnCheckPlayer1;   //Check on if it is Player1's turn to play cards.

    public int AttackCounter;       //Counting how many Attacks are left for a minion
    public int DivineCounter;       //Counting the Divine shields a minion has left

    private Vector3 screenPoint;
    private Vector3 curScreenPoint;
    private Vector3 curPosition;
    private Vector3 aux;

    private void Awake()
    {
        seta = GameObject.FindGameObjectWithTag("Seta");

        if (GetComponent<Hero>())
        {
            return;
        }

        if (GetComponent<BasicMinionSuperClass>().hasDivinieShield == true)
        {
            DivineCounter = 1;
        }
        else if (GetComponent<BasicMinionSuperClass>().hasMegaDivineShield == true)
        {
            DivineCounter = 4;
        }
        else
        {
            DivineCounter = 0;
        }

        if (GetComponent<BasicMinionSuperClass>().hasCharge)
        {
            isSleeping = false;

            if (GetComponent<BasicMinionSuperClass>().hasWindfury)
            {
                AttackCounter = 2;
            }

            if (GetComponent<BasicMinionSuperClass>().hasMegaWindfury)
            {
                AttackCounter = 4;
            }
        }
        else
        {
            isSleeping = true;
            AttackCounter = 0;
        }
    }

    private void Start()
    {
        linha = seta.GetComponent<LineRenderer>();
    }

    private void onNewTurn()
    {
        if (turnCheckPlayer1 == true)
        {
            turnCheckPlayer1 = false;
            notYourTurn = false;

            if (AttackCounter > 0)
            {
                //Green glow gets added here
            }

            if (GetComponent<BasicMinionSuperClass>().hasMegaWindfury)
            {
                AttackCounter = 4;
            }
            else
            {
                if (GetComponent<BasicMinionSuperClass>().hasWindfury)
                {
                    AttackCounter = 2;
                }
                else
                {
                    AttackCounter = 1;
                }
            }
        }
        else
        {
            turnCheckPlayer1 = true;
            notYourTurn = true;
            isSleeping = false;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Failing");

        if (isSleeping)
            return;

        if (notYourTurn)
            return;

        if (AttackCounter <= 0)
            return;

        Debug.Log("Downing");

        linha.enabled = true;

        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

        seta.transform.position = curPosition;

        linha.SetPosition(0, new Vector3(this.transform.position.x, 10f, this.transform.position.z));
        linha.SetPosition(1, curPosition);
    }

    private void OnMouseDrag()
    {
        // if (alreadyAttackedThisTurn)
        //	return;

        if (isSleeping)
            return;

        if (notYourTurn)
            return;

        if (AttackCounter <= 0)
            return;

        Debug.Log("Dragging");

        curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

        linha.SetPosition(1, curPosition);
    }

    private void OnMouseUp()
    {
        if (isSleeping)
            return;

        if (notYourTurn)
            return;

        if (AttackCounter <= 0)
            return;

        Debug.Log("Upping");

        linha.enabled = false;

        curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

        RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.down, 1000f);
        bool played = false;

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.name == "TheirHero")
            {
                StartCoroutine(attack(hits[i].transform.position, hits[i].transform.gameObject));
            }
            else if (hits[i].transform.parent != null && hits[i].transform.parent.name == "TheirSideOfBf")
            {
                StartCoroutine(attack(hits[i].transform.position, hits[i].transform.gameObject));
            }
        }
    }

    private IEnumerator attack(Vector3 finalPosition, GameObject hit)
    {
        finalPosition.z -= GetComponent<BoxCollider>().bounds.size.z / 2;
        Vector3 initialPosition = this.transform.position;

        Vector2 firstPoint = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 secondPoint = new Vector2(finalPosition.x, finalPosition.z);

        float Distance = Vector2.Distance(firstPoint, secondPoint);

        while (Vector3.Distance(this.transform.position, finalPosition) > 0.01f)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, finalPosition, Distance * Time.deltaTime * 10f);
            yield return null;
        }

        if (hit.GetComponent<Hero>() != null)
        {
            hit.GetComponent<Hero>().currentHp -= GetComponent<BasicMinionSuperClass>().currentAttack;
            hit.GetComponent<Hero>().attack -= GetComponent<BasicMinionSuperClass>().currentHealth;
        }

        if (hit.GetComponent<BasicMinionSuperClass>() != null)
        {
            if (DivineCounter > 0)
            {
                DivineCounter--;
                GetComponent<BasicMinionSuperClass>().currentHealth -= hit.GetComponent<BasicMinionSuperClass>().currentAttack;
            }

            else
            {
                hit.GetComponent<BasicMinionSuperClass>().currentHealth -= GetComponent<BasicMinionSuperClass>().currentAttack;
                hit.GetComponent<BasicMinionSuperClass>().currentAttack -= GetComponent<BasicMinionSuperClass>().currentHealth;
            }

            AttackCounter++;

            if (AttackCounter == 0)
            {
                // Green glow gets removed here
            }

            while (Vector3.Distance(this.transform.position, initialPosition) > 0.01f)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, initialPosition, Distance * Time.deltaTime * 10);
                yield return null;
            }
        }
    }
}
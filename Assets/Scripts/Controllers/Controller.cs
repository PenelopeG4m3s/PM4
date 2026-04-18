using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    [HideInInspector] public Pawn pawn;
    public int maxLives = 3;
    public int currentLives;
    public float maxRespawnTimer;
    public float currentRespawnTimer;
    public int score = 0;

    public virtual void Start() {
        currentLives = maxLives;
        currentRespawnTimer = 0;
    }

    public virtual void Update()
    {
        if (pawn != null)
        {
            MakeDecisions();
        }
        else
        {
            if (currentLives > 0)
            {
                RespawnTimer();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public abstract void MakeDecisions();
    public void Possess(Pawn pawnToPossess)
    {
        pawnToPossess.controller = this;
        this.pawn = pawnToPossess;
    }

    public void Unpossess ()
    {
        pawn.controller = null;
    }

    public virtual void RespawnTimer() 
    {
        // If the respawn timer isnt 0
        if ( currentRespawnTimer > 0 )
        {
            currentRespawnTimer -= Time.deltaTime;
            currentRespawnTimer = Mathf.Clamp( currentRespawnTimer, 0.0f, maxRespawnTimer );
        }
        else // respawn timer is 0 or less
        {
            Respawn();
            currentRespawnTimer = 0.0f;
        }
    }

    public virtual void Respawn() 
    {
        Debug.Log("RESPAWNED");
    }

    public virtual void AddScore( int amount )
    {
        score += amount;
        score = Mathf.Clamp(score, 0, 100000);
    }
}

using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    [HideInInspector] public Pawn pawn;

    public virtual void Start() {}

    public virtual void Update()
    {
        MakeDecisions();
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
}

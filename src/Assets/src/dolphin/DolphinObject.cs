using UnityEngine;
using System.Collections;

public class DolphinObject : MonoBehaviour {

    public int laneId;
    public Animator anim;

    public float distance;
    public bool isPooled;
    private bool isActivated;
    public bool isCorrectWord;
    public string word;
    private int offsetToBeOff = 10;    

    
    public void Init(DolphinObjectSettings settings, int laneId)
    {
        isPooled = false;
        isActivated = false;
        this.laneId = laneId;
        OnInit(settings, laneId);
        this.word = settings.word;
    }
    void Update()
    {
        if (isPooled) return;

        Vector3 pos = transform.localPosition;

        if (pos.x + offsetToBeOff  < DolphinGame.Instance.dolphinGameManager.distance)
        {
            Pool();
        }
        else if (pos.x < DolphinGame.Instance.dolphinGameManager.distance + 30)
        {
            //isActivated = solo cuando entra dentro del area activa
            if (!isActivated)
            {
                isActivated = true;
                Enemy_Activate();
            }
            Enemy_Update(pos);
        }
    }
    public void Pool()
    {
        isPooled = true;
        Enemy_Pooled();
        Destroy(gameObject);
    }
    public void Crashed()
    {
        OnCrashed();        
    }
    public void Explote()
    {
        OnExplote();
    }
    public virtual void OnHeroDie() { }
    public virtual void OnSecondaryCollision(Collider2D other) { }
    public virtual void Enemy_Pooled() { }
    public virtual void OnInit(DolphinObjectSettings settings, int laneId) { }
    public virtual void Enemy_Activate() { }
    public virtual void Enemy_Update(Vector3 pos)  {  }
    public virtual void OnCrashed() { }
    public virtual void OnExplote() { }
}

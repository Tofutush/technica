using UnityEngine;

public class Potions : MonoBehaviour
{
    public PlayerStats player;

    public void OnCollisionEnter2D(Collision2D collision) 
    {
        //stats
        int value = Random.Range(1, 11);
        int stat = Random.Range(1,4);
        int buff = 1;
        //Random.Range(1,3);
            Debug.Log(buff);
            Debug.Log(stat);
            Debug.Log(value);
            Debug.Log("finish");
        if (buff == 1) 
        {
            if (stat == 1)
            {
                player.health.Buff(value);
            }
            else if (stat == 2)
            {
                player.attack.Buff(value);
            }
            else
            {
                player.defense.Buff(value);
            }
        }
        /*else
        {
            if (stat == 1)
            {
                player.health.Debuff(value);
            }
            else if (stat == 2)
            {
                player.attack.Debuff(value);
            }
            else
            {
                player.defense.Debuff(value);
            }
        }*/
        
        
        
        //Dissapperance
        gameObject.SetActive(false);
        Invoke("Apperance", 5f);

        //color matching maybe?
    }

    void Apperance () 
    {
        gameObject.SetActive(true);
    }
}

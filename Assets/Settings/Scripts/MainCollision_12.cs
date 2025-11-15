using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCollision : MonoBehaviour
{
    public GameObject target1; 
    public GameObject target2; 
    public GameObject target3; 
    public int sceneIndex;
    //public HeartSystem heart;
    
    int count = 0;
    public void OnCollisionEnter2D(Collision2D collision) 
    {
        
        MinigameManagerTrue.SetStateToFailure();
        count++;
        if (count == 1)
        {
            target1.SetActive(false);
        }
        else if (count == 2)
        {
            target2.SetActive(false);
        }
        else if (count == 3)
        {
            target3.SetActive(false);
            SceneManager.LoadScene(sceneIndex);
        }
    }
}

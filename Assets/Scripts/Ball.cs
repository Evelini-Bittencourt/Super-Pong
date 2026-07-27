using UnityEngine;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{
   public float speed=5;
   private Vector2 direction = Vector2.one; //o mesmo que: private Vector2 direction = new Vector2(1,1);

   private void Update()
    {
      Move();
      BounceTopAndBottom();  
    }
    private void Move()
    {
        Vector3 movement= direction*speed*Time.deltaTime;
        transform.Translate(movement); //move o objeto
    }
    private void BounceTopAndBottom()
    {
        float screenTop= Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height,0)).y;
        float screenBottom= Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).y;

        Vector3 position = transform.position;// pega a posição atual do objeto
        if(direction.y>0 && position.y >= (screenTop - 0.25f)) //se estiver indo para cima e já estiver no máximo
        {
            direction.y=-1;//muda o movimento para cima
        }
        if(direction.y<0 && position.y <= (screenBottom + 0.25f))
        {
            direction.y=1;
        }

    }
}

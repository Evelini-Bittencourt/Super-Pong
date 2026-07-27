using UnityEngine;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{
   public float speed=5;
   private Vector2 direction = Vector2.one; //o mesmo que: private Vector2 direction = new Vector2(1,1);
   //forma de fazer referência a outros elementos da cena. Defini-se uma variável com o tipo Transform (componente que guarda as as localizaçãoes dos objetos) e depois arrasta o objeto até o epaço que vai aparecer na interface da unity, para fazer a ligação entre os objetos 
   public Transform paddleLeft;
   public Transform paddleRight;

   private void Update()
    {
      Move();
      BounceTopAndBottom();  
      BounceWithPaddles();
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
    private void BounceWithPaddles()
    {
        float paddleWidth= 0.5f;
        float paddleHeight= 2f;
        float ballSize= 0.5f;

        if (direction.x > 0)//a bola está se movendo para a direita 
        {
            if((transform.position.x + ballSize/2f) > (paddleRight.position.x-paddleWidth/2f)
            && (transform.position.x + ballSize/2f) < (paddleRight.position.x + paddleWidth/2f)
            && transform.position.y > (paddleRight.position.y - paddleHeight/2f)
            && transform.position.y < (paddleRight.position.y + paddleHeight/2f)
            )
            {
                direction.x=-1;
            }
        }else if (direction.x < 0)
        {
            if((transform.position.x - ballSize/2f) > (paddleLeft.position.x - paddleWidth/2f)
            && (transform.position.x - ballSize/2f) < (paddleLeft.position.x + paddleWidth/2f)
            && transform.position.y > (paddleLeft.position.y - paddleHeight/2f)
            && transform.position.y < (paddleLeft.position.y + paddleHeight/2f)
            )
            {
                direction.x=1;
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{
   public float speed=5;
   private Vector2 direction = Vector2.one; //o mesmo que: private Vector2 direction = new Vector2(1,1);
   //forma de fazer referência a outros elementos da cena. Defini-se uma variável com o tipo Transform (componente que guarda as as localizaçãoes dos objetos) e depois arrasta o objeto até o epaço que vai aparecer na interface da unity, para fazer a ligação entre os objetos 
   public Transform paddleLeft;
   public Transform paddleRight;
   public SpriteRenderer ballSprite;
   public SpriteRenderer paddleLeftSprite;
   public SpriteRenderer paddleRightSprite;
   public Color primaryColor1;
   public Color primaryColor2;
   public Color primaryColor3;
   public Color backgroundColor1;
   public Color backgroundColor2;
   public Color backgroundColor3;
   private int colorIndex=1;

   private bool isMoving= false;

   private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            isMoving=true;
        }
        Move();
        BounceTopAndBottom();  
        BounceWithPaddles();
    }
    private void Move()
    {
        if (isMoving)
        {
            Vector3 movement= direction*speed*Time.deltaTime;
            transform.Translate(movement); //move o objeto
        }
    }
    private void BounceTopAndBottom()
    {
        float screenTop= Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height,0)).y;
        float screenBottom= Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).y;

        Vector3 position = transform.position;// pega a posição atual do objeto
        if(direction.y>0 && position.y >= (screenTop - 0.25f)) //se estiver indo para cima e já estiver no máximo
        {
            direction.y=-1;//muda o movimento para cima
           SwapColors();
        }
        if(direction.y<0 && position.y <= (screenBottom + 0.25f))
        {
            direction.y=1;
            SwapColors();
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
                SwapColors();
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
                SwapColors();
            }
        }
    }
    public void ResetPosition()
    {
        transform.position= Vector3.zero;
        direction= -direction;
        isMoving= false;
        Invoke("StartMoving",2);//executa o método nomeado depois do tempo definido no segundo parâmetro
    }
    private void StartMoving()
    {
        isMoving=true;
    }
    private void SwapColors()
    {
        Color primarycolor= Color.white;
        Color backgroundColor=Color.white;

        if (colorIndex == 1)
        {
            colorIndex=2;
            primarycolor= primaryColor2;
            backgroundColor=backgroundColor2;
        }else if(colorIndex==2)
        {
            colorIndex=3;
            primarycolor= primaryColor3;
            backgroundColor=backgroundColor3;
        }else if(colorIndex==3)
        {
            colorIndex=1;
            primarycolor= primaryColor1;
            backgroundColor=backgroundColor1;
        }
        ballSprite.color= primarycolor;
        paddleLeftSprite.color=primarycolor;
        paddleRightSprite.color=primarycolor;
        Camera.main.backgroundColor=backgroundColor;
    }
}

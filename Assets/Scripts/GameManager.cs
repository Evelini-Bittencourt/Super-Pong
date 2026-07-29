using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int scorePlayer1=0;
    public int scorePlayer2=0;
    public Ball ball;
    public Text score;
    public int pointsToIncreaseSpeed=3;
    public float speedIncrement= 0.3f;
    public float maxScore= 30;

    private void Update()
    {
        float screenLeft= Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).x;
        float screenRight= Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,0)).x;

        if(ball.transform.position.x + 0.25f < screenLeft)
        {
            AddScore(2);
            ball.ResetPosition();
        }else if( ball.transform.position.x - 0.25f > screenRight)
        {
            AddScore(1);
            ball.ResetPosition();
        }

        RestartScene();
    }
    public void AddScore(int player)
    {
        if(player == 1)
        {
            scorePlayer1++;
        }else if(player == 2)
        {
            scorePlayer2++;
        }

        if((scorePlayer1 + scorePlayer2) % pointsToIncreaseSpeed ==0)
        {
            ball.speed += speedIncrement;
        }
        score.text= $"{scorePlayer1} X {scorePlayer2}";
    }
    private void RestartScene()
    {
        if(scorePlayer1 >= maxScore || scorePlayer2 >= maxScore)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//captura o index da cena atual para passar para o método que vai recarregar a cena
        }
    }
    

}

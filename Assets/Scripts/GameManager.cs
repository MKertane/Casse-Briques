using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ball ball {get; private set;}
    public Paddle paddle {get; private set;}

    public Brick[] bricks {get; private set;}
    public int level = 1;
    public int score = 0;
    public int lives = 3;

    // Fonction fournie par Unity, automatiquement appelée lors de la 1ère initialisation
    private void Awake() {
        // Fonctione fournie par Unity qui permet de conserver un gameObject sur toutes les scènes
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded;      
    } 
   
    // Fonction fournie par Unity, automatiquement appelée lors de la 1ère initialisation
    private void Start() {
        NewGame(); 
    }
   
    // Fonction qui permet de réinitialiser les valeurs de score et de vies lorsque l'on commence une nouvelle partie
    private void NewGame() {
        this.score = 0;
        this.lives = 3;

        LoadLevel(1);
    }

    // Fonction de chargement d'un niveau
    private void LoadLevel(int level) {
        this.level = level;

        // Chargement de la scène "Level1"
        SceneManager.LoadScene("Level" + level);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode) {

        this.ball = FindObjectOfType<Ball>();
        this.paddle = FindObjectOfType<Paddle>();
        this.bricks = FindObjectsOfType<Brick>();
    }

    // Fontion de gestion des collisions de la balle
    public void Hit(Brick brick) {
       
       // Le joueur gagnera 100 points s'il touche une brique
        this.score += brick.points;

        /*if (LevelCleared()) {
            LoadLevel(this.level + 1);
        }*/
    }

    // Fonction de gestion des vies du joueur
    public void Miss() {
        this.lives--;     

        if (this.lives > 0) {
            ResetLevel();
        } else {
            GameOver();
        }   
    }

    private void ResetLevel() {

        this.ball.ResetBall();
        this.paddle.ResetPaddle();

        // Resetting the bricks is optional
        // for (int i = 0; i < bricks.Length; i++) {
        //     bricks[i].ResetBrick();
        // }
    }

    private void GameOver() {
        // SceneManager.LoadScene("GameOver");
        NewGame();
    }

   /* private bool LevelCleared() {
        
        for (int i = 0; i < this.bricks.Length; i++) {
            if (this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].indestructible) {
                return false;
            }
        }

        return true;
    } */
}

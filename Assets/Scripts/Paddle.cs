using UnityEngine;

// Création et configuration de la classe Paddle
public class Paddle : MonoBehaviour
{
    // Création de l'attribut avec un get public et un set privé, afin que les valeurs obtenues dans tout le programme restent sur cette classe
    public new Rigidbody2D rigidbody { get; private set; }
    
    // Création du vecteur de direction du paddle
    public Vector2 direction { get; private set; }
    // Configuration de la vitesse du paddle
    public float speed = 30f;
    public float maxBounceAngle = 75f;

    private void Awake() {

        // Attribution du composant Rigidbody2D d'Unity pour l'attribut rigidbody
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    public void ResetPaddle() {
        // Réinitialisation de la position du paddle
        this.transform.position = new Vector2(0f, this.transform.position.y);
        this.rigidbody.velocity = Vector2.zero;
    }

    // Fonction fournie par Unity automatiquement appelée à chaque image par seconde du jeu
    private void Update() {

        // Création et configuration des raccourcis clavier de déplacement du paddle
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow)) {
            direction = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            direction = Vector2.right; 
        }
        // Le paddle ne bougera pas si aucun raccourci clavier de déplacement est utilisé
        else {
            direction = Vector2.zero;
        }
    }

    // Fonction fournie par Unity automatiquement appelée à un intervalle de temps défini
    private void FixedUpdate() {

        // Configuration de la vitesse de la balle 
        if (direction != Vector2.zero) {
            rigidbody.AddForce(direction * speed);
        }    
    }

    // Création de la fonction de gestion des collisions entre le paddle et la balle
    private void OnCollisionEnter2D(Collision2D collision) {

        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null) {
             // La position du paddle est réinitialisée s'il n'y a plus de balle à l'écran
            Vector3 paddlePosition = transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float offset = paddlePosition.x - contactPoint.x;
            float width = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.velocity);

            // Angle du rebond de la balle sur le paddle
            float bounceAngle = (offset / width) * maxBounceAngle;

            // Fonction fournie par Unity donnant une valeur à une plage définie par les valeurs minimale et maximale de l'angle.
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);

            // Configuration de la vélocité de la balle, qui est le temps et la vitesse de direction
            ball.rigidbody.velocity = rotation * Vector2.up * ball.rigidbody.velocity.magnitude;
        }
    }
}

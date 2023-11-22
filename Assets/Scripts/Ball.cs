using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Création de l'attribut avec un get public et un set privé, afin que les valeurs obtenues dans tout le programme restent sur cette classe
    public new Rigidbody2D rigidbody { get; private set; }
    public float speed = 500f;

    private void Awake() {

        // Attribution du composant Rigidbody2D d'Unity pour l'attribut rigidbody
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        ResetBall();
    }

    public void ResetBall() {
        this.transform.position = Vector2.zero;
        this.rigidbody.velocity = Vector2.zero;

        // Fonction qui va en invoquer une autre dans un délai défini (ici en l'occurence 1s)
        Invoke(nameof(SetRandomTrajectory), 1f);
    }

    private void SetRandomTrajectory() {
        // Configuration de la force de déplacement de la balle
        Vector2 force = Vector2.zero;
        // Attribut qui permettra à la balle de se déplacer aléatoirement de droite à gauche
        force.x = Random.Range(-1f, 1f);
        // Attribut qui permettra à la balle de faire une chute au moment où la partie commence
        force.y = -1f;

        this.rigidbody.AddForce(force.normalized * this.speed);
    }
}

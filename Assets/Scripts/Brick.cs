using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    public SpriteRenderer spriteRenderer { get; private set; }

    // Appel de la classe des sprites dans Unity
    public Sprite[] states;

    public int points = 100;

    // Création de l'attribut indestructible que l'on pourra définir comme une brique incassable dans le jeu
    public bool indestructible;

    // Configuration des points de vies actuels d'une brique
    public int health { get; private set;}

    private void Awake() {

        // Attribution du composant SpriteRnederer d'Unity pour l'attribut spriteRenderer
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        ResetBrick();
    }

    public void ResetBrick() {
        this.gameObject.SetActive(true);

        // Si la brique n'est pas indesctructible alors celle-ci aura des pv en fonction de son état actuel
        // Par exemple si B = état 3 ; B = 3 PV
        if (!this.indestructible) {
        
            // La brique aura un nombre de PV en fonction de son état
            this.health = this.states.Length;
            this.spriteRenderer.sprite = this.states[this.health - 1];
        }
    }

    // Fonction qui va permettre de réduire le nombre de points de vie d'une brique et de changer son apparence
    private void Hit() {

        // La méthode Hit est inutile pour notre jeu si la brique touchée est indestructible
        if (this.indestructible) {
            return;
        }

        // Création de la soustraction des points de vie d'une brique
        this.health--;

        // Le gameObject 'Brique' est désactivé si ses points de vie sont <= à 0
        if (this.health <= 0) {
            this.gameObject.SetActive(false);
        }

        // La brique perd un point de vie si elle lui reste encore des PV
        else {
            this.spriteRenderer.sprite = this.states[this.health - 1];
        }

        // Lorsqu'une brique sera touchée, le GameManager aura une référence qui appelera la méthode Hit
        // Il pourra décider quoi faire quand une brique est touchée
        FindObjectOfType<GameManager>().Hit(this);

    }

    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "Ball") {
            Hit();
        }
    }
}

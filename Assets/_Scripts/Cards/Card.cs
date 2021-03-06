using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Card : MonoBehaviour {

    public CardSettings settings;
    public bool IsFlipped {
        get {
            return rend != null && front != null && rend.sprite == front;
        }
    }

    private SpriteRenderer rend;
    private Sprite back;
    private Sprite front;

    public void Initialize(CardColor color, CardSuit suit, CardName name) {
        settings = new CardSettings(color, suit, name);

        if (rend == null) {
            rend = GetComponent<SpriteRenderer>();
        }

        LoadResources();

        rend.sprite = back;
    }

    public void Flip() {
        if (IsFlipped) {
            rend.sprite = back;
        } else {
            rend.sprite = front;
        }
    }

    private void OnMouseDown() {
        if (CardManager.instance.SelectCard(this)) {
            Flip();
        }
    }

    private void LoadResources() {
        string backPath = string.Format("Cards/back_{0:s}", settings.Color.ToString());
        back = Resources.Load<Sprite>(backPath);

        string frontPath = string.Format("Cards/{0:s}/{1:d}", settings.Suit.ToString(), settings.Name);
        front = Resources.Load<Sprite>(frontPath);

        gameObject.layer = LayerMask.NameToLayer(settings.Suit.ToString());
    }
}

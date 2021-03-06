using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {

    public static CardManager instance;

    [Header("References")]
    public Transform verticalLayout;
    [Header("Prefabs")]
    public GameObject horizontalLayoutPrefab;
    public GameObject cardPrefab;

    private List<Card> selecteds;
    private List<Transform> horizontalLayouts;
    private Coroutine checkCoroutine;

    private void Awake() {
        if (instance != null) {
            Destroy(this);
            return;
        }
        instance = this;

        horizontalLayouts = new List<Transform>();
        for (int row = 0; row < GameSettings.numberOfRow; row++) {
            GameObject horizontalLayout = Instantiate(horizontalLayoutPrefab, verticalLayout);
            SpawnRow(horizontalLayout.transform, row);
            horizontalLayout.GetComponent<HorizontalLayout>().UpdateChildPositions();
            horizontalLayouts.Add(horizontalLayout.transform);
        }
        verticalLayout.GetComponent<VerticalLayout>().UpdateChildPositions();

        selecteds = new List<Card>();
    }

    private void SpawnRow (Transform horizontalLayout, int rowIndex) {
        int[] row = new int[GameSettings.cardsPerRow];
        for (int i = 1; i <= GameSettings.cardsPerRow; i++) {
            row[i - 1] = i;
        }

        for (int i = 0; i < GameSettings.cardsPerRow; i++) {
            int rndIndex = Random.Range(i, GameSettings.cardsPerRow);

            int tmp = row[i];
            row[i] = row[rndIndex];
            row[rndIndex] = tmp;
        }

        foreach (int i in row) {
            GameObject newCardGO = Instantiate(cardPrefab, horizontalLayout) as GameObject;
            Card newCard = newCardGO.GetComponent<Card>();
            newCard.Initialize(GameSettings.Color, GameSettings.GetSuit(rowIndex), (CardName)i);
        }
    }

    public bool SelectCard (Card card) {
        if (checkCoroutine != null) {
            return false;
        }

        foreach (Card other in selecteds) {
            if (card.settings.Suit == other.settings.Suit) {
                return false;
            }
        }

        selecteds.Add(card);
        if (selecteds.Count == GameSettings.numberOfRow) {
            checkCoroutine = StartCoroutine(CheckSelection());
        }

        return true;
    }

    private IEnumerator CheckSelection () {
        bool isValid = true;
        for (int i = 1; i < GameSettings.numberOfRow; i++) {
            isValid = isValid && selecteds[0].settings.Name == selecteds[i].settings.Name;
        }

        if (isValid) {
            AudioManager.instance.PlaySFX("hit");
        } else {
            AudioManager.instance.PlaySFX("miss");
        }

        yield return new WaitForSeconds(2f);

        if (isValid) {
            foreach(Card card in selecteds) {
                Destroy(card.gameObject);
            }
        } else {
            foreach(Card card in selecteds) {
                card.Flip();
            }
        }
        GameManager.instance.IncreaseTries();

        yield return null;

        int childCount = 0;
        foreach (Transform t in horizontalLayouts) {
            childCount += t.childCount;
        }
        if (childCount == 0) {
            GameManager.instance.EndGame();
        }

        selecteds.Clear();
        checkCoroutine = null;
    }
}

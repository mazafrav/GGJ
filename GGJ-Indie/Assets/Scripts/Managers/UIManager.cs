using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer background;

    public void UpdateBackground(float progress) {
        if(!background) return;

        background.color = new Color(background.color.r, background.color.g, background.color.b, progress);
    }

}

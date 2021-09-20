using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace infinityStones
{
    class InfinityStoneIcon : MonoBehaviour
    {
        GameObject imageObj;

        private void Awake()
        {
            imageObj = new GameObject();
            Image image = imageObj.AddComponent<Image>();
            image.sprite = Resources.Load<Sprite>("procedural_ui_image_default_sprite");
            imageObj.transform.parent = this.transform;
        }

        public void setColor(Color color)
        {
            imageObj.GetComponent<Image>().GetComponent<Graphic>().color = color;
        }

        public void setPos(int pos)
        {
            RectTransform rect = this.gameObject.AddComponent<RectTransform>();
            rect.localPosition = new Vector3(-220 + (87 * pos), 105, 0);
            rect.localScale = new Vector3(.75f, .75f, .75f);
            rect.transform.Rotate(new Vector3(0, 120, 45));
        }

        public void setAlpha(float a)
        {
            Graphic graphic = imageObj.GetComponent<Image>().GetComponent<Graphic>();
            Color color = graphic.color;
            graphic.color = new Color(color.r, color.g, color.b, a);
        }
    }
}

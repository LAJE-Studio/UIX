using UnityEngine;
using UnityEngine.UI;

namespace UIX {
    public class UICircle : UIPrimitive {
        public const byte MinVertices = 3;
        public const ushort DefaultTotalVertices = 45;

        [SerializeField, HideInInspector]
        private ushort totalVertices;


        public ushort TotalVertices {
            get {
                return totalVertices;
            }
            set {
                if (SetTotalVerticesSilent(value)) {
                    SetVerticesDirty();
                }
            }
        }


        public bool SetTotalVerticesSilent(ushort value) {
            if (value < MinVertices) {
                UIXConstants.UIXLogger.Log("Minimum possible vertices is " + MinVertices + ".");
                return false;
            }

            totalVertices = value;
            return true;
        }

        protected override void OnPopulateMesh(VertexHelper vh) {
            var center = new UIVertex {
                color = color,
                position = Vector2.zero,
                uv0 = Vector2.zero,
            };
            var radius = rectTransform.sizeDelta;
            vh.Clear();
            vh.AddVert(center);
            for (ushort currentVert = 1; currentVert <= totalVertices; currentVert++) {
                var vert = new UIVertex();
                var percent = (float) currentVert / totalVertices;
                var angle = percent * UIUtility.TwoPi;
                var x = Mathf.Cos(angle) * radius.x;
                var y = Mathf.Sin(angle) * radius.y;
                vert.position = new Vector3(x, y);
                vert.color = color;
                vh.AddVert(vert);
                int next;
                if (currentVert != totalVertices) {
                    next = currentVert + 1;
                } else {
                    next = 1;
                }

                vh.AddTriangle(currentVert, next, 0);
            }
        }

        protected override void Reset() {
            base.Reset();
            TotalVertices = DefaultTotalVertices;
        }
    }
}
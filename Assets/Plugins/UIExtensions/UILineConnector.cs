/// Credit Alastair Aitchison
/// Sourced from - https://bitbucket.org/UnityUIExtensions/unity-ui-extensions/issues/123/uilinerenderer-issues-with-specifying

namespace UnityEngine.UI.Extensions
{
    [AddComponentMenu("UI/Extensions/UI Line Connector")]
    [RequireComponent(typeof(UILineRenderer))]
    [ExecuteInEditMode]
    public class UILineConnector : MonoBehaviour
    {

        // The elements between which line segments should be drawn
        public bool manualBezierPoints;
        public RectTransform[] transformsFromTo;
        [SerializeField] private RectTransform[] bezierPoints;
        [SerializeField] private Vector3[] positionsBezier;
        [SerializeField] private Vector2[] previousPositions;
        [SerializeField] private RectTransform canvas;
        [SerializeField] private RectTransform rt;
        [SerializeField] private UILineRenderer lr;


        public Vector3[] canvasSpaces;
        Vector2 thisPivot;
        Vector2 canvasPivot;

        private void Awake()
        {
            //canvas = GetComponentInParent<RectTransform>().GetParentCanvas().GetComponent<RectTransform>();
            canvas = GetComponent<RectTransform>();
            rt = GetComponent<RectTransform>();
            lr = GetComponent<UILineRenderer>();
        }

        public void Setup(RectTransform from, RectTransform to) {
            transformsFromTo = new RectTransform[2] { from, to };

            // Get the pivot points
            thisPivot = rt.pivot;
            canvasPivot = canvas.pivot;


            CheckAndRecalcPositions();
        }

        public void CheckAndRecalcPositions() {
            positionsBezier = new Vector3[4];

            if (!manualBezierPoints) {
                // установить позиции для промежуточных точек безье в зависимости от позиций источника и цели
                // разница между X и Y
                float diffX = transformsFromTo[1].position.x - transformsFromTo[0].position.x;
                float diffY = transformsFromTo[1].position.y - transformsFromTo[0].position.y;
                if (Mathf.Abs(diffX) >= Mathf.Abs(diffY)) {
                    bezierPoints[0].position = new Vector3(transformsFromTo[0].position.x + diffX / 2, transformsFromTo[0].position.y, 0);
                    bezierPoints[1].position = new Vector3(transformsFromTo[0].position.x + diffX / 2, transformsFromTo[1].position.y, 0);
                }
                else {
                    bezierPoints[0].position = new Vector3(transformsFromTo[0].position.x, transformsFromTo[0].position.y + diffY / 2, 0);
                    bezierPoints[1].position = new Vector3(transformsFromTo[1].position.x, transformsFromTo[0].position.y + diffY / 2, 0);
                }
            }



            // Set up some arrays of coordinates in various reference systems
            Vector3[] worldSpaces = new Vector3[transformsFromTo.Length];
            canvasSpaces = new Vector3[transformsFromTo.Length];

            Vector3[] worldSpacesB = new Vector3[bezierPoints.Length];
            Vector3[] canvasSpacesB = new Vector3[bezierPoints.Length];

            Vector2[] points = new Vector2[positionsBezier.Length];

            // First, convert the pivot to worldspace
            for (int i = 0; i < transformsFromTo.Length; i++) {
                worldSpaces[i] = transformsFromTo[i].TransformPoint(thisPivot);
            }

            // Then, convert to canvas space
            for (int i = 0; i < transformsFromTo.Length; i++) {
                canvasSpaces[i] = canvas.InverseTransformPoint(worldSpaces[i]);
            }

            for (int i = 0; i < bezierPoints.Length; i++) {
                worldSpacesB[i] = bezierPoints[i].TransformPoint(thisPivot);
            }

            // Then, convert to canvas space
            for (int i = 0; i < transformsFromTo.Length; i++) {
                canvasSpacesB[i] = canvas.InverseTransformPoint(worldSpacesB[i]);
            }

            // Add bezier points
            positionsBezier = new Vector3[4];
            positionsBezier[0] = canvasSpaces[0]; //start
            positionsBezier[3] = canvasSpaces[1]; //end 

            //positionsBezier[1] = new Vector3(transformsFromTo[1].anchoredPosition.x, transformsFromTo[0].anchoredPosition.y, 0);
            //positionsBezier[2] = new Vector3(transformsFromTo[0].anchoredPosition.x, transformsFromTo[1].anchoredPosition.y, 0);
            //bezierPoints[0].position = new Vector3(positionsBezier[3].x, positionsBezier[0].y, 0);
            //bezierPoints[1].position = new Vector3(positionsBezier[0].x, positionsBezier[3].y, 0);
            positionsBezier[1] = canvasSpacesB[0];
            positionsBezier[2] = canvasSpacesB[1];



            // Calculate delta from the canvas pivot point
            for (int i = 0; i < positionsBezier.Length; i++) {
                points[i] = new Vector2(positionsBezier[i].x, positionsBezier[i].y);
            }

            // And assign the converted points to the line renderer
            lr.Points = points;
            lr.RelativeSize = false;
            lr.drivenExternally = true;

            previousPositions = new Vector2[transformsFromTo.Length];
            for (int i = 0; i < transformsFromTo.Length; i++) {
                previousPositions[i] = transformsFromTo[i].anchoredPosition;
            }
        }

        public void SetLineColor(Color c) => lr.color = c;

        // Update is called once per frame
        //void Update()
        //{
        //    if (transformsFromTo == null || transformsFromTo.Length < 2)
        //    {
        //        return;
        //    }
        //    //Performance check to only redraw when the child transforms move 
        //    //or if this transform rotation changed
        //    if (previousPositions != null && previousPositions.Length == transformsFromTo.Length)
        //    {
        //        bool updateLine = false;
        //        for (int i = 0; i < transformsFromTo.Length; i++)
        //        {
        //            if (!updateLine && previousPositions[i] != transformsFromTo[i].anchoredPosition)
        //            {
        //                updateLine = true;
        //            }
        //        }
        //        if (!updateLine) return;
        //    }

            
        //}
    }
}
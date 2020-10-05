using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleRenderer : MonoBehaviour {
    LineRenderer circleRenderer;
    public bool useInspectorValues;
    public float radius, thickness, randomPercent;
    public int resolution;
    public Vector3 circleCentre;
    public Material material;

    public Vector3[] circlePoints;

    private void Start() {
        circleRenderer = GetComponent<LineRenderer>();
        circleRenderer.loop = true;
        circleRenderer.material = material;
        if (useInspectorValues) { RenderCircle(radius, thickness, resolution, circleCentre, randomPercent); }
    }

    public void RenderCircle(float rad, float width, int res, Vector3 centre, float randomisePercent = 0) {
        Circle circle = new Circle(rad, width, res, centre, randomisePercent);
        circlePoints = circle.DrawCircle();
        circleRenderer.positionCount = circlePoints.Length;
        circleRenderer.widthMultiplier = circle.thickness;
        circleRenderer.SetPositions(circlePoints);
    }

    struct Circle {
        float radius;
        public float thickness;
        int resolution;
        Vector3 centre;
        float randomisePercent;

        public Circle(float radius, float thickness, int resolution, Vector3 centre, float randomisePercent = 0) {
            this.radius = radius;
            this.thickness = thickness;
            this.resolution = resolution;
            this.centre = centre;
            this.randomisePercent = randomisePercent;
        }

        public Vector3[] DrawCircle() {
            float interval = 2 * Mathf.PI / resolution;
            Vector3[] points = new Vector3[(int)(2 * Mathf.PI / interval)];
            for (int step = 0; step < points.Length; step ++) {
                float newRadius = radius + Random.Range(-randomisePercent, randomisePercent) * radius;
                points[step] = new Vector3(centre.x + newRadius * Mathf.Cos(interval * step), centre.y + newRadius * Mathf.Sin(interval * step), 0);
            }

            return points;
        }
    }
}

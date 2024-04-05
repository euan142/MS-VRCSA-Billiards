﻿using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ModelConfiguration))]
public class ModelConfigurationEditor : Editor
{
    bool bShowCollision = false;

    static GUIStyle styleHeader;
    static GUIStyle styleError;
    static GUIStyle styleWarning;
    static bool gui_resource_ready = false;

    CollisionVisualizer cdata_displayTarget;
    private static void DrawError(string szError, GUIStyle style)
    {
        GUILayout.BeginVertical("GroupBox");
        GUILayout.Label(szError, style);
        GUILayout.EndVertical();
    }

    private static bool Material_ht8b_supports(ref Material mat)
    {
        bool isFullSupport = true;

        if (!mat.HasProperty("_EmissionColor"))
        {
            DrawError($"[!] Shader '{mat.shader.name}' does not have property: _EmissionColor", styleError);
            isFullSupport = false;
        }

        if (!mat.HasProperty("_Color"))
        {
            DrawError($"Shader {mat.shader.name} does not have property: _Color", styleWarning);
        }

        return isFullSupport;
    }

    private static void Ht8bUIGroup(string szHeader)
    {
        GUILayout.BeginVertical("HelpBox");
        GUILayout.Label(szHeader, styleHeader);
    }

    private static bool Ht8bUIGroupMitButton(string szHeader, string szButton)
    {
        GUILayout.BeginVertical("HelpBox");
        GUILayout.BeginHorizontal();
        GUILayout.Label(szHeader, styleHeader);
        bool b = GUILayout.Button(szButton);
        GUILayout.EndHorizontal();

        return b;
    }

    private static void Ht8bUIGroupEnd()
    {
        GUILayout.EndVertical();
    }

    private static void gui_resource_init()
    {
        styleHeader = new GUIStyle()
        {
            fontSize = 14,
            fontStyle = FontStyle.Bold
        };

        styleWarning = new GUIStyle()
        {
            wordWrap = true
        };

        styleError = new GUIStyle()
        {
            fontStyle = FontStyle.Bold,
            wordWrap = true
        };

        gui_resource_ready = true;
    }

    public override void OnInspectorGUI()
    {
        if (!gui_resource_ready)
        {
            gui_resource_init();
        }

        ModelConfiguration _editor = (ModelConfiguration)target;

        base.DrawDefaultInspector();

        ModelData data = _editor.data;

        if (data != null)
        {
            Ht8bUIGroup("Collision info");

            if (!cdata_displayTarget)
            {
                cdata_displayTarget = _editor.transform.parent.parent.Find("intl.balls").Find("__table_refiner__").gameObject.GetComponent<CollisionVisualizer>();
                if (cdata_displayTarget == null) { return; }
            }

            this.bShowCollision = EditorGUILayout.Toggle("Draw collision data", this.cdata_displayTarget.gameObject.activeSelf);
            this.cdata_displayTarget.gameObject.SetActive(this.bShowCollision);

            Ht8bUIGroupEnd();
            this.cdata_displayTarget.tableWidth = data.tableWidth;
            this.cdata_displayTarget.tableHeight = data.tableHeight;
            this.cdata_displayTarget.k_BALL_RADIUS = data.ballRadius;
            this.cdata_displayTarget.pocketWidthCorner = data.pocketWidthCorner;
            this.cdata_displayTarget.pocketHeightCorner = data.pocketHeightCorner;
            this.cdata_displayTarget.pocketRadiusSide = data.pocketRadiusSide;
            this.cdata_displayTarget.cushionRadius = data.cushionRadius;
            this.cdata_displayTarget.pocketInnerRadiusCorner = data.pocketInnerRadiusCorner;
            this.cdata_displayTarget.pocketInnerRadiusSide = data.pocketInnerRadiusSide;
            this.cdata_displayTarget.cornerPocket = data.cornerPocket;
            this.cdata_displayTarget.sidePocket = data.sidePocket;
            this.cdata_displayTarget.facingAngleCorner = data.facingAngleCorner;
            this.cdata_displayTarget.facingAngleSide = data.facingAngleSide;
            this.cdata_displayTarget.k_RAIL_HEIGHT_UPPER = data.railHeightUpper;
            this.cdata_displayTarget.k_RAIL_HEIGHT_LOWER = data.railHeightLower;
            this.cdata_displayTarget.k_RAIL_DEPTH_WIDTH = data.railDepthWidth;
            this.cdata_displayTarget.k_RAIL_DEPTH_HEIGHT = data.railDepthHeight;
        }
    }
    void OnEnable()
    {
        if (!cdata_displayTarget)
        {
            ModelConfiguration _editor = (ModelConfiguration)target;
            cdata_displayTarget = _editor.transform.parent.parent.Find("intl.balls").Find("__table_refiner__").gameObject.GetComponent<CollisionVisualizer>();
            if (cdata_displayTarget == null) { return; }
        }
        cdata_displayTarget.gameObject.SetActive(true);
    }
    void OnDisable()
    {
        if (!cdata_displayTarget)
        {
            ModelConfiguration _editor = (ModelConfiguration)target;
            cdata_displayTarget = _editor.transform.parent.parent.Find("intl.balls").Find("__table_refiner__").gameObject.GetComponent<CollisionVisualizer>();
            if (cdata_displayTarget == null) { return; }
        }
        cdata_displayTarget.gameObject.SetActive(false);
    }
}
using Microsoft.Azure.SpatialAnchors.Unity;
using Microsoft.Azure.SpatialAnchors.Unity.Samples;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnchorFeedbackScript : MonoBehaviour
{
    private string CurrentMessage;
    public TextMeshPro textMeshProObject;

    private AnchorModuleScript anchorModuleScript;
    private AzureSpatialAnchorsDemoWrapper azureSpatialAnchorsDemoWrapper;


    void Awake()
    {

        anchorModuleScript = GameObject.FindObjectOfType<AnchorModuleScript>();
        azureSpatialAnchorsDemoWrapper = FindObjectOfType<AzureSpatialAnchorsDemoWrapper>();

        anchorModuleScript.OnStartASASession += AnchorModuleScript_OnStartASASession;
        anchorModuleScript.OnEndASASession += AnchorModuleScript_OnEndASASession;
        anchorModuleScript.OnCreateAnchorStarted += AnchorModuleScript_OnCreateAnchorStarted;
        azureSpatialAnchorsDemoWrapper.OnAnchorCreatedSuccessfully += AnchorModuleScript_OnAnchorCreatedSuccessfully;
        azureSpatialAnchorsDemoWrapper.OnAnchorCreationfailed += AnchorModuleScript_OnAnchorCreationfailed;
        
        anchorModuleScript.OnFindASAAnchor += AnchorModuleScript_OnFindASAAnchor;
        anchorModuleScript.OnCreateLocalAnchor += AnchorModuleScript_OnCreateLocalAnchor;
        anchorModuleScript.OnRemoveLocalAnchor += AnchorModuleScript_OnRemoveLocalAnchor;
        anchorModuleScript.OnASAAnchorLocated += AnchorModuleScript_OnASAAnchorLocated;
        
    }

    private void AnchorModuleScript_OnASAAnchorLocated()
    {
        textMeshProObject.text = "Spatial anchor located. Moving Object to position.";
    }

    private void AnchorModuleScript_OnRemoveLocalAnchor()
    {
        textMeshProObject.text = "Deleting Local Anchor";
    }

    private void AnchorModuleScript_OnCreateLocalAnchor()
    {
        textMeshProObject.text = "Creating Local Anchor. Wait for confirmation before moving to the next step.";
    }

    private void AnchorModuleScript_OnFindASAAnchor()
    {
        textMeshProObject.text = "Locating Anchor";
    }

    private void AnchorModuleScript_OnEndASASession()
    {
        textMeshProObject.text = "Ending ASA Session";
    }

    private void AnchorModuleScript_OnAnchorCreationfailed()
    {
        textMeshProObject.text = "Anchor Creation Failed";
    }

    private void AnchorModuleScript_OnAnchorCreatedSuccessfully()
    {
        textMeshProObject.text = "Anchor Creation Successful";
    }

    private void AnchorModuleScript_OnCreateAnchorStarted()
    {
        textMeshProObject.text = "Creating Spatial Anchor";
    }

    private void AnchorModuleScript_OnStartASASession()
    {
        textMeshProObject.text = " Starting ASA Session";
    }
}

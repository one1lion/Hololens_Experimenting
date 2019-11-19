using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Collections.Generic;
using System.Threading.Tasks;
//using Microsoft.Azure.SpatialAnchors.Unity.Samples;
using Microsoft.Azure.SpatialAnchors.Unity;
using Microsoft.Azure.SpatialAnchors;
using System;
using System.IO;
using UnityEngine.Networking;
using RestSharp;
using Microsoft.Azure.SpatialAnchors.Unity.Samples;

#if WINDOWS_UWP
using Windows.Storage;
#endif

public class AnchorModuleScript : MonoBehaviour
{

    //This is the name of the anchor ID to find anchors stored on Azure. Users cannot customize this at this time. This will be provided by Azure.
    public string AzureAnchorID = "";
    public string publicSharingPin = "";


    AzureSpatialAnchorsDemoWrapper CloudManager;
    CloudSpatialAnchor currentCloudAnchor;
    CloudSpatialAnchorWatcher currentWatcher;
    Coroutine createAnchorCoroutine;

    private readonly Queue<Action> dispatchQueue = new Queue<Action>();

    public object feedbackBox { get; private set; }

    public delegate void StartASASessionDelegate();
    public event StartASASessionDelegate OnStartASASession;

    public delegate void EndASASessionDelegate();
    public event EndASASessionDelegate OnEndASASession;

    public delegate void CreateAnchorDelegate();
    public event CreateAnchorDelegate OnCreateAnchorStarted;

    public delegate void FindAnchorDelegate();
    public event FindAnchorDelegate OnFindASAAnchor;

    public delegate void DeleteASAAnchorDelegate();
    public event DeleteASAAnchorDelegate OnDeleteASAAnchor;

    public delegate void AnchorLocatedDelegate();
    public event AnchorLocatedDelegate OnASAAnchorLocated;

    public delegate void CreateLocalAnchorDelegate();
    public event CreateLocalAnchorDelegate OnCreateLocalAnchor;

    public delegate void RemoveLocalAnchorDelegate();
    public event RemoveLocalAnchorDelegate OnRemoveLocalAnchor;

    public int counter=0;

    // Start is called before the first frame update
    void Start()
    {
        //This gets the AzureSpatialAnchorsDemoWrapper.cs component in your scene (must be present in scene)
        CloudManager = AzureSpatialAnchorsDemoWrapper.Instance;


        ////The code below registers Azure Spatial Anchor events
        CloudManager.OnAnchorLocated += CloudManager_OnAnchorLocated;

    }


    void Update()
    {
        lock (dispatchQueue)
        {
            if (dispatchQueue.Count > 0)
            {
                dispatchQueue.Dequeue()();
            }
        }

    }

    public void StartAzureSession()
    {

        CloudManager.ResetSessionStatusIndicators();
        CloudManager.EnableProcessing = true;

        OnStartASASession?.Invoke();
        
    }

    public void StopAzureSession()
    {
        CloudManager.EnableProcessing = false;
        CloudManager.ResetSession();

        OnEndASASession?.Invoke();
    }

    public void CreateAzureAnchor(GameObject theObject)
    {
        OnCreateAnchorStarted?.Invoke();
        createAnchorCoroutine = StartCoroutine(CreateAzureAnchorIDCorotuine());

        //First we create a local anchor at the location of the object in question
        theObject.AddARAnchor();

        //Then we create a new local cloud anchor
        CloudSpatialAnchor localCloudAnchor = new CloudSpatialAnchor();

        //Now we set the local cloud anchor's position to the local anchor's position
        localCloudAnchor.LocalAnchor = theObject.GetNativeAnchorPointer();

        //Check to see if we got the local anchor pointer
        if (localCloudAnchor.LocalAnchor == IntPtr.Zero)
        {
            Debug.Log("Didn't get the local XR anchor pointer...");
            return;
        }

        // In this sample app we delete the cloud anchor explicitly, but here we show how to set an anchor to expire automatically
        localCloudAnchor.Expiration = DateTimeOffset.Now.AddDays(7);

        //Save anchor to cloud
        Task.Run(async () =>
        {
            while (!CloudManager.EnoughDataToCreate)
            {
                await Task.Delay(330);
                float createProgress = CloudManager.GetSessionStatusIndicator(AzureSpatialAnchorsDemoWrapper.SessionStatusIndicatorType.RecommendedForCreate);
                QueueOnUpdate(new Action(() => Debug.Log($"Move your device to capture more environment data: {createProgress:0%}")));
               
            }

            bool success = false;

            
            try
            {
                QueueOnUpdate(new Action(() => Debug.Log("Saving...")));

                currentCloudAnchor = await CloudManager.StoreAnchorInCloud(localCloudAnchor);

                //Save the Azure Anchor ID
                AzureAnchorID = currentCloudAnchor.Identifier;

                success = currentCloudAnchor != null;

                localCloudAnchor = null;

                if (success)
                {
                    //OnAnchorCreatedSuccessfully?.Invoke();
                    Debug.Log("Successfully Created Anchor");
                }
                else
                {
                    Debug.Log("Failed to save, but no exception was thrown.");
                }

            }
            catch (Exception ex)
            {
                //OnAnchorCreationfailed?.Invoke();
                Debug.Log(ex.ToString());
              
            }
        });
        
    }
        
    public IEnumerator CreateAzureAnchorIDCorotuine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
           if (AzureAnchorID != "")
            {
                Debug.Log("Anchor created succesfully!");

                StopCoroutine(createAnchorCoroutine);

            }
            else
            {
                
                if (counter <= 7) {
                    Debug.Log("Saving...Please wait...");
                    counter += 1;
                }
                else
                {
                    Debug.Log("Failed to save, but no exception was thrown.");
                    Debug.Log("Please close the app and try again...");
                    StopCoroutine(createAnchorCoroutine);
                }
                
            }
        }
    }
    public void RemoveLocalAnchor(GameObject theObject)
    {
        OnRemoveLocalAnchor?.Invoke();
        Debug.Log("Local anchor deleted succesfully.");
        theObject.RemoveARAnchor();
    }

    //Start looking for specified anchors
    public void FindAzureAnchor(string AnchorIDtoFind)
    {
        OnFindASAAnchor?.Invoke();

        Debug.Log("Looking for anchor...");
        //Provide list of anchor IDs to locate
        SetUpAnchorIDsToLocate();

        //Start watching for Anchors
        currentWatcher = CloudManager.CreateWatcher();
    }

    public void DeleteAzureAnchor(string AnchorIDtoDelete)
    {
        
        //Delete the anchor with the ID specified off the server and locally
        Task.Run(async () =>
        {
            await CloudManager.DeleteAnchorAsync(currentCloudAnchor);
            currentCloudAnchor = null;
            Debug.Log("Cloud anchor is deleted successfully.");
            OnDeleteASAAnchor?.Invoke();
        });
    }

    public void SaveAzureAnchorIDToDisk()
    {
        String path = "";

#if WINDOWS_UWP
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            path = storageFolder.Path.Replace('\\', '/') + "/";
        path = Path.Combine(path, "SavedAzureAnchorID.txt");
        File.WriteAllText(path, AzureAnchorID);              
#endif

        Debug.Log("Saved Anchor ID: " + AzureAnchorID + " to this path: " + path);

    }

    public void LoadAzureAnchorIDsFromDisk()
    {
        String path = "";
#if WINDOWS_UWP
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            path = storageFolder.Path.Replace('\\', '/') + "/";
        path = Path.Combine(path, "SavedAzureAnchorID.txt");

        AzureAnchorID = File.ReadAllText(path);

            
#endif

        Debug.Log("Loaded Azure Anchor ID from Disk: " + AzureAnchorID + " from this path: " + path);

    }

    public void SetUpAnchorIDsToLocate()
    {
        List<string> anchorsToFind = new List<string>();

        if (AzureAnchorID != "")
        {
            anchorsToFind.Add(AzureAnchorID);
        }

        CloudManager.SetAnchorIdsToLocate(anchorsToFind);
    }

    private void CloudManager_OnAnchorLocated(object sender, AnchorLocatedEventArgs args)
    {
        Debug.LogFormat("Anchor recognized as a possible anchor {0} {1}", args.Identifier, args.Status);
        if (args.Status == LocateAnchorStatus.Located)
        {
            OnCloudAnchorLocated(args);
            OnASAAnchorLocated?.Invoke();
            Debug.Log("Anchor Located!");
        }
    }

    private void OnCloudAnchorLocated(AnchorLocatedEventArgs args)
    {
        Debug.Log("Cloud Anchor Located:" + args.Status);

        if (args.Status == LocateAnchorStatus.Located)
        {
            currentCloudAnchor = args.Anchor;
            Debug.Log("Anchor ID Found!");

            QueueOnUpdate(() =>
            {
                Pose anchorPose = Pose.identity;

#if UNITY_ANDROID || UNITY_IOS
            anchorPose = currentCloudAnchor.GetAnchorPose();
#endif
                Debug.Log("Anchor Found!");
                Debug.Log("Now setting gameObject to anchor position and rotation.");

                // HoloLens: The position will be set based on the unityARUserAnchor that was located.
#if WINDOWS_UWP || UNITY_WSA
                //create a local anchor at the location of the object in question
                gameObject.AddARAnchor();

                // On HoloLens, if we do not have a cloudAnchor already, we will have already positioned the
                // object based on the passed in worldPos/worldRot and attached a new world anchor,
                // so we are ready to commit the anchor to the cloud if requested.
                // If we do have a cloudAnchor, we will use it's pointer to setup the world anchor,
                // which will position the object automatically.
                if (currentCloudAnchor != null)
                {
                    Debug.Log("Setting Local Anchor to Cloud Anchor Position.");
                  
                    gameObject.GetComponent<UnityEngine.XR.WSA.WorldAnchor>().SetNativeSpatialAnchorPtr(currentCloudAnchor.LocalAnchor);
                }
#else
                Debug.Log("Cloud anchor position: " + anchorPose.position + ". Cloud Anchor Rotation: " + anchorPose.rotation);
            SetObjectToAnchorPose(anchorPose.position, anchorPose.rotation);
            
#endif
            });

        }
    }

    private void SetObjectToAnchorPose(Vector3 position, Quaternion rotation)
    {
        Debug.Log("Setting Object To Anchor Pose");
        transform.position = position;
        transform.rotation = rotation;

        //create a local anchor at the location of the object in question
        gameObject.AddARAnchor();
    }

    void OnDestroy()
    {
        if (CloudManager != null)
        {
            CloudManager.EnableProcessing = false;
        }

        if (currentWatcher != null)
        {
            currentWatcher.Stop();
            currentWatcher = null;
        }

    }

    /// <summary>
    /// Queues the specified <see cref="Action"/> on update.
    /// </summary>
    /// <param name="updateAction">The update action.</param>
    protected void QueueOnUpdate(Action updateAction)
    {
        lock (dispatchQueue)
        {
            dispatchQueue.Enqueue(updateAction);
        }
    }

    public void ShareAnchor()
    {

        //Create filename with custom extension that is the Pin
        string filename = "file." + publicSharingPin;

        //Create the full file path
        string filePath = Application.persistentDataPath + "/" + filename;

#if WINDOWS_UWP
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            filePath = storageFolder.Path + "/" + filename;           
#endif

        //Create the anchor ID file to share
        File.WriteAllText(filePath, AzureAnchorID);

        try
        {
            var client = new RestClient("http://167.99.111.15:8090");


            var request = new RestRequest("/uploadFile.php", Method.POST)
                    .AddHeader("Accept", "application/json")
                    .AddHeader("Content-Type", "multipart/form-data");
            request.AddFile("the_file", filePath);
            request.AddParameter("replace_file", 1);  //only needed if you want to upload a static file

            var httpResponse = client.Execute(request);

            string json = httpResponse.Content.ToString();
        }
        catch (Exception ex)
        {
            Debug.Log(string.Format("Exception: {0}", ex.Message));
            throw;
        }
    }

    public void GetSharedAzureAnchor()
    {

        StartCoroutine(GetSharedAzureAnchorCoroutine(publicSharingPin));
    }

    public IEnumerator GetSharedAzureAnchorCoroutine(string sharingPin)
    {
        Debug.Log("Downloading File...");

        string url = "http://167.99.111.15:8090/file-uploads/static/file." + sharingPin.ToLower();
        Debug.Log("Attempting to look for network ping file: " + url);
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string path = Application.persistentDataPath;
#if WINDOWS_UWP
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                path = storageFolder.Path;
#endif
                string sharedAnchorID = www.downloadHandler.text;
                File.WriteAllText(path + "/file." + sharingPin, sharedAnchorID);

                Debug.Log("Loading Shared Anchor ID: " + sharedAnchorID);

                AzureAnchorID = sharedAnchorID;

            }
        }

    }

}

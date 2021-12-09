using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UniG.Experimental.ArxOOP {
    /// <summary>
    /// An Arx applet.
    /// </summary>
    public class Applet : IDisposable {

        /// <summary>
        /// Fired when a tag is tapped. The argument is the ID.
        /// </summary>
        public UnityEvent<string> Tap;
        public UnityEvent Connected;
        public bool uploadOnConnection;
        private List<ArxFile> files;

        private static Applet _instance;
        private bool disposedValue;

        public Applet(ArxFile[] files) {
            if (_instance != null) {
                throw new InvalidOperationException("Only one applet can be defined per app. Destroy the other applet.");
            }
            _instance = this;

            Tap = new UnityEvent<string>();
            Connected = new UnityEvent();
            this.files = new List<ArxFile>(files);

            Debug.LogWarning("The ArxOOP API is experimental. It might not feature everything available with the Arx class or have issues.");
            // Create a field for the callback
            Arx.logiArxCbContext contextCallback;
            // Set its content and callback funtion
            contextCallback.arxCallBack = SDKCallback;
            contextCallback.arxContext = IntPtr.Zero;
            // Initialize the SDK
            bool retVal = Arx.Init(ref contextCallback);

            if (!retVal) {
                int retCode = Arx.GetLastError();
                Debug.LogError("Cannot initialize Arx SDK: " + retCode);
                return;
            }
            // Upload files if needed
            if (uploadOnConnection) UploadFiles();
        }

        private void SDKCallback(int eventType, int eventValue, string eventArg, IntPtr context) {
            Debug.Log(eventType + " " + eventArg);
            // If our event is a device connecting:
            if (eventType == (int)Arx.Event.Arrival) {
                // Invoke event
                Connected.Invoke();
            }
            // Else, if it's a tap on a tag:
            else if (eventType == (int)Arx.Event.TagTapped) {
                // Invoke event
                Tap.Invoke(eventArg);
            }
        }

        public void UploadFiles() {
            foreach (var file in files) Upload(file);
        }

        public void Upload(ArxFile file) {
            Arx.AddFileAs(file.Path, file.Name, file.MIME);
            files.Add(file);
        }

        public void Upload(StreamingAssetArxFile file) {
            Arx.AddFileAs(file.Path, file.Name, file.MIME);
            files.Add(file);
        }

        public void Show(string homepage = "index.html") {
            Arx.SetIndex(homepage);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                // Destroy files array
                files = null;
                // Remove all listeners
                Tap.RemoveAllListeners();
                Connected.RemoveAllListeners();
                // Shut down Arx integration
                Arx.Shutdown();
                disposedValue = true;
            }
        }

        public void Dispose() {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
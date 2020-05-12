using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Applet's source code is in StreamingAssets/ArxShopApplet.

namespace UniG.Demos {
    public class ArxDemoController : MonoBehaviour {

        private static int gold = 5500;
        private static List<string> items = new List<string>();
        public Text itemList;
        static Dictionary<int, int> costs = new Dictionary<int, int>() {
            {1, 300},
            {2, 150}
        };
        static Dictionary<int, string> names = new Dictionary<int, string>() {
            {1, "Cannon"},
            {2, "Sword"}
        };
        
        // Start is called before the first frame update
        void Start() {
            // Create a field for the callback
            Arx.logiArxCbContext contextCallback;
            // Set its content and callback funtion
            contextCallback.arxCallBack = SDKCallback;
            contextCallback.arxContext = IntPtr.Zero;
            // Initialize the SDK
            bool retVal = Arx.LogiArxInit("com.unig.arx.shop", "Weapon Shop", ref contextCallback);

            if (!retVal) {
                int retCode = Arx.GetLastError();
                Debug.LogError("Cannot initialize Arx SDK: " + retCode);
            }

            StartCoroutine(GiveGold());
        }

        // Called when the app quits
        private void OnApplicationQuit() {
            // Shut down the Arx SDK
            Arx.Shutdown();
        }
        
        // Update is called once per frame
        void Update() {
            // Update the gold count in the applet
            Arx.SetTagContentById("gold", "You have " + gold + " gold.");
            // Make a temporary string
            var itemp = "";
            // For each item:
            foreach (var it in items) {
                // Add it to our string
                itemp += it;
                // Add a comma
                itemp += ", ";
            }
            // Set our item text to the temporary string without the trailing comma
            itemList.text = itemp.Substring(0, itemp.Length - 2);
            // Check if we can afford all items
            CanWeAfford();
        }
        
        // Callback function for Arx SDK
        static void SDKCallback(int eventType, int eventValue, string eventArg, IntPtr context) {
            Debug.Log(eventType + " " + eventArg);
            // If our event is a device connecting:
            if (eventType == (int)Arx.Event.Arrival) {
                // Send our applet's index file
                Arx.AddFileAs(Application.streamingAssetsPath + "/ArxShopApplet/index.html", "index.html");
                // Send all its dependencies
                Arx.AddFileAs(Application.streamingAssetsPath + "/ArxShopApplet/cannon.png", "cannon.png");
                Arx.AddFileAs(Application.streamingAssetsPath + "/ArxShopApplet/sword.png", "sword.png");
                // Set our index file
                // This makes the applet show up and become active in Arx Control
                Arx.SetIndex("index.html");
            }
            // Else, if it's a tap on a tag:
            else if (eventType == (int)Arx.Event.TagTapped) {
                // If the argument starts with "buy":
                if (eventArg.StartsWith("buy")) {
                    Debug.Log(eventArg);
                    // Get the ID of the item the user bought
                    var item = int.Parse(eventArg.Replace("buy_it_", ""));
                    // Buy it
                    BuyItem(item);
                }
            }
        }
        
        // Helper function for buying items
        static void BuyItem(int item) {
            // If we can afford it:
            if (gold >= costs[item]) {
                
                // Subtract its cost
                gold -= costs[item];
                // Add it to the item list
                items.Add(names[item]);
            }
            // else:
            else {
                // Show a warning that we are too poor
                Arx.SetTagContentById("cant_afford_" + item, "Cannot afford this weapon!");
            }
        }
        
        // Helper function for updating  texts
        void CanWeAfford() {
            // For each item:
            for (int item = 1; item < items.Count + 1; item++) {
                // If we can afford it:
                if (gold >= costs[item]) {
                    // Clear the affordability warning
                    Arx.SetTagContentById("cant_afford_" + item, "");
                }
                // else:
                else {
                    // Show a warning that we are too poor
                    Arx.SetTagContentById("cant_afford_" + item, "Cannot afford this weapon!");
                }
            }
        }
        
        // Helper function that gives gold
        IEnumerator GiveGold() {
            while (true) {
                yield return new WaitForSeconds(2);
                gold += 5;
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniG.Experimental.ArxOOP;
using UnityEngine;
using UnityEngine.UI;

// Applet's source code is in StreamingAssets/ArxShopApplet.

namespace UniG.Experimental.Demos {
    public class ArxOOPDemoController : MonoBehaviour {

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
        static ArxFile[] appletFiles = new ArxFile[] {
            new StreamingAssetArxFile("/ArxShopApplet/index.html", "index.html"),
            new StreamingAssetArxFile("/ArxShopApplet/cannon.png", "cannon.png"),
            new StreamingAssetArxFile("/ArxShopApplet/sword.png", "sword.png")
        };
        Dictionary<string, int> namesRev;
        Applet applet;

        // Start is called before the first frame update
        void Start() {
            Arx.Shutdown();
            applet = new Applet(appletFiles) {
                uploadOnConnection = true,
            };
            applet.Connected.AddListener(() => Show());
            applet.Tap.AddListener(z => BuyItem(z));
        }

        void Show() { applet.Show(); }

        // Called when the app quits
        private void OnApplicationQuit() {
            // Shut down the Arx SDK
            applet.Dispose();
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
            if (itemp.Length > 2) itemList.text = itemp.Substring(0, itemp.Length - 2);
            else itemList.text = itemp;
            // Check if we can afford all items
            CanWeAfford();
        }
        
        // Helper function for buying items
        static void BuyItem(string arg) {
            // If the argument starts with "buy":
            if (arg.StartsWith("buy")) {
                Debug.Log(arg);
                // Get the ID of the item the user bought
                var item = int.Parse(arg.Replace("buy_it_", ""));
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
        }
        
        // Helper function for updating  texts
        void CanWeAfford() {
            // For each item:
            for (int item = 1; item < items.Count; item++) {
                Debug.Log(item);
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
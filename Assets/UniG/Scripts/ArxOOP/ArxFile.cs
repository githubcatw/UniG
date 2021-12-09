using System.IO;
using UnityEngine;

namespace UniG.Experimental.ArxOOP {
    public class ArxFile {
        public string Path;
        public string Name;
        public string MIME;
        public ArxFile(string path, string name) {
            Path = path;
            Name = name;
        }
    }
    public class StreamingAssetArxFile: ArxFile {
        public string RelativePath;
        public new string Path { get; private set; }
        public StreamingAssetArxFile(string path, string name): base("_MANAGED",name) {
            RelativePath = path;
            base.Path = Path = System.IO.Path.Combine(Application.streamingAssetsPath, RelativePath);
            Name = name;
        }
    }
}
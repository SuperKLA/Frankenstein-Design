using System.Diagnostics;
using System.IO;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace Prototypes.API.EditorExtensions
{
    public static class SVGLayerToSperatedFiles
    {
        [MenuItem("Assets/SVG/ToSVGFiles")]
        public static void ToSVGFiles()
        {
            var batFile = @"C:\GameProjects\Prototypes\Assets\BATs\SVGToFiles.bat";

            if (!File.Exists(batFile))
            {
                Debug.LogError("SVGLayerToSperatedFiles batFile pointing towards nothing");
                return;
            }

            var path    = AssetDatabase.GetAssetPath(Selection.activeObject);
            var dirPath = Path.GetDirectoryName(path);
            var svgPath = dirPath + "/" + Selection.activeObject.name + ".svg";
            var env = System.Environment.CurrentDirectory;
            var fullSVGPath = Path.Combine(env, svgPath);
            var targetPath = Path.Combine(env, dirPath);
            
            var cmd = " \"" + fullSVGPath         + "\"";
            cmd += " --destdir \"" +targetPath +"/\"";

            Debug.LogWarning(cmd);

            ExecuteCommand(cmd);
        }

        public static void ExecuteCommand(string cmd)
        {
            var processInfo = new ProcessStartInfo(@"C:\GameProjects\Prototypes\Assets\BATs\SVGToFiles.bat");
            processInfo.CreateNoWindow  = false;
            processInfo.UseShellExecute = false;
            processInfo.Arguments = cmd;
            var process = Process.Start(processInfo);
            process.WaitForInputIdle();
            process.Close();
        }
    }
}
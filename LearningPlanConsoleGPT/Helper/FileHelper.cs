namespace LearningPlanConsoleGPT.Helper
{
    public class FileHelper
    {

        /// <summary>
        /// Create Directory If Not Exists
        /// </summary>
        /// <param name="directoryPath">directory path</param>
        public static void CreateDirectoryIfNotExists(string directoryPath)
        {
            // Check if the directory exists
            if (!Directory.Exists(directoryPath))
            {
                // If not, create the directory
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}

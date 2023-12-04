namespace LearningPlanConsoleGPT.Models
{
    
    /// <summary>
    /// AI Request - Ask question to AI
    /// </summary>
    public class AiRequest
    {
        public string SystemQuestion { get; set; }
        public string UserQuestion { get; set; }
        public int QuestionNumber { get; set; }
        public string Subject { get; set; }
        public string FileExtension { get; set; }
    }
}

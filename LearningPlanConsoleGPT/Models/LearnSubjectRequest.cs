namespace LearningPlanConsoleGPT.Models
{
    /// <summary>
    /// Learn Subject Request - Planning to learn subject and topics
    /// </summary>
    public class LearnSubjectRequest
    {
        public string Subject { get; set; }
        public List<object> Topics { get; set; }
    }
}

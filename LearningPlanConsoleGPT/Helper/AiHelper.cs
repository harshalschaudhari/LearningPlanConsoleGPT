using LearningPlanConsoleGPT.Implementation;
using LearningPlanConsoleGPT.Models;
using Newtonsoft.Json;

namespace LearningPlanConsoleGPT.Helper
{
    /// <summary>
    /// AI Helper is used for prepase AI request and AI Response
    /// </summary>
    public class AiHelper
    {
        public static List<AiRequest> GetAiRequests(List<LearnSubjectRequest> learnSubjectRequestList, List<RawQuestion> rawQuestionList)
        {
            int counter = 1;
            List<AiRequest> aiRequestList = new List<AiRequest>();
            foreach (var learnSubjectRequest in learnSubjectRequestList)
            {
                foreach (var topic in learnSubjectRequest.Topics)
                {
                    foreach (var rawQuestion in rawQuestionList)
                    {
                        string prepareQuestion = string.Format(rawQuestion.Question, learnSubjectRequest.Subject, topic);
                        AiRequest aiRequest = new AiRequest()
                        {
                            QuestionNumber = counter,
                            Subject = learnSubjectRequest.Subject,
                            SystemQuestion = prepareQuestion,
                            UserQuestion = prepareQuestion,
                            FileExtension = rawQuestion.FileExtension
                        };

                        aiRequestList.Add(aiRequest);
                        counter++;
                    }
                }
            }
            return aiRequestList;
        }

        public static List<LearnSubjectRequest> GetLearnSubjectRequests(string currentDirectory, string subjectListFilePath)
        {
            string filePath = Path.Combine(currentDirectory, subjectListFilePath);
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<LearnSubjectRequest>>(json);
        }

        public static List<RawQuestion> GetRawQuestions(string currentDirectory, string subjectListFilePath)
        {
            string filePath = Path.Combine(currentDirectory, subjectListFilePath);
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<RawQuestion>>(json);
        }

        public static async Task AiProcessQuestions(string currentDirectory, string aiOutPutDirPathText, List<AiRequest> aiRequestList)
        {
            
            AiService aiService = new AiService();
            foreach (var aiRequest in aiRequestList)
            {
                //Ask to AI 
                string aiResponse = await aiService.GetTextResponse(aiRequest.SystemQuestion, aiRequest.UserQuestion);
                string responseFileName = string.Format("{0}_{1}.{2}", aiRequest.QuestionNumber, StringHelper.ReplaceSpecialChars(aiRequest.SystemQuestion), aiRequest.FileExtension);
                string outputDirPath = Path.Combine(currentDirectory, aiOutPutDirPathText, aiRequest.Subject);
                FileHelper.CreateDirectoryIfNotExists(outputDirPath);
                File.WriteAllText(Path.Combine(outputDirPath, responseFileName), aiResponse);

                Console.WriteLine("AI Processed: {0}", responseFileName);
            }
        }
    }
}

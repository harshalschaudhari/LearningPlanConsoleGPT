using LearningPlanConsoleGPT.Helper;
using LearningPlanConsoleGPT.Models;
using System.Configuration;
using System.Reflection;

namespace LearningPlanConsoleGPT
{
    class Program
    {
        static async Task Main(string[] args)
        {

            Console.WriteLine("Start!");

            //Get Configuration
            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string subjectListFilePath = ConfigurationManager.AppSettings["SubjectListFilePath"];
            string aiQuestionFilePath = ConfigurationManager.AppSettings["AiQuestionFilePath"];
            string aiOutPutDirPathText = ConfigurationManager.AppSettings["AiOutPutDirPathText"];

            //Prepare AI Request model
            List<LearnSubjectRequest> learnSubjectRequestList = AiHelper.GetLearnSubjectRequests(currentDirectory, subjectListFilePath);
            List<RawQuestion> rawQuestionList = AiHelper.GetRawQuestions(currentDirectory, aiQuestionFilePath);
            List<AiRequest> aiRequestList = AiHelper.GetAiRequests(learnSubjectRequestList, rawQuestionList);

            //Process question with Azure AI.
            await AiHelper.AiProcessQuestions(currentDirectory, aiOutPutDirPathText, aiRequestList);

            Console.WriteLine("End!");

            //Note: To prevents the screen from running and closing quickly 
            Console.ReadKey();
        }
    }
}
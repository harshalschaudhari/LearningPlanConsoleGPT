using Azure;
using Azure.AI.OpenAI;
using System.Configuration;

namespace LearningPlanConsoleGPT.Implementation
{
    /// <summary>
    /// AI Service - Ask question and get answer
    /// </summary>
    public class AiService
    {
        string endpoint = string.Empty;
        string apiKey = string.Empty;
        string githubAlias = string.Empty;

        OpenAIClient openAIClient;

        public AiService()
        {
            endpoint = ConfigurationManager.AppSettings["AzureAIUrl"];
            apiKey = ConfigurationManager.AppSettings["AzureAIKey"];
            githubAlias = ConfigurationManager.AppSettings["YourGithubAlias"];

            // the full url is appended by /v1/api
            Uri azureAIUrl = new(endpoint + "/v1/api");

            // the full key is appended by "/YOUR-GITHUB-ALIAS"
            AzureKeyCredential token = new(apiKey + githubAlias);

            // instantiate the client with the "full" values for the url and key/token
            openAIClient = new(azureAIUrl, token);
        }

        public async Task<string> GetTextResponse(string systemContent, string userContent)
        {

            ChatCompletionsOptions completionsOptions = new()
            {
                MaxTokens = 2048,
                Temperature = 0.7f,
                NucleusSamplingFactor = 0.95f,
                DeploymentName = "gpt-35-turbo"
            };

            completionsOptions.Messages.Add(new ChatMessage(ChatRole.System, systemContent));
            completionsOptions.Messages.Add(new ChatMessage(ChatRole.User, userContent));

            // Assuming there's a method in the OpenAI SDK to generate an text response 
            var response = await openAIClient.GetChatCompletionsAsync(completionsOptions);

            string completion = response.Value.Choices[0].Message.Content.ToString();

            return completion;
        }

        public async Task<string> GetImageResponse(string systemContent, string userContent)
        {
            // Call the asynchronous method
            Response<ImageGenerations> response = await openAIClient.GetImageGenerationsAsync(new ImageGenerationOptions
            {
                Prompt = userContent
            });

            ImageGenerations imageGeneration = response.Value;
            Uri imageUri = imageGeneration.Data[0].Url;


            return imageUri.ToString();
        }
    }
}

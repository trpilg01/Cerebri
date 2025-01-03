using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;


namespace Cerebri.Infrastructure.ReportGeneration
{
    public class OpenAIConnector : IOpenAIConnector
    {
        private readonly string _apiKey;
        private readonly ILogger<OpenAIConnector> _logger;
        private readonly HttpClient _httpClient;

        public OpenAIConnector(ILogger<OpenAIConnector> logger, IConfiguration configuration)
        {
            _logger = logger;
            _apiKey = configuration["OpenAI:ApiKey"] ?? throw new InvalidOperationException("OpenAIApi key not found in configuration");
            _httpClient = new HttpClient();
            ConfigureHttpClient();
        }

        public async Task<OpenAIResponseModel?> Prompt(IList<JournalEntryModel> journals)
        {
            var payload = new StringContent(CreatePayLoad(journals), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("/v1/chat/completions", payload);
            var response = await result.Content.ReadAsStringAsync();
            return ParseResponse(response);
        }

        private OpenAIResponseModel? ParseResponse(string response)
        {
            try
            {
                var responseObject = JsonNode.Parse(response);

                var content = responseObject?["choices"]?[0]?["message"]?["content"]?.GetValue<string>();
                if (content == null)
                {
                    return null;
                }

                var contentObject = JsonNode.Parse(content);
                var summary = contentObject?["summary"]?.GetValue<string>() ?? "Summary not found in content";
                var insights = contentObject?["suggestions"]?.GetValue<string>() ?? "Insights not found in content";
                return new OpenAIResponseModel(summary, insights);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error parsing response from OpenAI");
                return null;
            }
        }

        private void ConfigureHttpClient()
        {
            _httpClient.BaseAddress = new Uri("https://api.openai.com");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        private string CreatePayLoad(IEnumerable<JournalEntryModel> journals)
        {
            string jsonString = JsonSerializer.Serialize(journals);

            JsonObject payload = new JsonObject();
            JsonObject responseFormatObject = new JsonObject();
            JsonObject developerMessageObject = new JsonObject();
            JsonObject userMessageObject = new JsonObject();
            JsonObject messageContentObject = new JsonObject();
            JsonArray messageContentArray = new JsonArray();
            JsonArray messageArray = new JsonArray();

            developerMessageObject.Add("role", "developer");
            developerMessageObject.Add("content", @"
                   You are an aid to mental healthcare providers. You aid them by providing summaries of journal entries written by their patients.
                   Briefly summarize the content of the journal entries highlighting trends in their mood and important events that should be
                   discussed in patients next therapy appointment.
                ");

            userMessageObject.Add("role", "user");
            userMessageObject.Add("content", jsonString);

            messageArray.Add(developerMessageObject);
            messageArray.Add(userMessageObject);
            payload.Add("messages", messageArray);

            responseFormatObject = CreateResponseFormat();
            payload.Add("response_format", responseFormatObject);

            payload.Add("model", "gpt-4o-2024-08-06");

            return payload.ToJsonString();
        }

        private JsonObject CreateResponseFormat()
        {
            JsonObject responseFormatObject = new JsonObject();
            JsonObject jsonSchemaObject = new JsonObject();
            JsonObject schemaObject = new JsonObject();
            JsonObject propertiesObject = new JsonObject();
            JsonObject summaryObject = new JsonObject();
            JsonObject suggestionsObject = new JsonObject();

            propertiesObject.Add("summary", summaryObject);
            propertiesObject.Add("suggestions", suggestionsObject);
            propertiesObject.Add("additionalProperties", false);

            summaryObject.Add("description", "The summary of the journal entries in paragraph format");
            summaryObject.Add("type", "string");

            suggestionsObject.Add("description", "The talking points that you suggest should be brought up in the next therapy session in paragraph format");
            suggestionsObject.Add("type", "string");

            schemaObject.Add("type", "object");
            schemaObject.Add("properties", propertiesObject);
            schemaObject.Add("additionalProperties", false);

            jsonSchemaObject.Add("name", "summary_schema");
            jsonSchemaObject.Add("schema", schemaObject);

            responseFormatObject.Add("type", "json_schema");
            responseFormatObject.Add("json_schema", jsonSchemaObject);

            Console.WriteLine(responseFormatObject.ToJsonString());

            return responseFormatObject;
        }
    }
}

using LetMeKnowAPI;
using LetMeKnowAPI.Controllers;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        var openAiService = new OpenAIService(new OpenAiOptions()
        {
            ApiKey =  "Your_Key_Goes_HERE"
        });
        var input =
            "Once upon a time, in a small village, there lived a poor woodcutter named Jack. Jack lived with his wife and two children in a small hut on the edge of the village. Despite his hard work and long hours, Jack could never seem to make ends meet. His family was always hungry and cold, and Jack felt like he was failing them.One day, while cutting wood in the forest, Jack stumbled upon a hidden grove. In the center of the grove stood a tall, magnificent tree, with leaves that sparkled like gold in the sun. Jack was in awe of the tree and, before he knew it, he had climbed to the top.At the top of the tree, Jack met a tiny old man with a long white beard. The old man told Jack that the tree was magic and that if Jack could answer three riddles correctly, the old man would grant him one wish. Jack eagerly agreed, and the old man posed the first riddle";
        input += "\nTl;dr";
        var completionResult = await openAiService.Completions.CreateCompletion(new CompletionCreateRequest()
        {
            Prompt = input,
            Model = Models.TextDavinciV3,
            MaxTokens = 200
        });
        if (completionResult.Successful)
        {
            Console.WriteLine(completionResult.Choices.FirstOrDefault());
        }
        else
        {
            if (completionResult.Error == null)
            {
                throw new Exception("Unknown Error");
            }
            Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
        }

    }
}
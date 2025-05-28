using Microsoft.Extensions.Configuration;
using TravelGPT.Server.Models.Llm;
using TravelGPT.Server.Models.Llm.Gemini;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TravelGPT.Server.Tests.Integration;

[TestClass]
public class LlmClientIntegrationTest
{
    private readonly GeminiLlmClient _client = new(
        new ConfigurationBuilder().AddUserSecrets<LlmClientIntegrationTest>().Build()["GEMINI_API_kEY"]
            ?? throw new KeyNotFoundException("Missing Gemini integration test API key")
    );

    public static IEnumerable<object[]> ValidRequests =>
    [
        [
            new LlmRequest {
                Instructions = [],
                Messages = [
                    new LlmMessage() {
                        Text = "hey",
                        Role = LlmMessageRole.User
                    }
                ]
            }
        ],
        [
            new LlmRequest {
                Instructions = ["foo"],
                Messages = [
                    new LlmMessage() {
                        Text = "hey",
                        Role = LlmMessageRole.User
                    },
                    new LlmMessage() {
                        Text = "hello wworld",
                        Role = LlmMessageRole.User
                    }
                ]
            }
        ],
        [
            new LlmRequest {
                Instructions = ["foo bar", "baz"],
                Messages = [
                    new LlmMessage() {
                        Text = "hey",
                        Role = LlmMessageRole.User
                    },
                    new LlmMessage() {
                        Text = "hello world",
                        Role = LlmMessageRole.Model
                    },
                    new LlmMessage() {
                        Text = "hey? how are you",
                        Role = LlmMessageRole.User
                    }
                ]
            }
        ]
    ];


    public static IEnumerable<object[]> InvalidRequests =>
    [
        [
            new LlmRequest {
                Instructions = [],
                Messages = [
                    new LlmMessage() {
                        Text = "",
                        Role = LlmMessageRole.Model
                    },
                ]
            }
        ],
        [
            new LlmRequest {
                Instructions = [""],
                Messages = [
                    new LlmMessage() {
                        Text = "",
                        Role = LlmMessageRole.Model
                    },
                    new LlmMessage() {
                        Text = "\n",
                        Role = LlmMessageRole.User
                    }
                ]
            }
        ],
        [
            new LlmRequest {
                Instructions = ["\n \n", "`"],
                Messages = [
                    new LlmMessage() {
                        Text = "",
                        Role = LlmMessageRole.Model
                    },
                    new LlmMessage() {
                        Text = "\n",
                        Role = LlmMessageRole.Model
                    },
                    new LlmMessage() {
                        Text = "\n `~@",
                        Role = LlmMessageRole.User
                    },
                ]
            }
        ]
    ];

    [DataTestMethod]
    [DynamicData(nameof(ValidRequests))]
    public void Fetch_ReturnsValidResponse_WhenCalledWithValidRequest(LlmRequest request)
    {
        _client.FetchResponse(request);
    }

    [DataTestMethod]
    [DynamicData(nameof(InvalidRequests))]
    public void Fetch_ThrowsException_WhenCalledWithInvalidRequest(LlmRequest request)
    {
        try
        {
            _client.FetchResponse(request);
        }
        catch (Exception)
        {
            return;
        }

        Assert.Fail("Successfully parsed invalid gemini request. Api changes?");
    }
}
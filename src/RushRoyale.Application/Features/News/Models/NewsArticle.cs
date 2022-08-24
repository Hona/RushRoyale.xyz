using System.Text.Json.Serialization;

namespace RushRoyale.Application.Features.News.Models;

    public class ChineseSimplified
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }

    public class ChineseTraditional
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }

    public class Content
    {
        [JsonPropertyName("English")]
        public English English { get; set; }

        [JsonPropertyName("Italian")]
        public Italian Italian { get; set; }

        [JsonPropertyName("Russian")]
        public Russian Russian { get; set; }

        [JsonPropertyName("French")]
        public French French { get; set; }

        [JsonPropertyName("Portuguese")]
        public Portuguese Portuguese { get; set; }

        [JsonPropertyName("ChineseTraditional")]
        public ChineseTraditional ChineseTraditional { get; set; }

        [JsonPropertyName("Korean")]
        public Korean Korean { get; set; }

        [JsonPropertyName("German")]
        public German German { get; set; }

        [JsonPropertyName("Spanish")]
        public Spanish Spanish { get; set; }

        [JsonPropertyName("Japanese")]
        public Japanese Japanese { get; set; }

        [JsonPropertyName("Turkish")]
        public Turkish Turkish { get; set; }

        [JsonPropertyName("ChineseSimplified")]
        public ChineseSimplified ChineseSimplified { get; set; }
    }

    public class ContentHTML
    {
        [JsonPropertyName("Russian")]
        public Russian Russian { get; set; }

        [JsonPropertyName("English")]
        public English English { get; set; }

        [JsonPropertyName("French")]
        public French French { get; set; }

        [JsonPropertyName("German")]
        public German German { get; set; }

        [JsonPropertyName("Spanish")]
        public Spanish Spanish { get; set; }

        [JsonPropertyName("Italian")]
        public Italian Italian { get; set; }

        [JsonPropertyName("Turkish")]
        public Turkish Turkish { get; set; }

        [JsonPropertyName("Portuguese")]
        public Portuguese Portuguese { get; set; }

        [JsonPropertyName("Korean")]
        public Korean Korean { get; set; }

        [JsonPropertyName("ChineseSimplified")]
        public ChineseSimplified ChineseSimplified { get; set; }

        [JsonPropertyName("Japanese")]
        public Japanese Japanese { get; set; }

        [JsonPropertyName("ChineseTraditional")]
        public ChineseTraditional ChineseTraditional { get; set; }
    }

    public class English
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }

    public class French
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }

    public class German
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }

    public class Italian
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }

    public class Japanese
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }

    public class Korean
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }

    public class Portuguese
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }

    public class NewsArticle
    {
        [JsonPropertyName("type")]
        public NewsType Type { get; set; }

        [JsonPropertyName("time")]
        public int? Time { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("banner")]
        public string Banner { get; set; }

        [JsonPropertyName("preview")]
        public bool Preview { get; set; }

        [JsonPropertyName("facebook")]
        public string Facebook { get; set; }

        [JsonPropertyName("vk")]
        public string Vk { get; set; }

        [JsonPropertyName("discord")]
        public string Discord { get; set; }

        [JsonPropertyName("contentHTML")]
        public ContentHTML ContentHTML { get; set; }

        [JsonPropertyName("content")]
        public Content Content { get; set; }
    }

public enum NewsType
{
    VersionUpdate = 0,
    ImportantInformationRussianOnly = 1,
    NewSeason = 2
}

    public class Russian
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }

    public class Spanish
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }

    public class Turkish
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }


using Blake.Types;
namespace Blake.Generated;
public static partial class GeneratedContentIndex
{
    public static partial List<PageModel> GetPages() => new()
    {
        new PageModel
        {
            Id = @"b2a80d58-563c-49ff-86a9-3b297684af8d",
            Title = @"My first test page",
            Slug = @"/pages/samplepage",
            Description = @"Get to know the fundamentals of Blake, the static site generator.",
            Date = new DateTime(2025, 7, 16),
            Draft = false,
            IconIdentifier = @"bi bi-plus-square-fill-nav-menu",
            Tags = new List<string> { "non-technical", "personal", "career", "community" },
            Image = @"images/blake-logo.png",
            Metadata = new Dictionary<string, string>
            {
            }
        },
    };
}

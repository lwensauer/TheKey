using MediatR;

namespace TheKey.Backend.Blog.Commands;

public record NewBlogEntryCommand(int Id, string Title, string Content) : IRequest<bool>
{
}

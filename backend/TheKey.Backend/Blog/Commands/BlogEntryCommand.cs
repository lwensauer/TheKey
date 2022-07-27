using MediatR;

namespace TheKey.Backend.Blog.Commands;

public record BlogEntryCommand(int Id, string Title, string Content) : IRequest<bool>
{
}

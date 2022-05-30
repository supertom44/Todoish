using ServiceStack;
using ServiceStack.Model;
using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace Todoish.ServiceModel;

[Tag("todos")]
[Route("/todos", "GET")]
public class QueryTodos : QueryDb<Todo>
{
    public int? Id { get; set; }
    public List<long>? Ids { get; set; }
    public string? TextContains { get; set; }
}

[Tag("todos")]
[Route("/todos", "POST")]
public class CreateTodo : ICreateDb<Todo>, IReturn<Todo>
{
    [ValidateNotEmpty]
    public string Text { get; set; }
}

[Tag("todos")]
[Route("/todos/{Id}", "PUT")]
public class UpdateTodo : IUpdateDb<Todo>, IReturn<Todo>
{
    public long Id { get; set; }
    [ValidateNotEmpty]
    public string Text { get; set; }
    public bool IsFinished { get; set; }
}

[Tag("todos")]
[Route("/todos/{Id}", "DELETE")]
public class DeleteTodo : IDeleteDb<Todo>, IReturnVoid
{
    public long Id { get; set; }
}

[Tag("todos")]
[Route("/todos", "DELETE")]
public class DeleteTodos : IDeleteDb<Todo>, IReturnVoid
{
    public List<long> Ids { get; set; }
}

public class Todo : IHasId<long>
{
    [AutoIncrement]
    public long Id { get; set; }
    public string Text { get; set; }
    public bool IsFinished { get; set; }
}

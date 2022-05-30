using System;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Todoish.ServiceModel;

[Description("Task Details")]
public class TodoTask
{
    [AutoIncrement]
    public int Id { get; set; }
    public string Title { get; set; }
    public bool Done { get; set; }
    public DateTime DueDate { get; set; }
}

[Tag("task"), Description("Get All Tasks")]
[Route("/task", "GET")]
[Route("/task/{Id}", "GET")]
public class QueryTask : QueryDb<TodoTask>
{
    public int? Id { get; set; }
    public bool? Done { get; set; }
    public DateTime? DueDate { get; set; }
}

[Tag("task"), Description("Update Task")]
[Route("/task/{Id}", "POST")]
public class UpdateTask : IUpdateDb<TodoTask>, IReturnVoid
{
    public int? Id { get; set; }
    public string Title { get; set; }
    public bool Done { get; set; }
    public DateTime DueDate { get; set; }
}

[Tag("task")]
[Route("/task", "POST")]
public class CreateTask : ICreateDb<TodoTask>, IReturn<TodoTask>
{
    [ValidateNotEmpty]
    public string Title { get; set; }
    public bool Done { get; set; }
    public DateTime DueDate { get; set; }
}

using System;
using ServiceStack;

namespace Todoish.ServiceModel;

[Route("/duedate")]
public class DueDate : IReturn<DueDateResponse>
{
    public string Title { get; set; }   
}

public class DueDateResponse
{
    public DateTime? Result { get; set; }
    public ResponseStatus ResponseStatus { get; set; }
}
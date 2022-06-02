using System;
using ServiceStack;
using Todoish.ServiceModel;

namespace Todoish.ServiceInterface;

public class DueDateService : Service
{
    public object Any(DueDate request)
    {
        if (request.Title.Contains(" tom", StringComparison.OrdinalIgnoreCase))
        {
            return new DueDateResponse
            {
                Result = DateTime.Today + TimeSpan.FromDays(1)
            };
        }

        return new DueDateResponse();
    }
}
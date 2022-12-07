using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class BlNullPropertyException: Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
}

public class BlWrongCategoryException : Exception
{
    public BlWrongCategoryException(string? message) : base(message) { }
}

public class BlMissingEntityException:Exception
{
    public BlMissingEntityException(string? message, DO.DalMissingIdException ex) : base(message, ex) { }
}

public class BlProductDoesNotExsist:Exception
{
    public BlProductDoesNotExsist(string? message, DO.DalDoesNotExistException ex) : base(message, ex) { }
}

public class BlOrderDoesNotExsist : Exception
{
    public BlOrderDoesNotExsist(string? message, DO.DalDoesNotExistException ex) : base(message, ex) { }
}

public class BlOrderAlreadyDelivered : Exception
{
    public BlOrderAlreadyDelivered(string? message) : base(message) { }
}

public class BlClientDeatalesNotValid : Exception
{
    public BlClientDeatalesNotValid(string? message) : base(message) { }
}

public class BlIdAlreadyExistException : Exception
{
    public BlIdAlreadyExistException(string? message) : base(message) { }
}

public class BlIncorrectDateException : Exception
{
    public BlIncorrectDateException(string? message) : base(message) { }
}

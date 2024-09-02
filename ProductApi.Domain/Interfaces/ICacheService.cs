using System;
namespace ProductApi.Domain.Interfaces
{
    public interface ICacheService
    {
        string GetStatusName(int status);
    }
}


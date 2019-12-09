using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateNotator.Application.ApplicationServices
{
    public abstract class BaseService
    {
        public ServiceResult<T> Result<T>(T data) => new ServiceResult<T>(data);
        public ServiceResult<T> Result<T>(Exception exception) => new ServiceResult<T>(exception);
        public ServiceResult Result() => new ServiceResult();
        public ServiceResult Result(Exception exception) => new ServiceResult(exception);
    }
}

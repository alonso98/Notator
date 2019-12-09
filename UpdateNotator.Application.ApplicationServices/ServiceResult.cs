using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateNotator.Application.ApplicationServices
{
    public class ServiceResult
    {
        private bool isSuccessed;
        public bool IsSuccessed { get => isSuccessed; }
        public Exception Exception { get; protected set; }

        internal ServiceResult(Exception exception)
        {
            this.Exception = exception;
            this.isSuccessed = false;
        }

        internal ServiceResult()
        {
            this.isSuccessed = true;
        }
    }

    public class ServiceResult<TClass> : ServiceResult
    {
        public TClass Data { get; protected set; }

        internal ServiceResult(TClass data) : base()
        {
            Data = data;
        }

        internal ServiceResult(Exception exception) : base(exception) {}
    }
}

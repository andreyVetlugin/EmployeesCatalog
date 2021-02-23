using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeesCatalog.Dal.ResultTypes
{
    public enum ErrorType
    {
        None,
        InvalidForm,
        InvalidInnerState,
        NotFound
    }

    public interface IOperationResult
    { }  // признак присутствтя ошибки и то как конвертировать в ответ ??
    public class OperationResult : IOperationResult
    {
        public bool Ok => ErrorMessage == null;
        public ErrorType ErrorType { get; }
        public string ErrorMessage { get; }

        public static OperationResult BuildSuccess()
        {
            return new OperationResult(ErrorType.None, null);
        }

        public static OperationResult BuildInnerStateError(string error)
        {
            return new OperationResult(ErrorType.InvalidInnerState, error);
        }

        public static OperationResult BuildNotFoundError(string error)
        {
            return new OperationResult(ErrorType.NotFound, error);
        }

        public static OperationResult BuildFormError(string error)
        {
            return new OperationResult(ErrorType.InvalidForm, error);
        }

        public static OperationResult BuildFromOperationResult<T>(OperationResult<T> inputOperationResult)
        {
            if (inputOperationResult.Ok)
                return OperationResult.BuildSuccess();
            return OperationResult.BuildFromOperationWithError(inputOperationResult);
        }

        public static OperationResult BuildFromOperationWithError(OperationResult resultWithError)
        {
            return new OperationResult(resultWithError.ErrorType, resultWithError.ErrorMessage);
        }

        public static OperationResult BuildFromOperationWithError<T>(OperationResult<T> resultWithError)
        {
            return new OperationResult(resultWithError.ErrorType, resultWithError.ErrorMessage);
        }

        private OperationResult(ErrorType errorType, string errorMessage)
        {
            ErrorType = errorType;
            ErrorMessage = errorMessage;
        }
    }

    public class OperationResult<TResultModel> : IOperationResult
    {
        public bool Ok => ErrorMessage == null;
        public ErrorType ErrorType { get; }
        public string ErrorMessage { get; }
        public TResultModel ResultModel { get; }

        public OperationResult<TNewModel> BuildWithChangeData<TNewModel>(Func<TResultModel, TNewModel> createNewResultModel)
        {
            return new OperationResult<TNewModel>(ErrorType, ErrorMessage, createNewResultModel(ResultModel));
        }
        public static OperationResult<TResultModel> BuildSuccess(TResultModel resultModel)
        {
            return new OperationResult<TResultModel>(ErrorType.None, null, resultModel);
        }

        public static OperationResult<TResultModel> BuildInnerStateError(string error)
        {
            return new OperationResult<TResultModel>(ErrorType.InvalidInnerState, error,
                default(TResultModel));
        }

        public static OperationResult<TResultModel> BuildNotFoundError(string error)
        {
            return new OperationResult<TResultModel>(ErrorType.NotFound, error, default(TResultModel));
        }

        public static OperationResult<TResultModel> BuildFormError(string error)
        {
            return new OperationResult<TResultModel>(ErrorType.InvalidForm, error, default(TResultModel));
        }

        public static OperationResult<TResultModel> BuilFromOperationResult(OperationResult<TResultModel> inputOpertaionResult)
        {
            if (!inputOpertaionResult.Ok)
                OperationResult<TResultModel>.BuildFromOperationWithError(inputOpertaionResult);
            return OperationResult<TResultModel>.BuildSuccess(inputOpertaionResult.ResultModel);
        }

        public static OperationResult<TResultModel> BuildFromOperationWithError<T>(OperationResult<T> opertaionResult)
        {
            return new OperationResult<TResultModel>(opertaionResult.ErrorType, opertaionResult.ErrorMessage, default(TResultModel));
        }

        private OperationResult(ErrorType errorType, string errorMessage, TResultModel resultModel)
        {
            ErrorType = errorType;
            ErrorMessage = errorMessage;
            ResultModel = resultModel;
        }
    }
}

namespace _4EPlatform_StrategicTelecom_Aggregator.ResponseDTO
{
    public interface IResponseHelper
    {
        ResponseDto SavedSuccessfully(string message);
        ResponseDto Exception();
        ResponseDto FailedToSave(string message);
        ResponseDto NotFound(string message);
        ResponseDto AlreadyExists(string message);
        ResponseDto RetrievedSuccessfully(dynamic result, string message);
        ResponseDto RetrievedSuccessfully(dynamic result, string message, ref ResponseDto responseDto);
        ResponseDto RetrievedSuccessfully(dynamic result, string message,int totalcount);
        ResponseDto SavedSuccessfully(dynamic result, string message);
    }
}
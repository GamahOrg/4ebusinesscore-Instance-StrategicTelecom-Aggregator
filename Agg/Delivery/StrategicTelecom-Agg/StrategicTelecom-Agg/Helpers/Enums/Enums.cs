namespace _4EPlatform_StrategicTelecom_Aggregator.Helpers.Enums
{
    public enum State
    {
        NotDeleted,
        Deleted
    }
    public enum StatusEnum
    {
        FailedToSave,
        SavedSuccessfully,
        Exception,
        Success,
        Failed,
        FailedToFindTheObject,
        AlreadyExisting
    }

    public enum Avalible_for
    {
        user,
        org
    }

}
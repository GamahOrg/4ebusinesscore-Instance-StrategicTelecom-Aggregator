

using _4EPlatform.Application.Protos;
using _4EPlatform_StrategicTelecom_Aggregator.Dto.Medals.GetAllMedalsByCategoryId;
using StrategicTelecom_Aggregator.API.Protos;

namespace _4EPlatform_StrategicTelecom_Aggregator.Dto.Medals.GetAllCauseStages
{
    public class GetAllMedalbyCategoryIdMapping
    {
        public static AllMedalsDto GetAllMedalbyCategoryId(GetAllMedalbyCategoryResponse medals, ResponseUserDataList userProto)
        {
            if (medals == null) return null;

            return new AllMedalsDto
            {
                Medals = MapCauseStages(medals.AllMedals, userProto),
            };

        }
        private static List<MedalsByCategoryIdDto> MapCauseStages(IEnumerable<GetAllMedalbyCategoryIdProto> stages, ResponseUserDataList userProto)
        {
            return stages?.Select(x => new MedalsByCategoryIdDto
            {
                Id = ParseGuid(x.Id),
                CreatedOn = ParseDateTime(x.CreatedOn),
                Name = x.Name ?? "",
                IsDraft = x.IsDraft,
                Owner = ParseGuid(x.Owner),
                TotalMedalsCount = x.TotalMedalsCount,
                GivingTime = x.GivingTime ?? "",

                OwnerFullName = GetUserFullName(x.Owner, userProto),
            }).ToList() ?? new List<MedalsByCategoryIdDto>();
        }

        private static Guid ParseGuid(string input)
        {
            return Guid.TryParse(input, out var result) ? result : Guid.Empty;
        }

        private static DateTime ParseDateTime(string input)
        {
            return DateTime.TryParse(input, out var result) ? result : DateTime.MinValue;
        }

        private static string GetUserFullName(string userId, ResponseUserDataList userProto)
        {
            var user = userProto?.UserData
                .FirstOrDefault(user => user.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase));

            return user != null
                ? $"{user.FirstName} {user.SecondName} {user.ThirdName} {user.FourthName}"
                : "";
        }



    }

}


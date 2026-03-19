using Mapster;
using Application.DTOs.Incident;
using Infrastructure.DTOs;
using Domain.Enums;

namespace Infrastructure.Mapping;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.NewConfig<IncidentResponse, IncidentDto>()
            .Map(dest => dest.Id, src => src.RecId)
            .Map(dest => dest.Number, src => src.IncidentNumber.ToString())
            .Map(dest => dest.Subject, src => src.Subject)
            .Map(dest => dest.Description, src => src.Symptom)
            .Map(dest => dest.Priority, src => src.Priority)
            .Map(dest => dest.Service, src => src.Service)
            .Map(dest => dest.Category, src => src.Category)
            .Map(dest => dest.Urgency, src => src.Urgency)
            .Map(dest => dest.Impact, src => src.Impact)
            .Map(dest => dest.Owner, src => src.Owner)
            .Map(dest => dest.OwnerTeam, src => src.OwnerTeam)

            // FIXED LINE
            .Map(dest => dest.Status,
                 src => ParseStatus(src.Status));
    }

    // 👇 Expression-tree safe method
    private static IncidentStatus ParseStatus(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return IncidentStatus.Open;

        if (Enum.TryParse(typeof(IncidentStatus), value, true, out var result))
            return (IncidentStatus)result;

        return IncidentStatus.Open;
    }
}
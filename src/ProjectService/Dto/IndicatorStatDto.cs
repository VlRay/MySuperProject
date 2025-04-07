namespace ProjectService.Dto;

public class IndicatorStatDto
{
    public string Name { get; set; } = "";
    public int Used { get; set; }
}

public class PopularIndicatorsResponse
{
    public List<IndicatorStatDto> Indicators { get; set; } = new();
}

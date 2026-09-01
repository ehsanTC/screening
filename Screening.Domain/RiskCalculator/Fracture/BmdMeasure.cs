namespace Screening.Domain.RiskCalculator.Fracture;

public readonly record struct BmdMeasure(string Name, float EquipmentNumber, string Unit = "g/cm2")
{
    public static readonly BmdMeasure GE_Lunar = new("GE-Lunar", 1);
    public static readonly BmdMeasure Hologic = new("Hologic", 2);
    public static readonly BmdMeasure Norland = new("Norland", 3);
    public static readonly BmdMeasure T_score = new("T-score", 4, "t");
    public static readonly BmdMeasure DMS_Medilink = new("DMS/Medilink", 5);
    public static readonly BmdMeasure IMex_Medical = new("IMex/Medical", 5.1F);
    public static readonly BmdMeasure Mindways_QCT = new("Mindways/QCT", 6);
    public static readonly BmdMeasure Osteosys = new("Osteosys", 7);

    public static IEnumerable<BmdMeasure> All =>
    [
        GE_Lunar, Hologic, Norland, T_score,
        DMS_Medilink, IMex_Medical, Mindways_QCT, Osteosys
    ];
}

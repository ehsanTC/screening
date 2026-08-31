using Screening.Domain.RiskCalculator.Models;

namespace Screening.Domain.RiskCalculator.Ascvd;

internal static class PreventCoefficientTable
{
    public static readonly IReadOnlyDictionary<(PreventModel, Sex, Outcome), PreventCoefficients> Data =
        new Dictionary<(PreventModel, Sex, Outcome), PreventCoefficients>
        {
            [(PreventModel.Base, Sex.Female, Outcome.TenYearCvd)] = new PreventCoefficients
            {
                Intercept = -3.307728, Age = 0.7939329, AgeSq = 0.0, NonHdl = 0.0305239, Hdl = -0.1606857,
                SbpMin = -0.2394003, SbpMax = 0.360078, Dm = 0.8667604, Smk = 0.5360739, EgfrMin = 0.6045917,
                EgfrMax = 0.0433769, BpTreat = 0.3151672, Statin = -0.1477655, BpTreatSbp = -0.0663612,
                StatinNonHdl = 0.1197879, AgeNonHdl = -0.0819715, AgeHdl = 0.0306769, AgeSbp = -0.0946348,
                AgeDm = -0.27057, AgeSmk = -0.078715, AgeEgfr = -0.1637806, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0, HbA1cC2 = 0.0,
                HbA1cMiss = 0.0
            },
            [(PreventModel.Base, Sex.Female, Outcome.TenYearAscvd)] = new PreventCoefficients
            {
                Intercept = -3.819975, Age = 0.719883, AgeSq = 0.0, NonHdl = 0.1176967, Hdl = -0.151185,
                SbpMin = -0.0835358, SbpMax = 0.3592852, Dm = 0.8348585, Smk = 0.4831078, EgfrMin = 0.4864619,
                EgfrMax = 0.0397779, BpTreat = 0.2265309, Statin = -0.0592374, BpTreatSbp = -0.0395762,
                StatinNonHdl = 0.0844423, AgeNonHdl = -0.0567839, AgeHdl = 0.0325692, AgeSbp = -0.1035985,
                AgeDm = -0.2417542, AgeSmk = -0.0791142, AgeEgfr = -0.1671492, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0, HbA1cC2 = 0.0,
                HbA1cMiss = 0.0
            },
            [(PreventModel.Base, Sex.Female, Outcome.TenYearHf)] = new PreventCoefficients
            {
                Intercept = -4.310409, Age = 0.8998235, AgeSq = 0.0, NonHdl = 0.0, Hdl = 0.0, SbpMin = -0.4559771,
                SbpMax = 0.3576505, Dm = 1.038346, Smk = 0.583916, EgfrMin = 0.7451638, EgfrMax = 0.0557087,
                BpTreat = 0.3534442, Statin = 0.0, BpTreatSbp = -0.0981511, StatinNonHdl = 0.0, AgeNonHdl = 0.0,
                AgeHdl = 0.0, AgeSbp = -0.0946663, AgeDm = -0.3581041, AgeSmk = -0.1159453, AgeEgfr = -0.1884289,
                BmiMin = -0.0072294, BmiMax = 0.2997706, AgeBmi = -0.003878, SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0,
                UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0, HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Base, Sex.Female, Outcome.ThirtyYearCvd)] = new PreventCoefficients
            {
                Intercept = -1.318827, Age = 0.5503079, AgeSq = -0.0928369, NonHdl = 0.0409794, Hdl = -0.1663306,
                SbpMin = -0.1628654, SbpMax = 0.3299505, Dm = 0.6793894, Smk = 0.3196112, EgfrMin = 0.1857101,
                EgfrMax = 0.0553528, BpTreat = 0.2894, Statin = -0.075688, BpTreatSbp = -0.056367,
                StatinNonHdl = 0.1071019, AgeNonHdl = -0.0751438, AgeHdl = 0.0301786, AgeSbp = -0.0998776,
                AgeDm = -0.3206166, AgeSmk = -0.1607862, AgeEgfr = -0.1450788, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0, HbA1cC2 = 0.0,
                HbA1cMiss = 0.0
            },
            [(PreventModel.Base, Sex.Female, Outcome.ThirtyYearAscvd)] = new PreventCoefficients
            {
                Intercept = -1.974074, Age = 0.4669202, AgeSq = -0.0893118, NonHdl = 0.1256901, Hdl = -0.1542255,
                SbpMin = -0.0018093, SbpMax = 0.322949, Dm = 0.6296707, Smk = 0.268292, EgfrMin = 0.100106,
                EgfrMax = 0.0499663, BpTreat = 0.1875292, Statin = 0.0152476, BpTreatSbp = -0.0276123,
                StatinNonHdl = 0.0736147, AgeNonHdl = -0.0521962, AgeHdl = 0.0316918, AgeSbp = -0.1046101,
                AgeDm = -0.2727793, AgeSmk = -0.1530907, AgeEgfr = -0.1299149, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0, HbA1cC2 = 0.0,
                HbA1cMiss = 0.0
            },
            [(PreventModel.Base, Sex.Female, Outcome.ThirtyYearHf)] = new PreventCoefficients
            {
                Intercept = -2.205379, Age = 0.6254374, AgeSq = -0.0983038, NonHdl = 0.0, Hdl = 0.0,
                SbpMin = -0.3919241, SbpMax = 0.3142295, Dm = 0.8330787, Smk = 0.3438651, EgfrMin = 0.2981642,
                EgfrMax = 0.0667159, BpTreat = 0.333921, Statin = 0.0, BpTreatSbp = -0.0893177, StatinNonHdl = 0.0,
                AgeNonHdl = 0.0, AgeHdl = 0.0, AgeSbp = -0.0974299, AgeDm = -0.404855, AgeSmk = -0.1982991,
                AgeEgfr = -0.1564215, BmiMin = 0.0594874, BmiMax = 0.2525536, AgeBmi = -0.0035619, SdiC1 = 0.0,
                SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0, HbA1cC2 = 0.0,
                HbA1cMiss = 0.0
            },
            [(PreventModel.Base, Sex.Male, Outcome.TenYearCvd)] = new PreventCoefficients
            {
                Intercept = -3.031168, Age = 0.7688528, AgeSq = 0.0, NonHdl = 0.0736174, Hdl = -0.0954431,
                SbpMin = -0.4347345, SbpMax = 0.3362658, Dm = 0.7692857, Smk = 0.4386871, EgfrMin = 0.5378979,
                EgfrMax = 0.0164827, BpTreat = 0.288879, Statin = -0.1337349, BpTreatSbp = -0.0475924,
                StatinNonHdl = 0.150273, AgeNonHdl = -0.0517874, AgeHdl = 0.0191169, AgeSbp = -0.1049477,
                AgeDm = -0.2251948, AgeSmk = -0.0895067, AgeEgfr = -0.1543702, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0, HbA1cC2 = 0.0,
                HbA1cMiss = 0.0
            },
            [(PreventModel.Base, Sex.Male, Outcome.TenYearAscvd)] = new PreventCoefficients
            {
                Intercept = -3.500655, Age = 0.7099847, AgeSq = 0.0, NonHdl = 0.1658663, Hdl = -0.1144285,
                SbpMin = -0.2837212, SbpMax = 0.3239977, Dm = 0.7189597, Smk = 0.3956973, EgfrMin = 0.3690075,
                EgfrMax = 0.0203619, BpTreat = 0.2036522, Statin = -0.0865581, BpTreatSbp = -0.0322916,
                StatinNonHdl = 0.114563, AgeNonHdl = -0.0300005, AgeHdl = 0.0232747, AgeSbp = -0.0927024,
                AgeDm = -0.2018525, AgeSmk = -0.0970527, AgeEgfr = -0.1217081, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0, HbA1cC2 = 0.0,
                HbA1cMiss = 0.0
            },
            [(PreventModel.Base, Sex.Male, Outcome.TenYearHf)] = new PreventCoefficients
            {
                Intercept = -3.946391, Age = 0.8972642, AgeSq = 0.0, NonHdl = 0.0, Hdl = 0.0, SbpMin = -0.6811466,
                SbpMax = 0.3634461, Dm = 0.923776, Smk = 0.5023736, EgfrMin = 0.6926917, EgfrMax = 0.0251827,
                BpTreat = 0.2980922, Statin = 0.0, BpTreatSbp = -0.0497731, StatinNonHdl = 0.0, AgeNonHdl = 0.0,
                AgeHdl = 0.0, AgeSbp = -0.1289201, AgeDm = -0.3040924, AgeSmk = -0.1401688, AgeEgfr = -0.1797778,
                BmiMin = -0.0485841, BmiMax = 0.3726929, AgeBmi = 0.0068126, SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0,
                UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0, HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Base, Sex.Male, Outcome.ThirtyYearCvd)] = new PreventCoefficients
            {
                Intercept = -1.148204, Age = 0.4627309, AgeSq = -0.0984281, NonHdl = 0.0836088, Hdl = -0.1029824,
                SbpMin = -0.2140352, SbpMax = 0.2904325, Dm = 0.5331276, Smk = 0.2141914, EgfrMin = 0.1155556,
                EgfrMax = 0.0603775, BpTreat = 0.232714, Statin = -0.0272112, BpTreatSbp = -0.0384488,
                StatinNonHdl = 0.134192, AgeNonHdl = -0.0511759, AgeHdl = 0.0165865, AgeSbp = -0.1101437,
                AgeDm = -0.2585943, AgeSmk = -0.1566406, AgeEgfr = -0.1166776, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0, HbA1cC2 = 0.0,
                HbA1cMiss = 0.0
            },
            [(PreventModel.Base, Sex.Male, Outcome.ThirtyYearAscvd)] = new PreventCoefficients
            {
                Intercept = -1.736444, Age = 0.3994099, AgeSq = -0.0937484, NonHdl = 0.1744643, Hdl = -0.120203,
                SbpMin = -0.0665117, SbpMax = 0.2753037, Dm = 0.4790257, Smk = 0.1782635, EgfrMin = -0.0218789,
                EgfrMax = 0.0602553, BpTreat = 0.1421182, Statin = 0.0135996, BpTreatSbp = -0.0218265,
                StatinNonHdl = 0.1013148, AgeNonHdl = -0.0312619, AgeHdl = 0.020673, AgeSbp = -0.0920935,
                AgeDm = -0.2159947, AgeSmk = -0.1548811, AgeEgfr = -0.0712547, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0, HbA1cC2 = 0.0,
                HbA1cMiss = 0.0
            },
            [(PreventModel.Base, Sex.Male, Outcome.ThirtyYearHf)] = new PreventCoefficients
            {
                Intercept = -1.95751, Age = 0.5681541, AgeSq = -0.1048388, NonHdl = 0.0, Hdl = 0.0, SbpMin = -0.4761564,
                SbpMax = 0.30324, Dm = 0.6840338, Smk = 0.2656273, EgfrMin = 0.2541805, EgfrMax = 0.0638923,
                BpTreat = 0.2583631, Statin = 0.0, BpTreatSbp = -0.0391938, StatinNonHdl = 0.0, AgeNonHdl = 0.0,
                AgeHdl = 0.0, AgeSbp = -0.1269124, AgeDm = -0.3273572, AgeSmk = -0.2043019, AgeEgfr = -0.1342618,
                BmiMin = 0.0833107, BmiMax = 0.26999, AgeBmi = -0.0182831, SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0,
                UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0, HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Uacr, Sex.Female, Outcome.TenYearCvd)] = new PreventCoefficients
            {
                Intercept = -3.738341, Age = 0.7969249, AgeSq = 0.0, NonHdl = 0.0256635, Hdl = -0.1588107,
                SbpMin = -0.2255701, SbpMax = 0.3396649, Dm = 0.8047515, Smk = 0.5285338, EgfrMin = 0.4803511,
                EgfrMax = 0.0434472, BpTreat = 0.2985207, Statin = -0.1497787, BpTreatSbp = -0.0742889,
                StatinNonHdl = 0.106756, AgeNonHdl = -0.0778126, AgeHdl = 0.0306768, AgeSbp = -0.0907168,
                AgeDm = -0.2705122, AgeSmk = -0.0830564, AgeEgfr = -0.1389249, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.1793037, UacrMiss = 0.0132073, HbA1cC1 = 0.0,
                HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Uacr, Sex.Female, Outcome.TenYearAscvd)] = new PreventCoefficients
            {
                Intercept = -4.174614, Age = 0.7201999, AgeSq = 0.0, NonHdl = 0.1135771, Hdl = -0.1493506,
                SbpMin = -0.0726677, SbpMax = 0.3436259, Dm = 0.7773094, Smk = 0.4746662, EgfrMin = 0.3824646,
                EgfrMax = 0.0394178, BpTreat = 0.2125182, Statin = -0.0603046, BpTreatSbp = -0.0466053,
                StatinNonHdl = 0.0733118, AgeNonHdl = -0.0534262, AgeHdl = 0.0325689, AgeSbp = -0.0999887,
                AgeDm = -0.2411762, AgeSmk = -0.0826941, AgeEgfr = -0.1444737, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.1501217, UacrMiss = 0.0050257, HbA1cC1 = 0.0,
                HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Uacr, Sex.Female, Outcome.TenYearHf)] = new PreventCoefficients
            {
                Intercept = -4.841506, Age = 0.9145975, AgeSq = 0.0, NonHdl = 0.0, Hdl = 0.0, SbpMin = -0.4441346,
                SbpMax = 0.3260323, Dm = 0.9611365, Smk = 0.5755787, EgfrMin = 0.5915291, EgfrMax = 0.0556823,
                BpTreat = 0.3314097, Statin = 0.0, BpTreatSbp = -0.1078596, StatinNonHdl = 0.0, AgeNonHdl = 0.0,
                AgeHdl = 0.0, AgeSbp = -0.0875231, AgeDm = -0.356859, AgeSmk = -0.1220248, AgeEgfr = -0.1610389,
                BmiMin = 0.0008831, BmiMax = 0.2988964, AgeBmi = -0.0053637, SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0,
                UacrCoef = 0.2197281, UacrMiss = 0.0326667, HbA1cC1 = 0.0, HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Uacr, Sex.Female, Outcome.ThirtyYearCvd)] = new PreventCoefficients
            {
                Intercept = -1.583738, Age = 0.5491768, AgeSq = -0.0937311, NonHdl = 0.0359847, Hdl = -0.1642965,
                SbpMin = -0.1483404, SbpMax = 0.313353, Dm = 0.6253766, Smk = 0.3147172, EgfrMin = 0.1094663,
                EgfrMax = 0.0550705, BpTreat = 0.2782433, Statin = -0.0786239, BpTreatSbp = -0.0628947,
                StatinNonHdl = 0.093204, AgeNonHdl = -0.0710685, AgeHdl = 0.0306363, AgeSbp = -0.0951455,
                AgeDm = -0.3168231, AgeSmk = -0.1636391, AgeEgfr = -0.1265483, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.1142251, UacrMiss = -0.0055863, HbA1cC1 = 0.0,
                HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Uacr, Sex.Female, Outcome.ThirtyYearAscvd)] = new PreventCoefficients
            {
                Intercept = -2.178888, Age = 0.4629669, AgeSq = -0.0902777, NonHdl = 0.1215214, Hdl = -0.1522069,
                SbpMin = 0.0092679, SbpMax = 0.3113609, Dm = 0.581256, Smk = 0.263167, EgfrMin = 0.0391726,
                EgfrMax = 0.0492959, BpTreat = 0.1786178, Statin = 0.0131058, BpTreatSbp = -0.0325135,
                StatinNonHdl = 0.0617093, AgeNonHdl = -0.0489189, AgeHdl = 0.0321079, AgeSbp = -0.1003185,
                AgeDm = -0.2684574, AgeSmk = -0.1547301, AgeEgfr = -0.1130703, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0903471, UacrMiss = -0.0145818, HbA1cC1 = 0.0,
                HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Uacr, Sex.Female, Outcome.ThirtyYearHf)] = new PreventCoefficients
            {
                Intercept = -2.538952, Age = 0.6319513, AgeSq = -0.1009284, NonHdl = 0.0, Hdl = 0.0,
                SbpMin = -0.3787175, SbpMax = 0.2863393, Dm = 0.7631221, Smk = 0.3355843, EgfrMin = 0.1940067,
                EgfrMax = 0.0664006, BpTreat = 0.3171436, Statin = 0.0, BpTreatSbp = -0.0970661, StatinNonHdl = 0.0,
                AgeNonHdl = 0.0, AgeHdl = 0.0, AgeSbp = -0.0896239, AgeDm = -0.400743, AgeSmk = -0.2042041,
                AgeEgfr = -0.13602, BmiMin = 0.0677084, BmiMax = 0.2517238, AgeBmi = -0.0054699, SdiC1 = 0.0,
                SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.1486028, UacrMiss = 0.011608, HbA1cC1 = 0.0, HbA1cC2 = 0.0,
                HbA1cMiss = 0.0
            },
            [(PreventModel.Uacr, Sex.Male, Outcome.TenYearCvd)] = new PreventCoefficients
            {
                Intercept = -3.510705, Age = 0.7768655, AgeSq = 0.0, NonHdl = 0.0659949, Hdl = -0.0951111,
                SbpMin = -0.420667, SbpMax = 0.3120151, Dm = 0.698521, Smk = 0.4314669, EgfrMin = 0.3841364,
                EgfrMax = 0.009384, BpTreat = 0.2676494, Statin = -0.1390966, BpTreatSbp = -0.0579315,
                StatinNonHdl = 0.1383719, AgeNonHdl = -0.0488332, AgeHdl = 0.0200406, AgeSbp = -0.102454,
                AgeDm = -0.2236355, AgeSmk = -0.089485, AgeEgfr = -0.1321848, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.1887974, UacrMiss = 0.0916979, HbA1cC1 = 0.0,
                HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Uacr, Sex.Male, Outcome.TenYearAscvd)] = new PreventCoefficients
            {
                Intercept = -3.85146, Age = 0.7141718, AgeSq = 0.0, NonHdl = 0.1602194, Hdl = -0.1139086,
                SbpMin = -0.2719456, SbpMax = 0.3058719, Dm = 0.6600631, Smk = 0.3884022, EgfrMin = 0.2466316,
                EgfrMax = 0.0151852, BpTreat = 0.186167, Statin = -0.0894395, BpTreatSbp = -0.0334959,
                StatinNonHdl = 0.1058212, AgeNonHdl = -0.028089, AgeHdl = 0.0240427, AgeSbp = -0.0912325,
                AgeDm = -0.2004894, AgeSmk = -0.096936, AgeEgfr = -0.1022867, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.1510073, UacrMiss = 0.0556, HbA1cC1 = 0.0,
                HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Uacr, Sex.Male, Outcome.TenYearHf)] = new PreventCoefficients
            {
                Intercept = -4.556907, Age = 0.9111795, AgeSq = 0.0, NonHdl = 0.0, Hdl = 0.0, SbpMin = -0.6693649,
                SbpMax = 0.3290082, Dm = 0.8377655, Smk = 0.4978917, EgfrMin = 0.5075796, EgfrMax = 0.0137716,
                BpTreat = 0.2739963, Statin = 0.0, BpTreatSbp = -0.0645712, StatinNonHdl = 0.0, AgeNonHdl = 0.0,
                AgeHdl = 0.0, AgeSbp = -0.1230039, AgeDm = -0.3013297, AgeSmk = -0.1410318, AgeEgfr = -0.1548018,
                BmiMin = -0.042749, BmiMax = 0.3624165, AgeBmi = 0.0021531, SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0,
                UacrCoef = 0.2306299, UacrMiss = 0.1472194, HbA1cC1 = 0.0, HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Uacr, Sex.Male, Outcome.ThirtyYearCvd)] = new PreventCoefficients
            {
                Intercept = -1.398727, Age = 0.464491, AgeSq = -0.0998895, NonHdl = 0.0757606, Hdl = -0.1031778,
                SbpMin = -0.1990714, SbpMax = 0.2715816, Dm = 0.4754637, Smk = 0.2069672, EgfrMin = 0.0331103,
                EgfrMax = 0.0540474, BpTreat = 0.2189911, Statin = -0.0331044, BpTreatSbp = -0.04534,
                StatinNonHdl = 0.1214535, AgeNonHdl = -0.0483995, AgeHdl = 0.0178997, AgeSbp = -0.1059324,
                AgeDm = -0.2492861, AgeSmk = -0.1561543, AgeEgfr = -0.1012429, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.1007571, UacrMiss = 0.0572456, HbA1cC1 = 0.0,
                HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Uacr, Sex.Male, Outcome.ThirtyYearAscvd)] = new PreventCoefficients
            {
                Intercept = -1.873449, Age = 0.3995607, AgeSq = -0.094557, NonHdl = 0.1686692, Hdl = -0.1202145,
                SbpMin = -0.0555561, SbpMax = 0.2633566, Dm = 0.4362036, Smk = 0.1716233, EgfrMin = -0.0775282,
                EgfrMax = 0.0561236, BpTreat = 0.1319331, Statin = 0.0102428, BpTreatSbp = -0.0269294,
                StatinNonHdl = 0.0920557, AgeNonHdl = -0.0297021, AgeHdl = 0.0217935, AgeSbp = -0.0893347,
                AgeDm = -0.2081467, AgeSmk = -0.1542716, AgeEgfr = -0.0597254, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0684872, UacrMiss = 0.0193962, HbA1cC1 = 0.0,
                HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Uacr, Sex.Male, Outcome.ThirtyYearHf)] = new PreventCoefficients
            {
                Intercept = -2.314872, Age = 0.5750236, AgeSq = -0.1062268, NonHdl = 0.0, Hdl = 0.0,
                SbpMin = -0.4633994, SbpMax = 0.2742874, Dm = 0.612208, Smk = 0.2614987, EgfrMin = 0.1430472,
                EgfrMax = 0.0535184, BpTreat = 0.2417468, Statin = 0.0, BpTreatSbp = -0.0498574, StatinNonHdl = 0.0,
                AgeNonHdl = 0.0, AgeHdl = 0.0, AgeSbp = -0.1193827, AgeDm = -0.316651, AgeSmk = -0.2046122,
                AgeEgfr = -0.1165637, BmiMin = 0.0895459, BmiMax = 0.2632424, AgeBmi = -0.0216878, SdiC1 = 0.0,
                SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.1366452, UacrMiss = 0.1078355, HbA1cC1 = 0.0, HbA1cC2 = 0.0,
                HbA1cMiss = 0.0
            },
            [(PreventModel.HbA1c, Sex.Female, Outcome.TenYearCvd)] = new PreventCoefficients
            {
                Intercept = -3.306162, Age = 0.7858178, AgeSq = 0.0, NonHdl = 0.0194438, Hdl = -0.1521964,
                SbpMin = -0.2296681, SbpMax = 0.3465777, Dm = 0.5366241, Smk = 0.5411682, EgfrMin = 0.5931898,
                EgfrMax = 0.0472458, BpTreat = 0.3158567, Statin = -0.1535174, BpTreatSbp = -0.0687752,
                StatinNonHdl = 0.1054746, AgeNonHdl = -0.0761119, AgeHdl = 0.0307469, AgeSbp = -0.0905966,
                AgeDm = -0.2241857, AgeSmk = -0.080186, AgeEgfr = -0.1667286, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.1338348,
                HbA1cC2 = 0.1622409, HbA1cMiss = -0.0142496
            },
            [(PreventModel.HbA1c, Sex.Female, Outcome.TenYearAscvd)] = new PreventCoefficients
            {
                Intercept = -3.838746, Age = 0.7111831, AgeSq = 0.0, NonHdl = 0.106797, Hdl = -0.1425745,
                SbpMin = -0.0736824, SbpMax = 0.3480844, Dm = 0.5112951, Smk = 0.4880292, EgfrMin = 0.4754997,
                EgfrMax = 0.0438132, BpTreat = 0.2259093, Statin = -0.0648872, BpTreatSbp = -0.0437645,
                StatinNonHdl = 0.0697082, AgeNonHdl = -0.0506382, AgeHdl = 0.0327475, AgeSbp = -0.0996442,
                AgeDm = -0.1924338, AgeSmk = -0.0803539, AgeEgfr = -0.1682586, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.1339055,
                HbA1cC2 = 0.1596461, HbA1cMiss = 0.0015678
            },
            [(PreventModel.HbA1c, Sex.Female, Outcome.TenYearHf)] = new PreventCoefficients
            {
                Intercept = -4.288225, Age = 0.8997391, AgeSq = 0.0, NonHdl = 0.0, Hdl = 0.0, SbpMin = -0.4422749,
                SbpMax = 0.3378691, Dm = 0.681284, Smk = 0.5886005, EgfrMin = 0.73447, EgfrMax = 0.05926,
                BpTreat = 0.3543475, Statin = 0.0, BpTreatSbp = -0.1002139, StatinNonHdl = 0.0, AgeNonHdl = 0.0,
                AgeHdl = 0.0, AgeSbp = -0.0878765, AgeDm = -0.303684, AgeSmk = -0.1178943, AgeEgfr = -0.1912183,
                BmiMin = -0.0148657, BmiMax = 0.2958374, AgeBmi = -0.008345, SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0,
                UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.1856442, HbA1cC2 = 0.1833083, HbA1cMiss = -0.0143112
            },
            [(PreventModel.HbA1c, Sex.Female, Outcome.ThirtyYearCvd)] = new PreventCoefficients
            {
                Intercept = -1.341059, Age = 0.5343493, AgeSq = -0.0952314, NonHdl = 0.0298124, Hdl = -0.1578451,
                SbpMin = -0.1504488, SbpMax = 0.3173368, Dm = 0.4314738, Smk = 0.3209399, EgfrMin = 0.1771435,
                EgfrMax = 0.0582828, BpTreat = 0.2888947, Statin = -0.0795886, BpTreatSbp = -0.0600438,
                StatinNonHdl = 0.0920598, AgeNonHdl = -0.0696108, AgeHdl = 0.0308807, AgeSbp = -0.0954051,
                AgeDm = -0.2763408, AgeSmk = -0.1623944, AgeEgfr = -0.1430514, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0940543,
                HbA1cC2 = 0.1116486, HbA1cMiss = -0.0024798
            },
            [(PreventModel.HbA1c, Sex.Female, Outcome.ThirtyYearAscvd)] = new PreventCoefficients
            {
                Intercept = -2.011533, Age = 0.4555574, AgeSq = -0.0903501, NonHdl = 0.1148321, Hdl = -0.1458754,
                SbpMin = 0.0089323, SbpMax = 0.3139029, Dm = 0.386281, Smk = 0.2714309, EgfrMin = 0.0930987,
                EgfrMax = 0.0532216, BpTreat = 0.1862181, Statin = 0.0106964, BpTreatSbp = -0.0329713,
                StatinNonHdl = 0.0583609, AgeNonHdl = -0.0463273, AgeHdl = 0.0324717, AgeSbp = -0.1004777,
                AgeDm = -0.2266944, AgeSmk = -0.1541859, AgeEgfr = -0.1286005, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0875827,
                HbA1cC2 = 0.1126417, HbA1cMiss = 0.0124356
            },
            [(PreventModel.HbA1c, Sex.Female, Outcome.ThirtyYearHf)] = new PreventCoefficients
            {
                Intercept = -2.193553, Age = 0.6210856, AgeSq = -0.1000972, NonHdl = 0.0, Hdl = 0.0,
                SbpMin = -0.3773697, SbpMax = 0.295316, Dm = 0.5681692, Smk = 0.3449139, EgfrMin = 0.2875781,
                EgfrMax = 0.0692013, BpTreat = 0.3334936, Statin = 0.0, BpTreatSbp = -0.0922339, StatinNonHdl = 0.0,
                AgeNonHdl = 0.0, AgeHdl = 0.0, AgeSbp = -0.0907885, AgeDm = -0.3554646, AgeSmk = -0.2008846,
                AgeEgfr = -0.156803, BmiMin = 0.0540094, BmiMax = 0.249767, AgeBmi = -0.0079611, SdiC1 = 0.0,
                SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.1101184, HbA1cC2 = 0.0949198,
                HbA1cMiss = -0.0022589
            },
            [(PreventModel.HbA1c, Sex.Male, Outcome.TenYearCvd)] = new PreventCoefficients
            {
                Intercept = -3.040901, Age = 0.7699177, AgeSq = 0.0, NonHdl = 0.0605093, Hdl = -0.0888525,
                SbpMin = -0.417713, SbpMax = 0.3288657, Dm = 0.4759471, Smk = 0.4385663, EgfrMin = 0.5334616,
                EgfrMax = 0.0206431, BpTreat = 0.2917524, Statin = -0.1383313, BpTreatSbp = -0.0482622,
                StatinNonHdl = 0.1393796, AgeNonHdl = -0.0463501, AgeHdl = 0.0205926, AgeSbp = -0.1037717,
                AgeDm = -0.1737697, AgeSmk = -0.0915839, AgeEgfr = -0.1637039, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.13159,
                HbA1cC2 = 0.1295185, HbA1cMiss = -0.0128373
            },
            [(PreventModel.HbA1c, Sex.Male, Outcome.TenYearAscvd)] = new PreventCoefficients
            {
                Intercept = -3.51835, Age = 0.7064146, AgeSq = 0.0, NonHdl = 0.1532267, Hdl = -0.1082166,
                SbpMin = -0.2675288, SbpMax = 0.3173809, Dm = 0.432604, Smk = 0.3958842, EgfrMin = 0.3665014,
                EgfrMax = 0.0250243, BpTreat = 0.2061158, Statin = -0.0899988, BpTreatSbp = -0.0334959,
                StatinNonHdl = 0.1034168, AgeNonHdl = -0.0255406, AgeHdl = 0.0247538, AgeSbp = -0.0917441,
                AgeDm = -0.1499195, AgeSmk = -0.098089, AgeEgfr = -0.1305231, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.1157161,
                HbA1cC2 = 0.1288303, HbA1cMiss = -0.0010001
            },
            [(PreventModel.HbA1c, Sex.Male, Outcome.TenYearHf)] = new PreventCoefficients
            {
                Intercept = -3.961954, Age = 0.911787, AgeSq = 0.0, NonHdl = 0.0, Hdl = 0.0, SbpMin = -0.6568071,
                SbpMax = 0.3524645, Dm = 0.5849752, Smk = 0.5014014, EgfrMin = 0.6892219, EgfrMax = 0.0292377,
                BpTreat = 0.3038296, Statin = 0.0, BpTreatSbp = -0.0515032, StatinNonHdl = 0.0, AgeNonHdl = 0.0,
                AgeHdl = 0.0, AgeSbp = -0.1262343, AgeDm = -0.2449514, AgeSmk = -0.1392217, AgeEgfr = -0.1917105,
                BmiMin = -0.0512352, BmiMax = 0.365294, AgeBmi = 0.0009592, SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0,
                UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.1652857, HbA1cC2 = 0.1505859, HbA1cMiss = -0.0113444
            },
            [(PreventModel.HbA1c, Sex.Male, Outcome.ThirtyYearCvd)] = new PreventCoefficients
            {
                Intercept = -1.180767, Age = 0.4519873, AgeSq = -0.101624, NonHdl = 0.0700456, Hdl = -0.0968005,
                SbpMin = -0.1923527, SbpMax = 0.2827043, Dm = 0.3417152, Smk = 0.2105272, EgfrMin = 0.1113291,
                EgfrMax = 0.0640135, BpTreat = 0.2334248, Statin = -0.0299421, BpTreatSbp = -0.0393204,
                StatinNonHdl = 0.1228854, AgeNonHdl = -0.0463737, AgeHdl = 0.0184599, AgeSbp = -0.1085744,
                AgeDm = -0.2208049, AgeSmk = -0.1577978, AgeEgfr = -0.1179375, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0768169,
                HbA1cC2 = 0.0777295, HbA1cMiss = 0.0092204
            },
            [(PreventModel.HbA1c, Sex.Male, Outcome.ThirtyYearAscvd)] = new PreventCoefficients
            {
                Intercept = -1.777708, Age = 0.3883267, AgeSq = -0.0958114, NonHdl = 0.1613374, Hdl = -0.1144418,
                SbpMin = -0.0474338, SbpMax = 0.2691281, Dm = 0.2859773, Smk = 0.1759553, EgfrMin = -0.0242898,
                EgfrMax = 0.0644523, BpTreat = 0.142874, Statin = 0.0115062, BpTreatSbp = -0.02333,
                StatinNonHdl = 0.0899664, AgeNonHdl = -0.0275478, AgeHdl = 0.022573, AgeSbp = -0.090802,
                AgeDm = -0.1771894, AgeSmk = -0.1548847, AgeEgfr = -0.0732754, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0, SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0591089,
                HbA1cC2 = 0.0821158, HbA1cMiss = 0.0179755
            },
            [(PreventModel.HbA1c, Sex.Male, Outcome.ThirtyYearHf)] = new PreventCoefficients
            {
                Intercept = -1.974999, Age = 0.5703729, AgeSq = -0.1084544, NonHdl = 0.0, Hdl = 0.0,
                SbpMin = -0.4471767, SbpMax = 0.2910152, Dm = 0.4507242, Smk = 0.259585, EgfrMin = 0.2454706,
                EgfrMax = 0.0675649, BpTreat = 0.2611991, Statin = 0.0, BpTreatSbp = -0.0408908, StatinNonHdl = 0.0,
                AgeNonHdl = 0.0, AgeHdl = 0.0, AgeSbp = -0.1241051, AgeDm = -0.2849461, AgeSmk = -0.2032308,
                AgeEgfr = -0.138301, BmiMin = 0.0850676, BmiMax = 0.2637222, AgeBmi = -0.0239714, SdiC1 = 0.0,
                SdiC2 = 0.0, SdiMiss = 0.0, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.1101184, HbA1cC2 = 0.0949198,
                HbA1cMiss = 0.0084192
            },
            [(PreventModel.Sdi, Sex.Female, Outcome.TenYearCvd)] = new PreventCoefficients
            {
                Intercept = -3.461564, Age = 0.7754083, AgeSq = 0.0, NonHdl = 0.0221756, Hdl = -0.1650828,
                SbpMin = -0.2180808, SbpMax = 0.3381188, Dm = 0.8624372, Smk = 0.4663953, EgfrMin = 0.5919004,
                EgfrMax = 0.0516821, BpTreat = 0.3182166, Statin = -0.1460816, BpTreatSbp = -0.0574455,
                StatinNonHdl = 0.1302287, AgeNonHdl = -0.083509, AgeHdl = 0.0282181, AgeSbp = -0.0952647,
                AgeDm = -0.2718966, AgeSmk = -0.0641738, AgeEgfr = -0.1717026, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.1442776, SdiC2 = 0.2421409, SdiMiss = 0.1885076, UacrCoef = 0.0, UacrMiss = 0.0,
                HbA1cC1 = 0.0, HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Sdi, Sex.Female, Outcome.TenYearAscvd)] = new PreventCoefficients
            {
                Intercept = -3.955898, Age = 0.7028123, AgeSq = 0.0, NonHdl = 0.1056078, Hdl = -0.1502263,
                SbpMin = -0.0488757, SbpMax = 0.3402681, Dm = 0.838022, Smk = 0.4064592, EgfrMin = 0.4838394,
                EgfrMax = 0.0480415, BpTreat = 0.2270648, Statin = -0.0585626, BpTreatSbp = -0.0349485,
                StatinNonHdl = 0.1017299, AgeNonHdl = -0.062389, AgeHdl = 0.0285106, AgeSbp = -0.1033711,
                AgeDm = -0.2477845, AgeSmk = -0.0544326, AgeEgfr = -0.1735372, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.1473705, SdiC2 = 0.2451878, SdiMiss = 0.1691593, UacrCoef = 0.0, UacrMiss = 0.0,
                HbA1cC1 = 0.0, HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Sdi, Sex.Female, Outcome.TenYearHf)] = new PreventCoefficients
            {
                Intercept = -4.409382, Age = 0.8819156, AgeSq = 0.0, NonHdl = 0.0, Hdl = 0.0, SbpMin = -0.4495491,
                SbpMax = 0.3457405, Dm = 1.02632, Smk = 0.5371646, EgfrMin = 0.7315223, EgfrMax = 0.0651679,
                BpTreat = 0.3491487, Statin = 0.0, BpTreatSbp = -0.0890335, StatinNonHdl = 0.0, AgeNonHdl = 0.0,
                AgeHdl = 0.0, AgeSbp = -0.0971028, AgeDm = -0.3528078, AgeSmk = -0.106216, AgeEgfr = -0.1899413,
                BmiMin = -0.0168447, BmiMax = 0.2805126, AgeBmi = 0.0064998, SdiC1 = 0.1343318, SdiC2 = 0.2496522,
                SdiMiss = 0.1915023, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0, HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Sdi, Sex.Female, Outcome.ThirtyYearCvd)] = new PreventCoefficients
            {
                Intercept = -1.493211, Age = 0.5124233, AgeSq = -0.0978159, NonHdl = 0.0322131, Hdl = -0.1717884,
                SbpMin = -0.1364536, SbpMax = 0.3074443, Dm = 0.6709275, Smk = 0.2897728, EgfrMin = 0.1670658,
                EgfrMax = 0.0618439, BpTreat = 0.2969806, Statin = -0.0665514, BpTreatSbp = -0.0458917,
                StatinNonHdl = 0.1168505, AgeNonHdl = -0.0770419, AgeHdl = 0.027634, AgeSbp = -0.0992045,
                AgeDm = -0.3208137, AgeSmk = -0.134847, AgeEgfr = -0.1399842, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.1129725, SdiC2 = 0.1975843, SdiMiss = 0.1627381, UacrCoef = 0.0, UacrMiss = 0.0,
                HbA1cC1 = 0.0, HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Sdi, Sex.Female, Outcome.ThirtyYearAscvd)] = new PreventCoefficients
            {
                Intercept = -2.116951, Age = 0.4396545, AgeSq = -0.0918489, NonHdl = 0.1132729, Hdl = -0.1544977,
                SbpMin = 0.036315, SbpMax = 0.3049229, Dm = 0.6344794, Smk = 0.234514, EgfrMin = 0.0898312,
                EgfrMax = 0.0564502, BpTreat = 0.1933487, Statin = 0.0220467, BpTreatSbp = -0.0229229,
                StatinNonHdl = 0.0903326, AgeNonHdl = -0.0579383, AgeHdl = 0.0274011, AgeSbp = -0.1039749,
                AgeDm = -0.2785102, AgeSmk = -0.1167267, AgeEgfr = -0.1269382, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.1149139, SdiC2 = 0.1976537, SdiMiss = 0.1391241, UacrCoef = 0.0, UacrMiss = 0.0,
                HbA1cC1 = 0.0, HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Sdi, Sex.Female, Outcome.ThirtyYearHf)] = new PreventCoefficients
            {
                Intercept = -2.317899, Age = 0.5919097, AgeSq = -0.1023133, NonHdl = 0.0, Hdl = 0.0,
                SbpMin = -0.3864727, SbpMax = 0.301876, Dm = 0.8162909, Smk = 0.3449647, EgfrMin = 0.2790347,
                EgfrMax = 0.0742645, BpTreat = 0.3352935, Statin = 0.0, BpTreatSbp = -0.0772532, StatinNonHdl = 0.0,
                AgeNonHdl = 0.0, AgeHdl = 0.0, AgeSbp = -0.0995144, AgeDm = -0.4000423, AgeSmk = -0.1770335,
                AgeEgfr = -0.149585, BmiMin = 0.0574975, BmiMax = 0.2367826, AgeBmi = 0.0083046, SdiC1 = 0.0960646,
                SdiC2 = 0.1987543, SdiMiss = 0.1562214, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0, HbA1cC2 = 0.0,
                HbA1cMiss = 0.0
            },
            [(PreventModel.Sdi, Sex.Male, Outcome.TenYearCvd)] = new PreventCoefficients
            {
                Intercept = -3.159572, Age = 0.7756377, AgeSq = 0.0, NonHdl = 0.0715325, Hdl = -0.0976775,
                SbpMin = -0.5186614, SbpMax = 0.3235653, Dm = 0.7722496, Smk = 0.3761129, EgfrMin = 0.5180893,
                EgfrMax = 0.0118451, BpTreat = 0.2634094, Statin = -0.1455263, BpTreatSbp = -0.0367013,
                StatinNonHdl = 0.1617785, AgeNonHdl = -0.0507669, AgeHdl = 0.0178356, AgeSbp = -0.1059337,
                AgeDm = -0.2236755, AgeSmk = -0.0723216, AgeEgfr = -0.1548205, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0889119, SdiC2 = 0.291897, SdiMiss = 0.1508151, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0,
                HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Sdi, Sex.Male, Outcome.TenYearAscvd)] = new PreventCoefficients
            {
                Intercept = -3.624712, Age = 0.7150087, AgeSq = 0.0, NonHdl = 0.1627339, Hdl = -0.1194988,
                SbpMin = -0.363659, SbpMax = 0.3179476, Dm = 0.7156422, Smk = 0.3404477, EgfrMin = 0.3545754,
                EgfrMax = 0.0157875, BpTreat = 0.1786233, Statin = -0.1018269, BpTreatSbp = -0.028313,
                StatinNonHdl = 0.1209467, AgeNonHdl = -0.0285806, AgeHdl = 0.0247348, AgeSbp = -0.0919494,
                AgeDm = -0.1981491, AgeSmk = -0.0776891, AgeEgfr = -0.1284899, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0728242, SdiC2 = 0.2824453, SdiMiss = 0.1437348, UacrCoef = 0.0, UacrMiss = 0.0,
                HbA1cC1 = 0.0, HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Sdi, Sex.Male, Outcome.TenYearHf)] = new PreventCoefficients
            {
                Intercept = -4.058977, Age = 0.894179, AgeSq = 0.0, NonHdl = 0.0, Hdl = 0.0, SbpMin = -0.7067398,
                SbpMax = 0.350241, Dm = 0.9252453, Smk = 0.4364765, EgfrMin = 0.6696768, EgfrMax = 0.0237374,
                BpTreat = 0.2688352, Statin = 0.0, BpTreatSbp = -0.0434892, StatinNonHdl = 0.0, AgeNonHdl = 0.0,
                AgeHdl = 0.0, AgeSbp = -0.1297155, AgeDm = -0.299086, AgeSmk = -0.1079522, AgeEgfr = -0.1797791,
                BmiMin = -0.0866297, BmiMax = 0.3706765, AgeBmi = 0.0130483, SdiC1 = 0.1235632, SdiC2 = 0.3592212,
                SdiMiss = 0.17924, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0, HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Sdi, Sex.Male, Outcome.ThirtyYearCvd)] = new PreventCoefficients
            {
                Intercept = -1.251031, Age = 0.437377, AgeSq = -0.104443, NonHdl = 0.0812573, Hdl = -0.1069199,
                SbpMin = -0.2786727, SbpMax = 0.2729256, Dm = 0.5279006, Smk = 0.1878949, EgfrMin = 0.0866569,
                EgfrMax = 0.0594948, BpTreat = 0.2028246, Statin = -0.0308404, BpTreatSbp = -0.0283679,
                StatinNonHdl = 0.1439353, AgeNonHdl = -0.0510854, AgeHdl = 0.0150236, AgeSbp = -0.1095448,
                AgeDm = -0.2561109, AgeSmk = -0.1282945, AgeEgfr = -0.1011023, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0314626, SdiC2 = 0.2003953, SdiMiss = 0.0927451, UacrCoef = 0.0, UacrMiss = 0.0,
                HbA1cC1 = 0.0, HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Sdi, Sex.Male, Outcome.ThirtyYearAscvd)] = new PreventCoefficients
            {
                Intercept = -1.836632, Age = 0.3749788, AgeSq = -0.0990063, NonHdl = 0.1708505, Hdl = -0.1272841,
                SbpMin = -0.1275555, SbpMax = 0.2659339, Dm = 0.4676531, Smk = 0.1610104, EgfrMin = -0.0465144,
                EgfrMax = 0.0596996, BpTreat = 0.1147096, Statin = 0.0052906, BpTreatSbp = -0.0186687,
                StatinNonHdl = 0.1063151, AgeNonHdl = -0.0307797, AgeHdl = 0.0218126, AgeSbp = -0.0898242,
                AgeDm = -0.210054, AgeSmk = -0.1246327, AgeEgfr = -0.0629358, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0199201, SdiC2 = 0.194949, SdiMiss = 0.0863835, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0,
                HbA1cC2 = 0.0, HbA1cMiss = 0.0
            },
            [(PreventModel.Sdi, Sex.Male, Outcome.ThirtyYearHf)] = new PreventCoefficients
            {
                Intercept = -2.060187, Age = 0.5387527, AgeSq = -0.1090333, NonHdl = 0.0, Hdl = 0.0,
                SbpMin = -0.4829094, SbpMax = 0.2843569, Dm = 0.6827667, Smk = 0.2406677, EgfrMin = 0.2255837,
                EgfrMax = 0.0653632, BpTreat = 0.2263243, Statin = 0.0, BpTreatSbp = -0.0316851, StatinNonHdl = 0.0,
                AgeNonHdl = 0.0, AgeHdl = 0.0, AgeSbp = -0.1258716, AgeDm = -0.3243709, AgeSmk = -0.1596172,
                AgeEgfr = -0.1204785, BmiMin = 0.0618028, BmiMax = 0.2705615, AgeBmi = -0.0103092, SdiC1 = 0.0680528,
                SdiC2 = 0.2619865, SdiMiss = 0.1151424, UacrCoef = 0.0, UacrMiss = 0.0, HbA1cC1 = 0.0, HbA1cC2 = 0.0,
                HbA1cMiss = 0.0
            },
            [(PreventModel.Full, Sex.Female, Outcome.TenYearCvd)] = new PreventCoefficients
            {
                Intercept = -3.860385, Age = 0.7716794, AgeSq = 0.0, NonHdl = 0.0062109, Hdl = -0.1547756,
                SbpMin = -0.1933123, SbpMax = 0.3071217, Dm = 0.496753, Smk = 0.466605, EgfrMin = 0.4780697,
                EgfrMax = 0.0529077, BpTreat = 0.3034892, Statin = -0.1556524, BpTreatSbp = -0.0667026,
                StatinNonHdl = 0.1061825, AgeNonHdl = -0.0742271, AgeHdl = 0.0288245, AgeSbp = -0.0875188,
                AgeDm = -0.2267102, AgeSmk = -0.0676125, AgeEgfr = -0.1493231, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.1361989, SdiC2 = 0.2261596, SdiMiss = 0.1804508, UacrCoef = 0.1645922, UacrMiss = 0.0198413,
                HbA1cC1 = 0.1298513, HbA1cC2 = 0.1412555, HbA1cMiss = -0.0031658
            },
            [(PreventModel.Full, Sex.Female, Outcome.TenYearAscvd)] = new PreventCoefficients
            {
                Intercept = -4.291503, Age = 0.7023067, AgeSq = 0.0, NonHdl = 0.0898765, Hdl = -0.1407316,
                SbpMin = -0.0256648, SbpMax = 0.314511, Dm = 0.4799217, Smk = 0.4062049, EgfrMin = 0.3847744,
                EgfrMax = 0.0495174, BpTreat = 0.2133861, Statin = -0.0678552, BpTreatSbp = -0.0451416,
                StatinNonHdl = 0.0788187, AgeNonHdl = -0.0535985, AgeHdl = 0.0291762, AgeSbp = -0.0961839,
                AgeDm = -0.2001466, AgeSmk = -0.0586472, AgeEgfr = -0.1537791, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.1413965, SdiC2 = 0.228136, SdiMiss = 0.1588908, UacrCoef = 0.1371824, UacrMiss = 0.0061613,
                HbA1cC1 = 0.123192, HbA1cC2 = 0.1410572, HbA1cMiss = 0.005866
            },
            [(PreventModel.Full, Sex.Female, Outcome.TenYearHf)] = new PreventCoefficients
            {
                Intercept = -4.896524, Age = 0.884209, AgeSq = 0.0, NonHdl = 0.0, Hdl = 0.0, SbpMin = -0.421474,
                SbpMax = 0.3002919, Dm = 0.6170359, Smk = 0.5380269, EgfrMin = 0.5975847, EgfrMax = 0.0654197,
                BpTreat = 0.3313614, Statin = 0.0, BpTreatSbp = -0.1002304, StatinNonHdl = 0.0, AgeNonHdl = 0.0,
                AgeHdl = 0.0, AgeSbp = -0.0845363, AgeDm = -0.2989062, AgeSmk = -0.1111354, AgeEgfr = -0.1666635,
                BmiMin = -0.0191335, BmiMax = 0.2764302, AgeBmi = 0.0008104, SdiC1 = 0.1213034, SdiC2 = 0.2314147,
                SdiMiss = 0.1819138, UacrCoef = 0.1948135, UacrMiss = 0.0395368, HbA1cC1 = 0.176668,
                HbA1cC2 = 0.1614911, HbA1cMiss = -0.0010583
            },
            [(PreventModel.Full, Sex.Female, Outcome.ThirtyYearCvd)] = new PreventCoefficients
            {
                Intercept = -1.748475, Age = 0.5073749, AgeSq = -0.0981751, NonHdl = 0.0162303, Hdl = -0.1617147,
                SbpMin = -0.1111241, SbpMax = 0.282946, Dm = 0.4004069, Smk = 0.2918701, EgfrMin = 0.1017102,
                EgfrMax = 0.0622643, BpTreat = 0.2872416, Statin = -0.0768135, BpTreatSbp = -0.0557282,
                StatinNonHdl = 0.0917585, AgeNonHdl = -0.0679131, AgeHdl = 0.029076, AgeSbp = -0.0907755,
                AgeDm = -0.2702118, AgeSmk = -0.1373216, AgeEgfr = -0.1255864, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.1067741, SdiC2 = 0.1853138, SdiMiss = 0.1567115, UacrCoef = 0.1028065, UacrMiss = -0.0006181,
                HbA1cC1 = 0.0925285, HbA1cC2 = 0.0975598, HbA1cMiss = 0.0101713
            },
            [(PreventModel.Full, Sex.Female, Outcome.ThirtyYearAscvd)] = new PreventCoefficients
            {
                Intercept = -2.314066, Age = 0.4386739, AgeSq = -0.0921956, NonHdl = 0.0977728, Hdl = -0.1453525,
                SbpMin = 0.0590925, SbpMax = 0.2862862, Dm = 0.3669136, Smk = 0.2354695, EgfrMin = 0.0354338,
                EgfrMax = 0.0573093, BpTreat = 0.1840085, Statin = 0.0117504, BpTreatSbp = -0.0331945,
                StatinNonHdl = 0.0664311, AgeNonHdl = -0.0492826, AgeHdl = 0.0288888, AgeSbp = -0.0964709,
                AgeDm = -0.2279648, AgeSmk = -0.120405, AgeEgfr = -0.1157635, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.1107632, SdiC2 = 0.1840367, SdiMiss = 0.1308962, UacrCoef = 0.0810739, UacrMiss = -0.0147785,
                HbA1cC1 = 0.0794709, HbA1cC2 = 0.1002615, HbA1cMiss = 0.017301
            },
            [(PreventModel.Full, Sex.Female, Outcome.ThirtyYearHf)] = new PreventCoefficients
            {
                Intercept = -2.642208, Age = 0.5927507, AgeSq = -0.1028754, NonHdl = 0.0, Hdl = 0.0,
                SbpMin = -0.3593781, SbpMax = 0.2628556, Dm = 0.5113472, Smk = 0.347344, EgfrMin = 0.1971295,
                EgfrMax = 0.0735227, BpTreat = 0.3219386, Statin = 0.0, BpTreatSbp = -0.0880321, StatinNonHdl = 0.0,
                AgeNonHdl = 0.0, AgeHdl = 0.0, AgeSbp = -0.0863132, AgeDm = -0.3425359, AgeSmk = -0.181405,
                AgeEgfr = -0.1356989, BmiMin = 0.0564656, BmiMax = 0.2363857, AgeBmi = 0.0031285, SdiC1 = 0.0847634,
                SdiC2 = 0.18397, SdiMiss = 0.1485802, UacrCoef = 0.1273306, UacrMiss = 0.0167008, HbA1cC1 = 0.1378342,
                HbA1cC2 = 0.1138832, HbA1cMiss = 0.0138979
            },
            [(PreventModel.Full, Sex.Male, Outcome.TenYearCvd)] = new PreventCoefficients
            {
                Intercept = -3.631387, Age = 0.7847578, AgeSq = 0.0, NonHdl = 0.0534485, Hdl = -0.0911282,
                SbpMin = -0.4921973, SbpMax = 0.2972415, Dm = 0.4527054, Smk = 0.3726641, EgfrMin = 0.3886854,
                EgfrMax = 0.0081661, BpTreat = 0.2508052, Statin = -0.1538484, BpTreatSbp = -0.0474695,
                StatinNonHdl = 0.1415382, AgeNonHdl = -0.0436455, AgeHdl = 0.0199549, AgeSbp = -0.1022686,
                AgeDm = -0.1762507, AgeSmk = -0.0715873, AgeEgfr = -0.1428668, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0802431, SdiC2 = 0.275073, SdiMiss = 0.144759, UacrCoef = 0.1772853, UacrMiss = 0.1095674,
                HbA1cC1 = 0.1165698, HbA1cC2 = 0.1048297, HbA1cMiss = -0.0230072
            },
            [(PreventModel.Full, Sex.Male, Outcome.TenYearAscvd)] = new PreventCoefficients
            {
                Intercept = -3.969788, Age = 0.7128741, AgeSq = 0.0, NonHdl = 0.1465201, Hdl = -0.1125794,
                SbpMin = -0.3387216, SbpMax = 0.2980252, Dm = 0.399583, Smk = 0.3379111, EgfrMin = 0.2582604,
                EgfrMax = 0.0147769, BpTreat = 0.1686621, Statin = -0.1073619, BpTreatSbp = -0.0381038,
                StatinNonHdl = 0.1034169, AgeNonHdl = -0.0228755, AgeHdl = 0.0267453, AgeSbp = -0.0897449,
                AgeDm = -0.1497464, AgeSmk = -0.077206, AgeEgfr = -0.1198368, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0651121, SdiC2 = 0.2676683, SdiMiss = 0.1388492, UacrCoef = 0.1375837, UacrMiss = 0.0652944,
                HbA1cC1 = 0.101282, HbA1cC2 = 0.1092726, HbA1cMiss = -0.0112852
            },
            [(PreventModel.Full, Sex.Male, Outcome.TenYearHf)] = new PreventCoefficients
            {
                Intercept = -4.663513, Age = 0.9095703, AgeSq = 0.0, NonHdl = 0.0, Hdl = 0.0, SbpMin = -0.6765184,
                SbpMax = 0.3111651, Dm = 0.5535052, Smk = 0.4326811, EgfrMin = 0.5102245, EgfrMax = 0.015472,
                BpTreat = 0.2570964, Statin = 0.0, BpTreatSbp = -0.0591177, StatinNonHdl = 0.0, AgeNonHdl = 0.0,
                AgeHdl = 0.0, AgeSbp = -0.1219056, AgeDm = -0.2437577, AgeSmk = -0.105363, AgeEgfr = -0.1660207,
                BmiMin = -0.0854286, BmiMax = 0.3551736, AgeBmi = 0.0037907, SdiC1 = 0.1106372, SdiC2 = 0.3371204,
                SdiMiss = 0.1694628, UacrCoef = 0.2164607, UacrMiss = 0.1702805, HbA1cC1 = 0.148297,
                HbA1cC2 = 0.1234088, HbA1cMiss = -0.0234637
            },
            [(PreventModel.Full, Sex.Male, Outcome.ThirtyYearCvd)] = new PreventCoefficients
            {
                Intercept = -1.504558, Age = 0.4427595, AgeSq = -0.1064108, NonHdl = 0.0629381, Hdl = -0.1015427,
                SbpMin = -0.2542326, SbpMax = 0.2549679, Dm = 0.333835, Smk = 0.1873833, EgfrMin = 0.0246102,
                EgfrMax = 0.0552014, BpTreat = 0.1979729, Statin = -0.0407714, BpTreatSbp = -0.0365522,
                StatinNonHdl = 0.1232822, AgeNonHdl = -0.0441334, AgeHdl = 0.0177865, AgeSbp = -0.1046657,
                AgeDm = -0.2116113, AgeSmk = -0.1277905, AgeEgfr = -0.0955922, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.0256704, SdiC2 = 0.1887637, SdiMiss = 0.089241, UacrCoef = 0.0894596, UacrMiss = 0.0710124,
                HbA1cC1 = 0.0676202, HbA1cC2 = 0.063409, HbA1cMiss = 0.0038783
            },
            [(PreventModel.Full, Sex.Male, Outcome.ThirtyYearAscvd)] = new PreventCoefficients
            {
                Intercept = -1.985368, Age = 0.3743566, AgeSq = -0.0995499, NonHdl = 0.1544808, Hdl = -0.1215297,
                SbpMin = -0.1083968, SbpMax = 0.2555179, Dm = 0.2696998, Smk = 0.1628432, EgfrMin = -0.077507,
                EgfrMax = 0.0583407, BpTreat = 0.1120322, Statin = -0.0025063, BpTreatSbp = -0.0256116,
                StatinNonHdl = 0.0886745, AgeNonHdl = -0.0254507, AgeHdl = 0.0244639, AgeSbp = -0.0869146,
                AgeDm = -0.165745, AgeSmk = -0.1244714, AgeEgfr = -0.0624552, BmiMin = 0.0, BmiMax = 0.0, AgeBmi = 0.0,
                SdiC1 = 0.015675, SdiC2 = 0.1864231, SdiMiss = 0.0845697, UacrCoef = 0.0560171, UacrMiss = 0.0252244,
                HbA1cC1 = 0.0501422, HbA1cC2 = 0.0722905, HbA1cMiss = 0.0114945
            },
            [(PreventModel.Full, Sex.Male, Outcome.ThirtyYearHf)] = new PreventCoefficients
            {
                Intercept = -2.425439, Age = 0.5478829, AgeSq = -0.1111928, NonHdl = 0.0, Hdl = 0.0,
                SbpMin = -0.4547346, SbpMax = 0.2527602, Dm = 0.4385384, Smk = 0.2397952, EgfrMin = 0.1354588,
                EgfrMax = 0.0570689, BpTreat = 0.220666, Statin = 0.0, BpTreatSbp = -0.0436769, StatinNonHdl = 0.0,
                AgeNonHdl = 0.0, AgeHdl = 0.0, AgeSbp = -0.1168376, AgeDm = -0.2730055, AgeSmk = -0.1573691,
                AgeEgfr = -0.1128676, BmiMin = 0.0640931, BmiMax = 0.2643081, AgeBmi = -0.0174998, SdiC1 = 0.057746,
                SdiC2 = 0.2446441, SdiMiss = 0.1076782, UacrCoef = 0.1233486, UacrMiss = 0.1274796, HbA1cC1 = 0.0985062,
                HbA1cC2 = 0.0804844, HbA1cMiss = 0.0022806
            },
        };
}

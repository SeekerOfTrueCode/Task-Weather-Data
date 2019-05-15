using System.ComponentModel;

namespace Technical_Task.Core.Enums
{
    public enum TemperatureEnum
    {
        [Description("Humans cannot survive such low temperatures - it's an absolute zero")]
        AbsoluteZeroHumansCannotSurvive = -273,
        [Description("Live in danger - too cold")]
        TooColdLifeInDanger = -60,
        [Description("Freezing")]
        Freezing = -30,
        [Description("Very cold")]
        VeryCold = -10,
        [Description("Bracing")]
        Bracing = -4,
        [Description("Chilly")]
        Chilly = 0,
        [Description("Cool")]
        Cool = 10,
        [Description("Mild")]
        Mild = 15,
        [Description("Warm")]
        Warm = 21,
        [Description("Balmy")]
        Balmy = 25,
        [Description("Hot")]
        Hot = 30,
        [Description("Sweltering")]
        Sweltering = 37,
        [Description("Scorching")]
        Scorching = 40,
        [Description("Live danger - temperature")]
        TooWarmLiveInDanger = 50,
        [Description("Humans cannot survive such high temperatures - too hot")]
        TooHotHumansCannotSurvive = 60
    };
}

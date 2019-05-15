namespace Technical_Task.Core.Logic.Validators
{
    public class IsInRangeValidator
    {
        private readonly double _min;
        private readonly double _max;
        public IsInRangeValidator(double min, double max)
        {
            _min = min;
            _max = max;
        }

        public bool IsValid(double value)
        {
            return value <= _max && value >= _min;
        }
    }
}
